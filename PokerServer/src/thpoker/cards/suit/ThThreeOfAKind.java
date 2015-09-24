package thpoker.cards.suit;

import thpoker.cards.ThCardsDiff;
import thpoker.cards.ThCardsSame;
import thpoker.cards.ThCardsSuit;

import java.util.Iterator;
import java.util.List;

/**
 * 三条
 * 由1个三条和2张杂牌组成
 * */
public class ThThreeOfAKind extends ThCardsSuit {
	
	private ThCardsSame three;
	private ThCardsDiff rags;

	public ThThreeOfAKind(ThCardsSame three, ThCardsDiff rags) {
		super(ThCardsSuit.SUIT_THREE_OF_A_KIND);
		this.three = three;
		this.rags = rags;
	}
	
	public int compareTo(ThCardsSuit other) {
		//先通过牌型判断大小
		int tag = super.compareTo(other);
		if(tag == 0) {
			//牌型相同
			//将参数转为相同类型
			ThThreeOfAKind temp = (ThThreeOfAKind)other;
			//先判断三条大小
			tag = this.three.compareTo(temp.three);
			if(tag == 0) {
				//三条相等，判断杂牌大小
				tag = this.rags.compareTo(temp.rags);
			}
		}
		return tag;
	}
	
	/**判断是否是三条*/
	public static boolean isThreeOfAKind(List<ThCardsSame> cardsSameList) {
		int threeCount = 0;
		int singleCount = 0;
		Iterator<ThCardsSame> it = cardsSameList.iterator();
		while(it.hasNext()) {
			ThCardsSame cardsSame = it.next();
			if(cardsSame.isThree()) {
				threeCount++;
			} else if(cardsSame.isSingle()) {
				singleCount++;
			}
		}
		if(threeCount == 1 && singleCount == 2) {
			return true;
		}
		return false;
	}
	
	/**构建三条*/
	public static ThThreeOfAKind build(List<ThCardsSame> cardsSameList) {
		ThCardsSame three = null;
		ThCardsDiff rags = new ThCardsDiff();
		
		Iterator<ThCardsSame> it = cardsSameList.iterator();
		while(it.hasNext()) {
			ThCardsSame cardsSame = it.next();
			if(three == null && cardsSame.isThree()) {
				three = cardsSame;
			} else if(cardsSame.isSingle()) {
				rags.add(cardsSame);
			}
		}
		
		return new ThThreeOfAKind(three, rags);
	}
}
