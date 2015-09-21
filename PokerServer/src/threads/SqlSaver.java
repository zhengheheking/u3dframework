package threads;

//import gang.GangInviteSql;
//import gang.GangJoinRqstSql;
//import gang.GangLogSql;
//import gang.GangMemberSql;
//import gang.GangSql;
//import gang.GangFlushMissionLogSql;
//import gang.vos.GangTableVo;
//import gang.vos.MemberTableVo;
//import gang.vos.JoinRqstTableVo;
//import gang.vos.LogItemTableVo;
//import gang.vos.FlushMissionLogItemTableVo;
//import gang.vos.GangInviteTableVo;
//import gift.AllServerGiftTableVo;
//import gift.GiftSql;

import java.util.concurrent.LinkedBlockingQueue;


//import mail.MailSql;
//import mail.MailTableVo;
//import passport.PassportSql;
//import passport.PassportTableVo;
//import pitch.PitchSql;
//import pitch.vos.PitchTableVo;
//import pkking.PKKingSql;
//import pkking.vos.PKKingTableVo;
//import specrank.CountrySql;
//import specrank.SpecRankSql;
//import specrank.record.SpecRankRecordSql;
//import specrank.record.vos.SpecRankRecordTableVo;
//import specrank.vos.CountryTableVo;
//import specrank.vos.SpecRankTableVo;
//import userdata.AntiAddictionSql;
//import userdata.game.GameSql;
//import userdata.game.GameTableVo;
//import userdata.rank.WeekRankSql;
//import userdata.rank.WeekRankUserVo;
//import userdata.requesters.PayRmbDRqst;
//import userdata.spread.InviteFriendsTableVo;
//import userdata.spread.SpreadSql;
//import userdata.vos.AntiAddictionTableVo
import utils.LogInfo;
//import arena.ArenaSql;
//import arena.ArenaTableVo;
//import auction.AuctionSql;
//import auction.AuctionTableVo;


import com.mysql.jdbc.exceptions.jdbc4.CommunicationsException;
import com.mysql.jdbc.exceptions.jdbc4.MySQLNonTransientConnectionException;

import comm.Global;
import comm.SqlBase;

public class SqlSaver extends Thread {
    public static LinkedBlockingQueue<Object> savingList;

    public static int AUTO_LOOP_CONNECT_NUM = 200;// 

    public static long AUTO_LOOP_TIME = 3000l;//
    
    private static int emptyCount = 1;
    
    private  final static  int  LOOP_COUNT = 1000;//

    public static boolean saveBatch() {
        Object vo = null;
        try {
            int saveCount = 0;
            for (int i = 0; i < 100; i++) {
                if (savingList.isEmpty()){
                    if(emptyCount % LOOP_COUNT==0){
                        emptyCount=1;
                        //GiftSql.getInstance().queryGift();
                    }else{
                        emptyCount++;
                    }
                    break;
                }

                vo = savingList.peek();
                if (vo == null) {
                    savingList.poll();
                    continue;
                }
                saveCount++;
//                if (vo instanceof PassportTableVo)
//                    PassportSql.instance.saveOne(vo);
//                else if (vo instanceof MailTableVo) {
//                    MailSql.instance.saveOne(vo);
//                } else if (vo instanceof AuctionTableVo) {
//                    AuctionSql.instance.saveOne(vo);
//                } else if (vo instanceof GangTableVo) {
//                    GangSql.instance.saveOne(vo);
//                } else if (vo instanceof MemberTableVo) {
//                    GangMemberSql.instance.saveOne(vo);
//                } else if ( vo instanceof JoinRqstTableVo ) {
//                	GangJoinRqstSql.instance.saveOne(vo);
//                } else if( vo instanceof LogItemTableVo ) {
//                	GangLogSql.instance.saveOne(vo);
//                } else if ( vo instanceof FlushMissionLogItemTableVo ) {
//                	GangFlushMissionLogSql.instance.saveOne(vo);
//                } else if ( vo instanceof GangInviteTableVo ) {
//                	GangInviteSql.instance.saveOne(vo);
//                } else if (vo instanceof ArenaTableVo) {
//                    ArenaSql.instance.saveOne(vo);
//                } else if (vo instanceof WeekRankUserVo) {
//                    WeekRankSql.getInstance().saveOne(vo);
//                }else if (vo instanceof GameTableVo) {
//                    GameSql.getInstance().saveOne(vo);
//                }else if (vo instanceof InviteFriendsTableVo) {
//                    SpreadSql.getInstance().saveOne(vo);
//                }else if (vo instanceof AllServerGiftTableVo) {
//                    GiftSql.getInstance().saveOne(vo);
//                }else if (vo instanceof SpecRankTableVo) {
//                	SpecRankSql.getInstance().saveOne(vo);
//                }else if (vo instanceof SpecRankRecordTableVo) {
//                	SpecRankRecordSql.getInstance().saveOne(vo);
//                }else if (vo instanceof AntiAddictionTableVo) {
//                	AntiAddictionSql.getInstance().saveOne(vo);
//                }else if (vo instanceof CountryTableVo) {
//                	CountrySql.getInstance().saveOne(vo);
//                }else if (vo instanceof PKKingTableVo) {
//                	PKKingSql.getInstance().saveOne(vo);
//                }else if (vo instanceof PitchTableVo) {
//                	PitchSql.instance.saveOne(vo);
//                }
                savingList.poll();
            }
            if (saveCount > 0) {
                SqlBase.conn.commit();
                if (savingList.size() > 5000) {
                    savingList.clear();
                    LogInfo.printError(new Exception("savingList too long"),-24);
                    System.exit(0);

                }
            }

        } catch (MySQLNonTransientConnectionException e) {
            LogInfo.printError(e,-25);
            tryReconnect();

        } catch (CommunicationsException e) {
            LogInfo.printError(e,-26);
            tryReconnect();

        } catch (Exception e) {
            savingList.poll();
            LogInfo.printError(e,-27);
        }
        return !savingList.isEmpty();
    }

    private static void tryReconnect() {
        for (int i = 0; i < AUTO_LOOP_CONNECT_NUM; i++) {
            if (SqlBase.init()) {
                try {
//                    PassportSql.instance.reconnect();
//                    MailSql.instance.reconnect();
//                    AuctionSql.instance.reconnect();
//                    GangMemberSql.instance.reconnect();
//                    GangSql.instance.reconnect();
//                    PayRmbDRqst.reconnect();
//                    ArenaSql.instance.reconnect();
//                    WeekRankSql.getInstance().reconnect();
//                    GameSql.getInstance().reconnect();
//                    SpreadSql.getInstance().reconnect();
//                    GiftSql.getInstance().reconnect();
//                    SpecRankSql.getInstance().reconnect();
//                    SpecRankRecordSql.getInstance().reconnect();
//                    AntiAddictionSql.getInstance().reconnect();
//                    CountrySql.getInstance().reconnect();
//                    PKKingSql.getInstance().reconnect();
//                    PitchSql.instance.reconnect();
                    break;
                } catch (Exception e2) {
                    LogInfo.printError(e2,-28);
                }
            } else {
                if (i == AUTO_LOOP_CONNECT_NUM) {
                    savingList.clear();
                    LogInfo.printError(new Exception("mysql reconnect "),-29);
                    System.exit(0);
                } else {
                    try {
                        Thread.sleep(AUTO_LOOP_TIME);

                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }
            }
        }
    }

    @Override
    public void run() {
        super.run();
        savingList = new LinkedBlockingQueue<Object>();
        while (true) {

            saveBatch();
            if (Global.WillStop) {
                break;
            }
            try {
                Thread.sleep(200);
            } catch (InterruptedException e) {

            }
        }
        LogInfo.info("sqlSaverExit");

    }

}
