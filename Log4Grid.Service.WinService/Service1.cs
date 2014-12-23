using System;
using System.ServiceProcess;

namespace Log4Grid.Service.WinService
{
    public partial class Log4GridService : ServiceBase
    {
        public Log4GridService()
        {
            InitializeComponent();
        }


        private LogServer _mLogServer;

        protected override void OnStart(string[] args)
        {
            try
            {
                Utils.Log.Info("Log4Grid Server Copyright @ www.ikende.com 2014 Version " + typeof (Log4GridService).Assembly.GetName().Version);
                Utils.Log.Info("Website:http://www.ikende.com");
                Utils.Log.Info("Email:henryfan@msn.com");
                _mLogServer = new LogServer();
                _mLogServer.Open();
                Utils.Log.InfoFormat("Log4Grid windows start at {0}", DateTime.Now);
            }
            catch (Exception e)
            {
                Utils.Log.ErrorFormat("Log4Grid windows start error {0}", e.Message);
            }
        }

        protected override void OnStop()
        {
            try
            {
                try
                {
                    if (_mLogServer != null)
                        _mLogServer.Dispose();
                }
                catch (Exception e)
                {
                    Utils.Log.ErrorFormat("Log4Grid server stop error {0}", e.Message);
                }


                Utils.Log.InfoFormat("Log4Grid windows service stop at {0}", DateTime.Now);
            }
            catch (Exception e)
            {
                Utils.Log.ErrorFormat("Log4Grid windows service stop error {0}", e.Message);
            }
        }
    }
}