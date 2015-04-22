using System;
using ObjCRuntime;

[assembly: LinkWith ("libStaticTTTLabel.a", LinkTarget.Simulator, SmartLink = true, ForceLoad = true)]
