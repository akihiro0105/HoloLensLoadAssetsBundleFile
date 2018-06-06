using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoadAssetsBundleFile
{
    public class AssetBundleLoader
    {
        public IEnumerator AssetBundleFile<T>(string name, string[] assets,Action<T[]> action, Action<float> progress=null) where T : UnityEngine.Object
        {
            byte[] data = null;
            yield return FileIOControl.ReadBytesFile(name, (d) => { data = d; });
            yield return AssetBundleData<T>(data, assets, action, progress);
        }

        public IEnumerator AssetBundleData<T>(byte[] data, string[] assets, Action<T[]> action, Action<float> progress=null) where T : UnityEngine.Object
        {
            AssetBundle assetBundle = null;
            // Load AssetBundle
            var resultassetbundle = AssetBundle.LoadFromMemoryAsync(data);
            while (!resultassetbundle.isDone)
            {
                if (progress != null) progress.Invoke(resultassetbundle.progress);
                yield return null;
            }
            assetBundle = resultassetbundle.assetBundle;

            // Load Asset
            var t = new T[assets.Length];
            for (int i = 0; i < t.Length; i++)
            {
                yield return LoadAsset<T>(assetBundle, assets[i], (o) => { t[i] = o as T; });
            }
            if (action != null) action.Invoke(t);

            // Unload AssetBundle
            if (assetBundle != null) assetBundle.Unload(false);
        }

        private IEnumerator LoadAsset<T>(AssetBundle assetbundle,string name, Action<T> action) where T : UnityEngine.Object
        {
            var obj = assetbundle.LoadAssetAsync<T>(name);
            yield return new WaitWhile(() => obj.isDone == false);
            if (action != null) action.Invoke((T)obj.asset);
        }
    }
}
