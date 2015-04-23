using System;
using ObjCRuntime;

[assembly: LinkWith ("TTTAttributedLabelSDK.a", LinkTarget.Simulator | LinkTarget.ArmV7 | LinkTarget.Arm64, LinkerFlags = "-ObjC", SmartLink = true, ForceLoad = true)]
