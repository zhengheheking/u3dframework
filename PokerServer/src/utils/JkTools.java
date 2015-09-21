package utils;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.charset.Charset;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Map;

import com.mysql.jdbc.StringUtils;

import comm.IBytes;

public class JkTools {
    public static Charset UTF8 = Charset.forName("UTF-8");

    public static int getRandRange(int[] rates, int baseRate, int step) {
        int r = (int) (Math.random() * baseRate), c = 0;
        for (int i = 0; i < rates.length; i += step) {
            c += rates[i];
            if (r < c)
                return i;
        }
        return -1;
    }

    public static boolean getRandRange(int rate, int max) {
        int r = (int) (Math.random() * max);
        if (r <= rate) {
            return true;
        } else {
            return false;
        }
    }

    public static int getRandRange(int[] rates, int baseRate) {
        return getRandRange(rates, baseRate, 1);
    }

    public static int getRandAverage(int[] rates) {
        return rates[(int) (Math.random() * rates.length)];
    }

    public static int getFac(int dx, int dy) {
        return getFac(dx, dy, 8);
    }

    public static int getFac(int dx, int dy, int dirCount) {
        double rot = Math.atan2(dy, dx);
        if (rot < 0)
            rot += Math.PI * 2;
        return (int) (Math.round(rot * dirCount / (2 * Math.PI)) + dirCount / 4) % dirCount;
    }

    public static long readLong(ByteBuffer bb) {
        long i1 = bb.getInt();
        long i2 = bb.getInt();
        return (long) (i2 * Math.pow(2, 32) + i1);
    }

    public static String readString(ByteBuffer bb) {
        return readString(bb, Short.MAX_VALUE);
    }

    public static String readString(ByteBuffer bb, int sizeMax) {
        if (bb.remaining() < 2)
            return "";
        short len = bb.getShort();
        if (len <= 0) {
            return "";

        }
        if (len > bb.remaining())
            return "";
        if (len > sizeMax)
            len = (short) sizeMax;
        byte[] bytes = new byte[len];
        bb.get(bytes);
        return new String(bytes, UTF8);
    }

    public static void writeMap(ByteBuffer bytes, Map<?, ?> map) {
        if (map == null) {
            bytes.putInt(0);
            return;
        }
        bytes.putInt(map.size());
        for (Map.Entry<?, ?> entry : map.entrySet()) {
            writeValue(bytes, entry.getKey());
            writeValue(bytes, entry.getValue());
        }
    }

    public static void writeValue(ByteBuffer bytes, Object value) {
        if (value instanceof Long) {
            bytes.putLong((Long) value);
        } else if (value instanceof Integer) {
            bytes.putInt((Integer) value);
        } else if (value instanceof String) {
            writeString(bytes, (String) value);
        } else if (value instanceof Short) {
            bytes.putShort((Short) value);
        } else if (value instanceof Byte) {
            bytes.putShort((Byte) value);
        } else if (value instanceof IBytes) {
            ((IBytes) value).toBytes(bytes);
        } else {
            new Exception("不支持的数组").printStackTrace();
        }
    }

    public static void writeString(ByteBuffer bb, String str) {
        if (str != null) {
            byte[] bytes = str.getBytes(UTF8);
            bb.putShort((short) bytes.length);
            bb.put(bytes);
        } else {
            bb.putShort((short) 0);
        }
    }

    public static byte[] obj2Bytes(ByteBuffer buffer, IBytes obj) {
        buffer.position(0);
        obj.toBytes(buffer);
        byte[] bytes = new byte[buffer.position()];
        buffer.position(0);
        buffer.get(bytes);
        return bytes;
    }

    public static int getRange(int value) {
        if (value == 0)
            return 0;
        return value > 0 ? 1 : -1;
    }

    public static ByteBuffer file2Bytebuff(String filename) {
        FileInputStream fs;
        byte[] byteArr = null;

        try {
            fs = new FileInputStream(filename);
            byteArr = new byte[fs.available()];
            fs.read(byteArr);
            fs.close();
        } catch (Exception e) {
            LogInfo.info("filename = " + filename + " not founded!");
            return null;

        }
        ByteBuffer bytes = ByteBuffer.wrap(byteArr);
        bytes.order(ByteOrder.LITTLE_ENDIAN);
        return bytes;
    }

    public static boolean bytebuff2File(ByteBuffer bytes, String filename) {
        FileOutputStream fs;
        try {
            fs = new FileOutputStream(filename);
            fs.write(bytes.array());

            fs.close();
        } catch (FileNotFoundException e) {

            LogInfo.printError(e, -1);
        } catch (IOException e) {
            LogInfo.printError(e, -2);
        }
        return true;
    }

    public static int indexOf(int[] arr, int val, boolean descMode) {
        if (descMode) {
            for (int i = 0; i < arr.length; i++) {
                if (val >= arr[i])
                    return i;
            }
        } else {
            for (int i = 0; i < arr.length; i++) {
                if (val < arr[i])
                    return i;
            }

        }
        return arr.length;
    }

    public static int indexOf(int[] arr, int val) {
        for (int i = 0; i < arr.length; i++) {
            if (val == arr[i])
                return i;
        }
        return -1;
    }

    //
    public static int indexOf(int[] arr, int val, int max) {
        int maxVal = 0;
        for (int i = 0; i < arr.length; i++) {
            maxVal = (arr[i] + max) >= 60 ? 60 : (arr[i] + max);
            if (val == arr[i] || (val >= arr[i] && val < maxVal))
                return i;
        }
        return -1;
    }

    public static byte indexOf(byte[] arr, byte val) {
        for (byte i = 0; i < arr.length; i++) {
            if (val == arr[i])
                return i;
        }
        return -1;
    }

    public static int[][] readArray(ByteBuffer bytes, int len) {
        String st = JkTools.readString(bytes);
        if (st == null || st.equals("")) {
            return null;
        }
        String[] strs = st.split("\\|");

        int[][] retArr = new int[strs.length][len];
        for (int i = 0; i < strs.length; i++) {
            String[] str1s = strs[i].split("\\-");
            for (int j = 0; j < str1s.length; j++) {
                retArr[i][j] = Integer.parseInt(str1s[j]);
            }
        }
        return retArr;
    }

    public static int[][] readArrayNolen(ByteBuffer bytes) {
        String st = JkTools.readString(bytes);
        if (st == null || st.equals("")) {
            return null;
        }
        String[] strs = st.split("\\|");

        int[][] retArr = new int[strs.length][];
        for (int i = 0; i < strs.length; i++) {
            String[] str1s = strs[i].split("\\-");
            retArr[i] = new int[str1s.length];
            for (int j = 0; j < str1s.length; j++) {
                retArr[i][j] = Integer.parseInt(str1s[j]);
            }
        }
        return retArr;
    }

    public static int[] readArray(ByteBuffer bytes) {
        return readArray(readString(bytes));
    }

    public static int[] readArray(String str) {
        return readArray(str, "-|\\|");
    }

    public static int[] readArray(String str, String tag) {
        if (str.isEmpty())
            return null;
        String[] vals = str.split(tag);
        int[] intVals = new int[vals.length];
        for (int i = 0; i < intVals.length; i++) {
            intVals[i] = Integer.parseInt(vals[i]);
        }
        return intVals;
    }

    public static final int COMPARE_TRUE = 0, COMPARE_EQUAL = 1, COMPARE_EQUAL_OR_BIG = 2, COMPARE_EQUAL_OR_LESS = 3;

    public static boolean compare(int a, int b, int mode) {
        switch (mode) {
        case COMPARE_TRUE:
            return true;
        case COMPARE_EQUAL:
            return a == b;
        case COMPARE_EQUAL_OR_BIG:
            return a >= b;
        case COMPARE_EQUAL_OR_LESS:
            return a <= b;

        }
        return false;
    }

    public static void addInMap(Map<Integer, Integer> map, int key, int addValue) {
        Integer v = map.get(key);
        if (v == null)
            v = 0;
        v += addValue;
        map.put(key, v);
    }

    public static int getIndexByCount(int count, int[] sets) {
        int[] rates = new int[4];
        if (count > 1000) {
            count = 1000;
        }
        rates[1] = (sets[0] + (int) (count * sets[1] / 100.00));
        rates[2] = (sets[2] + (int) (count * sets[3] / 100.00));
        rates[3] = (sets[4] + (int) (count * sets[5] / 100.00));
        rates[0] = 10000 - rates[1] - rates[2] - rates[3];
        if (rates[0] < 0) {
            rates[0] = 0;
        }
        return JkTools.getRandRange(rates, 10000);
    }
    
    /**
     * 按照yyyy-MM-dd HH:mm:ss的格式，字符串转日期
     * 
     * @param date
     * @return
     */
    public static Date str2Date(String date) {
        return str2Date(date, "yyyy-MM-dd HH:mm");
    }
    
    public static Date str2Date(String date, String format) {
        if (!StringUtils.isNullOrEmpty(date)) {
            SimpleDateFormat sdf = new SimpleDateFormat(format);
            try {
                return sdf.parse(date);
            } catch (ParseException e) {
                e.printStackTrace();
            }
            return new Date();
        } else {
            return null;
        }
    }

    public static void main(String[] args) {

    }
}
