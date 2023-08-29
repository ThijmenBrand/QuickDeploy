#include "deploy.h"

#include <string>
#include <iostream>
#include <array>
#include <sstream>
#include <filesystem>
#include <format>

#include "exec.h"
#include "command_line_options.h"
#include "configuration.h"
#include "constants.h"
#include "cli.h"
#include "helpers.h"

using namespace std;

int deployToRemote();
std::string getCorrectPath(char*);
char* checkCommandFlag(char**, char**, const char*);
std::string getParam(char** begin, char** end, const char* option, std::string configKey, std::string localPath, bool required);

std::string server;
std::string username;
std::string localDir;
std::string remoteDir;

void deploy::deployCommand(char** begin, char** end)
{
    std::string currentPath = helpers::getLocalConfigFilePath();

    server = getParam(begin, end, "-s", SERVER_CONFIG_KEY, currentPath, true);
    username = getParam(begin, end, "-u", USER_CONFIG_KEY, currentPath, true);
    localDir = getParam(begin, end, "-p", LOCAL_DIR_CONFIG_KEY, currentPath, false);
    if (localDir == "") localDir = std::filesystem::current_path().string();
    remoteDir = getParam(begin, end, "-p", REMOTE_DIR_CONFIG_KEY, currentPath, false);
    if (remoteDir == "") remoteDir = "./";

    deployToRemote();
}

int deployToRemote()
{
    std::string correctFilePath = getCorrectPath(localDir.data());

    cout << "deploying " << correctFilePath << endl;

    std::string command = "scp -r " + localDir + " " + username + "@" + server + ":" + remoteDir;

    std::string result = command_execution::exec(command.data());

    cout << "files deployed" << endl;

    return 0;
}

std::string getCorrectPath(char* providedPath)
{
    std::filesystem::path path(providedPath);

    if (path.is_absolute())
    {
        return providedPath;
    }

    std::string currentPath = std::filesystem::current_path().string();

    std::string fullPath = currentPath + "\\" + providedPath;

    if (!std::filesystem::exists(fullPath)) {
        throw std::exception("Provided path could not be found");
        exit(-1);
    }

    if (std::filesystem::is_directory(fullPath)) {
        return fullPath + "/**";
    }

    return fullPath;
}

std::string getParam(char** begin, char** end, const char* option, std::string configKey, std::string localPath, bool required) {
    char* provided = checkCommandFlag(begin, end, option);
    
    if (provided) {
        return provided;
    }

    std::string localConfig = configuration::readConfig(configKey, localPath);
    if (localConfig != "") {
        return localConfig;
    }

    std::string globalConfig = configuration::readConfig(configKey, CONFIG_FILENAME);
    if (globalConfig != "") {
        return globalConfig;
    }

    if (!required) return "";

    cli::error("could not find " + configKey + ". The following solutions might help");
    cli::error("1. Run with the neceserry command line option");
    cli::error("2. Run <QuickDeploy init> to initialise a local deployment");
    cli::error("3. Run <QuickDeploy setup> to setup a global configuration");
    exit(-1);
}

char* checkCommandFlag(char** begin, char** end, const char* flag) {
    if (clo::cmdOptionExists(begin, end, flag) && !clo::getCmdOption(begin, end, flag))
    {
        throw std::invalid_argument("provided vlag does not have any value");
        exit(-1);
    }

    return clo::getCmdOption(begin, end, flag);
}
