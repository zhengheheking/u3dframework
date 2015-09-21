
//============================================
//--4>:
//    Exported by ExcelConfigExport
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

using System;
using UnityEngine;

partial class CfgBulletMgr : CCfgKeyMgrTemplate<CfgBulletMgr, int, CfgBullet> { };

partial class CfgBullet : ITabItemWithKey<int>
{
	public static readonly string _KEY_BulletId = "BulletId";
	public static readonly string _KEY_HitRate = "HitRate";
	public static readonly string _KEY_PrefebName = "PrefebName";

	public int	BulletId { get; private set; }				// 子弹Id
	public float	HitRate { get; private set; }				// 飞行速度
	public string	PrefebName { get; private set; }				// Prefab名字

	public CfgBullet()
	{
	}

	public int GetKey() { return BulletId; }

	public bool ReadItem(TabFile tf)
	{
		BulletId = tf.Get<int>(_KEY_BulletId);
		HitRate = tf.Get<float>(_KEY_HitRate);
		PrefebName = tf.Get<string>(_KEY_PrefebName);
		return true;
	}
}

