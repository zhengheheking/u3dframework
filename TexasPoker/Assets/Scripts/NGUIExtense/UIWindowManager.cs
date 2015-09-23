using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Client.Base;

public enum EUIPanel
{
    UILogin,
    UIHall,
}
//An Manager To Manage All UI And Window
public class UIWindowManager : Singleton<UIWindowManager>
{
    public UIWindowManager()
    {
        OnCreate();
    }
    public GameObject[] AnchorObject = new GameObject[9];
    public enum Anchor
    {
        Center = 0,
        TopLeft,
        Top,
        TopRight,
        Left,
        Right,
        BottomLeft,
        Bottom,
        BottomRight,
    }
    private Dictionary<EUIPanel, UIWindowCtrl> mWindowCtrlMap = new Dictionary<EUIPanel, UIWindowCtrl>();
    public delegate bool UIClickProcesser(GameObject clickObj);
    public UIClickProcesser ClickProcesser = null;
    public delegate bool UIDoubleClickProcesser(GameObject clickObj);
    public UIDoubleClickProcesser DoubleClickProcesser;
    public GameObject ClickObject = null;
    public UIRoot mUIRoot;
    public Dictionary<EUIPanel, UIWindowCtrl> mWindowCtrlToRemoveList = new Dictionary<EUIPanel, UIWindowCtrl>();
    public bool LockUIInputProp { set; get; }

    public void OnCreate()
    {
        Transform uiRootTransform = GameObject.Find("Camera").transform;
        for (Anchor i = Anchor.Center; i <= Anchor.BottomRight; i++)
        {
            AnchorObject[(int)i] = uiRootTransform.FindChild(i + "Anchor").gameObject;
        }
        mUIRoot = uiRootTransform.gameObject.GetComponent<UIRoot>();
    }
    
    public void Update()
    {
        foreach (KeyValuePair<EUIPanel, UIWindowCtrl> pair in mWindowCtrlMap)
        {
            if (pair.Value.GetWindow().Visible)
            {
                pair.Value.OnUpdate();
            }
        }

        if (mWindowCtrlToRemoveList.Count != 0)
        {
            foreach (UIWindowCtrl pUIWindowCtrl in mWindowCtrlToRemoveList.Values)
            {
                DestroyWindow(pUIWindowCtrl);
            }
            mWindowCtrlToRemoveList.Clear();
        }

        ClickObject = null;
    }

    public void OnGUI()
    {
        foreach (KeyValuePair<EUIPanel, UIWindowCtrl> pair in mWindowCtrlMap)
        {
            if (pair.Value.GetWindow().Visible)
            {
                pair.Value.GetWindow().OnGUI();
            }

        }
    }

    public void UpdateWindowUIData(EUIPanel windowName)
    {
        UIWindowCtrl pWindowCtrl = GetUIWindow(windowName);
        if (pWindowCtrl == null)
        {
            //Debug.LogError("Can't find windowname " + windowName);
            return;
        }
        if (pWindowCtrl.GetWindow().Visible)
        {
            pWindowCtrl.GetWindow().UpdateUIData();
        }
    }

    public void UpdateUIData()
    {
        foreach (KeyValuePair<EUIPanel, UIWindowCtrl> pair in mWindowCtrlMap)
        {
            if (pair.Value.GetWindow().Visible)
            {
                pair.Value.GetWindow().UpdateUIData();
            }
        }
    }

    public int CloseWindowsWithFlag(int flag)
    {
        int count = 0;
        ExcuteFuncOnWindowsWithFlag(
            delegate(UIWindowCtrl pWinCtrl)
            {
                if (pWinCtrl.GetWindow().Visible)
                {
                    ++count;
                    pWinCtrl.HideWindow();
                }
            },
            flag);
        return count;
    }

    public void ExcuteFuncOnWindowsWithFlag(System.Action<UIWindowCtrl> action, int flag)
    {
        foreach (KeyValuePair<EUIPanel, UIWindowCtrl> pair in mWindowCtrlMap)
        {
            if ((pair.Value.GetWindow().mUIFlag & flag) > 0)
            {
                action(pair.Value);
            }
        }
    }

    public GameObject FindUIControll(string name)
    {
        foreach (KeyValuePair<EUIPanel, UIWindowCtrl> pair in mWindowCtrlMap)
        {
            if (!pair.Value.GetWindow().Visible)
            {
                continue;
            }
            if (pair.Value.GetWindow().mUIObject.transform.localPosition.x < -1000) continue;
            Transform trans = UIToolkits.FindChild(pair.Value.GetWindow().mUIObject.transform, name);
            if (trans != null)
            {
                return trans.gameObject;
            }
        }
        return null;
    }

    public GameObject FindUIControll(string name, out UIWindow pWindow)
    {
        foreach (KeyValuePair<EUIPanel, UIWindowCtrl> pair in mWindowCtrlMap)
        {
            if (pair.Value.GetWindow().mUIObject.transform.localPosition.x < -1000) continue;
            if (!pair.Value.GetWindow().Visible)
            {
                continue;
            }
            Transform trans = UIToolkits.FindChild(pair.Value.GetWindow().mUIObject.transform, name);
            if (trans != null)
            {
                pWindow = pair.Value.GetWindow();
                return trans.gameObject;
            }
        }
        pWindow = null;
        return null;
    }


    #region Create And Destory And Get Window Functions
    public UIWindowCtrl GetUIWindow(EUIPanel windowName)
    {
        UIWindowCtrl pWindowCtrl;
        bool bSuc = mWindowCtrlMap.TryGetValue(windowName, out pWindowCtrl);
        if (bSuc)
        {
            return pWindowCtrl;
        }
        return null;
    }

    public T CreateWindow<T>(EUIPanel prefabName) where T : UIWindowCtrl, new()
    {
        var window = CreateWindow<T>(prefabName, Anchor.Center);
        return window;
    }

    public GameObject InstancePreb(string prefabName)
    {
        GameObject prefab = ResourceManager.Instance.GetUIPrefabByName(prefabName);
        if (null == prefab)
        {
            Debug.LogError(string.Format("Load prefab name = {0} Error ", prefabName));
            return null;
        }
        return (GameObject)GameObject.Instantiate(prefab);
    }

    public bool CreatePrefab(UIWindowCtrl pWindowCtrl, UIWindow pWindow)
    {
        GameObject prefab = ResourceManager.Instance.GetUIPrefabByName(Convert.ToString(pWindow.prefabName)+".prefab");
        if (null == prefab)
        {
            Debug.LogError(string.Format("Load prefab name = {0} Error ", pWindow.prefabName));
            return false;
        }
        GameObject instance = (GameObject)GameObject.Instantiate(prefab);
        pWindow.mUIObject = instance;
        pWindow.Vo = null;
        pWindow.SetAnchor(AnchorObject[(int)pWindow.mAnchor]);
        pWindow.HideWindowWithoutInvoke();
        pWindowCtrl.OnCreate(pWindow);
        pWindow.AfterCreate();
        return true;
    }

    public T CreateWindow<T>(EUIPanel prefabName, Anchor anchor) where T : UIWindowCtrl, new()
    {
        UIWindowCtrl pUIWindowCtrl = null;
        if (mWindowCtrlToRemoveList.TryGetValue(prefabName, out pUIWindowCtrl))
        {
            mWindowCtrlToRemoveList.Remove(prefabName);
            return pUIWindowCtrl as T;
        }

        pUIWindowCtrl = GetUIWindow(prefabName);

        if (pUIWindowCtrl != null)
        {
            return pUIWindowCtrl as T;
        }
        pUIWindowCtrl = new T();
        mWindowCtrlMap[prefabName] = pUIWindowCtrl;
        UIWindow pUIWindow = Activator.CreateInstance(pUIWindowCtrl.GetWindowType()) as UIWindow;
        pUIWindow.prefabName = prefabName;
        pUIWindow.mAnchor = anchor;
        CreatePrefab(pUIWindowCtrl, pUIWindow);
        return (T)pUIWindowCtrl;
    }

    public void DestroyWindow(UIWindowCtrl pWindowCtrl)
    {
        pWindowCtrl.OnDestory();
        pWindowCtrl.ReleaseWindow();
        mWindowCtrlMap.Remove(pWindowCtrl.GetWindow().prefabName);
    }

    public void DestroyWindow(UIWindowCtrl pWindow, bool immediately)
    {
        if (!immediately)
        {
            mWindowCtrlToRemoveList.Add(pWindow.GetWindow().prefabName, pWindow);
            return;
        }
        DestroyWindow(pWindow);
    }

    public void HideAllWindow()
    {
        foreach (var key in mWindowCtrlMap.Keys)
        {
            if (mWindowCtrlMap[key].GetWindow().Visible)
                mWindowCtrlMap[key].HideWindow();
        }
    }

    ///// <summary>
    ///// try to hide ui if the ui exists
    ///// </summary>
    ///// <typeparam name="T">type of ui</typeparam>
    //public void TryToHideWindow<T>() where T: UIGameWindow
    //{
    //    var ui = GetUIWindow<T>();
    //    if (ui != null && ui.IsVisible)
    //    {
    //        ui.HideWindow();
    //    }
    //}
    #endregion
}
