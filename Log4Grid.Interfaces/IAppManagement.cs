using System.Collections.Generic;

namespace Log4Grid.Interfaces
{
    public interface IAppManagement
    {
        void Stat(Models.StatModel e);
        IList<Models.ApplicationData> ListApp();
        void CleanApp();
    }
}