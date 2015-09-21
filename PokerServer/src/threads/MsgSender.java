package threads;

import java.io.IOException;
import java.nio.ByteBuffer;

import user.commands.LogoutCmd;
//import scene.commands.LogoutSceneCmd;
import utils.JkSet;
import utils.LogInfo;
import comm.Client;
import comm.ClientManager;

public class MsgSender {

    private static final int ONCE_SIZE_MAX = 1024 * 6;//
    public static void sendAll() {
        JkSet<Client> ClientList = ClientManager.ClientList;
        for (int ci = 0, len = ClientList.capacity; ci < len; ci++) {
            Client c = ClientList.get(ci);
            if (c == null)
                continue;
            ByteBuffer bbuffer = c.sendBuffer;

            int allsize = bbuffer.position();
            if (c.isClosed() && allsize == 0) {
                new LogoutCmd(c).excute();
                ClientList.remove(ci);
                continue;
            }
            try {
                bbuffer.position(0);
                bbuffer.limit(ONCE_SIZE_MAX < allsize ? ONCE_SIZE_MAX : allsize);
                c.sock.write(bbuffer);
                bbuffer.limit(allsize);
                bbuffer.compact();

            } catch (IOException e) {
            	LogInfo.info(e.toString());
                c.setClosed(true);
            } finally {
                // 
                if (c.isClosed()) {
                    bbuffer.position(0);
                }
            }
        }

    }

}
