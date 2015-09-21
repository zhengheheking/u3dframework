using ExcelConfigExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
public class ExcelConfig
{
    private Excel.Worksheet Worksheet;
    private object[,] AllData;
    private ConfigData ConfigData;
    public static ExcelConfig LoadExcelConfig(Excel.Worksheet worksheet)
    {
        ExcelConfig excelConfig = new ExcelConfig();
        if (worksheet == null || excelConfig.Init(worksheet) == false)
        {
            return null;
        }
        return excelConfig;
    }
    private bool Init(Excel.Worksheet worksheet)
    {
        if (worksheet == null)
        {
            Form1.Instance.Log(ELogType.ERROR, "配置表单为空");
            return false;
        }

        Worksheet = worksheet;

        Excel.Range range = worksheet.UsedRange.CurrentRegion;
        int nRowNum = range.Rows.Count;
        int nColNum = range.Columns.Count;
        Form1.Instance.Log(ELogType.INFO, "--4>: 开始加载配置表单文件, 表单名:{0}, 行数:{1} 列数:{2}", worksheet.Name, nRowNum, nColNum);

        //--4>: 读取数据表表头信息
        if (nRowNum < 4 || nColNum < 1)
        {
            Form1.Instance.Log(ELogType.ERROR, "数据表格式有误");
            return false;
        }

        AllData = range.Value2 as object[,];

        ConfigData = ConfigData.CreateConfigData(AllData);

        if (null == ConfigData)
        {
            Form1.Instance.Log(ELogType.ERROR, "加载数据表表头失败, 表单名:{0}", worksheet.Name);
            return false;
        }

        Form1.Instance.Log(ELogType.INFO, "--4>: 成功加载数据表表头信息, 表单名:{0}", worksheet.Name);
        return true;
    }
    public bool DoExportData(bool client)
    {

        if (null == ConfigData || null == AllData)
        {
            Form1.Instance.Log(ELogType.ERROR, "导出数据失败, 尚未载入数据表信息");
            return false;
        }
        // if (null != cfgInfo)
        // {
        ConfigData.ClearData();
        if (ConfigData.AddMultiLine(AllData, AllData.GetLowerBound(0) + (int)EConfigHeadType.Count) == false)
        {
            Form1.Instance.Log(ELogType.ERROR, "加载数据表数据失败");
            return false;
        }
        Form1.Instance.Log(ELogType.INFO, "成功加载数据表的配置数据");
        string s = client == true ? "\\ClientText\\" : "\\ServerText\\";
        if (ConfigData.ExportConfigFile(Environment.CurrentDirectory.ToString() + s + Worksheet.Name+".txt") == false)
        {
            return false;
        }
        return true;
    }
    public bool DoExportCsharp()
    {

        if (null == ConfigData || null == AllData)
        {
            Form1.Instance.Log(ELogType.ERROR, "导出数据失败, 尚未载入数据表信息");
            return false;
        }
        // if (null != cfgInfo)
        // {
        ConfigData.ClearData();
        if (ConfigData.AddMultiLine(AllData, AllData.GetLowerBound(0) + (int)EConfigHeadType.Count) == false)
        {
            Form1.Instance.Log(ELogType.ERROR, "加载数据表数据失败");
            return false;
        }
        Form1.Instance.Log(ELogType.INFO, "成功加载数据表的配置数据");

        if (ConfigData.ExportCSharpSource("Cfg"+Worksheet.Name) == false)
        {
            return false;
        }
        return true;
    }
    public bool DoExportJava()
    {

        if (null == ConfigData || null == AllData)
        {
            Form1.Instance.Log(ELogType.ERROR, "导出数据失败, 尚未载入数据表信息");
            return false;
        }
        // if (null != cfgInfo)
        // {
        ConfigData.ClearData();
        if (ConfigData.AddMultiLine(AllData, AllData.GetLowerBound(0) + (int)EConfigHeadType.Count) == false)
        {
            Form1.Instance.Log(ELogType.ERROR, "加载数据表数据失败");
            return false;
        }
        Form1.Instance.Log(ELogType.INFO, "成功加载数据表的配置数据");

        if (ConfigData.ExportJavaSource("Cfg" + Worksheet.Name) == false)
        {
            return false;
        }
        return true;
    }
}
