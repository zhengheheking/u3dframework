package threads;


import java.nio.ByteBuffer;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import user.UserManager;
import user.UserSql;
import utils.JkSet;
import utils.JkTools;
import utils.LogInfo;
import utils.TimerManager;
import utils.Tools;
import comm.Client;
import comm.ClientManager;
import comm.Global;
import comm.RegRqstCmd;
import comm.SqlBase;
import config.Config;

public class GameServer {
    private long sysTime;
    public static long lastRestartSerTime;// 
    public boolean sendLevelPrize = false;
    public boolean sendFightPrize = false;
    public int loopCount = 0;
    public int loopBattleCount = 0;
    public boolean sendBattlePrize = false;// 
    public boolean gangWarBegin = false;//
    public long lastCheckTime = 0;
    public static void main(String[] args) {

        try {
            lastRestartSerTime = System.currentTimeMillis();
            LogInfo.info("Gameserver start");
            LogInfo.init();
            new GameServer();
            LogInfo.info("Gameserver end");
        } catch (Exception e) {
            LogInfo.printError(e, -30);
        } finally {
        }
    }

    private void doShutDownWork() {
        Runtime.getRuntime().addShutdownHook(new Thread() {
            public void run() {
                Global.WillStop = true;
                LogInfo.info("will exit and save all!!!");
                JkSet<Client> ClientList = ClientManager.ClientList;
                //
                // for (int i = 0, len = ClientList.capacity; i < len; i++) {
                // Client c = ClientList.get(i);
                // if (c != null) {
                // new LogoutSceneCmd(c).excute();
                // }
                // }
                LogInfo.info("save user info------!!");

                for (int ci = 0, len = ClientList.capacity; ci < len; ci++) {
                    Client c = ClientList.get(ci);
                    //if (c != null && c.role != null)
                        //PassportSql.instance.pushQueue(c, SqlBase.UPDATE_MODE);
                }
                // 
                //GameSql.getInstance().pushQueue(new GameTableVo(), SqlBase.ADD_MODE);
                while (SqlSaver.saveBatch()) {
                }
                LogInfo.info("save all end");
            }
        });
    }

    private GameServer() {
        try {
            init();
            while (true) {
                if (Global.WillStop) {
                    LogInfo.info(("GameServer break"));
                    break;
                }
                loop();
            }
        } catch (Throwable e) {
            e.printStackTrace();
        } finally {
            LogInfo.info(("GameServer return"));
            System.exit(0);
        }
    }

    private void init() throws SQLException {    	    	  	
        LogInfo.info("init start");
        //lastGangCostDay = Calendar.getInstance().get(Calendar.DAY_OF_MONTH);
        //UserModel.currentCalender = Calendar.getInstance();
        //GameSetModel.WillStop = false;
        //GameSetModel.SaveAllWhenExit = true;
        //GameSetModel.loadConfig();
        Config.load();
        SqlBase.init();
//        ByteBuffer bb = ByteBuffer.allocateDirect(10 * 1024);
//        bb.putInt(11);
//        bb.putInt(20);
//        bb.flip();
//        byte [] content = new byte[bb.limit()];
//        bb.get(content);
//        
//        ByteBuffer bb1 = ByteBuffer.allocateDirect(10 * 1024);
//        bb1.put(content);
//        bb1.flip();
//        int i = bb1.getInt();
//        int j = bb1.getInt();
        //SceneManager.getInstance().loadConfig();
        // 
        //PassportSql.instance.loadAllData();
        ClientManager.Init();
        UserSql.Instance.loadAllData();
        // new SandBox().start();
        new RegRqstCmd().excute();
        new MsgRecevier().start();
        new SqlSaver().start();
        // new LogSender().start();
        sysTime = System.currentTimeMillis();

        doShutDownWork();
        LogInfo.info("server started!!");
        LogInfo.info("init   end");
    }

    private void loop() throws InterruptedException {
        //LogInfo.info("loop start ");
        long startTime = System.currentTimeMillis();
        ClientManager.loop();
        UserManager.getInstance().loop();
        MsgSender.sendAll();
        TimerManager.getInstance().update();
        //LogInfo.info("loop   end ");
        if (System.currentTimeMillis() - sysTime > 11000) {
            sysTime = System.currentTimeMillis();
            sysLoop();
        }
        long dtime = System.currentTimeMillis() - startTime;
        if (dtime < 100) {
            Thread.sleep(100 - dtime);
        }

    }

    /**
     * 
     */
    private void sysLoop() {
        //LogInfo.info("sysLoop start ");

        long ct = System.currentTimeMillis();
        //
        //LogInfo.info("sysLoop   end ");
    }

}
