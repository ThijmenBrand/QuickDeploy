#ifndef userInteraction_h
#define userInteraction_h

#include <string>

namespace cli {

	std::string prompt(const std::string, bool);
	std::string prompt(const std::string, bool, int);
	void print(const std::string);
	void print(const std::string, int);
	void error(const std::string);
	void errorExit(const std::string);

}

#endif // userinteraction_h
