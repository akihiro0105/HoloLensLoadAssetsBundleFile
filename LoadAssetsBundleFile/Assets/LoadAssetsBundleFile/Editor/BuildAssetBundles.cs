using System.IO;
using UnityEditor;

public class CreateAssetBundles
{
    /// <summary>
    /// AssetBundleの生成(Standalone,UWP用)
    /// </summary>
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        var assetBundleDirectory = "Local";
        if (!Directory.Exists(assetBundleDirectory)) Directory.CreateDirectory(assetBundleDirectory);
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.WSAPlayer);
    }
}