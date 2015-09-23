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
            GameDispatcher.Instance.addEventListener(OnLoginClick, GameEvents.LOGIN_CLICK);
            
        }
        private void OnLoginClick(string evt, params object[] args)
        {
            SceneManager.Instance.LoadScene("Hall", ESceneType.HallScene);
        }
        
    }
}