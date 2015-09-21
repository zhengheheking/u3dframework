package comm;


import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.channels.SocketChannel;
import java.util.concurrent.LinkedBlockingQueue;

import user.User;
import utils.LogInfo;

public class Client {
    public SocketChannel sock;
    public ByteBuffer sendBuffer;
    public boolean hasRole;
    //public SceneUser role;
    public User m_User;
    public int uid;//
    public long lastScDataTim;
    private boolean isClosed;
    public int m_Id;
    public LinkedBlockingQueue<BaseRqst> rqstList = new LinkedBlockingQueue<BaseRqst>();
    // public ArrayList<BaseRspd> rspdList =new ArrayList<BaseRspd>();
    public int serverID;
    public boolean isGm;
    public String ip;
    public long lastSaveTime;
    public long lastFanTime;
    public long lastFanTipTime;
    public long lastFanHourTime;
    public String[] via;
    public boolean isNewUser;
    public String openId;
    public String openKey;
    public String pf;
    public String pfkey;
    public boolean isTest = false;
    public boolean isInitRoleInfo = false;

    public Client(SocketChannel sock) {
        this.sock = sock;
        //model = new UserModel();
        // sendBuffer=ByteBuffer.allocate(4024);//
        // 500k
        sendBuffer = ByteBuffer.allocate(1024 * 200);// 
                                                     // 
        sendBuffer.order(ByteOrder.BIG_ENDIAN);
        setClosed(false);
        hasRole = true;//
//        lastScDataTim = SceneManager.loopTime;
//        isGm = false;
//        ip = sock.socket().getInetAddress().getHostAddress();
//        lastSaveTime = SceneManager.loopTime;
//        lastFanTime = SceneManager.loopTime;
//        lastFanTipTime = SceneManager.loopTime;
//        lastFanHourTime = SceneManager.loopTime;
    }

    public void loop() {

        BaseRqst rqst = rqstList.poll();
        if (rqst != null) {
            try {
                // 
//                if (role != null && role.isDead()) {
//                    if (!(rqst.cmd == ModuleEnum.SCENE && rqst.para == SceneRqstEnum.REVIVAL)
//                            && !(rqst.cmd == ModuleEnum.PASSPORT && rqst.para == PassportRqstEnum.HEART)
//                            && !(rqst.cmd == ModuleEnum.GANG_WAR && rqst.para == GangWarRqstEnum.REVIVAL)) {
//                        rqst = null;
//                    }
//                }
                if (rqst != null) {
                    //LogInfo.debug("uid:" + uid + "cmd:" + rqst.cmd);
                    rqst.excute();
                }

            } catch (Exception e) {
                LogInfo.printError(e, uid);

            }

        }
//        // 
//        if (SceneManager.loopTime - lastSaveTime > 30000) {
//            model.onlineTime += 30;
//            lastSaveTime = SceneManager.loopTime;
//            PassportSql.instance.pushQueue(this, SqlBase.UPDATE_MODE);
//        }
//
//        if (SceneManager.loopTime - lastFanTime > 120000) {
//            lastFanTime = SceneManager.loopTime;
//            if (isClosed() == false && UserDataUtils.isFangChenMi(this, lastFanTime, false, true)) {
//                setClosed(true);
//            }
//        }
//        if (SceneManager.loopTime - lastFanTipTime > 50000) {
//            lastFanTipTime = SceneManager.loopTime;
//            if (isClosed() == false && UserDataUtils.isFangChenMi(this, lastFanTipTime, true, false)) {
//                setClosed(true);
//            }
//        }
//        if (SceneManager.loopTime - lastFanHourTime > 3600000) {
//            lastFanHourTime = SceneManager.loopTime;
//            if (isClosed() == false) {
//                UserDataUtils.fangChenMiShowHour(this);
//            }
//        }
//        if (isClosed() == false && SceneManager.loopTime - lastScDataTim > 50000 && isTest == false) {
//            setClosed(true);
//        }

    }

    public boolean isClosed() {
        return isClosed;
    }

    public void setClosed(boolean isClosed) {
        this.isClosed = isClosed;
    }
}
