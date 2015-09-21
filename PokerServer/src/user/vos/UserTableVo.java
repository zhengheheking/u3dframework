package user.vos;

import comm.ObjectVo;
import comm.TableVo;

public class UserTableVo extends TableVo{
	public int m_UserId;
	public String m_Name;
	public byte m_Sex;
	public int m_Age;
	public long m_Exp;
	public byte m_Level;
	public int m_Gold;
	public int m_Score;
	public int m_CannonScore;
	public void initForAdd(ObjectVo vo) {
		super.initForAdd(vo);
		UserVo userVo = (UserVo)vo;
		m_UserId = userVo.m_Id;
		m_Name = userVo.m_Name;
		m_Sex = userVo.m_Sex;
		m_Age = userVo.m_Age;
		m_Exp = userVo.m_Exp;
		m_Level = userVo.m_Level;
		m_Gold = userVo.m_Gold;
		m_Score = userVo.m_Score;
		m_CannonScore = userVo.m_CannonScore;
	}
	public void initForUpdate(ObjectVo vo) {
		super.initForUpdate(vo);
		UserVo userVo = (UserVo)vo;
		m_UserId = userVo.m_Id;
		m_Name = userVo.m_Name;
		m_Sex = userVo.m_Sex;
		m_Age = userVo.m_Age;
		m_Exp = userVo.m_Exp;
		m_Level = userVo.m_Level;
		m_Gold = userVo.m_Gold;
		m_Score = userVo.m_Score;
		m_CannonScore = userVo.m_CannonScore;
	}
	public void initForDel(ObjectVo vo) {
		super.initForDel(vo);
	}
}
