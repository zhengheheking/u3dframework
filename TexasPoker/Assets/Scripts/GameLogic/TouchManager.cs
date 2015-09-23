using UnityEngine;
using System.Collections;
using Client.Base;
using Contr;
using Events;
using Util;
using Configs;

public class TouchManager:Singleton<TouchManager> 
{
	private float m_LastTime;
    private float m_LockLastTime;
    private float m_LastUpScoreTime;
    private float m_LastDownScoreTime;
    private float m_LastUpCannonScoreTime;
    private float m_LastDownCannonScoreTime;
    private float m_ClickTime;
	public void  Init ()
    {
        m_LastTime = Time.time;
        m_LockLastTime = Time.time;
        m_LastUpScoreTime = Time.time;
        m_LastDownScoreTime = Time.time;
        m_ClickTime = Time.realtimeSinceStartup;
        m_LastUpCannonScoreTime = Time.time;
        m_LastDownCannonScoreTime = Time.time;
	}
	
	public void Loop ()
    {
        if (Time.realtimeSinceStartup - m_ClickTime>= 60)
        {
            m_ClickTime = Time.realtimeSinceStartup + 60;
            //GameDispatcher.Instance.dispatchEvent(GameEvents.TIP_SHOW, true);
        }
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Mouse ScrollWheel") > 0) && Time.time >= m_LastUpCannonScoreTime)
        {
            m_ClickTime = Time.realtimeSinceStartup;
            //GameDispatcher.Instance.dispatchEvent(GameEvents.TIP_SHOW, false);
           
            //GameDispatcher.Instance.dispatchEvent(GameEvents.CHANGE_CANNON_SCORE, 1);
        }
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Mouse ScrollWheel") < 0) && Time.time >= m_LastDownCannonScoreTime)
        {
            m_ClickTime = Time.realtimeSinceStartup;
            //GameDispatcher.Instance.dispatchEvent(GameEvents.TIP_SHOW, false);
           
            //GameDispatcher.Instance.dispatchEvent(GameEvents.CHANGE_CANNON_SCORE, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Time.time >= m_LastUpScoreTime)
        {
            m_ClickTime = Time.realtimeSinceStartup;
           // GameDispatcher.Instance.dispatchEvent(GameEvents.TIP_SHOW, false);
            
           // GameDispatcher.Instance.dispatchEvent(GameEvents.UP_SCORE);
        }
        if (Input.GetKey(KeyCode.RightArrow) && Time.time >= m_LastDownScoreTime)
        {
            m_ClickTime = Time.realtimeSinceStartup;
            //GameDispatcher.Instance.dispatchEvent(GameEvents.TIP_SHOW, false);
            
           // GameDispatcher.Instance.dispatchEvent(GameEvents.DOWM_SCORE);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //LogicWorld.Instance.ChangeScene("map1");
        }
        if (Input.GetKey(KeyCode.Space) && Time.time >= m_LastTime)
        {
            m_ClickTime = Time.realtimeSinceStartup;
        }
        ClickDetect();
	}
	
	public	void  ClickDetect(){
        if (Input.GetButton("Fire1") && Time.time >= m_LastTime)
        {
            Ray ray = UICamera.currentCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit, 100))
            {
                m_ClickTime = Time.realtimeSinceStartup;
               // GameDispatcher.Instance.dispatchEvent(GameEvents.TIP_SHOW, false);
				if (hit.transform.tag!="GUI")
                {
					
				}
			}
		}
        else if (Input.GetButton("Fire2") && Time.time >= m_LockLastTime)
        {
            Ray ray = UICamera.currentCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                m_ClickTime = Time.realtimeSinceStartup;
              //  GameDispatcher.Instance.dispatchEvent(GameEvents.TIP_SHOW, false);
                if (hit.transform.tag == "Fish")
                {
                    
                }
            }
        }
        else
        {
            Vector3 pos = UICamera.currentCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            
        }
	}
	
	void CannonFire() 
    {
        Vector3 pos = UICamera.currentCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        //Debug.Log(pos.x + " " + pos.y);
		
	}
    void Fire()
    {
       
    }
	
}


