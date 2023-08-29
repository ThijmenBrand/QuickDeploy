#include "cli.h"

#include <string>
#include <iostream>

std::string cli::prompt(const std::string message, bool required, int color) {
	std::string result;

	cli::print(message + ": ", color);

	if (!required) {
		std::getline(std::cin, result);

		return result;
	}

	while (result.empty())
	{
		std::getline(std::cin, result);
	}

	return result;
}

std::string cli::prompt(const std::string message, bool required) {
	std::string result;

	cli::print(message + ": ");

	if (!required) {
		std::getline(std::cin, result);

		return result;
	}

	while (result.empty())
	{
		std::getline(std::cin, result);
	}

	return result;
}

void cli::error(const std::string message) {
	cli::print(message, 31);
}

void cli::errorExit(const std::string message) {
	cli::print(message, 31);

	exit(-1);
}

void cli::print(std::string message, int color) {
	std::cout << "\033[1;" << color << "m" << message << "\033[0m" << std::endl;
}

void cli::print(std::string message) {
	std::cout <<  message << std::endl;
}

