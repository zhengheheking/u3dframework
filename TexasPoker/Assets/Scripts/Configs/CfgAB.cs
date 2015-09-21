using Util;
namespace Configs
{
    partial class CfgABMgr : CCfgKeyMgrTemplate<CfgABMgr, int, CfgAB> { };

    partial class CfgAB : ITabItemWithKey<int>
    {
        private static readonly string _KEY_Id = "Id";
        private static readonly string _KEY_ABDepend = "ABDepend";
        private static readonly string _KEY_AB = "AB";

        public int Id { get; private set; }
        public string ABDepend { get; private set; }
        public string AB { get; private set; }

        public CfgAB()
        {
        }

        public int GetKey() { return Id; }

        public bool ReadItem(TabFile tf)
        {
            Id = tf.Get<int>(_KEY_Id);
            ABDepend = tf.Get<string>(_KEY_ABDepend);
            AB = tf.Get<string>(_KEY_AB);
            return true;
        }
    }


}