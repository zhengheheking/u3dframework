package comm;

import java.nio.ByteBuffer;

public class BaseRqst implements IBytes,ICommand {

	// public int byteSize;
	// public int timeStamp;
	public int cmd;
	public Client cl;

	public void fromBytes(ByteBuffer bytes) {
		bytes.getInt();// 长度
		cmd = bytes.getInt();
	}

	public void toBytes(ByteBuffer bytes) {

	}
	public void excute() {

	}

	public void excute(Client cl) {
		this.cl = cl;
		excute();
	}

}
