package thpoker;

import java.util.ArrayList;
/**
 * 
 * 边池
 *
 */
public class ThPool {
	public int index;
	public int poolReward;
	private ArrayList<ThPlayer> shortlistedPlayers;
	private ArrayList<ThPlayer> winners;
	public ThPool(int index){
		this.index = index;
		this.winners = new ArrayList<ThPlayer>();
		this.shortlistedPlayers = new ArrayList<ThPlayer>();
	}
	public void calcPlayersReward(int poolReward, ArrayList<ThPlayer> poolPlayers){
		this.poolReward = poolReward;
		this.shortlistedPlayers.clear();
		for(int i=0; i<poolPlayers.size(); ++i){
			ThPlayer tmp = poolPlayers.get(i);
			if(!tmp.isFold()){
				this.shortlistedPlayers.add(tmp);
			}
		}
		if(this.shortlistedPlayers.isEmpty()) {
			return;
		}
		System.out.println("pool "+Integer.toString(index)+" poolReward "+Integer.toString(poolReward));
		for (int i = 0; i < shortlistedPlayers.size(); i++) {
			ThPlayer player = shortlistedPlayers.get(i);
			System.out.println(" cardType "+Integer.toString(player.bestCards().type())+" cards "+player.handCards().toString());
		}
		this.winners.add(this.shortlistedPlayers.get(0));
		for(int i=1; i<this.shortlistedPlayers.size(); ++i){
			ThPlayer player = this.shortlistedPlayers.get(i);
			//比较下一个玩家和赢家的牌大小
			int compare = player.bestCards().compareTo(this.winners.get(0).bestCards());
			//有玩家牌型更大时
			if(compare > 0){
				this.winners.clear();
				this.winners.add(player);				
			}//牌型相等
			else if(compare == 0){
				this.winners.add(player);
			}
		}
		
		int avgReward = this.poolReward / this.winners.size();
		for(int i=0; i<this.winners.size(); ++i){
			ThPlayer playerRst = this.winners.get(i);
			//该玩家是赢家
			playerRst.addBallance(avgReward);
			playerRst.addReward(avgReward);
		}
		System.out.println("winners");
		for (int i = 0; i < this.winners.size(); i++) {
			ThPlayer player = this.winners.get(i);
			System.out.println(" newballance "+Integer.toString(player.getBallance())+" cardType "+Integer.toString(player.bestCards().type())+" cards "+player.handCards().toString()+" reward "+Integer.toString(avgReward));
		}
	}
	public void reset(){
		this.poolReward = 0;
		this.shortlistedPlayers.clear();
		this.winners.clear();
	}
}
