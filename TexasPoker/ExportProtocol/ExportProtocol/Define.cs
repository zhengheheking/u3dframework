using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportProtocol
{
    public class Define
    {
        public static readonly string TAB_STRING_MAX = "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t";
        public static UTF8Encoding UTF8_EMITBOM = new UTF8Encoding(false);
        public static readonly string SOURCE_FILE_HEAD = @"
//============================================
//--4>:
//    Exported by ExportProtocol
//
//    此代码为工具根据配置自动生成, 建议不要修改
//
//============================================";
        public static readonly string CSHARP_INCLUDE = "using System;" + Environment.NewLine + "\tusing UnityEngine;" + Environment.NewLine + "\tusing Socket;" + Environment.NewLine + "\tusing Socket.Net;";
        public static readonly string JAVA_INCLUDE = "import java.nio.ByteBuffer" + Environment.NewLine + "import comm.BaseRqst;" + Environment.NewLine + "import utils.JkTools;";
        public static readonly string JAVA_RESPONDER_INCLUDE = "import java.nio.ByteBuffer" + Environment.NewLine + "import comm.BaseRspd;" + Environment.NewLine + "import utils.JkTools;" + Environment.NewLine + "import comm.Client;";
    }
}
