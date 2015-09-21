package threads;

import java.io.IOException;
import java.net.InetSocketAddress;
import java.net.ServerSocket;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.channels.ServerSocketChannel;
import java.nio.channels.SocketChannel;
import java.util.HashMap;

import utils.LogInfo;
import utils.Tools;
import comm.BaseRqst;
import comm.Client;
import comm.ClientManager;
import comm.Global;
import comm.ModuleEnum;
import responders.GameTipRspd;

public class MsgRecevier extends Thread {
    private static HashMap<Integer, BaseRqst> RqstMap = new HashMap<Integer, BaseRqst>();

    public static void regRspdClass(int cmd, BaseRqst rspd) {
        RqstMap.put(cmd, rspd);
    }

    @Override
    public void run() {
        super.run();
        ByteBuffer bb = ByteBuffer.allocateDirect(10 * 1024);
        bb.order(ByteOrder.BIG_ENDIAN);
        ServerSocketChannel ssc = null;
        try {

            ssc = ServerSocketChannel.open();
            ServerSocket ss = ssc.socket();

            ss.bind(new InetSocketAddress(8088));
            ssc.configureBlocking(false);
        } catch (Exception e) {
            LogInfo.printError(e, -20);
        }

        while (true) {
            if (Global.WillStop)
                break;
            try {
                Thread.sleep(50);
            } catch (InterruptedException e) {
                LogInfo.printError(e, -21);
            }
            if (!ClientManager.ClientList.isFull()) {
                SocketChannel sc = null;
                try {
                    while ((sc = ssc.accept()) != null) {
                        sc.configureBlocking(false);
                        Client c = new Client(sc);
                        ClientManager.ClientList.add(c);

                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            for (int ci = 0; ci < ClientManager.ClientList.capacity; ci++) {
                Client c = ClientManager.ClientList.get(ci);
                if (c == null)
                    continue;
                if (c.isClosed())
                    continue;
                // //异常协议超过10请求，则不能登录
                // if (GameModel.getBackListCount(c.uid) > 10) {
                // c.setClosed(true);
                // continue;
                // }
                if (c.rqstList.size() > 10 * 10) {
                    // new GametipRspd(429).excute(c);
                	LogInfo.info("request size to many :" + c.rqstList.size());
                    c.setClosed(true);
                    continue;

                }
                bb.clear();// 把position设为0，把limit设为capacity，一般在把数据写入Buffer前调用
                int size = -1;
                try {
                    size = c.sock.read(bb);
                    if (size == -1)
                        c.setClosed(true);
                } catch (IOException e) {
                    c.setClosed(true);
                    continue;
                }
                if (size <= 0)
                    continue;
                bb.flip();// 把limit设为当前position，把position设为0，一般在从Buffer读出数据前调用
                //byte [] content = new byte[bb.limit()];
                //bb.get(content);
                //bb.flip();
                int readSize = 0;
                // 返回剩余的可用长度，此长度为实际读取的数据长度，是底层数组的长度
                while (bb.remaining() > 4) {
                    if (c.rqstList.size() > 10 * 10) {
                        LogInfo.info("request size to many :" + c.rqstList.size());
                        // new GametipRspd(429).excute(c);
                        c.setClosed(true);
                        break;
                    }
                    int oneSize = bb.getInt();// 1819242556
                    readSize += oneSize + 4;// 1819242560
                    int readEndPos = bb.position() + oneSize;// 4+1819242556
                    int cmd = bb.getInt();// 10
                    //LogInfo.info("rev:" + c.uid + "," + cmd +"," + size);

                    if (ClientManager.ClientList.isFull()) {
                        new GameTipRspd(649).excute(c);
                        c.setClosed(true);
                        break;
                    }
                    if (readSize > size) {
                        c.setClosed(true);
                        LogInfo.printError(new Exception("error readSize:" + readSize + ",size:" + size + ",rev:"
                                + c.uid + "," + cmd), c.uid);
                        break;
                    }

                    bb.position(bb.position() - 8);
                    BaseRqst rqstInstance = RqstMap.get(cmd);
                    BaseRqst rqst = null;
                    try {
                        if (rqstInstance != null) {
                            rqst = rqstInstance.getClass().newInstance();
                        }
                    } catch (Exception e) {
                        LogInfo.printError(e, c.uid);
                    } finally {
                        try {
                            if (rqst != null) {
                                rqst.cl = c;
                                rqst.fromBytes(bb);
                                c.rqstList.add(rqst);
                            }
                            bb.position(readEndPos);
                        } catch (Exception e) {
                            LogInfo.printError(e, c.uid);
                            c.setClosed(true);
                            break;
                        }
                    }
                }
            }
        }
    }
}
