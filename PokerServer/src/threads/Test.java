package threads;

import java.nio.charset.Charset;
import java.util.Date;


import utils.Tools;

public class Test {
    public static Charset UTF8 = Charset.forName("UTF-8");
    public static void main(String[] args) {
        Date date = Tools.getDateTime(new Date(), -7);
        System.out.println(date.toGMTString()+" == " + date.getTime());
        String str="唐淑敏";
        byte[] bytes = str.getBytes(UTF8);
        

    }

}
