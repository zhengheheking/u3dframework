
//============================================
//--4>:
//    Exported by ExportProtocol
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

namespace Requesters
{
	using System;
	using UnityEngine;
	using Socket;
	using Socket.Net;

	public class LoginRqst : BaseRqst
	{
	public LoginRqst(
			int serverid,
			int uid
			)
			: base(10000)
		{
			m_Bytes.WriteInt(serverid);
			m_Bytes.WriteInt(uid);
		}
	}
}
