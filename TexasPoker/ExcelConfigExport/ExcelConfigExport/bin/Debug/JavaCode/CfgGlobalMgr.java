
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

public class CfgGlobalMgr extends CCfgKeyMgrTemplate{
	public CfgGlobalMgr()
	{
	}
	private static CfgGlobalMgr instance = new CfgGlobalMgr();
	public static CfgGlobalMgr getInstance()
	{
		return instance; 
	}
	protected ITabItemWithKey NewItem(){
		return new CfgGlobal ();
	 }
	public CfgGlobal GetConfig(int key){
		return (CfgGlobal)super.GetConfig(key);
	}
}
