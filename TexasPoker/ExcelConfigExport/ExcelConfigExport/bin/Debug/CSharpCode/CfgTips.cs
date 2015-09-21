
//============================================
//--4>:
//    Exported by ExcelConfigExport
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

using System;
using UnityEngine;

partial class CfgTipsMgr : CCfgKeyMgrTemplate<CfgTipsMgr, int, CfgTips> { };

partial class CfgTips : ITabItemWithKey<int>
{
	public static readonly string _KEY_TipId = "TipId";
	public static readonly string _KEY_TipContent = "TipContent";

	public int	TipId { get; private set; }				// TipId
	public string	TipContent { get; private set; }				// Tip内容

	public CfgTips()
	{
	}

	public int GetKey() { return TipId; }

	public bool ReadItem(TabFile tf)
	{
		TipId = tf.Get<int>(_KEY_TipId);
		TipContent = tf.Get<string>(_KEY_TipContent);
		return true;
	}
}

