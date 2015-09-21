
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

public class InitGameRspd extends BaseRspd {
	private final short mainMissionID;
	public InitGameRspd(
			short mainMissionID
		) {
		super(10001);
		this.mainMissionID = mainMissionID;
	}
	@Override
	protected void push(Client cl) {
		super.push(cl);
		bytes.putShort(mainMissionID);
	}
}
