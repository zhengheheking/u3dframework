using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportProtocol
{
    public class Parameter
    {
        public string Name;
        public string Type;
        public static Parameter Create(string Name, string Type)
        {
            Parameter p = new Parameter();
            p.Name = Name;
            p.Type = Type;
            return p;
        }
        public string GetCsWriteMethod()
        {
            if (Type == "int")
            {
                return "WriteInt";
            }
            else if (Type == "short")
            {
                return "WriteShort";
            }
            else if (Type == "byte")
            {
                return "WriteByte";
            }
            else if (Type == "float")
            {
                return "WriteFloat";
            }
            else
            {
                return "WriteString";
            }
        }
        public string GetCsReadMethod()
        {
            if (Type == "int")
            {
                return "ReadInt";
            }
            else if (Type == "short")
            {
                return "ReadShort";
            }
            else if (Type == "byte")
            {
                return "ReadByte";
            }
            else if (Type == "float")
            {
                return "ReadFloat";
            }
            else
            {
                return "ReadString";
            }
        }
        public string GetJavaReadMethod()
        {
            if (Type == "int")
            {
                return "getInt";
            }
            else if (Type == "short")
            {
                return "getShort";
            }
            else if (Type == "byte")
            {
                return "get";
            }
            else if (Type == "float")
            {
                return "getFloat";
            }
            else
            {
                return "JkTools.readString";
            }
        }
        public string GetJavaWriteMethod()
        {
            if (Type == "int")
            {
                return "putInt";
            }
            else if (Type == "short")
            {
                return "putShort";
            }
            else if (Type == "byte")
            {
                return "put";
            }
            else if (Type == "float")
            {
                return "putFloat";
            }
            else
            {
                return "JkTools.writeString";
            }
        }
        public string GetCsType()
        {
            if(Type == "int")
            {
                return "int";
            }
            else if (Type == "short")
            {
                return "short";
            }
            else if (Type == "byte")
            {
                return "byte";
            }
            else if (Type == "float")
            {
                return "float";
            }
            else
            {
                return "string";
            }
        }
        public string GetJavaType()
        {
            if (Type == "int")
            {
                return "int";
            }
            else if (Type == "short")
            {
                return "short";
            }
            else if (Type == "byte")
            {
                return "byte";
            }
            else if (Type == "float")
            {
                return "float";
            }
            else
            {
                return "String";
            }
        }
    }
    public class RRConfig
    {
        public string Name;
        public List<Parameter> PList = new List<Parameter>();
        public static RRConfig Create(string Name, List<Parameter> PList)
        {
            RRConfig rr = new RRConfig();
            rr.Name = Name;
            rr.PList = PList;
            return rr;
        }
    }

    public class ProtocolConfig
    {
        public int ProtocolId;
        public List<RRConfig> Requsters = new List<RRConfig>();
        public List<RRConfig> Responders = new List<RRConfig>();
        public void ExportJavaCode()
        {
            string fileDir = Environment.CurrentDirectory.ToString() + "\\javacode\\requesters\\";
            for (int j = 0; j < Requsters.Count; j++)
            {
                RRConfig rr = Requsters[j];
                string fileName = fileDir + rr.Name + "Rqst.java";
                string className = rr.Name + "Rqst";
                if (CreateFile(fileName) == false)
                {
                    continue;
                }
                try
                {
                    using (StreamWriter fs = new StreamWriter(fileName, false, Define.UTF8_EMITBOM))
                    {
                        WriteCode(fs, 0, Define.SOURCE_FILE_HEAD);
                        fs.WriteLine();
                        WriteCode(fs, 0, "package requesters;");
                        WriteCode(fs, 0, Define.JAVA_INCLUDE);
                        fs.WriteLine();
                        WriteCode(fs, 0, "public class {0} extends BaseRqst {{", className);
                        for (int i = 0; i < rr.PList.Count; i++)
                        {
                            Parameter p = rr.PList[i];
                            WriteCode(fs, 1, "private {0} {1};", p.GetJavaType(), p.Name);
                        }
                        WriteCode(fs, 1, "public void fromBytes(ByteBuffer bytes) {");
                        WriteCode(fs, 2, "super.fromBytes(bytes);");
                        for (int i = 0; i < rr.PList.Count; i++)
                        {
                            Parameter p = rr.PList[i];
                            if (p.Type == "string")
                            {
                                WriteCode(fs, 2, "{0} = {1}(bytes);", p.Name, p.GetJavaReadMethod());
                            }
                            else
                            {
                                WriteCode(fs, 2, "{0} = bytes.{1}();", p.Name, p.GetJavaReadMethod());
                            }
                        }
                        WriteCode(fs, 1, "}");
                        WriteCode(fs, 0, "}");
                    }
                }
                catch (System.Exception ex)
                {
                    Program.Log("导出 Java 代码文件失败, 文件名:{0}, 错误:{1}", fileName, ex.ToString());
                    continue;
                }
            }
            fileDir = Environment.CurrentDirectory.ToString() + "\\javacode\\responders\\";
            for (int j = 0; j < Responders.Count; j++)
            {
                RRConfig rr = Responders[j];
                string fileName = fileDir + rr.Name + "Rspd.java";
                string className = rr.Name + "Rspd";
                if (CreateFile(fileName) == false)
                {
                    continue;
                }
                try
                {
                    using (StreamWriter fs = new StreamWriter(fileName, false, Define.UTF8_EMITBOM))
                    {
                        WriteCode(fs, 0, Define.SOURCE_FILE_HEAD);
                        fs.WriteLine();
                        WriteCode(fs, 0, "package requesters;");
                        WriteCode(fs, 0, Define.JAVA_RESPONDER_INCLUDE);
                        fs.WriteLine();
                        WriteCode(fs, 0, "public class {0} extends BaseRspd {{", className);
                        for (int i = 0; i < rr.PList.Count; i++)
                        {
                            Parameter p = rr.PList[i];
                            WriteCode(fs, 1, "private final {0} {1};", p.GetJavaType(), p.Name);
                        }
                        WriteCode(fs, 1, "public {0}(", className);
                        for (int i = 0; i < rr.PList.Count; i++)
                        {
                            Parameter p = rr.PList[i];
                            if (i == rr.PList.Count - 1)
                            {
                                WriteCode(fs, 3, "{0} {1}", p.GetJavaType(), p.Name);
                            }
                            else
                            {
                                WriteCode(fs, 3, "{0} {1},", p.GetJavaType(), p.Name);
                            }
                        }
                        WriteCode(fs, 2, ") {");
                        WriteCode(fs, 2, "super({0});", (ProtocolId * 100 + j).ToString());
                        for (int i = 0; i < rr.PList.Count; i++)
                        {
                            Parameter p = rr.PList[i];
                            WriteCode(fs, 2, "this.{0} = {1};", p.Name, p.Name);
                        }
                        WriteCode(fs, 1, "}");
                        WriteCode(fs, 1, "@Override");
                        WriteCode(fs, 1, "protected void push(Client cl) {");
                        WriteCode(fs, 2, "super.push(cl);");
                        for (int i = 0; i < rr.PList.Count; i++)
                        {
                            Parameter p = rr.PList[i];
                            if (p.Type == "string")
                            {
                                WriteCode(fs, 2, "{0}(bytes, {1});", p.GetJavaWriteMethod(), p.Name);
                            }
                            else
                            {
                                WriteCode(fs, 2, "bytes.{0}({1});", p.GetJavaWriteMethod(), p.Name);
                            }
                        }
                        WriteCode(fs, 1, "}");
                        WriteCode(fs, 0, "}");
                    }
                }
                catch (System.Exception ex)
                {
                    Program.Log("导出 Java 代码文件失败, 文件名:{0}, 错误:{1}", fileName, ex.ToString());
                    continue;
                }
            }
        }
        public void ExportCharpCode()
        {
            string fileDir = Environment.CurrentDirectory.ToString() + "\\cscode\\requesters\\";
            for(int j=0; j<Requsters.Count; j++)
            {
                RRConfig rr = Requsters[j];
                string fileName = fileDir + rr.Name+"Rqst.cs";
                string className = rr.Name + "Rqst";
                if (CreateFile(fileName) == false)
                {
                    continue;
                }
                try
                {
                    using (StreamWriter fs = new StreamWriter(fileName, false, Define.UTF8_EMITBOM))
                    {
                        WriteCode(fs, 0, Define.SOURCE_FILE_HEAD);
                        fs.WriteLine();
                        WriteCode(fs, 0, "namespace Requesters");
                        WriteCode(fs, 0, "{");
                        WriteCode(fs, 1, Define.CSHARP_INCLUDE);
                        fs.WriteLine();
                        WriteCode(fs, 1, "public class {0} : BaseRqst", className);
                        WriteCode(fs, 1, "{");
                        WriteCode(fs, 1, "public {0}(", className);
                        for(int i=0; i<rr.PList.Count; i++)
                        {
                            Parameter p = rr.PList[i];
                            if(i == rr.PList.Count-1)
                            {
                                WriteCode(fs, 3, "{0} {1}", p.GetCsType(), p.Name);
                            }
                            else
                            {
                                WriteCode(fs, 3, "{0} {1},", p.GetCsType(), p.Name);
                            }
                            
                        }
                        WriteCode(fs, 3, ")");
                        WriteCode(fs, 3, ": base({0})", (ProtocolId*100+j).ToString());
                        WriteCode(fs, 2, "{");
                        
                        for (int i = 0; i < rr.PList.Count; i++)
                        {
                            Parameter p = rr.PList[i];
                            WriteCode(fs, 3, "m_Bytes.{0}({1});", p.GetCsWriteMethod(), p.Name);
                        }
                        WriteCode(fs, 2, "}");
                        WriteCode(fs, 1, "}");
                        WriteCode(fs, 0, "}");
                    }
                }
                catch (System.Exception ex)
                {
                    Program.Log("导出 c# 代码文件失败, 文件名:{0}, 错误:{1}", fileName, ex.ToString());
                    continue;
                }
            }
            fileDir = Environment.CurrentDirectory.ToString() + "\\cscode\\responders\\";
            for (int j = 0; j < Responders.Count; j++)
            {
                RRConfig rr = Responders[j];
                string fileName = fileDir + rr.Name + "Rspd.cs";
                string className = rr.Name + "Rspd";
                if (CreateFile(fileName) == false)
                {
                    continue;
                }
                try
                {
                    using (StreamWriter fs = new StreamWriter(fileName, false, Define.UTF8_EMITBOM))
                    {
                        WriteCode(fs, 0, Define.SOURCE_FILE_HEAD);
                        fs.WriteLine();
                        WriteCode(fs, 0, "namespace Responders");
                        WriteCode(fs, 0, "{");
                        WriteCode(fs, 1, Define.CSHARP_INCLUDE);
                        fs.WriteLine();
                        WriteCode(fs, 1, "public class {0} : BaseRspd", className);
                        WriteCode(fs, 1, "{");
                        for (int i = 0; i < rr.PList.Count; i++)
                        {
                            Parameter p = rr.PList[i];
                            WriteCode(fs, 2, "public {0} {1};", p.GetCsType(), p.Name);
                        }
                        WriteCode(fs, 2, "public {0}(ByteBuffer bytes)\n\t\t\t: base(bytes)\n\t\t{{}}", className);
                        WriteCode(fs, 2, "public override void ReadData()");
                        WriteCode(fs, 2, "{");
                        for (int i = 0; i < rr.PList.Count; i++)
                        {
                            Parameter p = rr.PList[i];
                            WriteCode(fs, 3, "{0} = m_Bytes.{1}();", p.Name, p.GetCsReadMethod());
                        }
                        WriteCode(fs, 2, "}");
                        WriteCode(fs, 1, "}");
                        WriteCode(fs, 0, "}");
                    }
                }
                catch (System.Exception ex)
                {
                    Program.Log("导出 c# 代码文件失败, 文件名:{0}, 错误:{1}", fileName, ex.ToString());
                    continue;
                }
            }
        }
        private void WriteCode(StreamWriter fs, int tabNum, string format)
        {
            WriteCode(fs, tabNum, format, null);
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
        private bool CreateFile(string sFileName)
        {
            if (null == sFileName || sFileName == string.Empty)
            {
                Program.Log("创建文件失败, 文件名为空");
                return false;
            }
            try
            {
                if (File.Exists(sFileName))
                {
                    if (FileAttributes.ReadOnly == (File.GetAttributes(sFileName) & FileAttributes.ReadOnly))
                    {
                        Program.Log("文件:{0} 是只读的", sFileName);
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
                Program.Log("创建文件失败, 文件:{0}, 错误:{1}", sFileName, ex.ToString());
                return false;
            }
            return true;
        }
    }
}
