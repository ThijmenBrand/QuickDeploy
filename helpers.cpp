#include "helpers.h"

#include <string>
#include <filesystem>

#include "constants.h"

std::string helpers::getLocalConfigFilePath()
{
	std::string localPath = std::filesystem::current_path().string();
	std::string localConfigFilePath = localPath + "\\" + CONFIG_FILENAME;

	return localConfigFilePath;
}