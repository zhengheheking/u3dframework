using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

public class Define
{
    public static readonly ConfigDataType[] ALL_DATA_TYPE = new ConfigDataType[] {
            new ConfigDataType("int", "int", "int", typeof(int)),
            new ConfigDataType("uint", "int", "uint", typeof(uint)),
            new ConfigDataType("short", "short", "short", typeof(short)),
            new ConfigDataType("ushort", "short", "ushort", typeof(ushort)),
            new ConfigDataType("word", "int", "ushort", typeof(ushort)),
            new ConfigDataType("byte", "byte", "byte", typeof(byte)),
            new ConfigDataType("char", "char", "char", typeof(char)),
            new ConfigDataType("float", "float", "float", typeof(float)),
            new ConfigDataType("string", "String", "string", typeof(string))

        };
    public static readonly string SPLIT_STRING = "\t";
    public static UTF8Encoding UTF8_EMITBOM = new UTF8Encoding(false);
    public static readonly string SOURCE_FILE_HEAD = @"
//============================================
//--4>:
//    Exported by ExcelConfigExport
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================";
    public static readonly int KEY_INDEX = 0;
    public static readonly string KEY_FUN = "GetKey";


    public static readonly string CSHARP_CLASS_BASE_KEY = "ITabItemWithKey";
    public static readonly string CSHARP_MANAGER_BASE = "XConfigOneKeyManager";
    public static readonly string CSHARP_INCLUDE = "using System;" + Environment.NewLine + "using UnityEngine;";
    public static readonly string CSHARP_KEY_PREFIX = "_KEY_";
    public static readonly string CSHARP_READ_READER = "tf";
    public static readonly string CSHARP_READ_METHOD = "public bool ReadItem(TabFile " + CSHARP_READ_READER + ")";
    public static readonly string TAB_STRING_MAX = "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t";
    public static readonly string MANAGER_CLASS_SUFFIX = "Mgr";

    public static readonly string JAVA_CLASS_BASE_KEY = "ITabItemWithKey";
    public static readonly string JAVA_KEY_PREFIX = "_KEY_";
    public static readonly string JAVA_MANAGER_BASE = "CCfgKeyMgrTemplate";
    public static readonly string JAVA_INCLUDE = "package config;" + Environment.NewLine + "import utils.Tools;" + Environment.NewLine + "import utils.TabFile;";
    public static readonly string JAVA_READ_METHOD = "public boolean ReadItem(TabFile " + CSHARP_READ_READER + ")";
}
public class ExcelItem
{
    public string SheetName;
    public Excel.Worksheet Sheet;
    public Excel.WorkbookClass Workbook;
}
public enum ELogType
{
    INFO,
    DEBUG,
    WARN,
    ERROR,
    FATAL,
}
public enum EConfigHeadType
{
    CodeName = 0,
    DataType,
    Desc,
    Count,
}

public class ConfigDataType
{
    public string ConfigName { get; private set; }
    public string JavaName { get; private set; }
    public string CsName { get; private set; }
    public Type CsType { get; private set; }

    public ConfigDataType(string cfg, string java, string cs, Type t)
    {
        JavaName = java;
        CsName = cs;
        ConfigName = cfg;
        CsType = t;
    }
    public string GetJavaReadName()
    {
        if(JavaName == "int")
        {
            return "Int32";
        }
        else if(JavaName == "short")
        {
            return "Int32";
        }
        else if(JavaName == "byte")
        {
            return "Int32";
        }
        else if(JavaName == "char")
        {
            return "Int32";
        }
        else if(JavaName == "float")
        {
            return "Float";
        }
        else
        {
            return "String";
        }
    }

    public object GetData(object data)
    {
        if (data == null || data.ToString().Length == 0)
        {
            return null;
        }
        try
        {
            return Convert.ChangeType(data, CsType);
        }
        catch (Exception)
        {
            return null;
        }
    }
}

public class ColumnInfo
{
    public string ColDesc { get; private set; }
    public string ConfigName { get; private set; }
    public ConfigDataType DataType { get; private set; }
    public static ColumnInfo CreateColumnInfo(object desc, object name, ConfigDataType dt)
    {
        ColumnInfo info = new ColumnInfo();
        string sName = null == name ? string.Empty : name.ToString().Trim();
        string sDesc = null == desc ? string.Empty : desc.ToString().Trim();

        info.ColDesc = sDesc;
        info.ConfigName = sName;
        info.DataType = dt;
        return info;
    }
}