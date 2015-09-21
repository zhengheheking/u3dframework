
//============================================
//--4>:
//    Exported by ExcelConfigExport
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

using System;
using UnityEngine;

partial class CfgCannonMgr : CCfgKeyMgrTemplate<CfgCannonMgr, int, CfgCannon> { };

partial class CfgCannon : ITabItemWithKey<int>
{
	public static readonly string _KEY_CannonId = "CannonId";
	public static readonly string _KEY_CannonName = "CannonName";
	public static readonly string _KEY_BulletId = "BulletId";
	public static readonly string _KEY_BulletScoreMin = "BulletScoreMin";
	public static readonly string _KEY_BulletScoreMax = "BulletScoreMax";

	public int	CannonId { get; private set; }				// 鱼炮Id
	public string	CannonName { get; private set; }				// 鱼炮名字
	public int	BulletId { get; private set; }				// 子弹Id
	public int	BulletScoreMin { get; private set; }				// 
	public int	BulletScoreMax { get; private set; }				// 

	public CfgCannon()
	{
	}

	public int GetKey() { return CannonId; }

	public bool ReadItem(TabFile tf)
	{
		CannonId = tf.Get<int>(_KEY_CannonId);
		CannonName = tf.Get<string>(_KEY_CannonName);
		BulletId = tf.Get<int>(_KEY_BulletId);
		BulletScoreMin = tf.Get<int>(_KEY_BulletScoreMin);
		BulletScoreMax = tf.Get<int>(_KEY_BulletScoreMax);
		return true;
	}
}

