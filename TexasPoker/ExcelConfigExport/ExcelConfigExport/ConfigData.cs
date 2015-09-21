using ExcelConfigExport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConfigData
{
    private ColumnInfo[] AllColumnInfo;
    private List<object[]> LoadedData;
    public static ConfigData CreateConfigData(object[,] head)
    {
        ConfigData cfgData = new ConfigData();
        if (null == head || cfgData.Init(head) == false)
        {
            return null;
        }
        return cfgData;
    }
    private ConfigData()
    {
        AllColumnInfo = null;
        LoadedData = new List<object[]>();
    }
    public static Hashtable DataTypeMap { get; private set; }
    static ConfigData()
    {
        DataTypeMap = new Hashtable();
        foreach (ConfigDataType dt in Define.ALL_DATA_TYPE)
        {
            DataTypeMap.Add(dt.ConfigName, dt);
        }

    }
    public void ClearData()
    {
        LoadedData.Clear();
    }
    private static ConfigDataType GetDataType(object s)
    {
        if (null == s)
        {
            return null;
        }
        if (DataTypeMap.Contains(s.ToString().Trim()))
        {
            return DataTypeMap[s] as ConfigDataType;
        }
        return null;
    }

    private bool SetColumnInfo(int col, object desc, object name, object dt)
    {

        ConfigDataType dataType = GetDataType(dt);
        if (null == dataType)
        {
            Form1.Instance.Log(ELogType.ERROR, "数据表的数据类型定义不正确, 列:{0}, 列名:{1}, 数据类型:{2}, 描述:{3}", col, name, dt, desc);
            return false;
        }
        ColumnInfo info = ColumnInfo.CreateColumnInfo(desc, name, dataType);
        if (null == info)
        {
            Form1.Instance.Log(ELogType.ERROR, "数据表的列名(变量名)不符合要求, 列:{0}, 列名:{1}, 描述:{2}", col, name, desc);
            return false;
        }
        AllColumnInfo[col] = info;
        return true;
    }
    private bool Init(object[,] head)
    {
        if (null == head)
        {
            Form1.Instance.Log(ELogType.ERROR, "数据表表头为空");
            return false;
        }
        if (head.GetLength(0) <= (int)EConfigHeadType.Count)
        {
            Form1.Instance.Log(ELogType.ERROR, "数据表表头行数不正确, 需求行数:{0}, 实际行数:{1}", (int)EConfigHeadType.Count, head.GetLength(0));
            return false;
        }

        AllColumnInfo = new ColumnInfo[head.GetLength(1)];
        int rowBase = head.GetLowerBound(0);
        int colBase = head.GetLowerBound(1);

        for (int col = 0; col < AllColumnInfo.Length; ++col)
        {
            if (SetColumnInfo(col,
                head[rowBase + (int)EConfigHeadType.Desc, col + colBase],
                head[rowBase + (int)EConfigHeadType.CodeName, col + colBase],
                head[rowBase + (int)EConfigHeadType.DataType, col + colBase]) == false)
            {
                Form1.Instance.Log(ELogType.ERROR, "获取数据表头信息失败, 列:{0}", col);
                return false;
            }
        }
        
        return true;
    }
    public bool AddMultiLine(object[,] multiLine, int addRowBegin)
    {
        if (null == multiLine)
        {
            Form1.Instance.Log(ELogType.ERROR, "加载数据行错误, 空行, ");
            return false;
        }

        Form1.Instance.Log(ELogType.INFO, "--4> 开始加载数据");

        if (addRowBegin < multiLine.GetLowerBound(0))
        {
            Form1.Instance.Log(ELogType.ERROR, "读取数据失败, 数据下标范围错误");
            return false;
        }
        if (multiLine.GetLength(1) != AllColumnInfo.Length)
        {
            Form1.Instance.Log(ELogType.ERROR, "加载数据行错误, 数据项和表的列数不一致, 需求列数:{0}, 实际列数:{1}", AllColumnInfo.Length, multiLine.GetLength(1));
            return false;
        }
        int addRowLast = multiLine.GetUpperBound(0);
        int colBase = multiLine.GetLowerBound(1);

        List<object[]> allDataLine = new List<object[]>();
        for (int row = addRowBegin; row <= addRowLast; ++row)
        {
            object[] dataLine = new object[AllColumnInfo.Length];
            for (int col = 0; col < AllColumnInfo.Length; ++col)
            {
                dataLine[col] = GetColumnData(col, multiLine[row, col + colBase]);
                if (null == dataLine[col])
                {
                    Form1.Instance.Log(ELogType.ERROR, "加载第 {0} 行数据失败", row);
                    return false;
                }
            }
            allDataLine.Add(dataLine);
        }
        LoadedData.AddRange(allDataLine);
        Form1.Instance.Log(ELogType.INFO, "成功加载 {0} 行数据, 加载的有效数据总行数:{1}", allDataLine.Count, LoadedData.Count);

        Form1.Instance.Log(ELogType.INFO, "--4> 成功加载数据");
        return true;
    }
    private object GetColumnData(int col, object data)
    {
        if (col < 0 || col >= AllColumnInfo.Length)
        {
            return null;
        }
        ColumnInfo info = AllColumnInfo[col];
        if (null == info)
        {
            Form1.Instance.Log(ELogType.ERROR, "加载数据行错误, 列定义未加载,  列:{0}", col);
            return null;
        }
        object ret = info.DataType.GetData(data);
        if (null == ret)
        {
            Form1.Instance.Log(ELogType.ERROR, "加载数据行错误, 数据值和列定义不相符, 列:{0} 列名:{1} 数据值:{2}", col, info.ConfigName, null != data ? data.ToString() : string.Empty);
            return null;
        }
        return ret;
    }
    private List<int> GetExportColumnList()
    {
        List<int> exColList = new List<int>();
        for (int col = 0; col < AllColumnInfo.Length; ++col)
        {
            exColList.Add(col);
        }
        return exColList;
    }
    private bool CreateFile(string sFileName)
    {
        if (null == sFileName || sFileName == string.Empty)
        {
            Form1.Instance.Log(ELogType.ERROR, "创建文件失败, 文件名为空");
            return false;
        }
        try
        {
            if (File.Exists(sFileName))
            {
                if (FileAttributes.ReadOnly == (File.GetAttributes(sFileName) & FileAttributes.ReadOnly))
                {
                    Form1.Instance.Log(ELogType.ERROR, "文件:{0} 是只读的", sFileName);
                    return false;
                }
                else
                {
                    File.Delete(sFileName);
                }
            }
            using (FileStream fs = File.Create(sFileName))
            {
                fs.Close();
            }
        }
        catch (System.Exception ex)
        {
            Form1.Instance.Log(ELogType.ERROR, "创建文件失败, 文件:{0}, 错误:{1}", sFileName, ex.ToString());
            return false;
        }
        return true;
    }
    public bool ExportConfigFile(string sFileName)
    {
        Form1.Instance.Log(ELogType.INFO, "--4> 开始导出文件名:{0}", sFileName);

        List<int> exColList = GetExportColumnList();
        if (exColList.Count > 0)
        {
            if (CreateFile(sFileName) == false)
            {
                return false;
            }
            try
            {
                using (StreamWriter fs = new StreamWriter(sFileName, false, Define.UTF8_EMITBOM))
                {

                    string oLine = string.Empty;

                    // 写表头
                    for (int i = 0; i < exColList.Count; ++i)
                    {
                        if (i > 0)
                        {
                            oLine += Define.SPLIT_STRING;
                        }
                        oLine += AllColumnInfo[exColList[i]].ConfigName;
                    }
                    fs.WriteLine(oLine);

                    // 写数据
                    foreach (object[] line in LoadedData)
                    {
                        //--4>TODO: 可以改成 string.Join() 来实现
                        oLine = string.Empty;
                        for (int i = 0; i < exColList.Count; ++i)
                        {
                            if (i > 0)
                            {
                                oLine += Define.SPLIT_STRING;
                            }
                            oLine += line[exColList[i]].ToString();
                        }
                        fs.WriteLine(oLine);
                    }
                    fs.Close();
                }
            }
            catch (System.Exception ex)
            {
                Form1.Instance.Log(ELogType.ERROR, "导出 配置文件失败, 文件名:{0}, 错误:{1}", sFileName, ex.ToString());
                return false;
            }
        }
        Form1.Instance.Log(ELogType.INFO, "--4> 成功导出 文件名:{0}, 数据行数:{1}", sFileName, LoadedData.Count);
        return true;
    }
    private void WriteCode(StreamWriter fs, int tabNum, string format, params object[] args)
    {
        if (tabNum < 0 || tabNum > Define.TAB_STRING_MAX.Length)
        {
            tabNum = 0;
        }
        if (null == args)
        {
            fs.WriteLine(Define.TAB_STRING_MAX.Substring(0, tabNum) + format);
        }
        else
        {
            fs.WriteLine(Define.TAB_STRING_MAX.Substring(0, tabNum) + string.Format(format, args));
        }
    }
    private void WriteCsMgrDef(StreamWriter fs, string sClassName)
    {
        //string sClassName = cfgSrc.ClassName;
        string sMgrName = sClassName + Define.MANAGER_CLASS_SUFFIX;
        WriteCode(fs, 0, "partial class {0} : {1}<{0}, {2}, {3}> {{ }};",
                   sMgrName, "CCfgKeyMgrTemplate", AllColumnInfo[Define.KEY_INDEX].DataType.CsName, sClassName);
        fs.WriteLine();
    }
    private void WriteCsClassDef(StreamWriter fs, string sClassName)
    {
        WriteCode(fs, 0, "partial class {0} : {1}<{2}>",
                    sClassName, Define.CSHARP_CLASS_BASE_KEY,
                    AllColumnInfo[Define.KEY_INDEX].DataType.CsName);
    }
    private void WriteJavaClassDef(StreamWriter fs, string sClassName)
    {
        WriteCode(fs, 0, "public class {0} extends {1}",
                    sClassName, Define.JAVA_CLASS_BASE_KEY);
    }
    private void WriteJavaMgrClassDef(StreamWriter fs, string sClassName)
    {
        WriteCode(fs, 0, "public class {0}Mgr extends {1}{{",
                    sClassName, Define.JAVA_MANAGER_BASE);
        WriteCode(fs, 1, "public {0}Mgr()",
                    sClassName);
        WriteCode(fs, 1, "{", null);

        WriteCode(fs, 1, "}", null);
        WriteCode(fs, 1, "private static {0}Mgr instance = new {1}Mgr();",
                    sClassName, sClassName);
        WriteCode(fs, 1, "public static {0}Mgr getInstance()",
                    sClassName);
        WriteCode(fs, 1, "{", null);
        WriteCode(fs, 2, "return instance; ", null);
        WriteCode(fs, 1, "}", null);
    }
    private void WriteJavaKey(StreamWriter fs, string sClassName)
    {
        WriteCode(fs, 1, "protected ITabItemWithKey NewItem(){{\n\t\treturn new {0} ();\n\t }}",
                    sClassName);
    }
    private void WriteJavaGet(StreamWriter fs, string sClassName)
    {
        WriteCode(fs, 1, "public {0} GetConfig(int key){{\n\t\treturn ({1})super.GetConfig(key);\n\t}}",
                    sClassName, sClassName);
    }
    private void WriteCsKeyInterface(StreamWriter fs)
    {
        WriteCode(fs, 1, "public {0} {1}() {{ return {2}; }}",
                    AllColumnInfo[Define.KEY_INDEX].DataType.CsName,
                    Define.KEY_FUN, AllColumnInfo[Define.KEY_INDEX].ConfigName);
        fs.WriteLine();
    }
    private void WriteJavaKeyInterface(StreamWriter fs)
    {
        WriteCode(fs, 1, "public {0} {1}() {{ return {2}; }}",
                    AllColumnInfo[Define.KEY_INDEX].DataType.JavaName,
                    Define.KEY_FUN, AllColumnInfo[Define.KEY_INDEX].ConfigName);
        fs.WriteLine();
    }
    public bool ExportJavaSource(string sClassName)
    {
        string sFileName = Environment.CurrentDirectory.ToString() + "\\JavaCode\\" + sClassName + ".java";
        if (CreateFile(sFileName) == false)
        {
            return false;
        }
        try
        {
            using (StreamWriter fs = new StreamWriter(sFileName, false, Define.UTF8_EMITBOM))
            {
                List<int> exColList = GetExportColumnList();

                // 头部数据
                WriteCode(fs, 0, Define.SOURCE_FILE_HEAD, null);
                WriteCode(fs, 0, Define.JAVA_INCLUDE, null);
                fs.WriteLine();
                WriteJavaClassDef(fs, sClassName);
                WriteCode(fs, 0, "{", null);

                // 导出列的字符串索引
                foreach (int col in exColList)
                {
                    ColumnInfo info = AllColumnInfo[col];
                    WriteCode(fs, 1, "private static final String {0}{1} = \"{2}\";",
                        Define.JAVA_KEY_PREFIX, info.ConfigName, info.ConfigName);
                }
                fs.WriteLine();
                foreach (int col in exColList)
                {
                    ColumnInfo info = AllColumnInfo[col];
                    WriteCode(fs, 1, "public {0}\t{1};\t\t\t\t// {2}", info.DataType.JavaName, info.ConfigName, info.ColDesc);
                }
                fs.WriteLine();
                WriteCode(fs, 1, "public {0}()", sClassName);
                WriteCode(fs, 1, "{", null);
        
                WriteCode(fs, 1, "}", null);
                fs.WriteLine();
                WriteJavaKeyInterface(fs);
                WriteCode(fs, 1, Define.JAVA_READ_METHOD, null);
                WriteCode(fs, 1, "{", null);
                foreach (int col in exColList)
                {
                    ColumnInfo info = AllColumnInfo[col];
                    WriteCode(fs, 2, "{0} = {1}.Get{2}({3}{4});", info.ConfigName, Define.CSHARP_READ_READER,
                            info.DataType.GetJavaReadName(), Define.CSHARP_KEY_PREFIX, info.ConfigName);
                }
                WriteCode(fs, 2, "return true;", null);
                WriteCode(fs, 1, "}", null);

                WriteCode(fs, 0, "}", null);
                fs.WriteLine();
                fs.Close();
            }
        }
         catch (System.Exception ex)
         {
             Form1.Instance.Log(ELogType.ERROR, "导出 Java 代码文件失败, 文件名:{0}, 类名:{1}, 错误:{2}", sFileName, sClassName, ex.ToString());
             return false;
         }
         sFileName = Environment.CurrentDirectory.ToString() + "\\JavaCode\\" + sClassName + "Mgr.java";
         if (CreateFile(sFileName) == false)
         {
             return false;
         }
         try
         {
             using (StreamWriter fs = new StreamWriter(sFileName, false, Define.UTF8_EMITBOM))
             {
                 WriteCode(fs, 0, Define.SOURCE_FILE_HEAD, null);
                 WriteCode(fs, 0, Define.JAVA_INCLUDE, null);
                 fs.WriteLine();
                 WriteJavaMgrClassDef(fs, sClassName);
                 WriteJavaKey(fs, sClassName);
                 WriteJavaGet(fs, sClassName);
                 WriteCode(fs, 0, "}", null);
                 fs.Close();
             }
         }
         catch (System.Exception ex)
         {
             Form1.Instance.Log(ELogType.ERROR, "导出 Java 代码文件失败, 文件名:{0}, 类名:{1}Mgr, 错误:{2}", sFileName, sClassName, ex.ToString());
             return false;
         }
         return true;
    }
    public bool ExportCSharpSource(string sClassName)
    {
        string sFileName = Environment.CurrentDirectory.ToString() + "\\CSharpCode\\" + sClassName + ".cs";
        if (CreateFile(sFileName) == false)
        {
            return false;
        }

        try
        {
            using (StreamWriter fs = new StreamWriter(sFileName, false, Define.UTF8_EMITBOM))
            {
                List<int> exColList = GetExportColumnList();

                // 头部数据
                WriteCode(fs, 0, Define.SOURCE_FILE_HEAD, null);
                fs.WriteLine();
                WriteCode(fs, 0, Define.CSHARP_INCLUDE, null);
                fs.WriteLine();
                WriteCsMgrDef(fs, sClassName);

                // 类定义
                WriteCsClassDef(fs, sClassName);
                WriteCode(fs, 0, "{", null);

                // 导出列的字符串索引
                foreach (int col in exColList)
                {
                    ColumnInfo info = AllColumnInfo[col];
                    WriteCode(fs, 1, "public static readonly string {0}{1} = \"{2}\";",
                        Define.CSHARP_KEY_PREFIX, info.ConfigName, info.ConfigName);
                }
                fs.WriteLine();

                // 成员变量定义
                foreach (int col in exColList)
                {
                    ColumnInfo info = AllColumnInfo[col];
                    WriteCode(fs, 1, "public {0}\t{1} {{ get; private set; }}\t\t\t\t// {2}", info.DataType.CsName, info.ConfigName, info.ColDesc);
                }
                fs.WriteLine();

                // 构造函数定义(用于初始化数组变量)
                WriteCode(fs, 1, "public {0}()", sClassName);
                WriteCode(fs, 1, "{", null);
        
                WriteCode(fs, 1, "}", null);
                fs.WriteLine();

                WriteCsKeyInterface(fs);

                // 数据读取函数
                WriteCode(fs, 1, Define.CSHARP_READ_METHOD, null);
                WriteCode(fs, 1, "{", null);
                foreach (int col in exColList)
                {
                    ColumnInfo info = AllColumnInfo[col];
                    WriteCode(fs, 2, "{0} = {1}.Get<{2}>({3}{4});", info.ConfigName, Define.CSHARP_READ_READER,
                            info.DataType.CsName, Define.CSHARP_KEY_PREFIX, info.ConfigName);
                }

                // 函数末尾
                WriteCode(fs, 2, "return true;", null);
                WriteCode(fs, 1, "}", null);

                WriteCode(fs, 0, "}", null);
                fs.WriteLine();
                fs.Close();
            }
        }
        catch (System.Exception ex)
        {
            Form1.Instance.Log(ELogType.ERROR, "导出 c# 代码文件失败, 文件名:{0}, 类名:{1}, 错误:{2}", sFileName, sClassName, ex.ToString());
            return false;
        }
        return true;
    }
}
