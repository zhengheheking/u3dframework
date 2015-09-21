package threads;

import java.io.IOException;
import java.net.InetSocketAddress;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.channels.SocketChannel;

import utils.JkTools;

/**
 * @author Administrator
 * 
 */
public class ScoketTest {

    private static SocketChannel sChannel;
    private static ByteBuffer sendBuffer, recBuffer;

    public ScoketTest(String ip, int port) {
        sendBuffer = ByteBuffer.allocate(1024);
        recBuffer = ByteBuffer.allocate(1024);
        sendBuffer.order(ByteOrder.LITTLE_ENDIAN);
        recBuffer.order(ByteOrder.LITTLE_ENDIAN);
        try {
            sChannel = SocketChannel.open();
            sChannel.connect(new InetSocketAddress(ip, port));
            ByteBuffer buffer = ByteBuffer.wrap( (ip + ":" + port ).getBytes());
            sChannel.write(buffer);
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

    }

    public ScoketTest(String ip, int port, boolean isTest) {
        sendBuffer = ByteBuffer.allocate(1024);
        recBuffer = ByteBuffer.allocate(1024);
        sendBuffer.order(ByteOrder.LITTLE_ENDIAN);
        recBuffer.order(ByteOrder.LITTLE_ENDIAN);
        try {
            sChannel = SocketChannel.open();

            sChannel.connect(new InetSocketAddress(ip, port));
            // 设置读超时
            sChannel.socket().setSoTimeout(500);
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

    }

    /**
     * 查看服务器在线人数
     * 
     */
    public int getOnline() {
        int count = 0;
        try {
            sendBuffer.clear();
            sendBuffer.position(4);
            sendBuffer.put((byte) (12));
            sendBuffer.put((byte) (1));
            sendBuffer.position(10);
            sendBuffer.flip();
            sendBuffer.putInt(sendBuffer.limit() - 4);
            sendBuffer.position(0);
            sChannel.write(sendBuffer);
            recBuffer.clear();
            sChannel.read(recBuffer);
            recBuffer.position(10);
            count = recBuffer.getInt();
            return count;
        } catch (IOException e) {
            e.printStackTrace();
            return count;
        } finally {
            closeChannel();
        }

    }

    public void test() {
        try {
            sendBuffer.clear();
            sendBuffer.position(4);
            sendBuffer.put((byte) (16));
            sendBuffer.put((byte) (1));
            sendBuffer.position(10);

            sendBuffer.flip();
            sendBuffer.putInt(sendBuffer.limit() - 4);
            sendBuffer.position(0);
            sChannel.write(sendBuffer);
            recBuffer.clear();
            sChannel.read(recBuffer);
            recBuffer.position(10);
            System.out.println("gangId=" + recBuffer.getInt());
            System.out.println("gangName=" + JkTools.readString(recBuffer));
            System.out.println("takeTime=" + recBuffer.getLong());
            System.out.println("attackGangId=" + recBuffer.getInt());
            System.out.println("attackGangName=" + JkTools.readString(recBuffer));
            System.out.println("attackDate=" + recBuffer.getLong());
            System.out.println("attackPrice=" + recBuffer.getInt());
            System.out.println("attackPriceDate=" + recBuffer.getLong());
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            closeChannel();
        }

    }

    public void test2(int money) {
        try {
            sendBuffer.clear();
            sendBuffer.position(4);
            sendBuffer.put((byte) (16));
            sendBuffer.put((byte) (2));
            sendBuffer.position(10);
            sendBuffer.putInt(money);
            sendBuffer.flip();
            sendBuffer.putInt(sendBuffer.limit() - 4);
            sendBuffer.position(0);
            sChannel.write(sendBuffer);
            recBuffer.clear();
            sChannel.read(recBuffer);
            recBuffer.position(10);
            System.out.println("gangId=" + recBuffer.getInt());
            System.out.println("gangName=" + JkTools.readString(recBuffer));
            System.out.println("takeTime=" + recBuffer.getLong());
            System.out.println("attackGangId=" + recBuffer.getInt());
            System.out.println("attackGangName=" + JkTools.readString(recBuffer));
            System.out.println("attackDate=" + recBuffer.getLong());
            System.out.println("attackPrice=" + recBuffer.getInt());
            System.out.println("attackPriceDate=" + recBuffer.getLong());
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            closeChannel();
        }

    }

    public void chageQuailty(int tempId, int stoneId) {
        try {
            sendBuffer.clear();
            sendBuffer.position(4);
            sendBuffer.put((byte) (3));
            sendBuffer.put((byte) (33));
            sendBuffer.position(10);
            sendBuffer.putInt(tempId);
            sendBuffer.putInt(stoneId);
            sendBuffer.flip();
            sendBuffer.putInt(sendBuffer.limit() - 4);
            sendBuffer.position(0);
            sChannel.write(sendBuffer);
            recBuffer.clear();
            sChannel.read(recBuffer);
            recBuffer.position(10);
            System.out.println("ret:" + recBuffer.get());
            System.out.println("ID:" + recBuffer.getShort());
            System.out.println("====");
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            closeChannel();
        }

    }

    public void pay(int sid, int uid, int qqPoint) {
        try {
            sendBuffer.clear();
            sendBuffer.position(4);
            sendBuffer.put((byte) (5));
            sendBuffer.put((byte) (15));
            sendBuffer.position(10);
            sendBuffer.putInt(uid);
            sendBuffer.putInt(sid);
            sendBuffer.putInt(qqPoint);
            JkTools.writeString(sendBuffer, "123456");
            JkTools.writeString(sendBuffer, "USD");
            byte[] token = new byte[5];
            sendBuffer.putShort((short) 5);
            sendBuffer.put(token);
            JkTools.writeString(sendBuffer, "4.99");
            JkTools.writeString(sendBuffer, "209");
            JkTools.writeString(sendBuffer, "str1");
            JkTools.writeString(sendBuffer, "str2");
            sendBuffer.flip();
            sendBuffer.putInt(sendBuffer.limit() - 4);
            sendBuffer.position(0);
            sChannel.write(sendBuffer);
            recBuffer.clear();
            sChannel.read(recBuffer);
            recBuffer.position(10);
            System.out.println("ret:" + recBuffer.get());
            System.out.println("level:" + recBuffer.get());
            System.out.println("====");
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            closeChannel();
        }

    }

    public void payNew(int sid, int uid, int qqPoint) {
        try {
            sendBuffer.clear();
            sendBuffer.position(4);
            sendBuffer.put((byte) (5));
            sendBuffer.put((byte) (48));
            sendBuffer.position(10);
            sendBuffer.putInt(uid);
            sendBuffer.putInt(sid);
            sendBuffer.putInt(qqPoint);
            JkTools.writeString(sendBuffer, "123456");
            JkTools.writeString(sendBuffer, "USD");
            byte[] token = new byte[5];
            sendBuffer.putShort((short) 5);
            sendBuffer.put(token);
            JkTools.writeString(sendBuffer, "4.99");
            JkTools.writeString(sendBuffer, "209");
            JkTools.writeString(sendBuffer, "str1");
            JkTools.writeString(sendBuffer, "str2");
            sendBuffer.flip();
            sendBuffer.putInt(sendBuffer.limit() - 4);
            sendBuffer.position(0);
            sChannel.write(sendBuffer);
            recBuffer.clear();
            sChannel.read(recBuffer);
            recBuffer.position(10);
            System.out.println("ret:" + recBuffer.get());
            System.out.println("level:" + recBuffer.get());
            System.out.println("====");
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            closeChannel();
        }

    }

    public void closeChannel() {
        try {
            sChannel.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void pingDonatedRqst(int sid, int uid, byte type) {

        try {
            sendBuffer.clear();
            sendBuffer.position(4);
            sendBuffer.put((byte) (1));
            sendBuffer.put((byte) (1));
            sendBuffer.position(10);

            // short haseLen = bytes.getShort();
            // hashval = new byte[haseLen];
            // bytes.get(hashval);

            // via = JkTools.readString(bytes, 128).split("_");
            // isNewUser = bytes.get() != 0;
            // openId = JkTools.readString(bytes);

            sendBuffer.putInt(1);// serverID
            sendBuffer.putInt(uid);
            sendBuffer.put((byte) 2);// yellowLevel
            sendBuffer.putShort((short) 1);// haseLen
            sendBuffer.put((byte) 1);// hashval

            sendBuffer.flip();
            sendBuffer.putInt(sendBuffer.limit() - 4);
            sendBuffer.position(0);
            sChannel.write(sendBuffer);
            recBuffer.clear();

            sendBuffer.clear();
            sendBuffer.position(4);
            sendBuffer.put((byte) (18));
            sendBuffer.put((byte) (2));
            sendBuffer.position(10);
            sendBuffer.put(type);
            sendBuffer.flip();
            sendBuffer.putInt(sendBuffer.limit() - 4);
            sendBuffer.position(0);
            sChannel.write(sendBuffer);
            recBuffer.clear();
            sChannel.read(recBuffer);
            recBuffer.position(10);

            System.out.println("gangId=" + recBuffer.getInt());
            System.out.println("gangName=" + JkTools.readString(recBuffer));
            System.out.println("takeTime=" + recBuffer.getLong());
            System.out.println("attackGangId=" + recBuffer.getInt());
            System.out.println("attackGangName=" + JkTools.readString(recBuffer));
            System.out.println("attackDate=" + recBuffer.getLong());
            System.out.println("attackPrice=" + recBuffer.getInt());
            System.out.println("attackPriceDate=" + recBuffer.getLong());

        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            closeChannel();
        }

    }

    /**
     * @param args
     */
    public static void main(String[] args) {
        ScoketTest sc = new ScoketTest("192.168.2.75", 8001);
        System.out.println("==="+sc.getOnline());
    }

}
