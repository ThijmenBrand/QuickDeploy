#ifndef configuration_h
#define configuration_h

#include <map>
#include <string>
#include <tuple>

namespace configuration {

	bool writeConfig(std::map<std::string, std::string>, std::string);
	bool writeConfig(std::tuple<std::string, std::string>, std::string);

	std::map<std::string, std::string> readConfig(std::string);
	std::string readConfig(const std::string, std::string);

}

#endif