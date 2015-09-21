package utils;

public abstract class Timer {
	private long m_Time;
	private long _time;
	public boolean m_Removing;
	public Timer(long time) {
		// TODO Auto-generated constructor stub
		_time = time;
	}
	abstract public void elapsedHandler();
	public void start(){
		m_Time = System.currentTimeMillis()+_time;
		m_Removing = false;
	}
	public void stop(){
		m_Removing = true;
	}
	public void update(){
		if (System.currentTimeMillis() >= m_Time) {
			elapsedHandler();
		}
	}
}
