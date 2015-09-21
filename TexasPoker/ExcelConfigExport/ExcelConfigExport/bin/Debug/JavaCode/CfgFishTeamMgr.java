
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

public class CfgFishTeamMgr extends CCfgKeyMgrTemplate{
	public CfgFishTeamMgr()
	{
	}
	private static CfgFishTeamMgr instance = new CfgFishTeamMgr();
	public static CfgFishTeamMgr getInstance()
	{
		return instance; 
	}
	protected ITabItemWithKey NewItem(){
		return new CfgFishTeam ();
	 }
	public CfgFishTeam GetConfig(int key){
		return (CfgFishTeam)super.GetConfig(key);
	}
}
