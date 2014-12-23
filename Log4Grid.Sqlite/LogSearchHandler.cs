﻿using Peanut;

namespace Log4Grid.Sqlite
{
    public class LogSearch4Sqlite : DataAccess.LogSearchHandlerBase<SqliteDriver>
    {
        protected override bool Exists(string table)
        {
            SQL sql = "SELECT count(*) FROM sqlite_master WHERE name =@p1 and type='table'";
            sql = sql["p1", table];
            return sql.GetValue<long>(DB) > 0;
        }
    }
}