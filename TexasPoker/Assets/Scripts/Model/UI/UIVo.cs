/**
* UIµÄvo
*/
namespace Model.UI.Vo
{
    using Client.Base;
    public class UIVo : EventDispatcher
    {
        public UIVo()
        {

        }
        public const string CHANGE = "change";
        public void change()
        {
            this.dispatchEvent(CHANGE, this);
        }
    }
}