package user.commands;

import java.io.IOException;

import user.UserManager;
import comm.Client;
import comm.ICommand;

public class LogoutCmd implements ICommand {

	private Client cl;

    public LogoutCmd(Client cl) {
        this.cl = cl;
    }


    public void excute() {
        try {
            cl.sock.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
        UserManager.getInstance().onLogout(cl);
    }

}
