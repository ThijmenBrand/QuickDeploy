#include "setup.h"

#include <iostream>
#include <fstream>
#include <stdlib.h>
#include <map>

#include "configuration.h"
#include "cli.h"
#include "constants.h"

using namespace std;

std::string definePrompt(const char*);

void setup::setupCommand() {
	cout << "welcome to the QuickDeploy setup tool" << endl;
	cout << "-------------------------------------" << endl;

	std::map<std::string, std::string> userInput;

	std::string username = cli::prompt("Please enter your remote username", true);
	userInput.insert({ "username", username });

	std::string remoteServer = cli::prompt("Please enter the adress of your remote server", true);
	userInput.insert({ "remoteServer", remoteServer});

	configuration::writeConfig(userInput, CONFIG_FILENAME);

	cli::print("configurations are saved, QuickDeploy is now ready for use!", 32);
}