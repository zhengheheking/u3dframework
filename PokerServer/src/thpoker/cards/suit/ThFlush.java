package thpoker.cards.suit;

import poker.PokerCard;
import thpoker.cards.ThCard;
import thpoker.cards.ThCardsDiff;
import thpoker.cards.ThCardsSuit;

import java.util.Iterator;
import java.util.List;

/**
 * 同花
 * 一组花色相同的牌组成
 * */
public class ThFlush extends ThHighCard {

	public ThFlush(ThCardsDiff flush) {
		super(flush, ThCardsSuit.SUIT_FLUSH);
	}

	/**检测是否同花*/
	public static boolean isFlush(List<ThCard> fiveCards) {
		int type = PokerCard.CARD_TYPE_NULL;
		Iterator<ThCard> it = fiveCards.iterator();
		while(it.hasNext()) {
			ThCard card = it.next();
			if(type == PokerCard.CARD_TYPE_NULL) {
				type = card.type();
			} else {
				if(type != card.type()) {
					return false;
				}
			}
		}
		return true;
	}
	
	/**构建同花*/
	public static ThFlush build(List<ThCard> fiveCards) {
		return new ThFlush(new ThCardsDiff(fiveCards));
	}
}
