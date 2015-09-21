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
using Components;


public class SpawnPool : Singleton<SpawnPool>
{
    public static bool UsePool = true;
    public IEnumerator Init()
    {
        yield return null;
    }
    private List<PrefabPool> m_PrefabPoolList = new List<PrefabPool>();
    public void CreatePrefabPool(string prefabName, int poolSize)
    {
        PrefabPool prefabPool = new PrefabPool(prefabName, poolSize);
        m_PrefabPoolList.Add(prefabPool);
    }
    public GameObject Spawn(string prefabName)
    {
        foreach(PrefabPool prefabPool in m_PrefabPoolList)
        {
            if(prefabPool.PrefabName == prefabName)
            {
                return prefabPool.Spawn();
            }
        }
        return null;
    }
    public void Despawn(GameObject go)
    {
        foreach (PrefabPool prefabPool in m_PrefabPoolList)
        {
            if (prefabPool.PrefabName == go.name)
            {
                prefabPool.Despawn(go);
            }
        }
    }
    public void DespawnAll()
    {
        foreach (PrefabPool prefabPool in m_PrefabPoolList)
        {
            prefabPool.DespawnAll();
        }
        m_PrefabPoolList.Clear();
    }
}

public class PrefabPool
{
    private int m_PoolSize;
    private const int MAX_POOL_SIZE = 50;
    public string PrefabName { get; private set; }
    private List<GameObject> m_GOList = new List<GameObject>();
    private GameObject m_Prefab;
    private void Init()
    {
        m_Prefab = ResourceManager.Instance.GetModelPrefebByName(PrefabName);
        if(SpawnPool.UsePool == false)
        {
            return;
        }
        for(int i=0; i<m_PoolSize; i++)
        {
            GameObject go = GameObject.Instantiate(m_Prefab);
            GameObject.DontDestroyOnLoad(go);
            go.SetActive(false);
            go.name = PrefabName;
            m_GOList.Add(go);
        }
    }
    public PrefabPool(string prefabName, int poolSize)
    {
        PrefabName = prefabName;
        m_PoolSize = poolSize > MAX_POOL_SIZE?MAX_POOL_SIZE:poolSize;
        Init();
    }
    public GameObject Spawn()
    {
        if(SpawnPool.UsePool)
        {
            if (m_GOList.Count > 0)
            {
                GameObject go = m_GOList[0];
                m_GOList.Remove(go);
                go.SetActive(true);
                return go;
            }
            else
            {
                GameObject go = GameObject.Instantiate(m_Prefab);
                GameObject.DontDestroyOnLoad(go);
                go.name = PrefabName;
                return go;
            }
        }
        else
        {
            U3dModel model = new U3dModel(PrefabName);
            return model.mGameObject;
        }
    }
    public void Despawn(GameObject go)
    {
        if(SpawnPool.UsePool)
        {
            go.SetActive(false);
            m_GOList.Add(go);
        }
        else
        {
            GameObject.Destroy(go);
        }
    }
    public void DespawnAll()
    {
        foreach (GameObject go in m_GOList)
        {
            GameObject.Destroy(go);
        }
    }
}