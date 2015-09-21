using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Windows.Forms;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ExcelConfigExport
{
    
    public partial class Form1 : Form
    {
        public static Form1 Instance; 
        private List<ExcelItem> ClientWorkSheetList = new List<ExcelItem>();
        private Excel._Application ExcelApp = new Excel.Application();
        private List<ExcelConfig> ClientConfigSheetList = new List<ExcelConfig>();
        private List<ExcelItem> ServerWorkSheetList = new List<ExcelItem>();
        private List<ExcelConfig> ServerConfigSheetList = new List<ExcelConfig>();
        public Form1()
        {
            Instance = this;
            InitializeComponent();
        }

        private void ClearClient()
        {
            ClientWorkSheetList.Clear();
            ExcelListBox.Items.Clear();
            ClientConfigSheetList.Clear();
            ExcelApp.Quit();
            foreach (ExcelItem item in ClientWorkSheetList)
            {
                item.Workbook.Close(false, false, Missing.Value);
            }
            ExcelApp = new Excel.Application();
        }
        private void ClearServer()
        {
            ServerWorkSheetList.Clear();
            ExcelListBox.Items.Clear();
            ServerConfigSheetList.Clear();
            ExcelApp.Quit();
            foreach (ExcelItem item in ServerWorkSheetList)
            {
                item.Workbook.Close(false, false, Missing.Value);
            }
            ExcelApp = new Excel.Application();
        }
        private void InitClient()
        {
            ClearClient();
            List<string> excelFileList = EnumFile(Environment.CurrentDirectory.ToString() + "\\ClientExcel");

            LoadExcel(excelFileList, ClientWorkSheetList);
            foreach (ExcelItem item in ClientWorkSheetList)
            {
                ExcelListBox.Items.Add(item.SheetName);
                ExcelConfig configSheet = ExcelConfig.LoadExcelConfig(item.Sheet);
                ClientConfigSheetList.Add(configSheet);
            }
        }
        private void InitServer()
        {
            ClearClient();
            List<string> excelFileList = EnumFile(Environment.CurrentDirectory.ToString() + "\\ServerExcel");

            LoadExcel(excelFileList, ServerWorkSheetList);
            foreach (ExcelItem item in ServerWorkSheetList)
            {
                ExcelListBox.Items.Add(item.SheetName);
                ExcelConfig configSheet = ExcelConfig.LoadExcelConfig(item.Sheet);
                ServerConfigSheetList.Add(configSheet);
            }
        }
        private void LoadExcel(List<string> excelFileList, List<ExcelItem> workSheetList)
        {
            foreach(string filePath in excelFileList)
            {
                Excel.WorkbookClass workbook;
                try
                {
                    
                    object miss = System.Reflection.Missing.Value;
                    workbook = (Excel.WorkbookClass)ExcelApp.Workbooks.Open(filePath,
                        miss, true, miss, miss, miss, true, miss, miss, true, miss, miss, miss, miss, miss);
                }
                catch (System.Exception)
                {
                    workbook = null;
                    ExcelApp.Quit();
                }

                if (workbook == null)
                {
                    Log(ELogType.ERROR, "载入文件失败, 文件名:{0}", filePath);
                    continue;
                }

                Excel.Worksheet sheet = workbook.Worksheets.get_Item(1) as Excel.Worksheet;
                ExcelItem item = new ExcelItem();
                item.SheetName = sheet.Name;
                item.Sheet = sheet;
                item.Workbook = workbook;
                workSheetList.Add(item);
            }
            
        }
        private List<string> EnumFile(string path)
        {
            List<string> fileList = new List<string>();
            DirectoryInfo TheFolder = new DirectoryInfo(path);
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                foreach (FileInfo NextFile in NextFolder.GetFiles())
                {
                    fileList.Add(NextFile.FullName);
                }
            }
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                fileList.Add(NextFile.FullName);
            }
            return fileList;
        }
        private void LoadExcelButton_Click(object sender, EventArgs e)
        {
            InitClient();
        }
        public void Log(ELogType t, string format, params object[] args)
        {
            string strData = string.Format(format, args);
            string strAppend = string.Format("[{0}] {1}", t.ToString(), strData + Environment.NewLine);
            int oldLen = LogRichTextBox.Text.Length;
            LogRichTextBox.AppendText(strAppend);
            LogRichTextBox.Select(oldLen, strAppend.Length);
            switch (t)
            {
                case ELogType.INFO:
                case ELogType.DEBUG:
                    LogRichTextBox.SelectionColor = Color.Black;
                    break;
                case ELogType.ERROR:
                case ELogType.FATAL:
                    LogRichTextBox.SelectionColor = Color.Red;
                    break;
                case ELogType.WARN:
                    LogRichTextBox.SelectionColor = Color.Blue;
                    break;
                default:
                    break;
            }
            LogRichTextBox.Select(LogRichTextBox.Text.Length, 0);
            LogRichTextBox.SelectionColor = Color.Black;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ExcelItem item in ClientWorkSheetList)
            {
                item.Workbook.Close(false, false, Missing.Value);
            }
            ExcelApp.Quit();
        }

        private void ExportDataButton_Click(object sender, System.EventArgs e)
        {
            foreach (ExcelConfig config in ClientConfigSheetList)
            {
                config.DoExportData(true);
            }
        }

        private void ExportCSharpButton_Click(object sender, System.EventArgs e)
        {
            foreach (ExcelConfig config in ClientConfigSheetList)
            {
                config.DoExportCsharp();
            }
        }

        private void ExportJavaButton_Click(object sender, System.EventArgs e)
        {
            foreach (ExcelConfig config in ServerConfigSheetList)
            {
                config.DoExportJava();
            }
        }

        private void LoadServerExcel_Click(object sender, System.EventArgs e)
        {
            InitServer();
        }

        private void ExportServerDataButton_Click(object sender, System.EventArgs e)
        {
            foreach (ExcelConfig config in ServerConfigSheetList)
            {
                config.DoExportData(false);
            }
        }

    }
    
}
