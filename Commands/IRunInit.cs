﻿using QuickDeploy.CliOptions;

namespace QuickDeploy.Commands;

public interface IRunInit
{
    Task<int> Init(InitOptions opts);
}