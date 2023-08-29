#include "configuration.h"

#include <map>
#include <string>
#include <fstream>
#include <tuple>
#include <stdlib.h>
#include <iostream>
#include <sstream>
#include <vector>
#include <filesystem>

#include "constants.h"


bool createConfigIfNotExist(std::string);
bool writeToFile(std::tuple<std::string, std::string> configOption, std::string filePath);



bool configuration::writeConfig(std::map<std::string, std::string> configuration, std::string writePath)
{
	createConfigIfNotExist(writePath);

	for (auto const& [key, val] : configuration)
	{
		if (val.empty()) continue;

		bool writeResult = writeToFile({ key, val }, writePath);
		if (!writeResult) return false;
	}

	return true;
}

bool configuration::writeConfig(std::tuple<std::string, std::string> configuration, std::string writePath)
{
	createConfigIfNotExist(writePath);

	bool writeResult = writeToFile(configuration, writePath);
	if (!writeResult) return false;

	return true;
}

std::map<std::string, std::string> configuration::readConfig(std::string readPath)
{
	std::string line;
	std::ifstream fileHandle(readPath);
	
	if (!fileHandle.is_open()) throw std::exception("Unable to open file");

	std::vector<std::string> configList;

	while (std::getline(fileHandle, line))
	{
		std::stringstream configLine(line);
		std::string segment;
		
		while (std::getline(configLine, segment, CONFIG_DELIMITER)) {
			configList.push_back(segment);
		}
	}

	fileHandle.close();

	std::map<std::string, std::string> parsedConfiguration;

	for (int i = 0; i < configList.size(); i+= 2) {
		parsedConfiguration.insert({ configList[i], configList[i + 1] });
	}
	
	return parsedConfiguration;
}

std::string configuration::readConfig(const std::string configOption, std::string configPath)
{
	std::map<std::string, std::string> configurations = configuration::readConfig(configPath);

	if (auto targetConfigOption = configurations.find(configOption); targetConfigOption != configurations.end()) {
		return targetConfigOption->second;
	}

	return "";
}

bool writeToFile(std::tuple<std::string, std::string> configOption, std::string filePath)
{
	std::fstream fileHandle;

	fileHandle.open(filePath, std::fstream::in | std::fstream::app);

	if (!fileHandle) return false;

	fileHandle << std::get<0>(configOption) << CONFIG_DELIMITER << std::get<1>(configOption) << std::endl;

	fileHandle.close();

	return true;
}

bool createConfigIfNotExist(std::string filePath) {
	std::fstream fileHandle;

	fileHandle.open(filePath, std::fstream::in | std::fstream::out | std::fstream::app);

	if (!fileHandle)
	{
		fileHandle.open(filePath, std::fstream::in | std::fstream::out | std::fstream::trunc);
		fileHandle << "\n";
		fileHandle.close();
	}

	return true;
}