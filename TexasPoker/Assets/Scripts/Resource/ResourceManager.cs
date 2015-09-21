using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Util;
using Client.Base;
using Configs;
using Contr;
using Events;

public enum UseFilePath
{
    WebBundlePath = 0,						//Bundle Assets On Web
    LocalBundlePath = 1,					//Local Bundle Assets
    LocalPath = 2							//Local Path Without Bundle
}

public enum AssetsEnum
{
    Audio = 1,
    Effect,
    Icon,
    Model,
    TextAsset,
    UIPanel,
    Scene
}

public class ResourceManager : Singleton<ResourceManager>,Controller
{
    public ResourceManager()
    {

        UnityEngine.Caching.CleanCache();
    }
    public void Destory()
    {
        foreach(WWW packet in m_PacketToUnload)
        {
            packet.assetBundle.Unload(true);
        }
    }
    public GameObject GetUIPrefabByName(string name)        //装载UI资源
    {
        return LoadResource<GameObject>(AssetsEnum.UIPanel, name);
    }
    public GameObject GetModelPrefebByName(string name)
    {
        return LoadResource<GameObject>(AssetsEnum.Model, name);
    }
    public TextAsset GetTextAssetByName(string name)
    {
        return LoadResource<TextAsset>(AssetsEnum.TextAsset, name);
    }
    public Texture2D GetIconByName(string name)        //装载UI资源
    {
        return LoadResource<Texture2D>(AssetsEnum.Icon, name);
    }
    public GameObject GetSceneByName(string name)
    {
        return LoadResource<GameObject>(AssetsEnum.Scene, name);
    }
    //public GameObject GetOtherByName(string name)
    //{
    //    return LoadResource<GameObject>(AssetsEnum.Other, name);
    //}

    public string LoadTextFile(string filePath)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.Android:
            case RuntimePlatform.OSXWebPlayer:
            case RuntimePlatform.WindowsWebPlayer:
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.WindowsPlayer:
                {
                    TextAsset XMLFile = (TextAsset)Resources.Load(filePath);    //Load from asset
                    if (XMLFile == null)
                    {
                        return string.Empty;
                    }
                    return XMLFile.text;
                }

        }
        return string.Empty;
    }

    public event System.Action<AssetsEnum, float> OnLoadProgressEvent;
    public UseFilePath useFilePath = UseFilePath.LocalPath;
    private WWW m_ABPakcet = null;
    public int m_TotalRes = 0;
    public int m_LoadedRes = 0;
    public static readonly string PathURL =
#if UNITY_ANDROID
	"jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
	Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
    "file://" + Application.dataPath + "/StreamingAssets/";
#else
     string.Empty;
#endif
    public IEnumerator LoadResource()
    {
        if(useFilePath != UseFilePath.LocalBundlePath)
        {
            yield return 0;
        }
        else
        {
            yield return CoroutineManager.StartCoroutine(ResourceManager.Instance.LoadABPakcet());
            for(AssetsEnum i = AssetsEnum.Audio; i<=AssetsEnum.Scene; i++)
            {
                CfgAB ab = CfgABMgr.Instance.GetConfig((int)i);
                if(ab == null)
                {
                    continue;
                }
                if(ab.ABDepend != "null")
                {
                    yield return CoroutineManager.StartCoroutine(ResourceManager.Instance.LoadDepend(PathURL + ab.ABDepend));
                    m_LoadedRes++;
                    GameDispatcher.Instance.dispatchEvent(GameEvents.RES_LOADED);
                }
                if(ab.AB != "null")
                {
                    yield return CoroutineManager.StartCoroutine(ResourceManager.Instance.LoadPackage(PathURL + ab.AB, i));
                    m_LoadedRes++;
                    GameDispatcher.Instance.dispatchEvent(GameEvents.RES_LOADED);
                }
            }
            yield return 0;
        }
    }
    public IEnumerator LoadPackage(string url, AssetsEnum index)         //装载包
    {
        WWW newPackage = null;
        if (useFilePath == UseFilePath.LocalBundlePath)
        {
            newPackage = WWW.LoadFromCacheOrDownload(url, 0);
        }
        else if (useFilePath == UseFilePath.WebBundlePath)
        {
            newPackage = new WWW(url);
        }
        else
        {
            yield return 0;
        }
        if (newPackage.error == null)
        {
            m_packageMap.Add(index, newPackage);
            while (!m_packageMap[index].isDone)                     //若装载没结束
            {
                if (OnLoadProgressEvent != null)
                {
                    OnLoadProgressEvent(index, m_packageMap[index].progress);
                }
                yield return 0;
            }
        }
        yield return 0;
    }
    public IEnumerator LoadDepend(string url)
    {
        WWW newPackage = null;
        newPackage = WWW.LoadFromCacheOrDownload(url, 0);
        if(newPackage.error == null)
        {
            while (!newPackage.isDone)
            {
                yield return 0;
            }
            newPackage.assetBundle.LoadAllAssets();
            m_PacketToUnload.Add(newPackage);
        }
        yield return 0;
    }
    public IEnumerator LoadABPakcet()
    {
        WWW newPackage = null;
        newPackage = WWW.LoadFromCacheOrDownload(PathURL + "ab.asset", 0);
        if (newPackage.error == null)
        {
            while (!newPackage.isDone)
            {
                yield return 0;
            }
            TextAsset text = newPackage.assetBundle.LoadAsset("ab", typeof(TextAsset)) as TextAsset;
            CfgABMgr.Instance.Init(text);
            foreach (CfgAB item in CfgABMgr.Instance.ItemTable.Values)
            {
                if(item.ABDepend != "null")
                {
                    m_TotalRes++;
                }
                if(item.AB != "null")
                {
                    m_TotalRes++;
                }
            }
            m_PacketToUnload.Add(newPackage);
        }
        yield return 0;
    }
    public T LoadResource<T>(AssetsEnum index, string name) where T : UnityEngine.Object
    {
        Resources.UnloadUnusedAssets();
        WWW loadBundleModel = null;
        m_packageMap.TryGetValue(index, out loadBundleModel);

        if (useFilePath == UseFilePath.LocalPath
            || loadBundleModel == null)		//Load Directly
        {
            //return (Resources.Load(name, typeof(T)) as T);
            string assetPath = "Assets/FishingGame/ResourcesDev/" + Convert.ToString(index) + "/"+name;
            return UnityEditor.AssetDatabase.LoadMainAssetAtPath(assetPath) as T;
        }
        else //Load From Bundle Asset
        {
            name = name.Split(new char[] { '.' })[0];
            if (loadBundleModel.assetBundle.Contains(name))
            {
                return loadBundleModel.assetBundle.LoadAsset(name, typeof(T)) as T;
            }
            return null;
        }
    }

    Dictionary<AssetsEnum, WWW> m_packageMap = new Dictionary<AssetsEnum, WWW>();
    List<WWW> m_PacketToUnload = new List<WWW>();



    public Events.GameDispatcher getDispatcher()
    {
        throw new NotImplementedException();
    }

    public void registerSocket()
    {
        throw new NotImplementedException();
    }

    public void addEvent()
    {
        throw new NotImplementedException();
    }

    public void Init()
    {
        UnityEngine.Caching.CleanCache();

    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}
