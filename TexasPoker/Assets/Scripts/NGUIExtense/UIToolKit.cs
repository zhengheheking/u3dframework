using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIToolkits
{
    static public void SetUILayer(GameObject obj, int layer)        //Todo 干啥用的？
    {
        SetRecursively(obj, (int)layer);
    }

    private static void SetRecursively(GameObject obj, int layer)   //递归设置Layer
    {
        obj.layer = layer;
        if (obj.transform.childCount == 0) return;
        for (var i = 0; i < obj.transform.childCount; i++)
        {
            SetRecursively(obj.transform.GetChild(i).gameObject, layer);
        }
    }

    static public Transform FindChildCheckActive(Transform panelTransform, string name)
    {
        foreach (Transform transform in panelTransform)
        {
            if (!transform.gameObject.activeSelf)
            {
                continue;
            }

            if (transform.name == name)
            {
                return transform;
            }

            if (transform.childCount > 0)
            {
                Transform childTransform = FindChild(transform, name);
                if (childTransform != null)
                {
                    return childTransform;
                }
            }
        }
        return null;
    }

    static public Transform FindChild(Transform panelTransform, string name)
    {
        foreach (Transform transform in panelTransform)
        {
            if (transform.name == name)
            {
                return transform;
            }

            if (transform.childCount > 0)
            {
                Transform childTransform = FindChild(transform, name);
                if (childTransform != null)
                {
                    return childTransform;
                }
            }
        }
        return null;
    }

    //用名字和组件类型查找组件
    public static T FindComponent<T>(Transform panelTransform, string name) where T : Component
    {
        return FindChild(panelTransform, name).GetComponent<T>();
    }

    //用组件类型查找组件
    static public T FindComponent<T>(Transform transParent) where T : Component
    {
        foreach (Transform transform in transParent)
        {
            T comp = transform.GetComponent<T>();
            if (comp != null)
            {
                return comp;
            }

            if (transform.childCount > 0)
            {
                T childComp = FindComponent<T>(transform);
                if (childComp != null)
                {
                    return childComp;
                }
            }
        }
        return null;
    }

    //查找Transform下面所有某类型控件，结果放到传入的List里面
    static public void FindComponents<T>(Transform panelTransform, List<T> list) where T : Component
    {
        T tt= panelTransform.GetComponent<T>();
        if (tt != null)
        {
            list.Add(tt);
        }
        foreach (Transform transform in panelTransform)
        {
            FindComponents(transform, list);			//查找子节点
        }
    }

    static public void FindComponents<T, T2>(Transform panelTransform, List<T2> list) where T : T2 where T2 : Component
    {
        T2 tt = panelTransform.GetComponent<T2>();
        if (tt != null)
        {
            list.Add(tt);
        }
        foreach (Transform transform in panelTransform)
        {
            FindComponents(transform, list);			//查找子节点
        }
    }

    public static bool AddChildComponentMouseClick(GameObject gameObject, EventDelegate.Callback callBack)
    {
        if (null == gameObject)
        {
            Debug.LogError(" gameObject is null .");
            return false;
        }

        UIButton button = gameObject.GetComponent<UIButton>();
        if (button == null)
        {
            return false;
        }
        EventDelegate.Set(button.onClick, callBack);
        return true;
    }

    public static bool AddChildComponentMouseClick(Transform paneltransform, string name, EventDelegate.Callback action)
    {
        Transform tansform = UIToolkits.FindChild(paneltransform, name);
        if (null == tansform)
            return false;
         return AddChildComponentMouseClick(tansform.gameObject, action);
    }

    static public Vector2 GetUIObjectScreenPos(Transform uiObjTrans)
    {
        if (uiObjTrans == null || Camera.main == null)
        {
            return new Vector2(0, 0);
        }
        Vector3 viewpos = Camera.main.WorldToViewportPoint(uiObjTrans.position);
        Vector3 screenpos = Camera.main.ViewportToScreenPoint(viewpos);

        //float multiplier = UIWindowManager.Singleton.mUIRoot.manualHeight / Screen.height;
        float multiplier = 1.0f;
        screenpos = new Vector3(screenpos.x * multiplier, Screen.height - screenpos.y * multiplier, 0);
        return new Vector2(screenpos.x, screenpos.y);
    }



    ///模仿NGUI写个PlaySound，主要是为了播放音乐
    static AudioListener mListener;
    static AudioSource mCurMusic;

    static public void PlayMusic(AudioClip clip)
    {
        if (mCurMusic == null)
        {
            mCurMusic = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        }

        if (clip != null)
        {
            mCurMusic.clip = clip;
            mCurMusic.Play();
        }
    }

    static public void StopMusic()
    {
        if (mCurMusic == null)
        {
            mCurMusic = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        }
        mCurMusic.Stop();
    }
}
