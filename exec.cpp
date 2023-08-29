#include "exec.h"

#include <iostream>
#include <array>
#include <string>
#include <stdexcept>
#include <stdlib.h>
#include <stdio.h>

std::string command_execution::exec(const char* cmd)
{
    char buffer[128];
    std::string result;
    auto pipe = _popen(cmd, "r");

    if (!pipe) 
    {
        cerr << "popen() failed!" << endl;
        exit(-1);
    }

    while (!feof(pipe))
    {
        if (fgets(buffer, sizeof buffer, pipe) != nullptr)
            result += buffer;
    }

    auto rc = _pclose(pipe);

    if (rc == EXIT_FAILURE)
    {
        cerr << "Execuiton of command failed" << endl;
        exit(-1);
    }

    return result;
}