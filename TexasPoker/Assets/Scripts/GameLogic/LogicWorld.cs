using Client.Base;
using Configs;
using Contr;
using Events;
using Model;
using Socket;
using Socket.Net;
using Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicWorld:Singleton<LogicWorld>,Controller
{
    public GameDispatcher getDispatcher()
    {
        return GameDispatcher.Instance;
    }
    public void registerSocket()
    {

    }
    public void addEvent()
    {

    }

    public int m_GameState;
    public void Init()
    {
        registerSocket();
    }

    public void Update()
    {
        
    }
    public void FixedUpdate()
    {
       
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Fish")
        {
            
        }
    }
    
}