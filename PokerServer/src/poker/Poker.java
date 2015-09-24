package poker;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;


/**
 * 表示整副扑克牌
 * */
public class Poker {
	//扑克牌所有范围
	public static final int CARD_ALL_INDEX_START = 0;
	public static final int CARD_ALL_INDEX_END = 53;
	
	//扑克正牌的index范围
	public static final int CARD_NORMAL_INDEX_START = 0;
	public static final int CARD_NORMAL_INDEX_END = 51;
	
	//扑克副牌的index范围
	public static final int CARD_JOKER_SMALL = 52;
	public static final int CARD_JOKER_BIG = 53;
	
	//牌值数
	public static final int CARD_COUNT_TYPE = 4;
	public static final int CARD_COUNT_VALUE = 13;
	
	/**扑克牌中包含的牌型，可以有各种牌型组合*/
	private List<PokerCard> cards;
	
	/**当前发牌位置*/
	private int curCardIndex;
	
	/**包含所有扑克牌，包括大小王*/
	public Poker() {
		this(CARD_ALL_INDEX_START, CARD_ALL_INDEX_END);
	}
	
	/**指定内部牌型的扑克牌*/
	public Poker(List<PokerCard> cards) {
		this.cards = new ArrayList<PokerCard>(cards.size());
		this.cards.addAll(cards);
	}
	
	/**
	 * 根据需要指定牌范围
	 * */
	public Poker(int startIndex, int endIndex) {
		int cardCount = endIndex - startIndex + 1;
		this.cards = new ArrayList<PokerCard>(cardCount);
		for(int index = startIndex; index <= endIndex; index++) {
			this.cards.add(Poker.createCardFromIndex(index));
		}
	}
	
	/**当前发牌位置*/
	public int curCardIndex() {
		return this.curCardIndex;
	}
	
	/**当前牌数量*/
	public int cardCount() {
		return this.cards.size();
	}
	
	/**洗牌，从新打乱牌型数组*/
	public void shuffle() {
		this.curCardIndex = 0;
		int count = this.cards.size();
		List<PokerCard> newCards = new ArrayList<PokerCard>(count);
		Random rand = new Random();
		for(int i = 0; i < count; i++) {
			//从就cards中随机一个位置
			int index = rand.nextInt(this.cards.size());
			//将指定的牌插入到新的牌组中
			newCards.add(this.cards.get(index));
			//将该牌从旧牌中删除
			this.cards.remove(index);
		}
		this.cards = newCards;
	}
	
	/**发牌*/
	public PokerCard deal() {
		return this.cards.get(this.curCardIndex++);
	}
	
	/**
	 * 整副扑克牌正牌从0开始到53
	 * 方块0-12
	 * 梅花13-25
	 * 红桃26-38
	 * 黑桃39-51
	 * 小王52
	 * 大王53
	 * 根据需要创建牌
	 * */
	public static PokerCard createCardFromIndex(int index) {
		if(index >= CARD_NORMAL_INDEX_START && index <= CARD_NORMAL_INDEX_END) {
			int type = index / CARD_COUNT_VALUE + 1;
			int value = index % CARD_COUNT_VALUE + 1;
			return new PokerCard(index, type, value);
		} else if(index >= Poker.CARD_JOKER_SMALL && index <= Poker.CARD_JOKER_BIG) {
			int type = index;
			int value = index;
			return new PokerCard(index, type, value);
		}
		return null;
	}
}
