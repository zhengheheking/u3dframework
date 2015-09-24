package thpoker.cards;

/**
 * 玩家手牌
 * */
public class ThCardsHand extends ThCards {

	private static final int MAX_COUNT = 2;
	
	public ThCardsHand() {
	}
	
	public synchronized boolean isFull() {
		return this.size() >= MAX_COUNT;
	}
	
	
}
