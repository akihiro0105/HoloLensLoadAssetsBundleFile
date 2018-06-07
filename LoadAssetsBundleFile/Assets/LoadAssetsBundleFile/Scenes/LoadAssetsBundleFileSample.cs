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
        var path = FileIOControl.LocalFolderPath + "\\" + assetbundleName;
        StartCoroutine(AssetBundleLoader.AssetBundleFile<GameObject>(path, assetName, LoadCompleted, (p) => { Debug.Log((int)(p * 100)); }));
    }

    private void LoadCompleted(GameObject[] obj)
    {
        if (obj.Length > 0) Instantiate(obj[0]);
    }
}
