using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Player;
using UnityEngine;

public static class ScriptCompileTester
{
    [MenuItem("Tools/Compile Check/All", priority = 100)]
    private static void CompileTestForAll()
    {
        CompileTestForDebugWindows();
        CompileTestForReleaseWindows();
    }

    [MenuItem("Tools/Compile Check/Windows(Debug)")]
    private static void CompileTestForDebugWindows()
    {
        CompileTest(BuildTarget.StandaloneWindows64, BuildTargetGroup.Standalone, true);
    }

    [MenuItem("Tools/Compile Check/Windows(Release)")]
    private static void CompileTestForReleaseWindows()
    {
        CompileTest(BuildTarget.StandaloneWindows64, BuildTargetGroup.Standalone, false);
    }

    private static void CompileTest(BuildTarget buildTarget, BuildTargetGroup buildTargetGroup, bool isDevelopmentBuild)
    {
        const string tempBuildPath = "Temp/CompileTest";

        var settings = new ScriptCompilationSettings
        {
            target = buildTarget,
            group = buildTargetGroup,
            options = isDevelopmentBuild ? ScriptCompilationOptions.DevelopmentBuild : ScriptCompilationOptions.None
        };

        var result = PlayerBuildInterface.CompilePlayerScripts(settings, tempBuildPath);

        if (result.assemblies != null && result.assemblies.Any() && result.typeDB != null)
        {
            Debug.Log($"Compile Test Success! - BuildTarget: {settings.target}");
        }

        // NOTE: tempBuildPathにはコンパイル後のDLLが吐き出されている
        if (Directory.Exists(tempBuildPath))
        {
            Directory.Delete(tempBuildPath, true);
        }
    }
}