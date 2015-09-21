
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

public class CfgFishSpawnMgr extends CCfgKeyMgrTemplate{
	public CfgFishSpawnMgr()
	{
	}
	private static CfgFishSpawnMgr instance = new CfgFishSpawnMgr();
	public static CfgFishSpawnMgr getInstance()
	{
		return instance; 
	}
	protected ITabItemWithKey NewItem(){
		return new CfgFishSpawn ();
	 }
	public CfgFishSpawn GetConfig(int key){
		return (CfgFishSpawn)super.GetConfig(key);
	}
}
