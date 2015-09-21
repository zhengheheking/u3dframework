using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Client.Base
{
  
    public class Timer
    {
        public delegate void ElapsedEventHandler(object sender);
        private float Time { set; get; }
        private float m_Time { set; get; }
        public event ElapsedEventHandler Elapsed;
        public bool Removing { set; get; }

        public Timer(float time)//Ãë
        {
            m_Time = time;

        }
        public void Start()
        {
            Time = UnityEngine.Time.realtimeSinceStartup + m_Time;
            Removing = false;
        }
        public void Stop()
        {
            Removing = true;
        }
        public void Update()
        {
            if (UnityEngine.Time.realtimeSinceStartup >= Time)
            {
                Elapsed(this);
            }
        }
    }
    public class TimerManager : Singleton<TimerManager>
    {
        private List<Timer> timerList = new List<Timer>();
        public void AddTimer(Timer timer)
        {
            timerList.Add(timer);
        }
        public void Update()
        {
            foreach (Timer timer in timerList)
            {
                if(!timer.Removing)
                {
                    timer.Update();
                }
            }
            List<Timer> toRemove = new List<Timer>();
            foreach (Timer timer in timerList)
            {
                if(timer.Removing)
                {
                    toRemove.Add(timer);
                }
            }
            foreach (Timer timer in toRemove)
            {
                timerList.Remove(timer);
            }
            toRemove.Clear();
        }
    }
}