
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

	public class CreateRoleRqst : BaseRqst
	{
	public CreateRoleRqst(
			string name,
			byte sex,
			byte job
			)
			: base(10002)
		{
			m_Bytes.WriteString(name);
			m_Bytes.WriteByte(sex);
			m_Bytes.WriteByte(job);
		}
	}
}
