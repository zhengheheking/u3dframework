
//============================================
//--4>:
//    Exported by ExcelConfigExport
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

using System;
using UnityEngine;

partial class CfgLanMgr : CCfgKeyMgrTemplate<CfgLanMgr, int, CfgLan> { };

partial class CfgLan : ITabItemWithKey<int>
{
	public static readonly string _KEY_LanId = "LanId";
	public static readonly string _KEY_LanContent = "LanContent";

	public int	LanId { get; private set; }				// 语言包Id
	public string	LanContent { get; private set; }				// 语言包内容

	public CfgLan()
	{
	}

	public int GetKey() { return LanId; }

	public bool ReadItem(TabFile tf)
	{
		LanId = tf.Get<int>(_KEY_LanId);
		LanContent = tf.Get<string>(_KEY_LanContent);
		return true;
	}
}

