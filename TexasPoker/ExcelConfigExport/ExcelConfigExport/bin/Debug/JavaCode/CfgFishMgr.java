
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

public class CfgFishMgr extends CCfgKeyMgrTemplate{
	public CfgFishMgr()
	{
	}
	private static CfgFishMgr instance = new CfgFishMgr();
	public static CfgFishMgr getInstance()
	{
		return instance; 
	}
	protected ITabItemWithKey NewItem(){
		return new CfgFish ();
	 }
	public CfgFish GetConfig(int key){
		return (CfgFish)super.GetConfig(key);
	}
}
