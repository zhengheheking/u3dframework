6.	其他说明
MyLogLibrary\Log4netLibForMono2.0下面有两个dll文件，其中log4net.dll是必须的，System.Web.dll是不是必须的。

6.1.需要System.Web.dll文件的原因
因为log4net框架原生设计带有对.Net Web项目的支持，就是在.Net Web项目中输出日志。
所以log4net编译的时候，需要以下这几个包的支持

System
System.Configuration
System.Data
System.Web
System.XML

Mono官方发布包里是有上述几个dll。Unity4.X到5.0.0f1编辑器是用了Mono2.8运行时，但是对于dll的选用则是不完全的。
在unity4.X,5.X中上述几个包不全，其中unity4.0.0f7有上述dll,unity4.6.4f1,unity5.0.0f1都没有System.Configuration.dll和System.Web.dll文件。
在使用UnityVS断点调试时候,Visual Studio会提示log4net有对System.Web.dll 2.0的依赖，需要加入这个类库文件，才能调试。
此时如果你直接用Visual Studio自带的System.Web.dll 2.0文件会报错，需要使用Mono官方发布的System.Web.dll 2.0文件。这里的这个System.Web.dll文件来源于Mono官方发布包。

6.2.使用说明
【UnityVS调试时候】在unity3d项目中使用log4net，完全没有用到System.Web的任何内容，只是为了满足UnityVS调试的要求，所以调试的时候需要这个dll.
【游戏正式发布的时候】当你在正式发布游戏包的时候，请把这个log4net.dll和System.Web.dll去掉（测试好之后，发布的时候，删除这个dll），进一步减少游戏发布文件的体积。并且对MyLogHelper.cs文件头部的四个条件编译选项，进行有选择的注释，即对2、3、4项保留，1项去掉。详见文件。

6.3为什么要发布的时候进行dll删除和条件编译注释选择的原因是？
Unity虽然是使用C#做脚本语言，但是自带的Mono 2.8运行时对C#的支持是非常有限的。.Net中有大量丰富的第三方类库本来可以使用，但是因为Mono运行时支持有限制，比如，不准在代码中使用Unity脚本中使用System.Configuration和System.Web命名空间有关一切类。而原生log4net对System.Web（用于Asp日志的输出）和System.Configuration(用于xml的处理)有使用，所以即使在编译阶段可以除去System.Web的引用(log4net非核心代码才用)，但是很难除去对System.Configuration引用(log4net核心代码使用)。在正式发布游戏项目时候，Unity会检查dll的引用依赖，无法在正式版发布时候引入log4net.dll。但是在游戏开发调试阶段可以使用log4net的功能。如果在发布时候引入log4net,除非在代码级别除去System.Configuration的影响。


6.4.题外话
从上述的使用.Net类库的过程来看，用C#开发Unity3d脚本，遇到上述的限制，真是戴着镣铐跳舞。因为各种原因，Unity4.X-5.X至今使用2010年发布的Mono 2.8，期待Unity引擎未来的尽快摆脱对Mono的依赖。未来的Unity引擎应该会对项目的发布平台使用IL2CPP来生成跨平台的代码，而不再用Mono跨平台。目前IL2CPP的应用，unity5.X用生成WebGL项目，iOS 64项目。来源：http://www.indieace.com/thread-8199-1-1.html

