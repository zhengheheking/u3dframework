package thpoker.cards.suit;

import thpoker.cards.ThCard;
import thpoker.cards.ThCardsDiff;
import thpoker.cards.ThCardsSuit;

import java.util.List;

/**
 * 高牌
 * 5张杂牌组成
 * */
public class ThHighCard extends ThCardsSuit {
	
	private ThCardsDiff rags;

	public ThHighCard(ThCardsDiff rags) {
		this(rags, ThCardsSuit.SUIT_HIGH_CARD);
	}

	protected ThHighCard(ThCardsDiff rags, int type) {
		super(type);
		this.rags = rags;
	}
	
	public int compareTo(ThCardsSuit other) {
		//先通过牌型判断大小
		int tag = super.compareTo(other);
		if(tag == 0) {
			//牌型相同
			//将参数转为相同类型
			ThHighCard temp = (ThHighCard)other;
			//先判断杂牌大小
			tag = this.rags.compareTo(temp.rags);
		}
		return tag;
	}
	
	/**构建高牌*/
	public static ThHighCard build(List<ThCard> fiveCards) {
		return new ThHighCard(new ThCardsDiff(fiveCards));
	}
}
