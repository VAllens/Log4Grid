using System;
using System.Reflection;

namespace Log4Grid.Config
{
    public class InterfaceFactory
    {
        private Log4GridSection GetConfigSection(string sectionName)
        {
            Log4GridSection result = null;

            System.Configuration.ExeConfigurationFileMap fm = new System.Configuration.ExeConfigurationFileMap();
            fm.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "Log4Grid.config";
            System.Configuration.Configuration mDomainConfig = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fm,
                System.Configuration.ConfigurationUserLevel.None);
            result = (Log4GridSection) mDomainConfig.GetSection(sectionName);
            return result;
        }

        public InterfaceFactory()
        {
            LoadConfig(GetConfigSection(Log4GridSection.Log4GridSectionSectionName));
        }

        public InterfaceFactory(string section)
        {
            LoadConfig(GetConfigSection(section));
        }

        private void LoadConfig(Log4GridSection section)
        {
            Management = (Interfaces.IAppManagement) Activator.CreateInstance(Type.GetType(section.Management.Type));
            LoadProperties(Management, section.Management.Properties);

            Store = (Interfaces.ILogStoreHandler) Activator.CreateInstance(Type.GetType(section.LogStore.Type));
            LoadProperties(Store, section.LogStore.Properties);

            Search = (Interfaces.ILogSearchHandler) Activator.CreateInstance(Type.GetType(section.LogSearch.Type));
            LoadProperties(Search, section.LogSearch.Properties);

            User = (Interfaces.IUserManagement) Activator.CreateInstance(Type.GetType(section.User.Type));
            LoadProperties(User, section.User.Properties);
        }

        private void LoadProperties(object data, PropertyCollection properties)
        {
            foreach (Property item in properties)
            {
                PropertyInfo pi = data.GetType().GetProperty(item.Name, BindingFlags.Public | BindingFlags.Instance);
                if (pi != null && pi.CanWrite)
                {
                    try
                    {
                        pi.SetValue(data, Convert.ChangeType(item.Value, pi.PropertyType), null);
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }

        public Interfaces.IUserManagement User { get; private set; }

        public Interfaces.ILogSearchHandler Search { get; private set; }

        public Interfaces.ILogStoreHandler Store { get; private set; }

        public Interfaces.IAppManagement Management { get; private set; }
    }
}