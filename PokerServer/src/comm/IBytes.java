package comm;

import java.nio.ByteBuffer;

public interface IBytes {
	void fromBytes(ByteBuffer bytes);

	void toBytes(ByteBuffer bytes);
}
