
//============================================
//--4>:
//    Exported by ExcelConfigExport
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

using System;
using UnityEngine;

partial class CfgPathMgr : CCfgKeyMgrTemplate<CfgPathMgr, int, CfgPath> { };

partial class CfgPath : ITabItemWithKey<int>
{
	public static readonly string _KEY_PathId = "PathId";
	public static readonly string _KEY_PathLen = "PathLen";
	public static readonly string _KEY_Path = "Path";

	public int	PathId { get; private set; }				// 路径Id
	public float	PathLen { get; private set; }				// 路径长度
	public string	Path { get; private set; }				// 路径

	public CfgPath()
	{
	}

	public int GetKey() { return PathId; }

	public bool ReadItem(TabFile tf)
	{
		PathId = tf.Get<int>(_KEY_PathId);
		PathLen = tf.Get<float>(_KEY_PathLen);
		Path = tf.Get<string>(_KEY_Path);
		return true;
	}
}

