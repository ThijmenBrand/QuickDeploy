#include "command_line_options.h"

#include <string>
#include <vector>
#include <algorithm>

using namespace std;

char* clo::getCmdOption(char** begin, char** end, const std::string& option)
{
    char** itr = std::find(begin, end, option);
    if (itr != end && itr++ != end)
    {
        return *itr;
    }

    return 0;
}

bool clo::cmdOptionExists(char** begin, char** end, const std::string& option)
{
    return std::find(begin, end, option) != end;
}