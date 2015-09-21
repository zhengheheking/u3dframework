package utils;

import java.io.File;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.util.Calendar;
import java.util.logging.FileHandler;
import java.util.logging.Logger;
import java.util.logging.SimpleFormatter;

public class LogInfo {
    private static Logger errorLog;

    public static void init() {

        new File("fishingLog").mkdir();
        errorLog = Logger.getLogger("errorLog");
        addFile(errorLog);
    }

    public static void printError(Exception e, int uid) {
        e.printStackTrace();
        StringWriter trace = new StringWriter();
        e.printStackTrace(new PrintWriter(trace));
        errorLog.severe("uid:" + uid + ":::" + trace.toString());
    }

    public static void info(String msg) {
        System.out.println(msg);
    }
    public static void error(String msg) {
        System.out.println(msg);
        errorLog.severe(msg);
    }
    
    public static void debug(String msg) {
    	System.out.println(msg);
    }

    private static void addFile(Logger log) {
        FileHandler h = null;
        try {
            h = new FileHandler("fishingLog/" + log.getName()
                    + "_" + Calendar.getInstance().get(Calendar.DATE) + ".txt", true);
            h.setEncoding("utf-8");
        } catch (SecurityException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }

        h.setFormatter(new SimpleFormatter());
        log.addHandler(h);
    }

}
