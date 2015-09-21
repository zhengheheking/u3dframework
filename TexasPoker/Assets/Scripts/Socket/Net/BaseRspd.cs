namespace Socket.Net
{
    public class BaseRspd
    {
        protected ByteBuffer m_Bytes;
        public BaseRspd(ByteBuffer bytes)
        {
            m_Bytes = bytes;
            ReadData();
        }
        public virtual void ReadData()
        {
        }
    }
}
