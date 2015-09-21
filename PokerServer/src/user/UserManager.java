package user;

import java.util.HashMap;
import java.util.Map;

import user.vos.UserTableVo;
import utils.LogInfo;
import utils.Singleton;
import comm.Client;
import comm.SqlBase;



public class UserManager {
	public Map<Integer, User> m_UserMap;
	private UserManager() {
		m_UserMap = new HashMap<Integer, User>();
    }  
	private static UserManager instance = new UserManager(); 
	public static UserManager getInstance() { 
	       return instance; 
	} 
    public User getUser(int userId){
    	return m_UserMap.get(userId);
    }
    public void addUser(User user){
    	m_UserMap.put(user.m_UserVo.m_Id, user);
    }
    public void removeUser(User user){
    	m_UserMap.remove(user);
    }
    public void loop() {
    	for(Map.Entry<Integer, User> entry:m_UserMap.entrySet()){
    		entry.getValue().loop();
    	}
    }
    public void onLogout(Client cl){
    	if(cl.m_User == null){
    		return;
    	}
    	
    	UserSql.Instance.pushQueue(cl.m_User.m_UserVo, SqlBase.UPDATE_MODE);
    	removeUser(cl.m_User);
    }
    public void onLogin(Client cl, String userName)
	{
		try
		{
			UserTableVo vo = UserSql.Instance.loadData(userName);
			if(vo == null)
			{
				
				cl.setClosed(true);
				return;
			}
			cl.m_Id = vo.m_UserId;
			User oldUser = getUser(cl.m_Id);
			if (oldUser != null) {
				oldUser.m_Client.setClosed(true);
			}
			User user = new User(vo.m_UserId, vo.m_Name, vo.m_Sex, vo.m_Age, vo.m_Gold, vo.m_Score, vo.m_CannonScore);
			addUser(user);
			cl.m_User = user;
			user.m_Client = cl;
			
		}
		catch (Exception e) 
		{
            LogInfo.printError(e, cl.uid);
		}
    }
}
