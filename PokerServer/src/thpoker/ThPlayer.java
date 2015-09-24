//                            _ooOoo_  
//                           o8888888o  
//                           88" . "88  
//                           (| -_- |)  
//                            O\ = /O  
//                        ____/`---'\____  
//                      .   ' \\| |// `.  
//                       / \\||| : |||// \  
//                     / _||||| -:- |||||- \  
//                       | | \\\ - /// | |  
//                     | \_| ''\---/'' | |  
//                      \ .-\__ `-` ___/-. /  
//                   ___`. .' /--.--\ `. . __  
//                ."" '< `.___\_<|>_/___.' >'"".  
//               | | : `- \`.;`\ _ /`;.`/ - ` : | |  
//                 \ \ `-. \_ __\ /__ _/ .-` / /  
//         ======`-.____`-.___\_____/___.-`____.-'======  
//                            `=---='  
//  
//         .............................................  
//                  佛祖保佑             永无BUG 
//          佛曰:  
//                  写字楼里写字间，写字间里程序员；  
//                  程序人员写程序，又拿程序换酒钱。  
//                  酒醒只在网上坐，酒醉还来网下眠；  
//                  酒醉酒醒日复日，网上网下年复年。  
//                  但愿老死电脑间，不愿鞠躬老板前；  
//                  奔驰宝马贵者趣，公交自行程序员。  
//                  别人笑我忒疯癫，我笑自己命太贱；  
//                  不见满街漂亮妹，哪个归得程序员？ 
package thpoker;

import thpoker.cards.ThCard;
import thpoker.cards.ThCardsHand;
import thpoker.cards.ThCardsSuit;

/**
 * 五张牌的玩家
 * */
public class ThPlayer{
	
	/**玩家拥有的手牌*/
	private ThCardsHand handCards;
	/**玩家形成的最佳牌型*/
	private ThCardsSuit bestCards;
	
	public ThPlayer() {
		this.handCards = new ThCardsHand();
	}
	public void reset(){
		initHandCards();
		this.bestCards = null;
	}
	public void bestCards(ThCardsSuit bestCards){
		this.bestCards = bestCards;
	}
	public ThCardsSuit bestCards(){
		return this.bestCards;
	}
	/**初始化游戏数据*/
	public void initHandCards() {
		this.handCards.clear();
	}
	
	public ThCardsHand handCards(){
		return this.handCards;
	}

	public void addBallance(int value){
		
	}
	
	public void addReward(int value){
		
	}
	
	public int getBallance(){
		return 0;
	}
	
	public boolean isFold(){
		return false;
	}
	
	public int totalBet(){
		return 0;
	}
	
	public int getUserId(){
		return 0;
	}
	
	public void printCards(){
		System.out.println(handCards.toString());
	}
	
}
