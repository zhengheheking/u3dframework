using Client.Base;
using Contr;
using Events;
using UnityEngine;
namespace GameLogic
{
    public enum ESceneType
    {
        LoginScene,
        SelectScene,
        FishScene,
    }
    public class SceneManager : Singleton<SceneManager>, Controller
    {
        public string LoadedSceneName { get;  private set; }
        public ESceneType LoadedSceneType { get;  private set; }
        public bool IsLoading { get; private set; }
        public SceneManager()
        {
            LoadedSceneName = string.Empty;
            LoadedSceneType = ESceneType.LoginScene;
            IsLoading = false;
        }
        public void Update()
        {
            if (!IsLoading) return;
        }
        public void LoadScene(string sceneName, ESceneType sceneType)
        {
            LoadedSceneName = sceneName;
            LoadedSceneType = sceneType;
            GameObject s = ResourceManager.Instance.GetSceneByName(sceneName + ".unity");
            Application.LoadLevelAsync(sceneName);
            UIWindowManager.Instance.HideAllWindow();
        }
        public void OnLevelLoaded()
        {
            IsLoading = false;
            if(LoadedSceneType == ESceneType.SelectScene)
            {
            
            }
            if(LoadedSceneType == ESceneType.FishScene)
            {
                LogicApp.Instance.InitGameState();
                //Debug.Log(Screen.width + " " + Screen.height);
            }
        }


        public GameDispatcher getDispatcher()
        {
            throw new System.NotImplementedException();
        }

        public void registerSocket()
        {
            throw new System.NotImplementedException();
        }

        public void addEvent()
        {
            throw new System.NotImplementedException();
        }

        public void Init()
        {
            throw new System.NotImplementedException();
        }
    }
}