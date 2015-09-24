package thpoker.cards.suit;

import thpoker.cards.ThCardsDiff;
import thpoker.cards.ThCardsSame;
import thpoker.cards.ThCardsSuit;

import java.util.Iterator;
import java.util.List;

/**
 * 一对
 * 包含一对和3张杂牌
 * */
public class ThOnePair extends ThCardsSuit {
	
	private ThCardsSame pair;
	private ThCardsDiff rags;

	public ThOnePair(ThCardsSame pair, ThCardsDiff rags) {
		super(ThCardsSuit.SUIT_ONE_PAIR);
		this.pair = pair;
		this.rags = rags;
	}
	
	public int compareTo(ThCardsSuit other) {
		//先通过牌型判断大小
		int tag = super.compareTo(other);
		if(tag == 0) {
			//牌型相同
			//将参数转为相同类型
			ThOnePair temp = (ThOnePair)other;
			//先判断对子的大小
			tag = this.pair.compareTo(temp.pair);
			if(tag == 0) {
				//对子相等，判断杂牌
				tag = this.rags.compareTo(temp.rags);
			}
		}
		return tag;
	}
	
	/**判断是否是一对*/
	public static boolean isOnePair(List<ThCardsSame> cardsSameList) {
		int pairCount = 0;
		Iterator<ThCardsSame> it = cardsSameList.iterator();
		while(it.hasNext()) {
			ThCardsSame cardsSame = it.next();
			if(cardsSame.isPair()) {
				pairCount++;
			}
		}
		if(pairCount == 1) {
			return true;
		}
		return false;
	}
	
	/**构建一对*/
	public static ThOnePair build(List<ThCardsSame> cardsSameList) {
		ThCardsSame pair = null;
		ThCardsDiff rags = new ThCardsDiff();
		
		Iterator<ThCardsSame> it = cardsSameList.iterator();
		while(it.hasNext()) {
			ThCardsSame cardsSame = it.next();
			if(pair == null && cardsSame.isPair()) {
				pair = cardsSame;
			} else if(cardsSame.isSingle()) {
				rags.add(cardsSame);
			}
		}
		
		return new ThOnePair(pair, rags);
	}
}
