using Contr;
using Events;
using Socket;
using UnityEngine;
namespace View
{
    public class UIHallList : UIWindow
    {
        private UISprite m_Background;
        private UISprite m_Chujibg;
        private UIButton m_Chuji;
        private UISprite m_Zhongjibg;
        private UIButton m_Zhongji;
        private UISprite m_Gaojibg;
        private UIButton m_Gaoji;
        private UIButton m_Qs;
        private Transform m_Grid;
        private UIGrid mGridGrid;
        public override void OnCreate()
        {
            base.OnCreate();
            AddChildComponentMouseClick("chuji", OnChujiClick);
            AddChildComponentMouseClick("zhongji", OnZhongjiClick);
            AddChildComponentMouseClick("gaoji", OnGaojiClick);
            AddChildComponentMouseClick("private_room", OnPrivateRoomClick);
            AddChildComponentMouseClick("qs", OnQsClick);
            AddChildComponentMouseClick("back_btn", OnBackBtnClick);
            m_Background = Utils.FindComponent<UISprite>(mUIObject.transform, "background");
            m_Chujibg = Utils.FindComponent<UISprite>(mUIObject.transform, "chuji_bg");
            m_Chuji = Utils.FindComponent<UIButton>(mUIObject.transform, "chuji");
            m_Zhongjibg = Utils.FindComponent<UISprite>(mUIObject.transform, "zhongji_bg");
            m_Zhongji = Utils.FindComponent<UIButton>(mUIObject.transform, "zhongji");
            m_Gaojibg = Utils.FindComponent<UISprite>(mUIObject.transform, "gaoji_bg");
            m_Gaoji = Utils.FindComponent<UIButton>(mUIObject.transform, "gaoji");
            m_Qs = Utils.FindComponent<UIButton>(mUIObject.transform, "qs");
            m_Grid = Utils.FindChild(mUIObject.transform, "Grid");
            mGridGrid = m_Grid.GetComponent<UIGrid>();
        }
        public override void OnShow()
        {
            base.OnShow();
            OnChujiClick();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        private void OnBackBtnClick()
        {
            UIWindowManager.Instance.HideAllWindow();
            UIWindowManager.Instance.GetUIWindow(EUIPanel.UIHall).ShowWindow();
        }
        private void OnChujiClick()
        {
            m_Background.spriteName = "chuji_bg.jpg";
            m_Chujibg.gameObject.SetActive(true);
            m_Chuji.normalSprite = "chuji_selected_bg";
            m_Zhongjibg.gameObject.SetActive(false);
            m_Zhongji.normalSprite = "zhongji_unselected_bg";
            m_Gaojibg.gameObject.SetActive(false);
            m_Gaoji.normalSprite = "gaoji_unselected_bg";
            m_Qs.normalSprite = "chuji_qs";
            Utils.ClearAllChild(m_Grid.gameObject);
            GameObject hallListItemPrefab = ResourceManager.Instance.GetModelPrefebByName("HalllistItemChuji.prefab");
            for (int i = 0; i < 5; i++)
            {
                GameObject gridItem = NGUITools.AddChild(m_Grid.gameObject, hallListItemPrefab);

            }
            mGridGrid.repositionNow = true;
        }
        private void OnZhongjiClick()
        {
            m_Background.spriteName = "zhongji_bg.jpg";
            m_Chujibg.gameObject.SetActive(false);
            m_Chuji.normalSprite = "chuji_unselected_bg";
            m_Zhongjibg.gameObject.SetActive(true);
            m_Zhongji.normalSprite = "zhongji_selected_bg";
            m_Gaojibg.gameObject.SetActive(false);
            m_Gaoji.normalSprite = "gaoji_unselected_bg";
            m_Qs.normalSprite = "zhongji_qs";
            Utils.ClearAllChild(m_Grid.gameObject);
            GameObject hallListItemPrefab = ResourceManager.Instance.GetModelPrefebByName("HalllistItemZhongji.prefab");
            for (int i = 0; i < 5; i++)
            {
                GameObject gridItem = NGUITools.AddChild(m_Grid.gameObject, hallListItemPrefab);

            }
            mGridGrid.repositionNow = true;
        }
        private void OnGaojiClick()
        {
            m_Background.spriteName = "gaoji_bg.jpg";
            m_Chujibg.gameObject.SetActive(false);
            m_Chuji.normalSprite = "chuji_unselected_bg";
            m_Zhongjibg.gameObject.SetActive(false);
            m_Zhongji.normalSprite = "zhongji_unselected_bg";
            m_Gaojibg.gameObject.SetActive(true);
            m_Gaoji.normalSprite = "gaoji_selected_bg";
            m_Qs.normalSprite = "gaoji_qs";
            Utils.ClearAllChild(m_Grid.gameObject);
            GameObject hallListItemPrefab = ResourceManager.Instance.GetModelPrefebByName("HalllistItemGaoji.prefab");
            for (int i = 0; i < 5; i++)
            {
                GameObject gridItem = NGUITools.AddChild(m_Grid.gameObject, hallListItemPrefab);

            }
            mGridGrid.repositionNow = true;
        }
        private void OnPrivateRoomClick()
        {
            UIWindowManager.Instance.HideAllWindow();
            UIWindowManager.Instance.GetUIWindow(EUIPanel.UIPrivateRoom).ShowWindow();
        }
        private void OnQsClick()
        {

        }
    }
}