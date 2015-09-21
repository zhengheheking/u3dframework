using UnityEngine;
using System.Collections;


public class MyLogTestExample1 : MonoBehaviour
{
	//错误的初始化日志类调用方式，会报错get_dataPath can only be called from the main thread.
	//MyLogHelper因为构造函数中用了Application.dataPath
	//private MyLogHelper log=new MyLogHelper(typeof(MyLogTestExample1));

	//正确的初始化方式，在Awake函数或者Start函数中初始化调用
    //1.初始化
	private  MyLogHelper log;

	void Awake()
	{
        //1.初始化
		log = new MyLogHelper(typeof(MyLogTestExample1));//需要传递调用日志类的类型
	}

    public float someFloatVariable = 123.4F;

	// Use this for initialization
	void Start ()
	{
		print("Now logging should start...");

        //这里就可以调用log的各种函数来输出日志了
        // 2.调用Debug、Info等函数
        log.Debug("Debug message-->there can be plenty of these");
        log.Info("Info message-->These should me more sparse");
        log.Warn("Warn message-->THIS is warn message");
        log.Error("Error message-->When some error has occured that can be handled");
        log.Fatal("Fatal message-->Use this when something really serious has happened!");
        log.InfoFormat("Handy shortcut for string.Format - {0}", someFloatVariable);
        
        //还可以输出异常日志，调用方式和log4net一样，请参考log4net官方文档
	}

	// Update is called once per frame
	void Update ()
    {
	   //log.Debug("Debug message-->there can be plenty of these");		
	}
}
