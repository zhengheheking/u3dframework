using Contr;
using Events;
using Socket;
using UnityEngine;
namespace View
{
    public class UILogin : UIWindow
    {
        public override void OnCreate()
        {
            base.OnCreate();
            AddChildComponentMouseClick("btn", OnLoginClick);
            AddChildComponentMouseClick("weixinBtn", OnWeixinBtnClick);
            AddChildComponentMouseClick("weiboBtn", OnWeiboBtnClick);
        }
        public override void OnShow()
        {
            base.OnShow();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        private void OnLoginClick()
        {
            GameDispatcher.Instance.dispatchEvent(GameEvents.LOGIN_CLICK);
        }
        private void OnWeixinBtnClick()
        {
            
        }
        private void OnWeiboBtnClick()
        {
            
        }
    }
}