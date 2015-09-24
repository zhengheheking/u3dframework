package thpoker;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;


/**
 * 
 * 边池列表
 *
 */
public class ThPoolList {
	private ThPool[] pools;
	private ArrayList<ThPlayer> bettedAscPlayers;
	public ThPoolList() {
		this.pools = new ThPool[6];
		for (int i = 0; i < 6; ++i) {			
			this.pools[i] = new ThPool(i);
		}
		this.bettedAscPlayers = new ArrayList<ThPlayer>();
	}
	public void calcRewards(List<ThPlayer> playerList) {
		this.reset();
		for (int i = 0; i < playerList.size(); ++i) {
			ThPlayer player = playerList.get(i);
			if (player.totalBet() > 0)
				this.bettedAscPlayers.add(player);
		}
		Collections.sort(this.bettedAscPlayers, new PlayerTotalBetASCComparator());
		int numBettedPlayers = this.bettedAscPlayers.size();
		ArrayList<ThPlayer> excludePlayers = new ArrayList<ThPlayer>();
		int minBet = this.bettedAscPlayers.get(0).totalBet();
		this.pools[0].calcPlayersReward(minBet*numBettedPlayers, this.bettedAscPlayers);
		for (int i = 1; i < numBettedPlayers; ++i) {
			ThPlayer playerA = this.bettedAscPlayers.get(i - 1);
			ThPlayer playerB = this.bettedAscPlayers.get(i);
			excludePlayers.add(playerA);
			int poolReward = (playerB.totalBet() - playerA.totalBet()) * (numBettedPlayers - i);
			//计算每个玩家的奖金
			if (poolReward > 0) {
				this.pools[i].calcPlayersReward(poolReward, this.filterPlayers(this.bettedAscPlayers, excludePlayers));
			}
		}
	}
	private void reset() {
		this.bettedAscPlayers.clear();
		for (int i = 0; i < this.pools.length; ++i) {
			ThPool pool = this.pools[i];
			pool.reset();
		}
	}
	private ArrayList<ThPlayer> filterPlayers(ArrayList<ThPlayer> source, ArrayList<ThPlayer> exclusion) {
		ArrayList<ThPlayer> newList = new ArrayList<ThPlayer>();
		// 逐个扫描源数据列表元素
		for (int i = 0; i < source.size(); ++i) {
			ThPlayer player = source.get(i);
			boolean bExclude = false;
			for (int j = 0; j < exclusion.size(); ++j) {
				ThPlayer tmp = exclusion.get(j);
				if(tmp.getUserId() == player.getUserId()){
					bExclude = true;
					break;	
				}				
			}
			// 玩家不属于排除对象并且未FOLD
			if (!bExclude && !player.isFold()) {
				newList.add(player);
			}
		}
		return newList;
	}
}
class PlayerTotalBetASCComparator implements Comparator<ThPlayer> {
	public int compare(ThPlayer p1, ThPlayer p2) {
		return p1.totalBet() - p2.totalBet();
	}
}