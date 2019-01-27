# LoadAssetsBundleFile
- Twitter : [@akihiro01051](https://twitter.com/akihiro01051)

## 動作環境
- Windows10 Creators Update
- Unity 2017.4
- Visual Studio 2017
- HoloLens RS4
- Windows MixedReality Device

----------

## 概要
- UnityEditorでAssetBundleを作成し，動作環境で読み込みと展開を行います

## 利用パッケージ
- [HoloLensModule](https://github.com/akihiro0105/HoloLensModule)

## 内容
- LoadAssetsBundleFileSample
    + Localフォルダに作成されたAssetBundleを読み込んで展開します

- AssetBundle作成方法
    + UnityEditorのProject内で適当なアセットを選択
    + InspectorのAssetLabelsの「New...」でAssetBundle名を設定
    + UnityEditorのメニューバーのAssets/BuildAssetBundlesでAssetBundleを作成
    + 作成されたAssetBundleはプロジェクト内のLocalフォルダ内に保存
