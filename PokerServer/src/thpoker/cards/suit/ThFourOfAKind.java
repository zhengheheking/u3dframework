package thpoker.cards.suit;

import thpoker.cards.ThCard;
import thpoker.cards.ThCardsSame;
import thpoker.cards.ThCardsSuit;

import java.util.Iterator;
import java.util.List;

/**
 * 四条
 * 由1个四条和1张杂牌组成
 * */
public class ThFourOfAKind extends ThCardsSuit {
	
	private ThCardsSame four;
	private ThCard rag;

	public ThFourOfAKind(ThCardsSame four, ThCard rag) {
		super(ThCardsSuit.SUIT_FOUR_OF_A_KIND);
		this.four = four;
		this.rag = rag;
	}
	
	public int compareTo(ThCardsSuit other) {
		//先通过牌型判断大小
		int tag = super.compareTo(other);
		if(tag == 0) {
			//牌型相同
			//将参数转为相同类型
			ThFourOfAKind temp = (ThFourOfAKind)other;
			//先判断四条大小
			tag = this.four.compareTo(temp.four);
			if(tag == 0) {
				//四条相等，判断杂牌大小
				tag = this.rag.compareTo(temp.rag);
			}
		}
		return tag;
	}
	
	/**判断是否是四条*/
	public static boolean isFourOfAKind(List<ThCardsSame> cardsSameList) {
		int fourCount = 0;
		int singleCount = 0;
		Iterator<ThCardsSame> it = cardsSameList.iterator();
		while(it.hasNext()) {
			ThCardsSame cardsSame = it.next();
			if(cardsSame.isFour()) {
				fourCount++;
			} else if(cardsSame.isSingle()) {
				singleCount++;
			}
		}
		if(fourCount == 1 && singleCount == 1) {
			return true;
		}
		return false;
	}
	
	/**构建四条*/
	public static ThFourOfAKind build(List<ThCardsSame> cardsSameList) {
		ThCardsSame four = null;
		ThCard rag = null;
		
		Iterator<ThCardsSame> it = cardsSameList.iterator();
		while(it.hasNext()) {
			ThCardsSame cardsSame = it.next();
			if(four == null && cardsSame.isFour()) {
				four = cardsSame;
			} else if(cardsSame.isSingle()) {
				rag = cardsSame.singleCard();
			}
		}
		
		return new ThFourOfAKind(four, rag);
	}
}
