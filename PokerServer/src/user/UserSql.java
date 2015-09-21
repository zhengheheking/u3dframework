package user;


import java.sql.ResultSet;
import java.sql.SQLException;

import threads.SqlSaver;
import user.vos.UserTableVo;
import user.vos.UserVo;
import utils.LogInfo;
import comm.Client;
import comm.SqlBase;

public class UserSql extends SqlBase {
	public static UserSql Instance = new UserSql();
	@Override
	public void loadAllData() throws SQLException {
		// TODO Auto-generated method stub
		reconnect();
	}

	@Override
	public void pushQueue(Object arg0, byte arg1) {
		// TODO Auto-generated method stub
		UserVo item = (UserVo)arg0;
		UserTableVo vo = new UserTableVo();
		if (arg1 == ADD_MODE) {
            vo.initForAdd(item);
        } else if (arg1 == DELETE_MODE) {
            vo.initForDel(item);
        } else {
            vo.initForUpdate(item);

        }
        SqlSaver.savingList.add(vo);
	}

	@Override
	public void reconnect() throws SQLException {
		// TODO Auto-generated method stub
		psRead = conn.prepareStatement("select userId,name,sex,age,exp,level,gold,score,cannonScore from tb_user where name=?");
		psAdd = conn.prepareStatement("insert into tb_user(userId,name,sex,age,exp,level,gold,score,cannonScore) values(?,?,?,?,?,?,?,?)");
		psUpdate = conn.prepareStatement("update  tb_user set name=?,sex=?,age=?,exp=?,level=?,gold=?,score=?,cannonScore=? where userId=?");
	}

	@Override
	public void saveOne(Object arg0) throws SQLException {
		// TODO Auto-generated method stub
		UserTableVo vo = (UserTableVo)arg0;
		if (vo == null)
            return;

        switch (vo.sqlMode) {
        case ADD_MODE:
        	psAdd.setInt(1, vo.m_UserId);
        	psAdd.setString(2, vo.m_Name);
        	psAdd.setByte(3, vo.m_Sex);
        	psAdd.setInt(4, vo.m_Age);
        	psAdd.setLong(5, vo.m_Exp);
        	psAdd.setByte(6, vo.m_Level);
        	psAdd.setInt(7, vo.m_Gold);
        	psAdd.setInt(8, vo.m_Score);
        	psAdd.setInt(9, vo.m_CannonScore);
        	psAdd.execute();
        	break;
        case UPDATE_MODE:
        	psUpdate.setString(1, vo.m_Name);
        	psUpdate.setByte(2, vo.m_Sex);
        	psUpdate.setInt(3, vo.m_Age);
        	psUpdate.setLong(4, vo.m_Exp);
        	psUpdate.setByte(5, vo.m_Level);
        	psUpdate.setInt(6, vo.m_Gold);
        	psUpdate.setInt(7, vo.m_UserId);
        	psUpdate.setInt(8, vo.m_Score);
        	psUpdate.setInt(9, vo.m_CannonScore);
        	psUpdate.execute();
        	break;
        case DELETE_MODE:
        	break;
        }
	}
	public UserTableVo loadData(String userName) throws SQLException
	{
		UserTableVo vo = new UserTableVo();
		ResultSet rst = null;
        try 
        {
            psRead.clearParameters();
            psRead.setString(1, userName);
            rst = psRead.executeQuery();
            // 没有角色 需要创建
            if (!rst.next()) 
            {
            	return null;
            }
            vo.m_UserId = rst.getInt(1);
            vo.m_Name = rst.getString(2);
            vo.m_Sex = rst.getByte(3);
            vo.m_Age = rst.getInt(4);
            vo.m_Exp = rst.getLong(5);
            vo.m_Level = rst.getByte(6);
            vo.m_Gold = rst.getInt(7);
            vo.m_Score = rst.getInt(8);
            vo.m_CannonScore = rst.getInt(9);
        }
        catch (Exception e) 
        {
            LogInfo.error(e.getMessage());
            if (rst != null) 
            {
                try 
                {
                    rst.close();
                } 
                catch (Exception e2) 
                {

                }
                rst = null;
            }
            if (psRead != null) {
                try 
                {
                    psRead.close();
                } 
                catch (Exception e3) 
                {
                }
                psRead = null;
            }
            reconnect();
        }
        return vo;
	}
	
}
