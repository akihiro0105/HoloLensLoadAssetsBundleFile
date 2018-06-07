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
            Debug.Log("Load File");
            yield return FileIOControl.ReadBytesFile(name, (d) => { data = d; });
            Debug.Log("Load AssetBundle");
            yield return AssetBundleData<T>(data, assets, action, progress);
            Debug.Log("Load Complete");
        }

        public IEnumerator AssetBundleData<T>(byte[] data, string[] assets, Action<T[]> action, Action<float> progress=null) where T : UnityEngine.Object
        {
            AssetBundle assetBundle = null;
            // Load AssetBundle
            if (data == null) yield break;
            var resultassetbundle = AssetBundle.LoadFromMemoryAsync(data);
            yield return resultassetbundle;
            assetBundle = resultassetbundle.assetBundle;

            // Load Asset
            var t = new List<T>();
            for (int i = 0; i < assets.Length; i++)
            {
                float allprogress = (float)i / assets.Length;
                yield return LoadAsset<T>(assetBundle, assets[i], (o) => { if (o != null) t.Add((T)o); }, (p) => { if (progress != null) progress.Invoke(p + allprogress); });
            }
            if (action != null) action.Invoke(t.ToArray());

            // Unload AssetBundle
            if (assetBundle != null) assetBundle.Unload(false);
        }

        private IEnumerator LoadAsset<T>(AssetBundle assetbundle, string name, Action<T> action, Action<float> progress = null) where T : UnityEngine.Object
        {
            var obj = assetbundle.LoadAssetAsync<T>(name);
            while (!obj.isDone)
            {
                if (progress != null) progress.Invoke(obj.progress);
                yield return new WaitForEndOfFrame();
            }
            if (action != null) action.Invoke((T)obj.asset);
        }
    }
}
