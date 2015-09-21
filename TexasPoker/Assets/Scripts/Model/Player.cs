using Client.Base;
using Contr;
using Events;

namespace FishingGame.Model
{
    public class Player : EventDispatcher
    {
        public int m_Id;

        private int m_Money;
        public int Money
        {
            get
            {
                return m_Money;
            }
            set
            {
                m_Money = value;

            }
        }
        private int m_Score;
        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;

            }
        }
        private int m_Gold;
        public int Gold
        {
            get
            {
                return m_Gold;
            }
            set
            {
                m_Gold = value;

            }
        }

        public int SeatId { get; set; }
        public int LockedFishId { get; set; }
        public Player(int id)
        {
            m_Id = id;
            SeatId = 0;
            m_Score = 0;
            m_Money = 0;
            m_Gold = 0;
            LockedFishId = 0;
        }
    }
}