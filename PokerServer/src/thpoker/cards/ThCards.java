package thpoker.cards;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;


public class ThCards {
	
	private List<ThCard> cards;

	public ThCards(List<ThCard> cards) {
		this.cards = cards;
	}
	
	public ThCards() {
		this(new ArrayList<ThCard>());
	}
	
	public void add(ThCard card) {
		this.cards.add(card);
	}
	
	public void add(ThCards cards) {
		this.cards.addAll(cards.cards());
	}
	
	public ThCard get(int index) {
		return this.cards.get(index);
	}
	public ThCard getById(int id){
		for(int i=0; i<this.cards.size(); i++){
			if (this.cards.get(i).id() == id) {
				return this.cards.get(i);
			}
		}
		return null;
	}
	public void removeById(int id){
		this.cards.remove(getById(id));
	}
	public int size() {
		return this.cards.size();
	}
	
	public void clear() {
		this.cards.clear();
	}
	
	public List<ThCard> cards() {
		return this.cards;
	}

	/**对牌组进行牌值大小排序，最大的牌最前面*/
	public synchronized void sort() {
//		Collections.reverse(this.cards);
		Collections.sort(this.cards);
		Collections.reverse(this.cards);
	}
	public synchronized String toString(){
		String temp = "";
		for(int i=0; i<this.cards.size(); i++){
			ThCard card = this.cards.get(i);
			temp += card.toString();
			temp += "|";
		}
		return temp;
	}

}
