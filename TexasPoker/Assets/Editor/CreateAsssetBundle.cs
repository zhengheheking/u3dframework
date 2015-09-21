using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MainAsset
{
    public string m_strFilePath;
    public string m_Name;
    public UnityEngine.Object m_obj;
    public MainAsset(string strFilePath, UnityEngine.Object obj, string name)
    {

        m_strFilePath = strFilePath;
        m_obj = obj;
        m_Name = name;
    }

}
public class AssetEntry
{
    public List<MainAsset> m_AssetList;
    public List<UnityEngine.Object> m_DependList;
}
class ResourceDeploy
{
    static SortedList<AssetsEnum, AssetEntry> m_AssetMgr = new SortedList<AssetsEnum, AssetEntry>();
	
	private class objectCompare : IComparer<UnityEngine.Object>
	{
		public int Compare(UnityEngine.Object x, UnityEngine.Object y)
		{
			return x.GetInstanceID() - y.GetInstanceID();
		}
	}
	
	private class assetListCompare : IComparer<List<MainAsset>>
	{
		public int Compare(List<MainAsset> x, List<MainAsset> y)
		{
			if(x.Count != y.Count)
				return y.Count - x.Count;
			
			for(int i=0; i<x.Count; i++)
			{
				MainAsset mx = x[i];
				MainAsset my = y[i];
				if(mx.m_strFilePath != my.m_strFilePath)
				{
					return string.Compare(mx.m_strFilePath, my.m_strFilePath);
				}
			}	
			return 0;
		}
	}
	
	public static string buildDir
	{
		get
		{
			switch(EditorUserBuildSettings.activeBuildTarget)
			{
			case(BuildTarget.StandaloneWindows):
			case(BuildTarget.WebPlayer):
                return Application.dataPath + "/StreamingAssets/";
				
			default:
				return null;
			}
		}
	}
	
	public static string SrcResPath
	{
		get
		{
			return Application.dataPath + "/FishingGame/ResourcesDev/";
		}
	}
	
	[MenuItem("Tools/Deploy")]
	public static void Deploy()
	{
        m_AssetMgr.Clear();
        for(AssetsEnum i=AssetsEnum.Audio; i<=AssetsEnum.Scene; i++)
        {
            string directoryPath = SrcResPath + Convert.ToString(i) + "/";
            string assetPath = "Assets/FishingGame/ResourcesDev/" + Convert.ToString(i) + "/";
            DirectoryInfo TheFolder = new DirectoryInfo(directoryPath);
            List<MainAsset> assetList = new List<MainAsset>();
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                UnityEngine.Object fileObj = AssetDatabase.LoadMainAssetAtPath(assetPath + NextFile.Name);
                if (fileObj == null)
                {
                    continue;
                }
                MainAsset asset = new MainAsset(assetPath + NextFile.Name, fileObj, NextFile.Name);
                assetList.Add(asset);
            }
            AssetEntry entry = new AssetEntry();
            entry.m_AssetList = assetList;
            m_AssetMgr.Add(i, entry);
        }
        
        /*TheFolder = new DirectoryInfo(SrcResPath + "../scenes");
        assetList = new List<MainAsset>();
        assetPath = "Assets/FishingGame/scenes/";
        foreach (FileInfo NextFile in TheFolder.GetFiles())
        {
            UnityEngine.Object fileObj = AssetDatabase.LoadMainAssetAtPath(assetPath + NextFile.Name);
            if (fileObj == null)
            {
                continue;
            }
            MainAsset asset = new MainAsset(assetPath + NextFile.Name, fileObj, NextFile.Name);
            assetList.Add(asset);
        }
        m_AssetMgr.Add(AssetEnum.Scene, assetList);*/
        if (Directory.Exists(buildDir))
        {
            Directory.Delete(buildDir, true);
            Directory.CreateDirectory(buildDir);
        }
        foreach(KeyValuePair<AssetsEnum, AssetEntry> pair in m_AssetMgr)
        {
            List<UnityEngine.Object> objList = new List<UnityEngine.Object>();
            foreach (MainAsset asset in pair.Value.m_AssetList)
            {
                objList.Add(asset.m_obj);
            }
            List<UnityEngine.Object> listDepends = new List<UnityEngine.Object>();
            UnityEngine.Object[] dependObjects = EditorUtility.CollectDependencies(objList.ToArray());
            
            for (int j = 0; j < dependObjects.Length; j++)
            {
                UnityEngine.Object dependObj = dependObjects[j];
                listDepends.Add(dependObj);
            }
            if(dependObjects.Length <= objList.Count)
            {
                continue;
            }
            pair.Value.m_DependList = listDepends;
        }
        StringBuilder builder = new StringBuilder();
        builder.Append("Id"+"\t"+"ABDepend"+"\t"+"AB\r\n");
		//----------------------------------------------------------------------打包
        BuildAssetBundleOptions options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle;
		BuildPipeline.PushAssetDependencies();
		
		// 打包Shared

        foreach (KeyValuePair<AssetsEnum, AssetEntry> kvp in m_AssetMgr)
        {
            string line = ((int)kvp.Key).ToString()+"\t";
            BuildPipeline.PushAssetDependencies();
            string strTgtPath;
            List<UnityEngine.Object> listObject = kvp.Value.m_DependList;
            if(listObject != null)
            {
                strTgtPath = buildDir + Convert.ToString(kvp.Key) + "Depends.asset";

                bool isSucess = BuildPipeline.BuildAssetBundle(null, listObject.ToArray(), strTgtPath,
                    options, EditorUserBuildSettings.activeBuildTarget);
                if(isSucess)
                {
                    line += Convert.ToString(kvp.Key) + "Depends.asset"+"\t";
                }
                else
                {
                    line += "null\t";
                }
               
            }
            else
            {
                line += "null\t";
            }
            if(kvp.Key == AssetsEnum.Scene)
            {
                List<string> pathList = new List<string>();
                foreach (MainAsset asset in kvp.Value.m_AssetList)
                {
                    pathList.Add(asset.m_strFilePath);
                }
                if(pathList.Count > 0)
                {
                    strTgtPath = buildDir + Convert.ToString(kvp.Key) + ".asset";
                    BuildPipeline.PushAssetDependencies();
                    string errorScene = BuildPipeline.BuildPlayer(pathList.ToArray(), strTgtPath,
                                EditorUserBuildSettings.activeBuildTarget, BuildOptions.BuildAdditionalStreamedScenes);
                    if (string.IsNullOrEmpty(errorScene))
                    {
                        line += Convert.ToString(kvp.Key) + ".asset";
                    }
                    else
                    {
                        line += "null";
                    }
                    BuildPipeline.PopAssetDependencies();
                }
                else
                {
                    line += "null";
                }
            }
            else
            {
                List<UnityEngine.Object> objList = new List<UnityEngine.Object>();
                foreach (MainAsset asset in kvp.Value.m_AssetList)
                {
                    objList.Add(asset.m_obj);
                }
                if (objList.Count > 0)
                {
                    strTgtPath = buildDir + Convert.ToString(kvp.Key) + ".asset";
                    BuildPipeline.PushAssetDependencies();
                    bool isSuccess = BuildPipeline.BuildAssetBundle(null, objList.ToArray(), strTgtPath,
                                options, EditorUserBuildSettings.activeBuildTarget);
                    if (isSuccess)
                    {
                        line += Convert.ToString(kvp.Key) + ".asset";
                    }
                    else
                    {
                        line += "null";
                    }
                    BuildPipeline.PopAssetDependencies();

                }
                else
                {
                    line += "null";
                }
            }
            
            BuildPipeline.PopAssetDependencies();
            line += "\r\n";
            builder.Append(line);
        }

		BuildPipeline.PopAssetDependencies();
        if (File.Exists(Application.dataPath + "/FishingGame/ResourcesDev/TestAsset/" + "ab.txt"))
        {
            File.Delete(Application.dataPath + "/FishingGame/ResourcesDev/TextAsset/" + "ab.txt");
        }
        Utils.WriteFile(Application.dataPath + "/FishingGame/ResourcesDev/TextAsset/" + "ab.txt", builder.ToString());
        AssetDatabase.Refresh();

        BuildAB();
        Debug.Log("build success");
        AssetDatabase.Refresh();
	}
    public static void BuildAB()
    {
        UnityEngine.Object fileObj = AssetDatabase.LoadMainAssetAtPath("Assets/FishingGame/ResourcesDev/TextAsset/" + "ab.txt");
        if(fileObj == null)
        {
            return;
        }
        BuildAssetBundleOptions options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle;
        BuildPipeline.PushAssetDependencies();
        string strTgtPath = buildDir + "ab.asset";
        BuildPipeline.BuildAssetBundle(fileObj, null, strTgtPath,
            options, EditorUserBuildSettings.activeBuildTarget);
        BuildPipeline.PopAssetDependencies();
        //File.Delete(Application.dataPath + "/FishingGame/ResourcesDev/TextAsset/" + "ab.txt");
    }
        [MenuItem("Tools/Test1")]

	public static void Test1()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("Id" + "\t" + "ABDepend" + "\t" + "AB");
        string[] Title = builder.ToString().Split(new char[] { '\t' });
       /* UnityEngine.Object fileObj = AssetDatabase.LoadMainAssetAtPath("Assets/FishingGame/Resources/UIPanel/" + "UIScene.prefab");
        UnityEngine.Object[] dependObjects = EditorUtility.CollectDependencies(new UnityEngine.Object[] { fileObj });
        List<UnityEngine.Object> listDepends = new List<UnityEngine.Object>();      
        for (int j = 0; j < dependObjects.Length; j++)
        {
            UnityEngine.Object dependObj = dependObjects[j];
            listDepends.Add(dependObj);
        }
        BuildAssetBundleOptions options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle;
        BuildPipeline.PushAssetDependencies();
        string strTgtPath = buildDir + "aaDepends.asset";

        bool isSucess = BuildPipeline.BuildAssetBundle(null, listDepends.ToArray(), strTgtPath,
            options, EditorUserBuildSettings.activeBuildTarget);
        BuildPipeline.PushAssetDependencies();
        bool isSuccess = BuildPipeline.BuildAssetBundle(null, new UnityEngine.Object[] { fileObj }, buildDir + "aa.asset",
                    options, EditorUserBuildSettings.activeBuildTarget);
        BuildPipeline.PopAssetDependencies();
        BuildPipeline.PopAssetDependencies();
        AssetDatabase.Refresh();*/
    }
	public static void Test()
	{
		BuildPipeline.BuildAssetBundle(Selection.activeObject, null, "E:/wwwResource.ab", BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
	}
	
	public static void TestDepend()
	{
		BuildPipeline.PushAssetDependencies();
		UnityEngine.Object temp1 = AssetDatabase.LoadMainAssetAtPath("Assets/UIResources/Atlases/Common/CommonAtlas.prefab");
		BuildPipeline.BuildAssetBundle(temp1,null,"E:/TestAB/com.ab",BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
		
		
		UnityEngine.Object temp4 = AssetDatabase.LoadMainAssetAtPath("Assets/UIResources/Font/DySongTi/songti12.prefab");
		BuildPipeline.BuildAssetBundle(temp4,null,"E:/TestAB/Font12.ab",BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
		
		UnityEngine.Object temp5 = AssetDatabase.LoadMainAssetAtPath("Assets/UIResources/Font/DySongTi/songti14.prefab");
		BuildPipeline.BuildAssetBundle(temp5,null,"E:/TestAB/Font14.ab",BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
		
		UnityEngine.Object temp6 = AssetDatabase.LoadMainAssetAtPath("Assets/UIResources/Font/DySongTi/songti16.prefab");
		BuildPipeline.BuildAssetBundle(temp6,null,"E:/TestAB/Font16.ab",BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
		
//		BuildPipeline.PushAssetDependencies();
//		UnityEngine.Object temp2 = AssetDatabase.LoadMainAssetAtPath("Assets/UIResources/Atlases/Login/LoginAtlas.prefab");
//		BuildPipeline.BuildAssetBundle(temp2,null,"E:/TestAB/LoginUI.ab",BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
//		BuildPipeline.PopAssetDependencies();
//		
//		BuildPipeline.PushAssetDependencies();
//		UnityEngine.Object temp2 = AssetDatabase.LoadMainAssetAtPath("Assets/UIResources/Prefab/LoginUI.prefab");
//		BuildPipeline.BuildAssetBundle(temp2,null,"E:/TestAB/LoginUI.ab",BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
//		BuildPipeline.PopAssetDependencies();

		
		BuildPipeline.PopAssetDependencies();
		
		//BuildPipeline.PushAssetDependencies();
		UnityEngine.Object temp2 = AssetDatabase.LoadMainAssetAtPath("Assets/UIResources/Prefab/BagWindow.prefab");
		BuildPipeline.BuildAssetBundle(temp2,null,"E:/TestAB/BagWindow.ab",BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
		//BuildPipeline.PopAssetDependencies();
		
		//BuildPipeline.PushAssetDependencies();
		UnityEngine.Object temp3 = AssetDatabase.LoadMainAssetAtPath("Assets/UIResources/Prefab/ChatWindow.prefab");
		BuildPipeline.BuildAssetBundle(temp3,null,"E:/TestAB/ChatWindow.ab",BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
		//BuildPipeline.PopAssetDependencies();
		
		//BuildPipeline.BuildAssetBundle(Selection.activeObject, null, "E:/222.asset", BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
	}
	
	
	public static void TestBuildPlayer()
	{
        string error = BuildPipeline.BuildPlayer(new string[] { "Assets/FishingGame/scenes/fishMain.unity" }, "E:/scene.asset", EditorUserBuildSettings.activeBuildTarget, BuildOptions.BuildAdditionalStreamedScenes);
	}
	
}