using Client.Base;
using Util;
using System.Collections.Generic;
using UnityEngine;
public interface ITabItemWithKey<TKey>
{
    bool ReadItem(TabFile tf);
    TKey GetKey();
}
public abstract class CCfgKeyMgrTemplate<TManager, TKey, TItem> : Singleton<TManager>
    where TManager : class, new()
    where TItem : ITabItemWithKey<TKey>, new()
{
    protected SortedList<TKey, TItem> m_ItemTable = new SortedList<TKey, TItem>();

    public virtual SortedList<TKey, TItem> ItemTable { get { return m_ItemTable; } }

    public virtual bool Init(TextAsset text)
    {
        if (null == text)
        {
            return false;
        }

        if (null != m_ItemTable && m_ItemTable.Count > 0)
        {
            return false;
        }

        m_ItemTable.Clear();

        TabFile tf = new TabFile(text.name, text.text);
        while (tf.Next())
        {
            TItem item = new TItem();
            if (item.ReadItem(tf) == false)
            {
                continue;
            }
            if (m_ItemTable.ContainsKey(item.GetKey()))
            {
                continue;
            }
            m_ItemTable.Add(item.GetKey(), item);
        }
        return true;
    }

    public virtual TItem GetConfig(TKey key)
    {
        if (m_ItemTable.ContainsKey(key))
        {
            return m_ItemTable[key];
        }
        return default(TItem);
    }

    public virtual bool Has(TKey key)
    {
        return m_ItemTable.ContainsKey(key);
    }

    public virtual TItem this[TKey key]
    {
        get { return GetConfig(key); }
    }
}