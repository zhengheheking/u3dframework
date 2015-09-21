using System.Collections.Generic;
using UnityEngine;
using Socket.Net;
using Client.Base;
namespace Socket
{
    
    public delegate void NetEventHandler(ByteBuffer bytes);
    public class CustomSocket : BaseGameSocket<CustomSocket>
    {
        private object m_Lock = new object();
        private Queue<ByteBuffer> m_RecvPacketList = new Queue<ByteBuffer>();
        private SortedList<int, List<NetEventHandler>> m_NetHandlerMgr = new SortedList<int, List<NetEventHandler>>();
        public void RegisterHandler(int protoId, NetEventHandler handler)
        {
            GetHandler(protoId).Add(handler);
        }
        public void RemoveHandler(int protoId, NetEventHandler handler)
        {
            GetHandler(protoId).Remove(handler);
        }
        public void Destory()
        {
            m_NetHandlerMgr.Clear();
        }
        private List<NetEventHandler> GetHandler(int protoId)
        {
            List<NetEventHandler> handlers = null;
            if (m_NetHandlerMgr.TryGetValue(protoId, out handlers))
            {
                return handlers;
            }
            else
            {
                handlers = new List<NetEventHandler>();
                m_NetHandlerMgr.Add(protoId, handlers);
                return handlers;
            }
        }
        protected override void OnConnect()
        {
        }
        protected override void OnError(string msg)
        {
            Log.Instance.Debug(msg);
        }
        protected override void OnPack(byte[] data)
        {
            ByteBuffer bytes = new ByteBuffer(data);
            lock (m_Lock)
            {
                m_RecvPacketList.Enqueue(bytes);
            }
            //int protoId = bytes.ReadInt();
            //List<NetEventHandler> handlers = GetHandler(protoId);
            //foreach(NetEventHandler handler in handlers)
            //{
            //    handler(bytes);
            //}
        }
        public void Loop()
        {
            lock (m_Lock)
            {
                if (m_RecvPacketList.Count > 0)
                {
                    ByteBuffer bytes = m_RecvPacketList.Dequeue();
                    int protoId = bytes.ReadInt();
                    List<NetEventHandler> handlers = GetHandler(protoId);
                    foreach (NetEventHandler handler in handlers)
                    {
                        handler(bytes);
                    }
                }

            }
        }
        public void SendPakcet(BaseRqst rqst)
        {
            ByteBuffer bytes = new ByteBuffer();
            bytes.WriteInt(rqst.m_Bytes.Length);
            bytes.WriteByteArray(rqst.m_Bytes.ToByteArray());
            Send(bytes.ToByteArray());
        }
    }
}
