
//============================================
//--4>:
//    Exported by ExportProtocol
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================

package requesters;
import java.nio.ByteBuffer
import comm.BaseRqst;
import utils.JkTools;

public class LoginRqst extends BaseRqst {
	private int serverid;
	private int uid;
	public void fromBytes(ByteBuffer bytes) {
		super.fromBytes(bytes);
		serverid = bytes.getInt();
		uid = bytes.getInt();
	}
}
