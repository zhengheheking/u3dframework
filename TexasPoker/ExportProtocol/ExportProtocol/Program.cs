using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ExportProtocol
{
    public class Program
    {
        static List<ProtocolConfig> XmlConfigList = new List<ProtocolConfig>();
        static void Main(string[] args)
        {
            List<string> xmlFileList = EnumFile(Environment.CurrentDirectory.ToString() + "\\protocol");
            LoadXMl(xmlFileList);
            foreach(ProtocolConfig pc in XmlConfigList)
            {
                pc.ExportCharpCode();
                pc.ExportJavaCode();
            }
        }
        static List<string> EnumFile(string path)
        {
            List<string> fileList = new List<string>();
            DirectoryInfo TheFolder = new DirectoryInfo(path);
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                foreach (FileInfo NextFile in NextFolder.GetFiles())
                {
                    if (NextFile.Name != "void.xml")
                    {
                        fileList.Add(NextFile.FullName);
                    }
                    
                }
            }
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                if (NextFile.Name != "void.xml")
                {
                    fileList.Add(NextFile.FullName);
                }
            }
            return fileList;
        }
        static void LoadXMl(List<string> xmlFileList)
        {
            foreach(string filePath in xmlFileList)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                ProtocolConfig pc = new ProtocolConfig();
                XmlNode xn = xmlDoc.SelectSingleNode("/Module");
                pc.ProtocolId = Int32.Parse(((XmlElement)xn).GetAttribute("id"));
                List<RRConfig> requsters = new List<RRConfig>();
                xn = xmlDoc.SelectSingleNode("/Module/Requester");
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode xn1 in xnl)
                {
                    XmlElement xe = (XmlElement)xn1;
                    string name = xe.Name;
                    List<Parameter> pList = new List<Parameter>();
                    XmlNodeList xn1l = xn1.ChildNodes;
                    foreach (XmlNode xn11 in xn1l)
                    {
                        XmlElement xe1 = (XmlElement)xn11;
                        string name1 = xe1.Name;
                        string type = xe1.GetAttribute("type");
                        Parameter p = Parameter.Create(name1, type);
                        pList.Add(p);
                    }
                    RRConfig r = RRConfig.Create(name, pList);
                    requsters.Add(r);
                }
                List<RRConfig> responders = new List<RRConfig>();
                xn = xmlDoc.SelectSingleNode("/Module/Responder");
                xnl = xn.ChildNodes;
                foreach (XmlNode xn1 in xnl)
                {
                    XmlElement xe = (XmlElement)xn1;
                    string name = xe.Name;
                    List<Parameter> pList = new List<Parameter>();
                    XmlNodeList xn1l = xn1.ChildNodes;
                    foreach (XmlNode xn11 in xn1l)
                    {
                        XmlElement xe1 = (XmlElement)xn11;
                        string name1 = xe1.Name;
                        string type = xe1.GetAttribute("type");
                        Parameter p = Parameter.Create(name1, type);
                        pList.Add(p);
                    }
                    RRConfig r = RRConfig.Create(name, pList);
                    responders.Add(r);
                }
                pc.Requsters = requsters;
                pc.Responders = responders;
                XmlConfigList.Add(pc);
            }
        }
        public static void Log(string format, params object[] args)
        {
            string strData = string.Format(format, args);
            string strAppend = string.Format("{0}", strData + Environment.NewLine);
            Console.Write(strAppend);
        }
    }
}
