
//============================================
//--4>:
//    Exported by ExportProtocol
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

package requesters;
import java.nio.ByteBuffer
import comm.BaseRspd;
import utils.JkTools;
import comm.Client;

public class LoginRspd extends BaseRspd {
	private final byte hasRole;
	public LoginRspd(
			byte hasRole
		) {
		super(10000);
		this.hasRole = hasRole;
	}
	@Override
	protected void push(Client cl) {
		super.push(cl);
		bytes.put(hasRole);
	}
}
