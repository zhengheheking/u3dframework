
//============================================
//--4>:
//    Exported by ExcelConfigExport
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

using System;
using UnityEngine;

partial class CfgGlobalMgr : CCfgKeyMgrTemplate<CfgGlobalMgr, int, CfgGlobal> { };

partial class CfgGlobal : ITabItemWithKey<int>
{
	public static readonly string _KEY_Id = "Id";
	public static readonly string _KEY_Value = "Value";
	public static readonly string _KEY_Name = "Name";

	public int	Id { get; private set; }				// Id
	public string	Value { get; private set; }				// 数值
	public string	Name { get; private set; }				// 名字

	public CfgGlobal()
	{
	}

	public int GetKey() { return Id; }

	public bool ReadItem(TabFile tf)
	{
		Id = tf.Get<int>(_KEY_Id);
		Value = tf.Get<string>(_KEY_Value);
		Name = tf.Get<string>(_KEY_Name);
		return true;
	}
}

