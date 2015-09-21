using System.Collections;
using System;
using System.Collections.Generic;
namespace Util
{


    public class ConfigFile
    {
        private string FileName;
        private SortedList<string, string> m_ContentMgr = new SortedList<string,string>();
        public int CurrentLine { get; private set; }
        private string[] SplitAndRemoveNote(string line)
        {
            string[] ss = line.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
            string[] duans = ss[0].Split(new char[] { '\t' });
            List<string> duanList = new List<string>();
            foreach (string duan in duans)
            {
                if (duan != "")
                {
                    duanList.Add(duan);
                }
            }
            return duanList.ToArray();
        }
        public ConfigFile(string strFileName, string strContent)
        {
            FileName = strFileName;
            string[] content = strContent.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < content.Length; i++)
            {
                //string[] line = content[i].Split(new char[] { '\t' });
                string[] line = SplitAndRemoveNote(content[i]);
                m_ContentMgr.Add(line[0], line[1]);
            }
            
        }


        private string getValueString(string nColIndex)
        {
            string value = null;
            if(m_ContentMgr.TryGetValue(nColIndex, out value))
            {
                return value;
            }
            return string.Empty;
        }

        public string GetString(string strColName)
        {
            return getValueString(strColName);
        }

        public int GetInt32(string strColName)
        {
            try
            {
                return Convert.ToInt32(getValueString(strColName));
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public uint GetUInt32(string strColName)
        {
            try
            {
                return Convert.ToUInt32(getValueString(strColName));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public double GetDouble(string strColName)
        {
            try
            {
                return Convert.ToDouble(getValueString(strColName));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public float GetFloat(string strColName)
        {
            return (float)GetDouble(strColName);
        }

    }
}
