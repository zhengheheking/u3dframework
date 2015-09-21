
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

public class CfgShowGold extends ITabItemWithKey
{
	private static final String _KEY_ShowId = "ShowId";
	private static final String _KEY_ScoreMin = "ScoreMin";
	private static final String _KEY_ScoreMax = "ScoreMax";
	private static final String _KEY_GoldMin = "GoldMin";
	private static final String _KEY_GoldMax = "GoldMax";

	public int	ShowId;				// Id
	public int	ScoreMin;				// 最小倍率
	public int	ScoreMax;				// 最大倍率
	public int	GoldMin;				// 金币最小数
	public int	GoldMax;				// 金币最大数

	public CfgShowGold()
	{
	}

	public int GetKey() { return ShowId; }

	public boolean ReadItem(TabFile tf)
	{
		ShowId = tf.GetInt32(_KEY_ShowId);
		ScoreMin = tf.GetInt32(_KEY_ScoreMin);
		ScoreMax = tf.GetInt32(_KEY_ScoreMax);
		GoldMin = tf.GetInt32(_KEY_GoldMin);
		GoldMax = tf.GetInt32(_KEY_GoldMax);
		return true;
	}
}

