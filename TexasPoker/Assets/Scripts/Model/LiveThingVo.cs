/**
* ÉúÎïµÄvo
*/
namespace Model.Vo
{
    using Client.Base;

    public class LiveThingVo : EventDispatcher
    {
        public LiveThingVo()
        {
            
        }
        public const string CHANGEHP = "changeHP";
        public const string CHANGEMP = "changeMP";
        public const string CHANGELEV = "changeLev";
        public const string CHANGEMAXHP = "changeMaxHp";
        public const string CHANGEMAXMP = "changeMaxMp";

        public int id;
        public string name;

        private int _level;
        private int _hp;
        private int _maxHp;
        private int _mp;
        private int _maxMp;

        public int Hp
        {
            get
            {
                return _hp;
            }
            set
            {
                if(_hp != value)
                {
                    _hp = value;
                    this.dispatchEvent(CHANGEHP);
                }
            }
        }

        public int MaxHp
        {
            get
            {
                return _maxHp;
            }
            set
            {
                if (_maxHp != value)
                {
                    if (Hp > value)
                    {
                        Hp = value;
                    }
                    _maxHp = value;
                    this.dispatchEvent(CHANGEMAXHP);
                }
            }
        }

        public int Mp
        {
            get
            {
                return _mp;
            }
            set
            {
                if (_mp != value)
                {
                    _mp = value;
                    this.dispatchEvent(CHANGEMP);
                }
            }
        }

        public int MaxMp
        {
            get
            {
                return _maxMp;
            }
            set
            {
                if (_maxMp != value)
                {
                    if (Mp > value)
                    {
                        Mp = value;
                    }
                    _maxMp = value;
                    this.dispatchEvent(CHANGEMAXMP);
                }
            }
        }

        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                if (_level != value)
                {
                    _level = value;
                    this.dispatchEvent(CHANGELEV);
                }
            }
        }
    }
}