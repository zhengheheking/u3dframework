package comm;

import java.nio.ByteBuffer;

import java.nio.ByteOrder;

//import scene.commands.LogoutSceneCmd;
import utils.LogInfo;

public class BaseRspd {

	public ByteBuffer bytes;
	public int cmd;
	private int buffStart;
	// ByteBuffer bb;
	public BaseRspd(int cmd) {
		this.cmd = cmd;
		//bb = ByteBuffer.allocate(1024 * 10);
		//bb.order(ByteOrder.BIG_ENDIAN);
	}

	protected void push(Client cl) {
		bytes = cl.sendBuffer;
		buffStart = bytes.position();
		bytes.position(buffStart + 4);// len head
		bytes.putInt(cmd);
		//bb.position(4);
		//bb.putInt(cmd);
	}

	private void pushEnd() {
		int pos = bytes.position();
		bytes.position(buffStart);
		bytes.putInt(pos - buffStart - 4);// len head
		bytes.position(pos);
		//int p = bb.position();
		//bb.position(0);
		//.putInt(p-4);
		//bb.position(p);
		//.flip();
		// [] content = new byte[bb.limit()];
        //bb.get(content);
	}

	final public void excute(Client cl) {
		try {
			if (cl.isClosed() == true)
				return;
			push(cl);
			pushEnd();
		} catch (Exception e) {
		    //new LogoutSceneCmd(cl).excute();
			LogInfo.printError(e, cl.uid);
		}

	}
}
