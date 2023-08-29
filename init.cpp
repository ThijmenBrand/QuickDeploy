#include "init.h"

#include <iostream>
#include <map>
#include <string>
#include <filesystem>

#include "command_line_options.h"
#include "cli.h"
#include "configuration.h"
#include "constants.h"
#include "helpers.h"

std::map<std::string, std::string> gatherConfigurations(char**, char**);
void writeToLocalConfig(std::map<std::string, std::string>);

void init::initCommand(char** begin, char** end)
{
	std::cout << "welcome to the QuickDeploy setup tool" << std::endl;
	std::cout << "-------------------------------------" << std::endl;

	auto configurations = gatherConfigurations(begin, end);

	writeToLocalConfig(configurations);

	cli::print("configurations are saved, QuickDeploy is now ready for use!", 32);
}

std::map<std::string, std::string> gatherConfigurations(char** begin, char** end) {
	std::map<std::string, std::string> userInput;

	if (clo::cmdOptionExists(begin, end, "-f") || clo::cmdOptionExists(begin, end, "--full"))
	{
		std::string username = cli::prompt("Please enter your remote username", true);
		userInput.insert({ USER_CONFIG_KEY, username });

		std::string server = cli::prompt("Please enter your remote server address", true);
		userInput.insert({ SERVER_CONFIG_KEY, server });
	}

	std::string localFolder = cli::prompt("Please enter you local deploy folder", false);
	userInput.insert({ LOCAL_DIR_CONFIG_KEY, localFolder });

	std::string remoteFolder = cli::prompt("Please enter your remote folder path (optional)", false);
	userInput.insert({ REMOTE_DIR_CONFIG_KEY, remoteFolder });

	return userInput;
}

void writeToLocalConfig(std::map<std::string, std::string> input)
{
	std::string localConfigFile = helpers::getLocalConfigFilePath();

	bool writeResult = configuration::writeConfig(input, localConfigFile);
	
	if (!writeResult) 
	{ 
		std::cerr << "Could not write configurations" << std::endl; 
		exit(-1); 
	}
}