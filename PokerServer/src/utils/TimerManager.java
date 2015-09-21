package utils;

import java.util.ArrayList;
import java.util.List;

import user.UserManager;

public class TimerManager {

	public TimerManager() {
		// TODO Auto-generated constructor stub
	}
	private static TimerManager instance = new TimerManager(); 
	public static TimerManager getInstance() { 
	       return instance; 
	} 
	private List<Timer> m_Timers = new ArrayList<Timer>();
	public void addTimer(Timer timer){
		m_Timers.add(timer);
	}
	public void update(){
		List<Timer> toRemoveTimers = new ArrayList<Timer>();
		for (Timer timer : m_Timers) {
			if (!timer.m_Removing) {
				timer.update();
			}else {
				toRemoveTimers.add(timer);
			}
		}
		for (Timer timer : toRemoveTimers) {
			m_Timers.remove(timer);
		}
		toRemoveTimers.clear();
	}
}
