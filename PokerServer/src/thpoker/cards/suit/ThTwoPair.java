package thpoker.cards.suit;

import thpoker.cards.ThCard;
import thpoker.cards.ThCardsSame;
import thpoker.cards.ThCardsSuit;

import java.util.Iterator;
import java.util.List;

/**
 * 2对
 * 包含2个对子和一张杂牌
 * */
public class ThTwoPair extends ThCardsSuit {
	
	private ThCardsSame pair1;
	private ThCardsSame pair2;
	private ThCard rag;

	public ThTwoPair(ThCardsSame pair1, ThCardsSame pair2, ThCard rag) {
		super(ThCardsSuit.SUIT_TWO_PAIR);
		this.pair1 = pair1;
		this.pair2 = pair2;
		this.rag = rag;
	}
	

	public int compareTo(ThCardsSuit other) {
		//先通过牌型判断大小
		int tag = super.compareTo(other);
		if(tag == 0) {
			//牌型相同
			//将参数转为相同类型
			ThTwoPair temp = (ThTwoPair)other;
			//先判断对子1的大小
			tag = this.pair1.compareTo(temp.pair1);
			if(tag == 0) {
				//对子1相等，判断对子2
				tag = this.pair2.compareTo(temp.pair2);
				if(tag == 0) {
					//对子2也相等，判断杂牌
					tag = this.rag.compareTo(temp.rag);
				}
			}
		}
		return tag;
	}
	
	/**判断是否是两对*/
	public static boolean isTwoPair(List<ThCardsSame> cardsSameList) {
		int pairCount = 0;
		Iterator<ThCardsSame> it = cardsSameList.iterator();
		while(it.hasNext()) {
			ThCardsSame cardsSame = it.next();
			if(cardsSame.isPair()) {
				pairCount++;
			}
		}
		if(pairCount == 2) {
			return true;
		}
		return false;
	}
	
	/**构建两对*/
	public static ThTwoPair build(List<ThCardsSame> cardsSameList) {
		ThCardsSame pair1 = null;
		ThCardsSame pair2 = null;
		ThCard rag = null;
		
		Iterator<ThCardsSame> it = cardsSameList.iterator();
		while(it.hasNext()) {
			ThCardsSame cardsSame = it.next();
			if(pair1 == null && cardsSame.isPair()) {
				pair1 = cardsSame;
			} else if(pair2 == null && cardsSame.isPair()) {
				pair2 = cardsSame;
			} else if(cardsSame.isSingle()) {
				rag = cardsSame.singleCard();
			}
		}
		
		return new ThTwoPair(pair1, pair2, rag);
	}
}
