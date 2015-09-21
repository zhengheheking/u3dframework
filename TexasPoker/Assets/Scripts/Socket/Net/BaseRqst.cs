namespace Socket.Net
{
    public class BaseRqst
    {
        public ByteBuffer m_Bytes;
        public BaseRqst(int protoId)
        {
            m_Bytes = new ByteBuffer();
            m_Bytes.WriteInt(protoId);
        }
    }
}
