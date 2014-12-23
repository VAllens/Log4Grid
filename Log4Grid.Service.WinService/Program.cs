using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Log4Grid.Service.WinService
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        private static void Main()
        {
            var servicesToRun = new ServiceBase[]
            {
                new Log4GridService()
            };
            ServiceBase.Run(servicesToRun);
        }
    }

    [RunInstaller(true)]
    public class WindowsServiceInstaller : Installer
    {
        public WindowsServiceInstaller()
        {
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();


            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;


            serviceInstaller.DisplayName = "Log4Grid Service";
            serviceInstaller.StartType = ServiceStartMode.Automatic;


            serviceInstaller.ServiceName = "Log4GridService";
            serviceInstaller.Description = "Log4Grid日志收集服务";

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}