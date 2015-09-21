
//============================================
//--4>:
//    Exported by ExcelConfigExport
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

using System;
using UnityEngine;

partial class CfgOneNetMgr : CCfgKeyMgrTemplate<CfgOneNetMgr, int, CfgOneNet> { };

partial class CfgOneNet : ITabItemWithKey<int>
{
	public static readonly string _KEY_NetId = "NetId";
	public static readonly string _KEY_FishId = "FishId";

	public int	NetId { get; private set; }				// 鱼网Id
	public string	FishId { get; private set; }				// 鱼Id

	public CfgOneNet()
	{
	}

	public int GetKey() { return NetId; }

	public bool ReadItem(TabFile tf)
	{
		NetId = tf.Get<int>(_KEY_NetId);
		FishId = tf.Get<string>(_KEY_FishId);
		return true;
	}
}

