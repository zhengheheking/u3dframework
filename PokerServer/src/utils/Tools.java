package utils;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Random;

public class Tools {

    public static boolean isSameDay(Calendar tgDt) {
        Calendar dt = Calendar.getInstance();
        return dt.get(Calendar.DAY_OF_YEAR) == tgDt.get(Calendar.DAY_OF_YEAR);
    }

    /**
     * 判断两个时间是否在同一天
     * 
     * */
    public static boolean isSameDay(long time1, long time2) {
        int days = Math.abs(getNotAbsForDays(time1, time2));
        return days == 0;
    }

    /**
     * 计算两个时间是否相隔N天
     * 
     * */
    public static boolean isSameDay(long time1, long time2, int n) {
        int days = Math.abs(getNotAbsForDays(time1, time2));
        return days >= n;
    }

    /**
     * 计算两个时间相隔天数
     * 
     * */
    public static int getDaysForDiffDate(long time1, long time2) {
        long dt1 = getTodayTime(new Date(time1));
        long dt2 = getTodayTime(new Date(time2));
        long days = (dt1 - dt2) / (24 * 3600 * 1000);
        return Math.abs((int) days);
    }

    /**
     * 计算两个毫秒时间相隔天数,
     * 
     * */
    public static int getDaysForDiffTime(long time1, long time2) {
        long days = (time1 - time2) / (24 * 3600 * 1000);
        return Math.abs((int) days);
    }

    /** 计算两个时间相隔天数 前返回正值,后返回负值 */
    public static int getNotAbsForDays(long begin_t, long end_t) {
        int days = 0;
        Calendar _dt1 = Calendar.getInstance();
        _dt1.setTimeInMillis(begin_t);

        Calendar _dt2 = Calendar.getInstance();
        _dt2.setTimeInMillis(end_t);

        Calendar begin = Calendar.getInstance();
        begin.set(Calendar.YEAR, _dt1.get(Calendar.YEAR));
        begin.set(Calendar.MONTH, _dt1.get(Calendar.MONTH));
        begin.set(Calendar.DAY_OF_MONTH, _dt1.get(Calendar.DAY_OF_MONTH));

        Calendar end = Calendar.getInstance();
        end.set(Calendar.YEAR, _dt2.get(Calendar.YEAR));
        end.set(Calendar.MONTH, _dt2.get(Calendar.MONTH));
        end.set(Calendar.DAY_OF_MONTH, _dt2.get(Calendar.DAY_OF_MONTH));

        if (begin_t < end_t) {
            while (begin.before(end)) {
                days++;
                begin.add(Calendar.DAY_OF_YEAR, 1);
            }
        } else {
            while (begin.after(end)) {
                days--;
                end.add(Calendar.DAY_OF_YEAR, 1);
            }
        }
        return (int) days;
    }

    /**
     * 计算两个时间是否相隔多少小时
     * 
     * */
    public static int getHourForDiffDay(long time1, long time2) {
        return Math.abs((int) ((time1 - time2) / (3600 * 1000)));
    }

    /**
     * 按照yyyyMMdd的格式，日期转字符串
     * 
     * @param date
     * @return
     */
    public static String DateToString(Date date) {
        SimpleDateFormat format = new SimpleDateFormat("yyyyMMdd");
        String time = format.format(date.getTime());
        return time;
    }

    public static String Date2String(Date date) {
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
        String time = format.format(date.getTime());
        return time;
    }

    /**
     * 
     * */
    public static String getDate2String(Date date) {
        SimpleDateFormat format = new SimpleDateFormat("MM月dd日HH:mm");
        String time = format.format(date.getTime());
        return time;
    }

    public static String Date2String(Date date, String fmt) {
        SimpleDateFormat format = new SimpleDateFormat(fmt);
        String time = format.format(date.getTime());
        return time;
    }

    /**
     * 获取距离明天0点还有多少毫秒 当前时间加1天或者减一天
     * */
    public static Date getDateTime(Date date, int day) {
        long nowTime = new Date().getTime();
        long strTime = nowTime + (day * 24 * 60 * 60 * 1000);
        return new Date(strTime);
    }

    /**
     * 获取距离某天0点还有多少毫秒 传入时间加1天或者减一天
     * */
    public static Date getInputDateTime(Date date, int day) {
        long inTime = date.getTime();
        long strTime = inTime + (day * 24 * 60 * 60 * 1000);
        return new Date(strTime);
    }

    public static long getTomorrowTime(Date date) {
        if (date != null) {
            long nowTime = new Date().getTime();
            long strTime = nowTime + (24 * 60 * 60 * 1000);
            SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
            String strDate = sdf.format(new Date(strTime));
            return (JkTools.str2Date(strDate + " 00:00:00").getTime() - nowTime);
        } else {
            return 0;
        }
    }

    public static long getTodayTime(Date date) {
        if (date != null) {
            long nowTime = date.getTime();
            SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
            String strDate = sdf.format(new Date(nowTime));
            return (JkTools.str2Date(strDate + " 00:00:00").getTime());
        } else {
            return 0;
        }
    }

    /**
     * 
     * 
     * */
    // public static long getDateTime(int dayOfWeek,int hour,int min){
    // Calendar c = Calendar.getInstance();
    //
    // }

    // 获得当前日期与本周日相差的天数
    public static int getMondayPlus() {
        Calendar cd = Calendar.getInstance();
        // 获得今天是一周的第几天，星期日是第一天，星期二是第二天......
        int dayOfWeek = cd.get(Calendar.DAY_OF_WEEK) - 1; // 因为按中国礼拜一作为第一天所以这里减1
        if (dayOfWeek == 0) {
            return 7;
        } else {
            return dayOfWeek;
        }
    }

    public static boolean isInTime(int[] timeStartDays, int[] timeStartHours, int[] timeStartMins, int lastMinutes) {
        if (lastMinutes <= 0 || timeStartDays == null)
            return false;
        long ct = Calendar.getInstance().getTimeInMillis();
        Calendar openTime = Calendar.getInstance();
        int cutWeek = openTime.get(Calendar.DAY_OF_WEEK);
        for (int i = 0; i < timeStartDays.length; i++) {
            if (timeStartDays.length == 1 && timeStartDays[0] == 0) {
                openTime.set(Calendar.DAY_OF_WEEK, cutWeek);
            } else {
                openTime.set(Calendar.DAY_OF_WEEK, timeStartDays[i]);
            }
            openTime.set(Calendar.HOUR_OF_DAY, timeStartHours[i]);
            openTime.set(Calendar.MINUTE, timeStartMins[i]);
            openTime.set(Calendar.SECOND, 0);
            long startTime = openTime.getTimeInMillis();
            if (ct >= startTime && ct < (startTime + lastMinutes * 60000)) {
                return true;
            }
        }

        return false;

    }

    public static boolean isInActTime(int[] timeStartDays, int[] timeStartHours, int[] timeStartMins, int lastMinutes) {
        if (lastMinutes <= 0 || timeStartDays == null)
            return true;
        long ct = Calendar.getInstance().getTimeInMillis();
        Calendar openTime = Calendar.getInstance();
        int cutWeek = openTime.get(Calendar.DAY_OF_WEEK);
        for (int i = 0; i < timeStartDays.length; i++) {
            if (timeStartDays.length == 1 && timeStartDays[0] == 0) {
                openTime.set(Calendar.DAY_OF_WEEK, cutWeek);
            } else {
                openTime.set(Calendar.DAY_OF_WEEK, timeStartDays[i]);
            }
            openTime.set(Calendar.HOUR_OF_DAY, timeStartHours[i]);
            openTime.set(Calendar.MINUTE, timeStartMins[i]);
            openTime.set(Calendar.SECOND, 0);
            long startTime = openTime.getTimeInMillis();
            if (ct >= startTime && ct < (startTime + lastMinutes * 60000)) {
                return true;
            }
        }

        return false;

    }

    /**
     * 一个int 存放多个信息
     * */
    public static int setIntInfo(int count, int quality, int bingo) {
        int val = 100000 + count * 100 + quality * 10 + bingo * 1;
        return val;
    }

    /**
     * 读取int 存放的多个信息
     * */
    public static int[] getIntInfo(int val) {
        int[] ret = new int[3];
        ret[0] = (val - 100000) / 100;// 数量
        ret[1] = (val - 100000 - ret[0] * 100) / 10;// 品质
        ret[2] = val - 100000 - ret[0] * 100 - ret[1] * 10;// 是否中奖
        return ret;
    }
    /**
     * 根据等级取出数组中的最大值
     * 数组必须是从小到大的排序,如10-10，50-20，1010-30
     * */
    public static int getArrayVal(int[][] arrs, int level) {
        int ret = -1;
        for (int[] arr : arrs) {
            if (arr[0] > level) {
                break;
            } else {
                ret = arr[1];
            }
        }
        return ret;
    }

    public static void main(String[] args) {
        long t1 = System.currentTimeMillis();
        long t2 = t1 - 8 * 24 * 3600 * 1000l + 1000;
        System.out.println("now:" + new Date(t1));
        System.out.println("now:" + new Date(t2));
        System.out.println(Tools.getDaysForDiffTime(t1, t2));
    }
    public static int randomRange(int min, int max){
    	if (min == max) {
			return min;
		}
    	Random random = new Random();

        int s = random.nextInt(max)%(max-min+1) + min;
        return s;
    }
    public static float randomRange(float min, float max){
    	Random random = new Random();
    	return random.nextFloat()*(max-min)+min;
    }
    public static float rand() {
    	Random random = new Random();
    	return random.nextFloat();
	}
    public static int rand(List<Integer> list, int index){
    	while(true){
    		int i = randomRange(1, list.size());
    		if (list.get(i-1) != index) {
				return list.get(i-1);
			}
    	}
    }
    public static List<Integer> randomIndexNum(Vector2[] array, int num){
    	List<Integer> list = new ArrayList<Integer>();
    	while(list.size() < num){
    		int index = randomRange(1, array.length);
    		boolean found = false;
    		for (int i = 0; i < list.size(); i++) {
				if (list.get(i) == (index-1)) {
					found = true;
					break;
				}
			}
    		if (!found) {
    			list.add(index-1);
			}
    	}
    	return list;
    }
    public static String[] string2StringArray(String s){
    	String[] strs = s.split("\\|");
    	return strs;
    }
    public static Vector2[] string2Vector2Array(String s){
    	String[] strs = s.split("\\|");
    	Vector2[] array = new Vector2[strs.length];
   	 	for(int i=0; i<strs.length; i++){
   	 		String[] ss = strs[i].split(",");
   	 		Vector2 vec = new Vector2(Float.parseFloat(ss[0]), Float.parseFloat(ss[1]));
   	 		array[i] = vec;
   	 	}
   	 	return array;
    }
    public static Vector2 string2Vector2(String s){
    	String[] strs = s.split(",");
   	 	return new Vector2(Float.parseFloat(strs[0]), Float.parseFloat(strs[1]));
    }
    public static int string2Int(String s){
    	return Integer.parseInt(s);
    }
    public static float string2Float(String s){
    	return Float.parseFloat(s);
    }
}
