Overview
---------------
Log4Grid是一款开源分布式应用监控和日志管理系统,通过该系统可以实时查看每个应用的进程情况外还能看到相应用户记录的程序处理日志信息.为了保证不对用户现有程序的改动,Log4Grid提供相应Log4Net的日志插件,通过插件应用在不修改代码的情况就可以把插件添加到Log4net的配置中实现自动的监控信息和日志提交给管理系统.

1.日志功能管理
---------------
Log4Grid提供一个Web界面用一监控和管理应用日志,通过应该Web管理模块相关人员可以实时查看应用的CPU/内存使用情况和应该产生的日志.
管理模块的右边是应用部所在服务器的情况,而右边列表则是相关应用所在服务器产生的应用日志.如果某个应用出现异常那会在应用服务列表中反映出来.这样相关人员就能马上知道那些应用存在异常的情况.

2.信息收集服务
---------------
Log4Grid提供个专门的服务用收集各应用的日志和应用统计信息,并写入相应的存储.服务采用UDP作为通讯协议,而应用协议则采用Protobuf.服务分别提供Console和windows servcie两种模式.使用者可以根据自己的情况来启用相应的服务程序.

3.自定义日志存储
---------------
系统默认实现了基于sqlite的日志存储,然而对于大量日志存储的情况下sqlite显然很难满足实际应用的需要.使用者可以根据自己实现的情况来实现具体的日志存储和操作提供设备.具体查看Log4Grid.Interfaces这个接口的描述.

4.日志插件
---------------
为了让日志收集的前提下不调整现有应用程序的代码,因此系统针对一些通过开源的日志组件提供插件支持.系统默认提供基于log4net的Appender,通过配置相应的Appender在不调整应用代码的情况就可以把信息提供到系统中.

Change Log
---------------
1.0.1.0
* 界面汉化.
* 未结束div标签引起的UI BUG修正.
* 添加键值对时未检查是否存在的BUG修复.
* 将一些调用google在线js和css资源链接下载到Web项目中,原因你懂的.
* 完善日志列表的日志类型颜色.
* 在右上角增加宽版和窄版界面的在线调整按钮.
* 对解决方案执行using排序与检查.
* 移除项目中用不到的引用.
* 更新nuget包

Demo
---------------
Distributed Application Log Management: http://l4g.ikende.com 

![Bitdeli Badge](http://images.cnblogs.com/cnblogs_com/VAllen/416551/o_QQ20141224093841.png)

Author
---------------
Email: [henryfan@msn.com](mailto:henryfan@msn.com)  
Home: (http://www.ikende.com/)  
Project Home: (http://www.ikende.com/log4grid)
