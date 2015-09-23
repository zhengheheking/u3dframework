using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Splash : MonoBehaviour
{
    private float m_StartTime;
    public void Start()
    {
        m_StartTime = Time.realtimeSinceStartup;
    }
    public void Update()
    {
        if(Time.realtimeSinceStartup-m_StartTime>3)
        {
            Application.LoadLevel("Login");
        }
    }
}
