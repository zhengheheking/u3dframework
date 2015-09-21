
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

public class CfgFish extends ITabItemWithKey
{
	private static final String _KEY_FishId = "FishId";
	private static final String _KEY_ScoreMin = "ScoreMin";
	private static final String _KEY_ScoreMax = "ScoreMax";
	private static final String _KEY_Speed = "Speed";
	private static final String _KEY_ExistTime = "ExistTime";
	private static final String _KEY_Drop = "Drop";
	private static final String _KEY_CoinExplosion = "CoinExplosion";

	public int	FishId;				// 鱼Id
	public int	ScoreMin;				// 最小倍数
	public int	ScoreMax;				// 最大倍数
	public int	Speed;				// 速度
	public int	ExistTime;				// 存活时间
	public int	Drop;				// 掉落
	public int	CoinExplosion;				// 是否金币爆炸

	public CfgFish()
	{
	}

	public int GetKey() { return FishId; }

	public boolean ReadItem(TabFile tf)
	{
		FishId = tf.GetInt32(_KEY_FishId);
		ScoreMin = tf.GetInt32(_KEY_ScoreMin);
		ScoreMax = tf.GetInt32(_KEY_ScoreMax);
		Speed = tf.GetInt32(_KEY_Speed);
		ExistTime = tf.GetInt32(_KEY_ExistTime);
		Drop = tf.GetInt32(_KEY_Drop);
		CoinExplosion = tf.GetInt32(_KEY_CoinExplosion);
		return true;
	}
}

