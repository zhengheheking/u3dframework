
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

public class CfgShowGoldMgr extends CCfgKeyMgrTemplate{
	public CfgShowGoldMgr()
	{
	}
	private static CfgShowGoldMgr instance = new CfgShowGoldMgr();
	public static CfgShowGoldMgr getInstance()
	{
		return instance; 
	}
	protected ITabItemWithKey NewItem(){
		return new CfgShowGold ();
	 }
	public CfgShowGold GetConfig(int key){
		return (CfgShowGold)super.GetConfig(key);
	}
}
