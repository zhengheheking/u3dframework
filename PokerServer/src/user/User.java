package user;

import comm.Client;

import user.vos.UserVo;

public class User {
	public UserVo m_UserVo;
	public Client m_Client;
	public int m_CostScore;
	public int m_GetCoin;
	public User(int id, String name, byte sex, int age, int gold, int score, int cannonScore){
		m_UserVo = new UserVo(id, name, sex, age);
		m_UserVo.m_Gold = gold;
		m_UserVo.m_Score = score;
		m_UserVo.m_CannonScore = cannonScore;
		m_CostScore = 0;
		m_GetCoin = 0;
	}
	public boolean costGold(int gold)
	{
		if(m_UserVo.m_Gold < gold){
			return false;
		}
		m_UserVo.m_Gold -= gold;
		return true;
	}
	public void addGold(int gold){
		m_UserVo.m_Gold += gold;
	}
	public int getGold(){
		return m_UserVo.m_Gold;
	}
	public boolean costCannonScore(int score){
		if(m_UserVo.m_CannonScore < score){
			return false;
		}
		m_UserVo.m_CannonScore -= score;
		return true;
	}
	public void addCannonScore(int score){
		m_UserVo.m_CannonScore += score;
	}
	public int getCannonScore(){
		return m_UserVo.m_CannonScore;
	}
	public boolean costScore(int score){
		if (m_UserVo.m_Score < score) {
			return false;
		}
		m_UserVo.m_Score -= score;
		return true;
	}
	public void addScore(int score){
		m_UserVo.m_Score += score;
	}
	public int getScore(){
		return m_UserVo.m_Score;
	}
	public void loop() {
		
	}
	public int getRoomId(){
		return m_UserVo.m_RoomId;
	}
	public void setRoomId(int roomId){
		m_UserVo.m_RoomId = roomId;
	}
	public int getSeatId(){
		return m_UserVo.m_SeatId;
	}
	public void setSeatId(int seatId){
		m_UserVo.m_SeatId = seatId;
	}
	public int getId(){
		return m_UserVo.m_Id;
	}
}
