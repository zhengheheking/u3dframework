
//============================================
//--4>:
//    Exported by ExcelConfigExport
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

using System;
using UnityEngine;

partial class CfgShowBiMgr : CCfgKeyMgrTemplate<CfgShowBiMgr, int, CfgShowBi> { };

partial class CfgShowBi : ITabItemWithKey<int>
{
	public static readonly string _KEY_Id = "Id";
	public static readonly string _KEY_ScoreMin = "ScoreMin";
	public static readonly string _KEY_ScoreMax = "ScoreMax";
	public static readonly string _KEY_JinBiCount = "JinBiCount";
	public static readonly string _KEY_YinBiCount = "YinBiCount";

	public int	Id { get; private set; }				// Id
	public int	ScoreMin { get; private set; }				// 最小倍率
	public int	ScoreMax { get; private set; }				// 最大倍率
	public int	JinBiCount { get; private set; }				// 金币数
	public int	YinBiCount { get; private set; }				// 银币数

	public CfgShowBi()
	{
	}

	public int GetKey() { return Id; }

	public bool ReadItem(TabFile tf)
	{
		Id = tf.Get<int>(_KEY_Id);
		ScoreMin = tf.Get<int>(_KEY_ScoreMin);
		ScoreMax = tf.Get<int>(_KEY_ScoreMax);
		JinBiCount = tf.Get<int>(_KEY_JinBiCount);
		YinBiCount = tf.Get<int>(_KEY_YinBiCount);
		return true;
	}
}

