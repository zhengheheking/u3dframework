package config;

import java.io.File;
import java.io.FileReader;

import com.mysql.jdbc.Util;

import utils.Tools;

public class Config {
	public static void load(){
//		CfgFishMgr.getInstance().Init("Fish.txt");
//		CfgFishTeamMgr.getInstance().Init("FishTeam.txt");
//		CfgFishSpawnMgr.getInstance().Init("FishSpawn.txt");
//		CfgGlobalMgr.getInstance().Init("Global.txt");
//		CfgShowGoldMgr.getInstance().Init("ShowGold.txt");
//		CfgFishSchoolSpawnMgr.getInstance().Init("FishSchoolSpawn.txt");
	}
	public static String ReadFileAsString(String strFileName){
		try{
			String relativelyPath=System.getProperty("user.dir")+"\\config\\"; 
			File file = new File(relativelyPath+strFileName);
	        FileReader reader = new FileReader(file);
	        int fileLen = (int)file.length();
	        char[] chars = new char[fileLen];
	        reader.read(chars);
	        String txt = String.valueOf(chars);
	        return txt;
		}catch(Exception e){
			return "";
		}
	}
}
