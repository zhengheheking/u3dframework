
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

public class CfgFishSpawn extends ITabItemWithKey
{
	private static final String _KEY_Id = "Id";
	private static final String _KEY_FishId = "FishId";
	private static final String _KEY_FishTeamId = "FishTeamId";
	private static final String _KEY_TargetPoint = "TargetPoint";
	private static final String _KEY_Path = "Path";
	private static final String _KEY_PathId = "PathId";
	private static final String _KEY_OneScreenMaxNum = "OneScreenMaxNum";
	private static final String _KEY_Pro = "Pro";

	public int	Id;				// Id
	public int	FishId;				// 鱼的Id
	public int	FishTeamId;				// 鱼队Id
	public String	TargetPoint;				// 目标点
	public int	Path;				// 路径类型
	public int	PathId;				// 路径Id
	public int	OneScreenMaxNum;				// 同屏最大数
	public float	Pro;				// 概率

	public CfgFishSpawn()
	{
	}

	public int GetKey() { return Id; }

	public boolean ReadItem(TabFile tf)
	{
		Id = tf.GetInt32(_KEY_Id);
		FishId = tf.GetInt32(_KEY_FishId);
		FishTeamId = tf.GetInt32(_KEY_FishTeamId);
		TargetPoint = tf.GetString(_KEY_TargetPoint);
		Path = tf.GetInt32(_KEY_Path);
		PathId = tf.GetInt32(_KEY_PathId);
		OneScreenMaxNum = tf.GetInt32(_KEY_OneScreenMaxNum);
		Pro = tf.GetFloat(_KEY_Pro);
		return true;
	}
}

