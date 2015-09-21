package comm;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.SQLException;

import utils.LogInfo;

public abstract class SqlBase {
    public static final byte ADD_MODE = 1, UPDATE_MODE = 2, DELETE_MODE = 3;
    public static Connection conn;
    protected PreparedStatement psRead, psAdd, psUpdate, psDel;// 写入用
//    private int tempID;

    public static boolean init() {
        try {
            closeConn();
            Class.forName("com.mysql.jdbc.Driver");
            conn = DriverManager.getConnection(Global.CONN_GAME, Global.CONN_USER, Global.CONN_PWD);
            conn.setAutoCommit(false);
            LogInfo.info("reset mysql");
        } catch (Exception e) {
            LogInfo.printError(e,-13);
            return false;
        }
        return true;
    }

    public static void closeConn() {
        if (conn != null) {
            try {
                conn.close();
            } catch (Exception e) {
            }
            conn = null;
        }
    }

    public SqlBase() {
//        //处理0服
//        if(GameSetModel.MAIN_SERVER==0){
//            tempID = 7000000;
//        }else{
//            tempID = GameSetModel.SERV_FLAG * GameSetModel.MAIN_SERVER;
//        }
        
    }

//    public void setMixTempID(int id) {
//        if (id / GameSetModel.SERV_FLAG != GameSetModel.MAIN_SERVER) {
//            return;
//        }
//        if (tempID < id)
//            tempID = id;
//    }

    public abstract void loadAllData() throws SQLException;

    public abstract void saveOne(Object object) throws SQLException;

    public abstract void pushQueue(Object item, byte mode);

    public abstract void reconnect() throws SQLException;

//    public void onRoleLogin(Client cl) {
//    }

    public void onRoleLogout(int roleID) {
    }

//    public synchronized int getTempID() {
//        return ++tempID;
//    }
}
