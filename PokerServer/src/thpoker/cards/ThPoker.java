package thpoker.cards;

import poker.Poker;

/**
 * 德州扑克特有的扑克牌
 * */
public class ThPoker {

	private Poker poker;
	
	public ThPoker() {
		//德州扑克没有大小王
		this.poker = new Poker(Poker.CARD_NORMAL_INDEX_START, Poker.CARD_NORMAL_INDEX_END);
	}
	
	/**洗牌*/
	public void shuffle() {
		this.poker.shuffle();
	}
	
	/**发牌*/
	public ThCard deal() {
		return new ThCard(this.poker.deal());
	}
	
	//-----------------------------------德州扑克使用的一些牌型算法
}
