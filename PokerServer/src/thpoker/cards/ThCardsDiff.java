package thpoker.cards;

import java.util.List;

/**
 * 牌值不同的牌组
 * 构造时传入的牌组必须是先从大到小排序的
 * */
public class ThCardsDiff extends ThCards implements Comparable<ThCardsDiff> {

	public ThCardsDiff(List<ThCard> cards) {
		super(cards);
	}
	
	public ThCardsDiff() {
		super();
	}
	
	/**比较大小，必须是牌数相同的才能比较大小*/
	public int compareTo(ThCardsDiff other) {
		for(int i = 0; i < this.size(); i++) {
			ThCard card1 = this.cards().get(i);
			ThCard card2 = other.cards().get(i);
			int res = card1.compareTo(card2);
			if(res != 0) {
				return res;
			}
		}
		return 0;
	}
}
