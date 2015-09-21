
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

public class CfgFishTeam extends ITabItemWithKey
{
	private static final String _KEY_FishTeamId = "FishTeamId";
	private static final String _KEY_FishId = "FishId";
	private static final String _KEY_SpawnNumMin = "SpawnNumMin";
	private static final String _KEY_SpawnNumMax = "SpawnNumMax";
	private static final String _KEY_SpawnPoint = "SpawnPoint";
	private static final String _KEY_Pos = "Pos";
	private static final String _KEY_Speed = "Speed";
	private static final String _KEY_ExistTime = "ExistTime";

	public int	FishTeamId;				// 鱼队Id
	public int	FishId;				// 鱼的Id
	public int	SpawnNumMin;				// 生成最小数
	public int	SpawnNumMax;				// 生成最大数
	public String	SpawnPoint;				// 出生点
	public String	Pos;				// 座位
	public float	Speed;				// 速度
	public int	ExistTime;				// 存活时间

	public CfgFishTeam()
	{
	}

	public int GetKey() { return FishTeamId; }

	public boolean ReadItem(TabFile tf)
	{
		FishTeamId = tf.GetInt32(_KEY_FishTeamId);
		FishId = tf.GetInt32(_KEY_FishId);
		SpawnNumMin = tf.GetInt32(_KEY_SpawnNumMin);
		SpawnNumMax = tf.GetInt32(_KEY_SpawnNumMax);
		SpawnPoint = tf.GetString(_KEY_SpawnPoint);
		Pos = tf.GetString(_KEY_Pos);
		Speed = tf.GetFloat(_KEY_Speed);
		ExistTime = tf.GetInt32(_KEY_ExistTime);
		return true;
	}
}

