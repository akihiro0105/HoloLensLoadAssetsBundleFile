using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoadAssetsBundleFile;

public class LoadAssetsBundleFileSample : MonoBehaviour {

    public string assetbundleName = "pari";
    public string[] assetName = { "Paris2010_0" };

    // Use this for initialization
    void Start() {
        AssetBundleLoader assetBundleLoader = new AssetBundleLoader();
        var path = FileIOControl.LocalFolderPath + "\\" + assetbundleName;
        StartCoroutine(assetBundleLoader.AssetBundleFile<GameObject>(path, assetName, (go) => { Instantiate(go[0]); }, (p) => { Debug.Log(p); }));
    }
}
