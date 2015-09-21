
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

public class CfgFishSchoolSpawn extends ITabItemWithKey
{
	private static final String _KEY_FishSchoolId = "FishSchoolId";
	private static final String _KEY_FishSchoolType = "FishSchoolType";
	private static final String _KEY_MiddleFishId = "MiddleFishId";
	private static final String _KEY_ArroudFishId = "ArroudFishId";
	private static final String _KEY_TargetPoint = "TargetPoint";
	private static final String _KEY_Speed = "Speed";
	private static final String _KEY_ExistTime = "ExistTime";
	private static final String _KEY_Path = "Path";
	private static final String _KEY_Radius = "Radius";
	private static final String _KEY_Pos = "Pos";
	private static final String _KEY_Pro = "Pro";
	private static final String _KEY_OneScreenMaxNum = "OneScreenMaxNum";

	public int	FishSchoolId;				// 鱼群Id
	public int	FishSchoolType;				// 鱼群类型
	public int	MiddleFishId;				// 中间鱼Id
	public int	ArroudFishId;				// 周围鱼Id
	public String	TargetPoint;				// 目标点
	public float	Speed;				// 速度
	public int	ExistTime;				// 存活时间
	public int	Path;				// 路径类型
	public float	Radius;				// 半径
	public String	Pos;				// 座位
	public float	Pro;				// 概率
	public int	OneScreenMaxNum;				// 同屏最大数

	public CfgFishSchoolSpawn()
	{
	}

	public int GetKey() { return FishSchoolId; }

	public boolean ReadItem(TabFile tf)
	{
		FishSchoolId = tf.GetInt32(_KEY_FishSchoolId);
		FishSchoolType = tf.GetInt32(_KEY_FishSchoolType);
		MiddleFishId = tf.GetInt32(_KEY_MiddleFishId);
		ArroudFishId = tf.GetInt32(_KEY_ArroudFishId);
		TargetPoint = tf.GetString(_KEY_TargetPoint);
		Speed = tf.GetFloat(_KEY_Speed);
		ExistTime = tf.GetInt32(_KEY_ExistTime);
		Path = tf.GetInt32(_KEY_Path);
		Radius = tf.GetFloat(_KEY_Radius);
		Pos = tf.GetString(_KEY_Pos);
		Pro = tf.GetFloat(_KEY_Pro);
		OneScreenMaxNum = tf.GetInt32(_KEY_OneScreenMaxNum);
		return true;
	}
}

