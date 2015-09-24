package poker;


/**
 * 扑克牌，表示单张扑克
 * 值分别为 1 2 3 4 5 6 7 8 9 10 J Q K small_joker big_joker
 * */
public class PokerCard {
	
	//扑克类型
	/**没有类型*/
	public static final int CARD_TYPE_NULL = 0;
	/**方块*/
	public static final int CARD_TYPE_DIAMOND = 1;
	/**梅花*/
	public static final int CARD_TYPE_CLUB = 2;
	/**红桃*/
	public static final int CARD_TYPE_HEART = 3;
	/**黑桃*/
	public static final int CARD_TYPE_SPADE = 4;
	/**小王*/
	public static final int CARD_TYPE_JOKER_SMALL = 53;
	/**大王*/
	public static final int CARD_TYPE_JOKER_BIG = 54;
	
	//扑克牌值
	public static final int CARD_VALUE_NULL = 0;
	public static final int CARD_VALUE_ACE = 1;
	public static final int CARD_VALUE_2 = 2;
	public static final int CARD_VALUE_3 = 3;
	public static final int CARD_VALUE_4 = 4;
	public static final int CARD_VALUE_5 = 5;
	public static final int CARD_VALUE_6 = 6;
	public static final int CARD_VALUE_7 = 7;
	public static final int CARD_VALUE_8 = 8;
	public static final int CARD_VALUE_9 = 9;
	public static final int CARD_VALUE_10 = 10;
	public static final int CARD_VALUE_JACK = 11;
	public static final int CARD_VALUE_QUEEN = 12;
	public static final int CARD_VALUE_KING = 13;
	public static final int CARD_VALUE_JOKER_SMALL = 53;
	public static final int CARD_VALUE_JOKER_BIG = 54;
	
	private int type = CARD_TYPE_NULL;
	private int value = CARD_VALUE_NULL;
	
	private int id;
	
	public PokerCard(int id, int type, int value) {
		this.id = id;
		this.type = type;
		this.value = value;
	}
	public int id(){
		return this.id;
	}
	/**返回牌类型*/
	public int type() {
		return this.type;
	}
	
	/**返回牌值*/
	public int value() {
		return this.value;
	}
	
	/**是否为黑桃*/
	public boolean isSpade() {
		return this.type == CARD_TYPE_SPADE;
	}
	
	/**是否为红心*/
	public boolean isHeart() {
		return this.type == CARD_TYPE_HEART;
	}
	
	/**是否为梅花*/
	public boolean isClub() {
		return this.type == CARD_TYPE_CLUB;
	}
	
	/**是否为方块*/
	public boolean isDiamond() {
		return this.type == CARD_TYPE_DIAMOND;
	}
	
	public boolean isAce() {
		return this.value == CARD_VALUE_ACE;
	}
	
	public boolean is2() {
		return this.value == CARD_VALUE_2;
	}
	
	public boolean is3() {
		return this.value == CARD_VALUE_3;
	}
	
	public boolean is4() {
		return this.value == CARD_VALUE_4;
	}
	
	public boolean is5() {
		return this.value == CARD_VALUE_5;
	}
	
	public boolean is6() {
		return this.value == CARD_VALUE_6;
	}
	
	public boolean is7() {
		return this.value == CARD_VALUE_7;
	}
	
	public boolean is8() {
		return this.value == CARD_VALUE_8;
	}
	
	public boolean is9() {
		return this.value == CARD_VALUE_9;
	}
	
	public boolean is10() {
		return this.value == CARD_VALUE_10;
	}
	
	public boolean isJack() {
		return this.value == CARD_VALUE_JACK;
	}
	
	public boolean isQueen() {
		return this.value == CARD_VALUE_QUEEN;
	}
	
	public boolean isKing() {
		return this.value == CARD_VALUE_KING;
	}
	
	public boolean isJokerSmall() {
		return this.type == CARD_TYPE_JOKER_SMALL && this.value == CARD_VALUE_JOKER_SMALL;
	}
	
	public boolean isJokerBig() {
		return this.type == CARD_TYPE_JOKER_BIG && this.value == CARD_VALUE_JOKER_BIG;
	}
}
