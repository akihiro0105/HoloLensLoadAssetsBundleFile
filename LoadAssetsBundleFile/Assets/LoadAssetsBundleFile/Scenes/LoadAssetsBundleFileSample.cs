using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoadAssetsBundleFile;
using System;

public class LoadAssetsBundleFileSample : MonoBehaviour {

    public string assetbundleName = "pari";
    public string[] assetName = { "Paris2010_0" };

    // Use this for initialization
    void Start()
    {
        AssetBundleLoader assetBundleLoader = new AssetBundleLoader();
        var path = FileIOControl.LocalFolderPath + "\\" + assetbundleName;
        StartCoroutine(assetBundleLoader.AssetBundleFile<GameObject>(path, assetName, LoadCompleted, LoadProgress));
    }

    private void LoadCompleted(GameObject[] obj)
    {
        if (obj.Length > 0) Instantiate(obj[0]);
    }

    private void LoadProgress(float obj)
    {
        Debug.Log((int)(obj * 100));
    }
}
