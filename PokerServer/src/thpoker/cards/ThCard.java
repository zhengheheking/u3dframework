package thpoker.cards;

import poker.Poker;
import poker.PokerCard;

/**
 * 德州扑克特有的扑克牌
 * */
public class ThCard implements Comparable<ThCard> {

	/**包含的单张扑克牌*/
	private PokerCard card;
	/**德州扑克中使用的扑克值*/
	private int txValue = PokerCard.CARD_VALUE_NULL;
	
	public ThCard(PokerCard card) {
		this.card = card;
		//由于Ace为特殊情况，所以当出现Ace时先将ace的值加到比king大，好用于比较牌大小
		if(this.card.isAce()) {
			this.txValue = this.card.value() + Poker.CARD_COUNT_VALUE;
		} else {
			this.txValue = this.card.value();
		}
	}
	public synchronized int id(){
		return this.card.id();
	}
	/**返回牌类型*/
	public synchronized int type() {
		return this.card.type();
	}
	
	/**返回牌值*/
	public synchronized int value() {
		return this.card.value();
	}
	
	/**返回德州扑克牌值*/
	public synchronized int txValue() {
		return this.txValue;
	}
	
	/**是否为黑桃*/
	public synchronized boolean isSpade() {
		return this.card.isSpade();
	}
	
	/**是否为红心*/
	public synchronized boolean isHeart() {
		return this.card.isHeart();
	}
	
	/**是否为梅花*/
	public synchronized boolean isClub() {
		return this.card.isClub();
	}
	
	/**是否为方块*/
	public synchronized boolean isDiamond() {
		return this.card.isDiamond();
	}
	
	public synchronized boolean isAce() {
		return this.card.isAce();
	}
	
	public synchronized boolean is2() {
		return this.card.is2();
	}
	
	public synchronized boolean is3() {
		return this.card.is3();
	}
	
	public synchronized boolean is4() {
		return this.card.is4();
	}
	
	public synchronized boolean is5() {
		return this.card.is5();
	}
	
	public synchronized boolean is6() {
		return this.card.is6();
	}
	
	public synchronized boolean is7() {
		return this.card.is7();
	}
	
	public synchronized boolean is8() {
		return this.card.is8();
	}
	
	public synchronized boolean is9() {
		return this.card.is9();
	}
	
	public synchronized boolean is10() {
		return this.card.is10();
	}
	
	public synchronized boolean isJack() {
		return this.card.isJack();
	}
	
	public synchronized boolean isQueen() {
		return this.card.isQueen();
	}
	
	public synchronized boolean isKing() {
		return this.card.isKing();
	}
	
	//------------------------德州扑克特有逻辑-------------------------
	/**
	 * 比较2张牌的大小
	 * 如果本牌大于参数牌，返回正整数
	 * 如果本牌等于参数牌，返回0
	 * 如果本牌小于参数牌，返回负整数
	 * */
	public  int compareTo(ThCard other) {
		
		//这版本德州扑克不计较花色，只比较牌值
		//此时再判断牌值大小
		if(this.txValue() > other.txValue()) {
			return 1;
		} else if(this.txValue() == other.txValue()) {
			return 0;
		} else {
			return -1;
		}
	}
	public String toString(){
		return Integer.toString(this.card.id())+"_"+Integer.toString(this.card.type())+"_"+Integer.toString(this.card.value());
	}
}
