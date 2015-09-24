package thpoker.cards.suit;

import thpoker.cards.ThCard;
import thpoker.cards.ThCardsDiff;
import thpoker.cards.ThCardsSuit;

import java.util.List;

/**
 * 同花顺
 * 有连续并且花色相同的牌组成
 * */
public class ThStraightFlush extends ThStraight {
	
	public ThStraightFlush(ThCardsDiff straightFlush) {
		this(straightFlush, ThCardsSuit.SUIT_STRAIGHT_FLUSH);
	}
	
	protected ThStraightFlush(ThCardsDiff straightFlush, int type) {
		super(straightFlush, type);
	}

	/**检测是否同花顺*/
	public static boolean isStraightFlush(List<ThCard> fiveCards) {
		if(ThFlush.isFlush(fiveCards) && isStraight(fiveCards)) {
			return true;
		}
		return false;
	}
	
	/**构建同花顺*/
	public static ThStraightFlush build(List<ThCard> fiveCards) {
		return new ThStraightFlush(new ThCardsDiff(fiveCards));
	}
}
