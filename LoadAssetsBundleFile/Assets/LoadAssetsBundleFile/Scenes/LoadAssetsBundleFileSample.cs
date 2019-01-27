using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoadAssetsBundleFile;
using HoloLensModule.Environment;

public class LoadAssetsBundleFileSample : MonoBehaviour {

    public string assetbundleName = "sample";
    public string[] assetName = { "root" };

    // Use this for initialization
    void Start()
    {
        var path = FileIOControl.LocalFolderPath + "\\" + assetbundleName;
        StartCoroutine(AssetBundleLoader.AssetBundleFile<GameObject>(path, assetName, (obj) =>
        {
            for (var i = 0; i < obj.Length; i++) Instantiate(obj[i]);
        }, (p) => Debug.Log((int) (p * 100))));
    }
}
