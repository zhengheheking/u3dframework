
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

public class CfgFishSchoolSpawnMgr extends CCfgKeyMgrTemplate{
	public CfgFishSchoolSpawnMgr()
	{
	}
	private static CfgFishSchoolSpawnMgr instance = new CfgFishSchoolSpawnMgr();
	public static CfgFishSchoolSpawnMgr getInstance()
	{
		return instance; 
	}
	protected ITabItemWithKey NewItem(){
		return new CfgFishSchoolSpawn ();
	 }
	public CfgFishSchoolSpawn GetConfig(int key){
		return (CfgFishSchoolSpawn)super.GetConfig(key);
	}
}
