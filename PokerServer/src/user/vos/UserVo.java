package user.vos;

import java.nio.ByteBuffer;

import utils.Tools;
import comm.ObjectVo;

public class UserVo extends ObjectVo{
	public int m_Id;
	public String m_Name;
	public byte m_Sex;
	public int m_Age;
	public long m_Exp;
	public byte m_Level;
	public int m_Gold;
	public int m_Score;
	public int m_RoomId;
	public int m_SeatId;
	public int m_CannonScore;
	public UserVo(int id, String name, byte sex, int age){
		m_Id = id;
		m_Name = name;
		m_Sex = sex;
		m_Age = age;
		m_Exp = 0;
		m_Level = 0;
		m_Gold = 0;
		m_Score = 0;
		m_RoomId = 0;
		m_SeatId = 0;
		m_CannonScore = 0;
	}
	@Override
	public void fromBytes(ByteBuffer bytes) {

	}

	@Override
	public void toBytes(ByteBuffer bytes) {
		
	}
	@Override
	public String toString() {
		return "";
	}
}
