package utils;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class TabFile {
	private String FileName;
	private String[] Title;
	private ArrayList Body;
	public int CurrentLine;
	private String[] splitAndRemoveNote(String line)
    {
		String[] ss = line.split("//");
		String[] duans = ss[0].split("\t");
        List<String> duanList = new ArrayList<String>();
        for (String duan : duans)
        {
            if (duan != "")
            {
                duanList.add(duan);
            }
        }
        return duanList.toArray(new String[duanList.size()]);
    }
	public TabFile(String strFileName, String strContent){
		FileName = strFileName;
		String[] content = strContent.split("\r\n");
		//Title = content[0].split("\t");
		Title = splitAndRemoveNote(content[0]);
		Body = new ArrayList();
		for(int i=1; i<content.length; i++){
			//String[] line = content[i].split("\t");
			String[] line = splitAndRemoveNote(content[i]);
			Body.add(line);
		}
		Body.trimToSize();
		Begin();
	}
	public void Begin(){
		CurrentLine = -1;
	}
	public boolean Next(){
		CurrentLine++;
		if(CurrentLine >= Body.size()){
			return false;
		}
		return true;
	}
	
	private String getValueString(int nColIndex){
		String[] line = (String[])Body.get(CurrentLine);
		if(nColIndex < 0 || nColIndex >= line.length){
			return null;
		}
		return line[nColIndex];
	}
	public String GetString(String strColName){
		int nColIndex = Arrays.asList(Title).indexOf(strColName);
		return getValueString(nColIndex);
	}
	public int GetInt32(String strColName){
		int nColIndex = Arrays.asList(Title).indexOf(strColName);
		return GetInt32(nColIndex);
	}
	public int GetInt32(int nColIndex){
		try{
			return Integer.parseInt(getValueString(nColIndex));
		}catch(Exception e){
			return 0;
		}
	}
	public int GetUInt32(String strColName){
		int nColIndex = Arrays.asList(Title).indexOf(strColName);
		return GetUInt32(nColIndex);
	}
	public int GetUInt32(int nColIndex){
		try{
			return Integer.parseUnsignedInt(getValueString(nColIndex));
		}catch(Exception e){
			return 0;
		}
	}
	public double GetDouble(int nColIndex){
		try{
			return Double.parseDouble(getValueString(nColIndex));
		}catch(Exception e){
			return 0;
		}
	}
	public double GetDouble(String strColName){
		int nColIndex = Arrays.asList(Title).indexOf(strColName);
		return GetDouble(nColIndex);
	}
	public float GetFloat(String strColName){
		return (float)GetDouble(strColName);
	}
	public float GetFloat(int nColIndex){
		return (float)GetDouble(nColIndex);
	}
}
