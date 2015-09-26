using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class Utils
{
    public static readonly char[] CONFIG_VECTOR3_SEPARATOR = new char[]{';'};
    public static Vector3 String2Vector3(string strPoint)
    {
        Vector3 pt = new Vector3();
        if (string.IsNullOrEmpty(strPoint) == false)
        {
            string[] sepStr = strPoint.Split(CONFIG_VECTOR3_SEPARATOR);
            try
            {
                pt.x = sepStr.Length > 0 ? float.Parse(sepStr[0]) : 0f;
                pt.y = sepStr.Length > 1 ? float.Parse(sepStr[1]) : 0f;
                pt.z = sepStr.Length > 2 ? float.Parse(sepStr[2]) : 0f;
            }
            catch (System.Exception ex)
            {
            }
        }
        return pt;
    }
    public static int[] String2IntArray(string str)
    {
        string[] ss = str.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        int[] array = new int[ss.Length];
        for (int i = 0; i < ss.Length; i++)
        {
            array[i] = int.Parse(ss[i]);
        }
        return array;
    }
    public static Vector3[] String2Vector3Array(string str)
    {
        string[] ss = str.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        Vector3[] array = new Vector3[ss.Length];
        for (int i = 0; i < ss.Length; i++)
        {
            try
            {
                string[] sss = ss[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                Vector3 p = new Vector3(float.Parse(sss[0]), float.Parse(sss[1]), float.Parse(sss[2]));
                array[i] = p;
            }
            catch(Exception e)
            {
                string s = ss[i];
            }
        }
        return array;
    }
    public static int String2Int(string str)
    {
        return int.Parse(str);
    }
    public static float String2Float(string str)
    {
        return float.Parse(str);
    }

    public static float CalcDistanceXZ(Vector3 pos1, Vector3 pos2)
    {
        return new Vector3(pos1.x - pos2.x, 0, pos1.z - pos2.z).magnitude;
    }

    public static bool IsInRange<T>(T value, T vBegin, T vEnd) where T : IComparable
    {
        return value.CompareTo(vBegin) >= 0 && value.CompareTo(vEnd) < 0;
    }

    public static bool NotInRange<T>(T value, T vBegin, T vEnd) where T : IComparable
    {
        return !IsInRange<T>(value, vBegin, vEnd);
    }

    public static readonly float DEFAULT_TRIGGER_DISTANCE = 3.0f;
    public static bool IsTrigger(Vector3 pos1, Vector3 pos2)
    {
        return IsTrigger(pos1, pos2, DEFAULT_TRIGGER_DISTANCE);
    }

    //public static bool IsTrigger(Vector3 posTarget)
    //{
    //    return IsTrigger(posTarget, DEFAULT_TRIGGER_DISTANCE);
   // }

   // public static bool IsTrigger(Vector3 posTarget, float dist)
    //{
        //if (XLogicWorld.SP.MainPlayer == null)
        //{
        //    return false;
        //}
        //return IsTrigger(posTarget, XLogicWorld.SP.MainPlayer.Position, dist);
   // }

    public static bool IsTrigger(Vector3 pos1, Vector3 pos2, float dist)
    {
        return CalcDistanceXZ(pos1, pos2) <= dist;
    }
	
	public static T Instantiate<T>(T original) where T : MonoBehaviour
	{
		if(null == original) 
			return null;
		
		GameObject go = Instantiate(original.gameObject);
		if(null == go)
			return null;
		
		return go.GetComponent<T>();
	}
	
	public static GameObject Instantiate(GameObject original, Transform parent, Vector3 localPosition, Vector3 localRocation, Vector3 localScale)
	{
		if(null == original) return null;
		GameObject go = GameObject.Instantiate(original) as GameObject;
		go.name = original.name;
		go.transform.parent = parent;
		go.transform.localPosition = localPosition;
        go.transform.localScale = localScale;
		go.transform.localRotation = Quaternion.Euler(localRocation);
		return go;
	}

    public static void AttachChild(Transform transform, Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
	
	public static GameObject Instantiate(GameObject original, Transform parent)
	{
		return Instantiate(original, parent, Vector3.zero, Vector3.zero, Vector3.one);
	}
	
	public static GameObject Instantiate(GameObject original)
	{
        GameObject go = Instantiate(original, original.transform.parent, original.transform.localPosition, original.transform.localRotation.eulerAngles, original.transform.localScale);
		go.transform.localScale = original.transform.localScale;
		return go;
	}
    public static Rect NGUIObjectToRect(GameObject go)
    {
        Camera camera = NGUITools.FindCameraForLayer(go.layer);
        Bounds bounds = NGUIMath.CalculateAbsoluteWidgetBounds(go.transform);
        Vector3 min = camera.WorldToScreenPoint(bounds.min);
        Vector3 max = camera.WorldToScreenPoint(bounds.max);
        return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
    }
	public static void SetLayer(GameObject go, int layer)
	{
		Transform[] trans = go.GetComponentsInChildren<Transform>(true);
		for(int i=0; i<trans.Length; i++) trans[i].gameObject.layer = layer;
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
        T tt = panelTransform.GetComponent<T>();
        if (tt != null)
        {
            list.Add(tt);
        }
        foreach (Transform transform in panelTransform)
        {
            FindComponents(transform, list);			//查找子节点
        }
    }

    static public void FindComponents<T, T2>(Transform panelTransform, List<T2> list)
        where T : T2
        where T2 : Component
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
    public static bool WriteFile(string fileName, string content)
    {
        if (content == null)
        {
            return false;
        }
        //将文件信息读入流中
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            lock (fs)//锁住流
            {
                if (!fs.CanWrite)
                {
                    throw new System.Security.SecurityException("文件fileName=" + fileName + "是只读文件不能写入!");
                }

                byte[] buffer = Encoding.Default.GetBytes(content);
                fs.Write(buffer, 0, buffer.Length);
                return true;
            }
        }
    }
    public static Vector3 SemiCircleLerp(Vector3 left, Vector3 right, float per, float radius, bool down = true)
    {
        int flag = down == true ? 1 : -1;
        per = per * 100;
        Vector3 center = Vector3.Lerp(left, right, 0.5f);
        Vector3 r = left - center;
        Vector3 d = Quaternion.Euler(0f, 0f, flag*1.8f * per) * r;
        d = d.normalized;
        Vector3 pos = d*radius + center;
        return pos;
    }
    public static Vector3 Tangent(Vector3 pos, Vector3 center, bool down = true)
    {
        int flag = down == true ? 1 : -1;
        Vector3 r = pos - center;
        Vector3 d = Quaternion.Euler(0f, 0f, flag*90f) * r;
        return d;
    }
    public static Vector3 Rotate(Vector3 from, Vector3 to, float angle, bool down = true)
    {
        int flag = down == true ? 1 : -1;
        Vector3 r = to - from;
        Vector3 d = Quaternion.Euler(0f, 0f, flag*angle) * r;
        d = d.normalized;
        float dis = Vector3.Distance(from, to);
        Vector3 pos = d * dis + from;
        return pos;
    }
    public static Vector3 compute(Vector3 from, Vector3 to, float angle, float dis)
    {
        Vector3 r = to - from;
        Vector3 d = Quaternion.Euler(0f, 0f, angle) * r;
        d = d.normalized;
        Vector3 pos = d * dis + from;
        return pos;
    }
    public static Vector3 compute2(Vector3 from, Vector3 to, float angle)
    {
        Vector3 r = to - from;
        float dis = Vector3.Distance(to, from);
        Vector3 d = Quaternion.Euler(0f, 0f, -angle) * r;
        d = d.normalized;
        Vector3 pos = to-d * dis;
        return pos;
    }
    public static List<Vector3> GenerateRoundPos(Vector3 center, float radius)
    {
        List<Vector3> posList = new List<Vector3>();
        Vector3 left = new Vector3(center.x - radius, center.y);
        Vector3 right = new Vector3(center.x + radius, center.y);
        Vector3 ld = left - center;
        for (int i = 1; i <= 17; i++)
        {
            Vector3 d = Quaternion.Euler(0f, 0f, -10f * i) * ld;
            Vector3 pos = d + center;
            posList.Add(pos);
        }
        Vector3 rd = right - center;
        for (int i = 1; i <= 17; i++)
        {
            Vector3 d = Quaternion.Euler(0f, 0f, -10f * i) * rd;
            Vector3 pos = d + center;
            posList.Add(pos);
        }
        posList.Add(left);
        posList.Add(right);
        return posList;
    }
    public static List<Vector3> GenerateRoundDir(Vector3 center)
    {
        List<Vector3> posList = new List<Vector3>();
        Vector3 left = new Vector3(center.x - 1, center.y);
        Vector3 right = new Vector3(center.x + 1, center.y);
        Vector3 ld = left - center;
        for (int i = 1; i <= 10; i++)
        {
            Vector3 d = Quaternion.Euler(0f, 0f, -18f * i) * ld;
            posList.Add(d);
        }
        Vector3 rd = right - center;
        for (int i = 1; i <= 13; i++)
        {
            Vector3 d = Quaternion.Euler(0f, 0f, -13.84f * i) * rd;
            posList.Add(d);
        }
        posList.Add(ld);
        posList.Add(rd);
        return posList;
    }
    private static Vector3 QueryCenter(Vector3 left, Vector3 right, float radius, bool down = true)
    {
        int flag = down == true ? 1 : -1;
        float a = Vector3.Distance(left, right) / 2;
        float b = (float)Math.Sqrt(Math.Abs(radius * radius - a * a));
        float g = (float)(Math.Asin(Math.Min(b / radius, 1f)) * 180 / Math.PI);
        Vector3 r = right - left;
        Vector3 d = Quaternion.Euler(0f, 0f, flag * g) * r;
        d = d.normalized;
        Vector3 pos = d * radius + left;
        return pos;
    }
    private static float Digree(Vector3 left, Vector3 right, float radius)
    {
        float a = Vector3.Distance(left, right) / 2;
        float g = (float)(Math.Asin(Math.Min(a / radius, 1f)) * 180 / Math.PI);
        return 2 * g / 100;
    }
    public static Vector3 Center2(Vector3 p1, Vector3 p2, float b)
    {
        float a = Vector3.Distance(p1, p2);
        float c = (float)Math.Sqrt(a * a + b * b);
        float g = (float)(Math.Asin(Math.Min(b / c, 1f)) * 180 / Math.PI);
        Vector3 r = p1 - p2;
        Vector3 d = Quaternion.Euler(0f, 0f, -g) * r;
        d = d.normalized;
        Vector3 pos = p1-d * c;
        return pos;
    }
    public static Vector3 point(Vector3 center, Vector3 p1, float per, float radius)
    {
        per = per * 100;
        Vector3 r = center - p1;
        Vector3 d = Quaternion.Euler(0f, 0f, -0.8f*per) * r;
        d = d.normalized;
        Vector3 pos = center - d * radius;
        return pos;
    }
    public static Vector3 SemiLerp(Vector3 left, Vector3 right, float per, float radius, bool down = true)
    {
        int flag = down == true ? 1 : -1;
        per = per * 100;
        Vector3 center = QueryCenter(left, right, radius, down);
        Vector3 r = left - center;
        float digree = Digree(left, right, radius);
        Vector3 d = Quaternion.Euler(0f, 0f, flag * digree * per) * r;
        d = d.normalized;
        Vector3 pos = d * radius + center;
        return pos;
    }
    public static List<Vector3> GenerateRandRoundPos(Vector3 center, int count)
    {
        List<Vector3> list = new List<Vector3>();
        if(count == 0)
        {
            return list;
        }
        for(int i=0; i<count; i++)
        {
            Vector2 rand = UnityEngine.Random.insideUnitCircle;
            Vector3 pos = new Vector3(rand.x, rand.y);
            pos = pos.normalized * 0.1f;
            list.Add(pos);
        }
        return list;
    }
    public static List<Vector3> GeneratePlanePos(Vector3 center, int count)
    {
        List<Vector3> list = new List<Vector3>();
        if (count == 0)
        {
            return list;
        }
        for(int i=0; i<count/2; i++)
        {
            Vector3 pos = new Vector3(center.x + 0.1f*i, center.y, center.z);
            list.Add(pos);
        }
        for (int i = count / 2; i < count; i++)
        {
            Vector3 pos = new Vector3(center.x - 0.1f * i, center.y, center.z);
            list.Add(pos);
        }
        return list;
    }
    public static void ClearAllChild(GameObject parentObj)
    {
        for(int i = 0;i<parentObj.transform.childCount;i++)
        {
            GameObject go = parentObj.transform.GetChild(i).gameObject;
            GameObject.Destroy(go);
        }
    }
}
