package thpoker.cards.suit;

import thpoker.cards.ThCardsSame;
import thpoker.cards.ThCardsSuit;

import java.util.Iterator;
import java.util.List;

/**
 * 葫芦
 * 由1个三条和1对组成
 * */
public class ThFullHouse extends ThCardsSuit {
	
	private ThCardsSame three;
	private ThCardsSame pair;

	public ThFullHouse(ThCardsSame three, ThCardsSame pair) {
		super(ThCardsSuit.SUIT_FULL_HOUSE);
		this.three = three;
		this.pair = pair;
	}
	
	public int compareTo(ThCardsSuit other) {
		//先通过牌型判断大小
		int tag = super.compareTo(other);
		if(tag == 0) {
			//牌型相同
			//将参数转为相同类型
			ThFullHouse temp = (ThFullHouse)other;
			//先判断三条大小
			tag = this.three.compareTo(temp.three);
			if(tag == 0) {
				//三条相同，虽然这种情况不会出现，但是还是接下去判断一下
				tag = this.pair.compareTo(temp.pair);
			}
		}
		return tag;
	}
	
	/**判断是否是葫芦*/
	public static boolean isFullHouse(List<ThCardsSame> cardsSameList) {
		int threeCount = 0;
		int pairCount = 0;
		Iterator<ThCardsSame> it = cardsSameList.iterator();
		while(it.hasNext()) {
			ThCardsSame cardsSame = it.next();
			if(cardsSame.isThree()) {
				threeCount++;
			} else if(cardsSame.isPair()) {
				pairCount++;
			}
		}
		if(threeCount == 1 && pairCount == 1) {
			return true;
		}
		return false;
	}
	
	/**构建葫芦*/
	public static ThFullHouse build(List<ThCardsSame> cardsSameList) {
		ThCardsSame three = null;
		ThCardsSame pair = null;
		
		Iterator<ThCardsSame> it = cardsSameList.iterator();
		while(it.hasNext()) {
			ThCardsSame cardsSame = it.next();
			if(three == null && cardsSame.isThree()) {
				three = cardsSame;
			} else if(pair == null && cardsSame.isPair()) {
				pair = cardsSame;
			}
		}
		
		return new ThFullHouse(three, pair);
	}
}
