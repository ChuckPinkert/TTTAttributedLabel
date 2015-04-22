using System;
using ObjCRuntime;

[assembly: LinkWith ("TTTAttributedLabelSDK.a", LinkTarget.Simulator | LinkTarget.ArmV7, SmartLink = true, ForceLoad = true)]
