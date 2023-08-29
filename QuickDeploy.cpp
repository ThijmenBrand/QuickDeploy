#include <iostream>

#include "manual.h"
#include "deploy.h"
#include "setup.h"
#include "command_line_options.h"
#include "configuration.h"
#include "init.h"

using namespace std;
using namespace clo;

int main(int argc, char* argv[])
{
	if (argc == 1)
	{
		printManual();
		return 0;
	}

	if (clo::cmdOptionExists(argv, argv + argc, "init"))
	{
		init::initCommand(argv, argv + argc);

		return 0;
	}

	if (clo::cmdOptionExists(argv, argv + argc, "deploy"))
	{
		deploy::deployCommand(argv, argv + argc);

		return 0;
	}

	if (clo::cmdOptionExists(argv, argv + argc, "setup")) 
	{
		setup::setupCommand();

		return 0;
	}

	clog << "No command line arguments provided, Exiting application" << endl;

	return 0;
}
