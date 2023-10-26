using UnityEditor;
using UnityEngine;

public class BuildAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        // Define the output directory for the AssetBundles.
        string outputPath = "Assets/AssetBundles";

        // Build the AssetBundles for the target platform.
        BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
        if (buildTarget == BuildTarget.StandaloneWindows ||
            buildTarget == BuildTarget.StandaloneWindows64 ||
            buildTarget == BuildTarget.Android)
        {
            // Create the output directory if it doesn't exist.
            if (!System.IO.Directory.Exists(outputPath))
            {
                System.IO.Directory.CreateDirectory(outputPath);
            }

            // Build the AssetBundles.
            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, buildTarget);
        }
        else
        {
            Debug.LogError("Unsupported build target. AssetBundles can only be built for Standalone Windows, Standalone Windows 64, and Android platforms.");
        }
    }
}

