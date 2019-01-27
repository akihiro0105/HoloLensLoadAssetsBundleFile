using System;
using System.Collections;
using System.Collections.Generic;
using HoloLensModule.Environment;
using UnityEngine;

namespace LoadAssetsBundleFile
{
    /// <summary>
    /// AssetBundleの読み込みと展開
    /// </summary>
    public class AssetBundleLoader
    {
        /// <summary>
        /// AssetBundleの読み込みと展開
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="assets"></param>
        /// <param name="action"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public static IEnumerator AssetBundleFile<T>(string name, string[] assets,Action<T[]> action, Action<float> progress=null) where T : UnityEngine.Object
        {
            byte[] data = null;
            Debug.Log("Load File");
            yield return FileIOControl.ReadBytesFile(name, (d) => data = d);
            Debug.Log("Load AssetBundle");
            yield return assetBundleData<T>(data, assets, action, progress);
            Debug.Log("Load Complete");
        }

        /// <summary>
        /// AssetBundleの読み込み
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="assets"></param>
        /// <param name="action"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        private static IEnumerator assetBundleData<T>(byte[] data, string[] assets, Action<T[]> action, Action<float> progress=null) where T : UnityEngine.Object
        {
            // AssetBundleの読み込み
            if (data == null) yield break;
            var resultAssetBundle = AssetBundle.LoadFromMemoryAsync(data);
            yield return resultAssetBundle;
            var assetBundle = resultAssetBundle.assetBundle;

            // 内部Assetの展開
            var t = new List<T>();
            for (var i = 0; i < assets.Length; i++)
            {
                var allProgress = (float)i / assets.Length;
                yield return loadAsset<T>(assetBundle, assets[i], (o) => { if (o != null) t.Add((T)o); }, (p) => { if (progress != null) progress.Invoke(p + allProgress); });
            }
            if (action != null) action.Invoke(t.ToArray());

            // AssetBundleの解放
            if (assetBundle != null) assetBundle.Unload(false);
        }

        /// <summary>
        /// Assetの読み込み
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetBundle"></param>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        private static IEnumerator loadAsset<T>(AssetBundle assetBundle, string name, Action<T> action, Action<float> progress) where T : UnityEngine.Object
        {
            var obj = assetBundle.LoadAssetAsync<T>(name);
            while (!obj.isDone)
            {
                if (progress != null) progress.Invoke(obj.progress);
                yield return new WaitForEndOfFrame();
            }
            if (action != null) action.Invoke((T)obj.asset);
        }
    }
}
