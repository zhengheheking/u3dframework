
//============================================
//--4>:
//    Exported by ExcelConfigExport
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================
package config;
import utils.Tools;
import utils.TabFile;

public class CfgGlobal extends ITabItemWithKey
{
	private static final String _KEY_Id = "Id";
	private static final String _KEY_Name = "Name";
	private static final String _KEY_Value = "Value";

	public int	Id;				// Id
	public String	Name;				// 名字
	public String	Value;				// 值

	public CfgGlobal()
	{
	}

	public int GetKey() { return Id; }

	public boolean ReadItem(TabFile tf)
	{
		Id = tf.GetInt32(_KEY_Id);
		Name = tf.GetString(_KEY_Name);
		Value = tf.GetString(_KEY_Value);
		return true;
	}
}

