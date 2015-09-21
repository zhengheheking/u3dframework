
//============================================
//--4>:
//    Exported by ExcelConfigExport
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

using System;
using UnityEngine;

partial class CfgFishMgr : CCfgKeyMgrTemplate<CfgFishMgr, int, CfgFish> { };

partial class CfgFish : ITabItemWithKey<int>
{
	public static readonly string _KEY_FishId = "FishId";
	public static readonly string _KEY_PrefebName = "PrefebName";

	public int	FishId { get; private set; }				// 鱼Id
	public string	PrefebName { get; private set; }				// Prefab名字

	public CfgFish()
	{
	}

	public int GetKey() { return FishId; }

	public bool ReadItem(TabFile tf)
	{
		FishId = tf.Get<int>(_KEY_FishId);
		PrefebName = tf.Get<string>(_KEY_PrefebName);
		return true;
	}
}

