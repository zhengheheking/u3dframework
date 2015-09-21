package responders;

import utils.JkTools;
import comm.BaseRspd;
import comm.Client;
import comm.ModuleEnum;

public class GameTipRspd extends BaseRspd {
	private final int tipID;
    private final int para1;
    private final int para2;
    private String para3 = null;

    public GameTipRspd(int tipID) {
        super(10201);
        this.tipID = tipID;
        para1 = 0;
        para2 = 0;

    }

    public GameTipRspd(int tipID, int para1, int para2) {
        super(10201);
        this.tipID = tipID;
        this.para1 = para1;
        this.para2 = para2;

    }
    public GameTipRspd(int tipID, int para1, int para2,String para3) {
        super(10201);
        this.tipID = tipID;
        this.para1 = para1;
        this.para2 = para2;
        this.para3 = para3;

    }
    

    @Override
    protected void push(Client cl) {
        super.push(cl);
        bytes.putInt(tipID);
        bytes.putInt(para1);
        bytes.putInt(para2);
        JkTools.writeString(bytes, para3);
    }
}
