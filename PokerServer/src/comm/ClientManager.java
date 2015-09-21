package comm;

import java.util.HashSet;
import java.util.Set;

import utils.JkSet;
import utils.LogInfo;
import utils.Tools;

public class ClientManager {
    public static JkSet<Client> ClientList;
    public static long max = 2l << 31;
    public static Set<Long> set = new HashSet<Long>();//
    public static void Init(){
    	ClientList = new JkSet<Client>( 10000);
    }
    public static void loop() {

        set.clear();
        for (int ci = ClientList.capacity - 1; ci >= 0; ci--) {
            Client c = ClientList.get(ci);
            if (c == null)
                continue;
            //
            if(c.uid!=0){//
             // 
                if (set.contains(c.serverID * max + c.uid)) {
                    LogInfo.info("uid>===>>>:" + c.uid);
                    //new LogoutSceneCmd(c).excute();
                    continue;
                } else {
                    set.add(c.serverID * max + c.uid);
                }
            }
            // 
            try {
                c.loop();
            } catch (Exception e) {
                LogInfo.printError(e, c.uid);
            }
        }

    }

}
