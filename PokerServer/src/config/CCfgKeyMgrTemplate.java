package config;

import java.util.HashMap;
import java.util.Map;

import utils.TabFile;

public class CCfgKeyMgrTemplate {
	public Map<Integer, ITabItemWithKey> m_ItemTable = new HashMap<Integer, ITabItemWithKey>();
	protected ITabItemWithKey NewItem(){
		return null;
	}
	public boolean Init(String strFileName){

        if(null != m_ItemTable && m_ItemTable.size() > 0){
        	return false;
        }
        m_ItemTable.clear();
        TabFile tf = new TabFile(strFileName, Config.ReadFileAsString(strFileName));
        while(tf.Next()){
        	ITabItemWithKey item  = NewItem();
        	if(item.ReadItem(tf) == false){
        		continue;
        	}
        	if(m_ItemTable.containsKey(item.GetKey())){
        		continue;
        	}
        	m_ItemTable.put(item.GetKey(), item);
        }
        return true;
	
		
	}
	public ITabItemWithKey GetConfig(int key){
		if(m_ItemTable.containsKey(key)){
			return m_ItemTable.get(key);
		}
		return null;
	}
	public boolean Has(int key){
		return m_ItemTable.containsKey(key);
	}
}
