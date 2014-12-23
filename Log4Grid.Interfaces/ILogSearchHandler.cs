using System;
using System.Collections.Generic;

namespace Log4Grid.Interfaces
{
    public interface ILogSearchHandler
    {
        IList<Models.LogModel> List(string app, string host, Models.LogType? type, DateTime? from, DateTime? to, int pageindex, out int pages);
    }
}