#ifndef command_line_options_h
#define command_line_options_h

#include <string>
#include <vector>

namespace clo
{
    char* getCmdOption(char**, char**, const std::string&);
    bool cmdOptionExists(char**, char**, const std::string&);
}

#endif