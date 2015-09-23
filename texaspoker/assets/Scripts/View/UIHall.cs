using Client.Base;
using Contr;
using Events;
using Socket;
using System.Collections;
using UnityEngine;
using Util;
namespace View
{
    public class UIHall : UIWindow
    {
        private Transform m_Grid;
        private UIGrid mGridGrid;
        private Timer m_GirlEyeAnimTimer;
        private Transform m_GirlEyeFrame1;
        private Transform m_GirlEyeFrame2;
        private Transform m_GirlEyeFrame3;
        private int m_FrameIndex = 1;
        private Transform m_FaceRed;
        private Transform m_MouthClose;
        private Transform m_MouthHalf;
        private Transform m_EyeClose;
        private Transform m_EyeHalf;
        private int m_FaceIndex = 1;
        private bool m_GirlEyeTimerRunning = true;
        public override void OnCreate()
        {
            base.OnCreate();
            AddChildComponentMouseClick("beginBtn", OnBeginBtnClick);
            AddChildComponentMouseClick("halllistBtn", OnHalllistBtnClick);
            AddChildComponentMouseClick("sngBtn", OnSngBtnClick);
            AddChildComponentMouseClick("mttBtn", OnMttBtnClick);
            AddChildComponentMouseClick("taskBtn", OnTaskBtnClick);
            AddChildComponentMouseClick("rechargeBtn", OnRechargeBtnClick);
            AddChildComponentMouseClick("huodongBtn", OnHuodongBtnClick);
            AddChildComponentMouseClick("interactBtn", OnInteractBtnClick);
            AddChildComponentMouseClick("billboardBtn", OnBillboardBtnClick);
            AddChildComponentMouseClick("faqBtn", OnFaqBtnClick);
            AddChildComponentMouseClick("settingBtn", OnSettingBtnClick);
            AddChildComponentMouseClick("girl", OnGirlClick);
            m_Grid = Utils.FindChild(mUIObject.transform, "Grid");
            mGridGrid = m_Grid.GetComponent<UIGrid>();
            Transform girlEye = Utils.FindChild(mUIObject.transform, "GirlEye");
            m_GirlEyeFrame1 = Utils.FindChild(girlEye, "frame1");
            m_GirlEyeFrame2 = Utils.FindChild(girlEye, "frame2");
            m_GirlEyeFrame3 = Utils.FindChild(girlEye, "frame3");
            Transform girlFace = Utils.FindChild(mUIObject.transform, "GirlFace");
            m_FaceRed = Utils.FindChild(girlFace, "faceRed");
            m_MouthClose = Utils.FindChild(girlFace, "mouthClose");
            m_MouthHalf = Utils.FindChild(girlFace, "mouthHalf");
            m_EyeClose = Utils.FindChild(girlFace, "eyeClose");
            m_EyeHalf = Utils.FindChild(girlFace, "eyeHalf");
        }
        public override void OnShow()
        {
            base.OnShow();
            GameObject rankItemPrefab = ResourceManager.Instance.GetModelPrefebByName("HallRankItem.prefab");
            for(int i=0; i<5; i++)
            {
                GameObject gridItem = NGUITools.AddChild(m_Grid.gameObject, rankItemPrefab);

            }
            mGridGrid.repositionNow = true;
            m_GirlEyeAnimTimer = new Timer(4f);
            m_GirlEyeAnimTimer.Elapsed += GirlEyeAnimHandler;
            m_GirlEyeAnimTimer.Start();
            TimerManager.Instance.AddTimer(m_GirlEyeAnimTimer);

        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        private void GirlEyeAnimHandler(object sender)
        {
            if (m_GirlEyeTimerRunning)
            {
                m_GirlEyeFrame1.gameObject.SetActive(true);
            }
            
            m_FrameIndex = 1;
            CoroutineManager.StartCoroutine(StartFrameTimer());
            m_GirlEyeAnimTimer.Start();
        }
        private IEnumerator StartFrameTimer()
        {
            yield return new WaitForEndOfFrame();
            Timer frameTimer = new Timer(0.2f);
            frameTimer.Elapsed += GirlEyeFrameHandler;
            frameTimer.Start();
            TimerManager.Instance.AddTimer(frameTimer);
            yield return null;
        }
        private void GirlEyeFrameHandler(object sender)
        {
            Timer frameTimer = sender as Timer;
            m_FrameIndex++;
            if(m_FrameIndex == 2)
            {
                m_GirlEyeFrame1.gameObject.SetActive(false);
                if (m_GirlEyeTimerRunning)
                {
                    m_GirlEyeFrame2.gameObject.SetActive(true);
                }
                
                m_GirlEyeFrame3.gameObject.SetActive(false);
                frameTimer.Start();
            }
            if(m_FrameIndex == 3)
            {
                m_GirlEyeFrame1.gameObject.SetActive(false);
                m_GirlEyeFrame2.gameObject.SetActive(false);
                if (m_GirlEyeTimerRunning)
                {
                    m_GirlEyeFrame3.gameObject.SetActive(true);
                }
                
                frameTimer.Start();
            }
            if(m_FrameIndex == 4)
            {
                m_GirlEyeFrame1.gameObject.SetActive(false);
                m_GirlEyeFrame2.gameObject.SetActive(false);
                m_GirlEyeFrame3.gameObject.SetActive(false);
                frameTimer.Stop();
            }
        }
        private void OnBeginBtnClick()
        {

        }
        private void OnHalllistBtnClick()
        {

        }
        private void OnSngBtnClick()
        {

        }
        private void OnMttBtnClick()
        {

        }
        private void OnTaskBtnClick()
        {

        }
        private void OnRechargeBtnClick()
        {

        }
        private void OnHuodongBtnClick()
        {

        }
        private void OnInteractBtnClick()
        {

        }
        private void OnBillboardBtnClick()
        {

        }
        private void OnFaqBtnClick()
        {

        }
        private void OnSettingBtnClick()
        {

        }
        private void OnGirlClick()
        {
            m_FaceRed.gameObject.SetActive(true);
            m_MouthClose.gameObject.SetActive(true);
            m_MouthHalf.gameObject.SetActive(false);
            m_EyeClose.gameObject.SetActive(true);
            m_EyeHalf.gameObject.SetActive(false);
            Timer timer = new Timer(0.2f);
            timer.Elapsed += GirlFaceHandler;
            timer.Start();
            TimerManager.Instance.AddTimer(timer);
            m_FaceIndex = 1;
            m_GirlEyeTimerRunning = false;
        }
        private void GirlFaceHandler(object sender)
        {
            Timer timer = sender as Timer;
            m_FaceIndex++;
            if(m_FaceIndex == 2)
            {
                m_MouthClose.gameObject.SetActive(false);
                m_MouthHalf.gameObject.SetActive(true);
                m_EyeClose.gameObject.SetActive(false);
                m_EyeHalf.gameObject.SetActive(true);
                timer.Start();
            }
            if(m_FaceIndex == 3)
            {
                m_FaceRed.gameObject.SetActive(false);
                m_MouthClose.gameObject.SetActive(false);
                m_MouthHalf.gameObject.SetActive(false);
                m_EyeClose.gameObject.SetActive(false);
                m_EyeHalf.gameObject.SetActive(false);
                timer.Stop();
                m_GirlEyeTimerRunning = true;
            }
        }
    }
}