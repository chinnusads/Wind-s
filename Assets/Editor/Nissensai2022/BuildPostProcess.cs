using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildPostProcess : IPostprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPostprocessBuild(BuildReport report)
    {
        string outputPath = report.summary.outputPath;
        string targetPath = outputPath.Substring(0, outputPath.LastIndexOf("/") + 1) + "ExceptionHandler.exe";
        string sourcePath = Application.dataPath + "/Nissensai2022/Internal/ExceptionHandler/ExceptionHandler.exe";
        if (!File.Exists(sourcePath))
        {
            Debug.LogError($"{sourcePath} can not be found.");
            return;
        }

        File.Copy(sourcePath, targetPath, true);
        Debug.Log($"Copied {sourcePath} to {targetPath}");
    }
}