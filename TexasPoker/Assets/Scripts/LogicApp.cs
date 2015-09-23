using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Socket.Net;
using Socket;
using Util;
using Contr;
using GameLogic;
using Configs;
using Events;
using Client.Base;

public enum GameState
{
    MAIN_UI = 0,
    GAME
}
class LogicApp : MonoBehaviour
{
    public static LogicApp Instance { get; private set; }
    [HideInInspector]
    public GameState m_GameState;

    public void Start()
    {
        LogicApp.Instance = this;
        DontDestroyOnLoad(this);
        Invoke("SetCustomCursor", 1f);
        Application.targetFrameRate = 60;
        CoroutineManager.Init(this);

        CoroutineManager.StartCoroutine(StartInit());
    }
    private IEnumerator StartInit()
    {
        yield return CoroutineManager.StartCoroutine(ResourceManager.Instance.LoadResource());
        yield return CoroutineManager.StartCoroutine(Config.Load());
        yield return CoroutineManager.StartCoroutine(SpawnPool.Instance.Init());
        m_GameState = GameState.MAIN_UI;

        UIWindowManager.Instance.CreateWindow<UCLogin>(EUIPanel.UILogin);
        UIWindowManager.Instance.GetUIWindow(EUIPanel.UILogin).ShowWindow();
        yield return null;
    }

    public void Update()
    {
        CustomSocket.Instance.Loop();
        if (m_GameState == GameState.GAME)
        {
            LogicWorld.Instance.Update();
            TouchManager.Instance.Loop();
        }
        UIWindowManager.Instance.Update();
        TimerManager.Instance.Update();

    }
    void FixedUpdate()
    {
        if (m_GameState == GameState.GAME)
        {
            LogicWorld.Instance.FixedUpdate();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (m_GameState == GameState.GAME)
        {
            LogicWorld.Instance.OnCollisionEnter(collision);
        }
    }
    void OnTriggerEnter(Collider collider)
    {

    }
    void OnGUI()
    {
        UIWindowManager.Instance.OnGUI();
    }
    void LateUpdate()
    {
    }
    public void OnApplicationQuit()
    {
        ResourceManager.Instance.Destory();
        CustomSocket.Instance.Close();
        SpawnPool.Instance.DespawnAll();
    }
    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
    void OnLevelWasLoaded(int level)
    {
        SceneManager.Instance.OnLevelLoaded();
    }
    public void InitGameState()
    {
        m_GameState = GameState.GAME;
        TouchManager.Instance.Init();
        LogicWorld.Instance.Init();
    }
    private void SetCustomCursor()
    {
        Texture2D cursorTexture = ResourceManager.Instance.GetIconByName("Cursor.png");
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
    public void Gc()
    {
        GC.Collect();
    }
}
