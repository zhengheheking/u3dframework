package thpoker.cards.suit;

import thpoker.cards.ThCard;
import thpoker.cards.ThCardsDiff;
import thpoker.cards.ThCardsSuit;

import java.util.List;

/**
 * 皇家同花顺
 * A-10的同花顺
 * */
public class ThRoyalStraightFlush extends ThStraightFlush {

	public ThRoyalStraightFlush(ThCardsDiff royalStraightFlush) {
		super(royalStraightFlush, ThCardsSuit.SUIT_ROYAL_STRAIGHT_FLUSH);
	}

	/**检测是否皇家同花顺*/
	public static boolean isRoyalStraightFlush(List<ThCard> fiveCards) {
		if(isMaxStraight(fiveCards) && isStraightFlush(fiveCards)) {
			return true;
		}
		return false;
	}
	
	/**构建同花顺*/
	public static ThRoyalStraightFlush build(List<ThCard> fiveCards) {
		return new ThRoyalStraightFlush(new ThCardsDiff(fiveCards));
	}
}
