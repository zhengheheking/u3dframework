package thpoker.cards;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;

/**
 * 牌值相同的牌组
 * 对子，三条，四条等
 * */
public class ThCardsSame extends ThCards implements Comparable<ThCardsSame> {

	public ThCardsSame(List<ThCard> cards) {
		super(cards);
	}
	
	public ThCard singleCard() {
		return this.cards().get(0);
	}
	
	public boolean isSingle() {
		return this.size() == 1;
	}
	
	public boolean isPair() {
		return this.size() == 2;
	}
	
	public boolean isThree() {
		return this.size() == 3;
	}
	
	public boolean isFour() {
		return this.size() == 4;
	}
	
	/**比较大小*/
	public int compareTo(ThCardsSame other) {
		if(this.size() > other.size()) {
			return 1;
		} else if(this.size() < other.size()) {
			return -1;
		} else {
			return this.singleCard().compareTo(other.singleCard());
		}
	}
	
	/**从多张牌中挑选出相同的卡，传入的牌组必须是从大到小先排好序的*/
	public static List<List<ThCard>> getSameCardFromCards(List<ThCard> cards) {
		List<List<ThCard>> sameCardsList = new ArrayList<List<ThCard>>();
		ThCard preCard = null;
		List<ThCard> sameCards = null;
		Iterator<ThCard> it = cards.iterator();
		while(it.hasNext()) {
			ThCard card = it.next();
			if(preCard != null) {
				if(preCard.value() != card.value()) {
					sameCardsList.add(sameCards);
					sameCards = null;
				}
			}
			if(sameCards == null) {
				sameCards = new ArrayList<ThCard>();
			}
			preCard = card;
			sameCards.add(preCard);
		}
		if(sameCards != null) {
			sameCardsList.add(sameCards);
		}
		return sameCardsList;
	}
	
	/**获得一个根据相同牌数量排序的相同牌列表*/
	public static List<ThCardsSame> getCardsSameSortListFromCards(List<ThCard> cards) {
		List<ThCardsSame> cardsSameList = new ArrayList<ThCardsSame>();
		List<List<ThCard>> sameCardsList = getSameCardFromCards(cards);
		Iterator<List<ThCard>> it = sameCardsList.iterator();
		while(it.hasNext()) {
			List<ThCard> sameCards = it.next();
			cardsSameList.add(new ThCardsSame(sameCards));
		}
		Collections.reverse(cardsSameList);
		return cardsSameList;
	}
}
