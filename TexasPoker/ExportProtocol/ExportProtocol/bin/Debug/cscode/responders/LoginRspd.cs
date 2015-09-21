
//============================================
//--4>:
//    Exported by ExportProtocol
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

namespace Responders
{
	using System;
	using UnityEngine;
	using Socket;
	using Socket.Net;

	public class LoginRspd : BaseRspd
	{
		public byte hasRole;
		public LoginRspd(ByteBuffer bytes)
			: base(bytes)
		{}
		public override void ReadData()
		{
			hasRole = m_Bytes.ReadByte();
		}
	}
}
