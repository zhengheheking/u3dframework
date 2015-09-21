
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

public class CreateRoleRqst extends BaseRqst {
	private String name;
	private byte sex;
	private byte job;
	public void fromBytes(ByteBuffer bytes) {
		super.fromBytes(bytes);
		name = JkTools.readString(bytes);
		sex = bytes.get();
		job = bytes.get();
	}
}
