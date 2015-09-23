using Events;
using GameLogic;
using Socket;
using Socket.Net;
using View;
using UnityEngine;
namespace Contr
{
    public class UCLogin:UIWindowCtrlTemplate<UILogin>
    {
        public UCLogin()
        {
            GameDispatcher.Instance.addEventListener(OnLoginSuccess, GameEvents.LOGIN_SUCCESS);
            
        }
        private void OnLoginSuccess(string evt, params object[] args)
        {
            
        }
        
    }
}