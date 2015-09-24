package thpoker.cards;

public class ThCardsSuit implements Comparable<ThCardsSuit> {
	
	/**牌型*/
	private int type;
	
	/**所有牌型组合*/
	/**高牌*/
	public static final int SUIT_HIGH_CARD = 0;
	/**一对*/
	public static final int SUIT_ONE_PAIR = 1;
	/**两对*/
	public static final int SUIT_TWO_PAIR = 2;
	/**三条*/
	public static final int SUIT_THREE_OF_A_KIND = 3;
	/**顺子*/
	public static final int SUIT_STRAIGHT = 4;
	/**同花*/
	public static final int SUIT_FLUSH = 5;
	/**葫芦，三条加一对*/
	public static final int SUIT_FULL_HOUSE = 6;
	/**四条*/
	public static final int SUIT_FOUR_OF_A_KIND = 7;
	/**同花顺*/
	public static final int SUIT_STRAIGHT_FLUSH = 8;
	/**皇家同花顺，10到A的同花顺*/
	public static final int SUIT_ROYAL_STRAIGHT_FLUSH = 9;
	
	public ThCardsSuit(int type) {
		this.type = type;
	}
	public synchronized int type(){
		return this.type;
	}
	public synchronized boolean isHighCard() {
		return this.type == SUIT_HIGH_CARD;
	}
	
	public synchronized boolean isOnePair() {
		return this.type == SUIT_ONE_PAIR;
	}
	
	public synchronized boolean isTwoPair() {
		return this.type == SUIT_TWO_PAIR;
	}
	
	public synchronized boolean isThreeOfAKind() {
		return this.type == SUIT_THREE_OF_A_KIND;
	}
	
	public synchronized boolean isStraight() {
		return this.type == SUIT_STRAIGHT;
	}
	
	public synchronized boolean isFlush() {
		return this.type == SUIT_FLUSH;
	}
	
	public synchronized boolean isFullHouse() {
		return this.type == SUIT_FULL_HOUSE;
	}
	
	public synchronized boolean isFourOfAKind() {
		return this.type == SUIT_FOUR_OF_A_KIND;
	}
	
	public synchronized boolean isStraightFlush() {
		return this.type == SUIT_STRAIGHT_FLUSH;
	}
	
	public synchronized boolean isRoyalStraightFlush() {
		return this.type == SUIT_ROYAL_STRAIGHT_FLUSH;
	}
	
	/**通过牌类型判断大小*/
	public int compareTo(ThCardsSuit other) {
		if(this.type > other.type) {
			return 1;
		} else if(this.type < other.type) {
			return -1;
		} else {
			return 0;
		}
	}
}
