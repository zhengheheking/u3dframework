package thpoker.cards.suit;

import poker.PokerCard;
import thpoker.cards.ThCard;
import thpoker.cards.ThCardsDiff;
import thpoker.cards.ThCardsSuit;

import java.util.Iterator;
import java.util.List;

/**
 * 顺子
 * 由一组数值连续的牌组成
 * */
public class ThStraight extends ThCardsSuit {
	
	private ThCardsDiff straight;

	public ThStraight(ThCardsDiff straight) {
		this(straight, ThCardsSuit.SUIT_STRAIGHT);
	}
	
	protected ThStraight(ThCardsDiff straight, int type) {
		super(type);
		this.straight = straight;
	}
	
	
	public int compareTo(ThCardsSuit other) {
		//先通过牌型判断大小
		int tag = super.compareTo(other);
		if(tag == 0) {
			//牌型相同
			//将参数转为相同类型
			ThStraight temp = (ThStraight)other;
			//先判断顺子大小
			tag = this.straight.compareTo(temp.straight);
		}
		return tag;
	}

	/**检测是否最大顺子，需要排序再传入*/
	protected static boolean isMaxStraight(List<ThCard> fiveCards) {
		if(fiveCards.get(0).isAce()
				&& fiveCards.get(1).isKing()
				&& fiveCards.get(2).isQueen()
				&& fiveCards.get(3).isJack()
				&& fiveCards.get(4).is10()) {
			return true;
		}
		return false;
	}

	/**检测是否最小顺子，需要排序再传入*/
	public static boolean isMinStraight(List<ThCard> fiveCards) {
		if(fiveCards.get(0).isAce()
				&& fiveCards.get(1).is5()
				&& fiveCards.get(2).is4()
				&& fiveCards.get(3).is3()
				&& fiveCards.get(4).is2()) {
			return true;
		}
		return false;
	}

	/**检测是否顺子，需要排序再传入*/
	public static boolean isStraight(List<ThCard> fiveCards) {
		//由于会有A-5这种不符合连续减小的顺子，所以先直接判断是不是最小顺
		if(isMinStraight(fiveCards)) {
			return true;
		}
		//不是最小顺，用普通的方式来判断是不是顺子
		int txValue = PokerCard.CARD_VALUE_NULL;
		Iterator<ThCard> it = fiveCards.iterator();
		while(it.hasNext()) {
			ThCard card = it.next();
			if(txValue != PokerCard.CARD_VALUE_NULL && txValue != card.txValue() + 1) {
				return false;
			}
			txValue = card.txValue();
		}
		return true;
	}
	
	/**构建顺子*/
	public static ThStraight build(List<ThCard> fiveCards) {
		return new ThStraight(new ThCardsDiff(fiveCards));
	}
}
