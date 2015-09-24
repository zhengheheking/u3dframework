package thpoker.cards;

import thpoker.cards.suit.ThFlush;
import thpoker.cards.suit.ThFourOfAKind;
import thpoker.cards.suit.ThFullHouse;
import thpoker.cards.suit.ThHighCard;
import thpoker.cards.suit.ThOnePair;
import thpoker.cards.suit.ThRoyalStraightFlush;
import thpoker.cards.suit.ThStraight;
import thpoker.cards.suit.ThStraightFlush;
import thpoker.cards.suit.ThThreeOfAKind;
import thpoker.cards.suit.ThTwoPair;

import java.util.Iterator;
import java.util.List;

public class ThCardsUtils {
	/**判断牌型*/
	public static ThCardsSuit getSuitFrom5Cards(List<ThCard> fiveCards) {
		//牌型从大到小判断
		if(ThRoyalStraightFlush.isRoyalStraightFlush(fiveCards)) {
			return ThRoyalStraightFlush.build(fiveCards);
		}
		if(ThStraightFlush.isStraightFlush(fiveCards)) {
			return ThStraightFlush.build(fiveCards);
		}
		List<ThCardsSame> cardsSameList = ThCardsSame.getCardsSameSortListFromCards(fiveCards);
		if(ThFourOfAKind.isFourOfAKind(cardsSameList)) {
			return ThFourOfAKind.build(cardsSameList);
		}
		if(ThFullHouse.isFullHouse(cardsSameList)) {
			return ThFullHouse.build(cardsSameList);
		}
		if(ThFlush.isFlush(fiveCards)) {
			return ThFlush.build(fiveCards);
		}
		if(ThStraight.isStraight(fiveCards)) {
			return ThStraight.build(fiveCards);
		}
		if(ThThreeOfAKind.isThreeOfAKind(cardsSameList)) {
			return ThThreeOfAKind.build(cardsSameList);
		}
		if(ThTwoPair.isTwoPair(cardsSameList)) {
			return ThTwoPair.build(cardsSameList);
		}
		if(ThOnePair.isOnePair(cardsSameList)) {
			return ThOnePair.build(cardsSameList);
		}
		return ThHighCard.build(fiveCards);
	}
	public static ThCardsSuit getBestSuit(List<ThCardsSuit> suitList) {
		ThCardsSuit bestSuit = null;
		Iterator<ThCardsSuit> it = suitList.iterator();
		while(it.hasNext()) {
			ThCardsSuit suit = it.next();
			if(bestSuit == null) {
				bestSuit = suit;
			} else {
				if(bestSuit.compareTo(suit) < 0) {
					bestSuit = suit;
				}
			}
			
		}
		return bestSuit;
	}
}
