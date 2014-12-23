using System;

namespace Log4Grid.Service
{
    public class LogServer : IDisposable
    {
        private LogServerSection GetConfigSection(string sectionName)
        {
            LogServerSection result = null;

            System.Configuration.ExeConfigurationFileMap fm = new System.Configuration.ExeConfigurationFileMap();
            fm.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "Log4Grid.config";
            System.Configuration.Configuration mDomainConfig = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fm,
                System.Configuration.ConfigurationUserLevel.None);
            result = (LogServerSection) mDomainConfig.GetSection(sectionName);
            return result;
        }

        public LogServer()
        {
            LogServerSection section = GetConfigSection(LogServerSection.LogServerSectionSectionName);
            _mServer = Beetle.Express.ServerFactory.CreateUDP();
            _mServer.Host = section.Host;
            _mServer.Port = section.Port;
            _mServer.SendBufferSize = 1024*64;
            _mServer.ReceiveBufferSize = 1024*64;
            _mServer.Handler = new MessageHandler(section.WorkThreads);
        }

        private readonly Beetle.Express.IServer _mServer;

        public void Open()
        {
            try
            {
                _mServer.Open();
                Utils.Log.InfoFormat("log server open {0}@{1} success", _mServer.Host, _mServer.Port);
            }
            catch (Exception e)
            {
                Utils.Log.ErrorFormat("log server open error {0}", e.Message);
            }
        }

        public void Dispose()
        {
            if (_mServer != null)
                _mServer.Dispose();
        }
    }
}