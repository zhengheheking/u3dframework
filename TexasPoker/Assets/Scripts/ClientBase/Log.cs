using UnityEngine;
using System.Collections;
namespace Client.Base
{
   
    public class Log : Singleton<Log>
    {
        private MyLogHelper log;

        public void Init()
        {
            //1.初始化
            log = new MyLogHelper(typeof(Log));//需要传递调用日志类的类型
        }
        //log.Debug("Debug message-->there can be plenty of these");
        public void Debug(object message)
        {
            log.Debug(message);
            UnityEngine.Debug.Log(message);
        }
        //log.Info("Info message-->These should me more sparse");
        public void Info(object message)
        {
            log.Info(message);
            UnityEngine.Debug.Log(message);
        }
        //log.Warn("Warn message-->THIS is warn message");
        public void Warn(object message)
        {
            log.Warn(message);
            UnityEngine.Debug.LogWarning(message);
        }
        //log.Error("Error message-->When some error has occured that can be handled");
        public void Error(object message)
        {
            log.Error(message);
            UnityEngine.Debug.LogError(message);
        }
        //log.Fatal("Fatal message-->Use this when something really serious has happened!");
        public void Fatal(object message)
        {
            log.Fatal(message);
            UnityEngine.Debug.LogError(message);
        }
        //log.InfoFormat("Handy shortcut for string.Format - {0}", someFloatVariable);
        public void InfoFormat(string format, params object[] args)
        {
            log.InfoFormat(format, args);
            UnityEngine.Debug.LogFormat(format, args);
        }
    }

}
