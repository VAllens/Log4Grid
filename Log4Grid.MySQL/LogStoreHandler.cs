using System.Collections.Generic;
using Peanut;

namespace Log4Grid.MySQL
{
    public class LogStore4MySQL : DataAccess.LogStoreHandlerBase<MySqlDriver>
    {
        private readonly Dictionary<string, string> _mTables = new Dictionary<string, string>();

        protected override bool Exists(string table)
        {
            if (_mTables.ContainsKey(table))
                return true;
            SQL sql = @"SHOW TABLES LIKE @p1";
            sql = sql["p1", table];
            return !string.IsNullOrEmpty(sql.GetValue<string>(DB));
        }

        protected override void OnCreateTable(string table)
        {
            SQL sql = string.Format(@"CREATE TABLE [{0}] (
  [ID] VARCHAR(50), 
  [App] VARCHAR(50), 
  [Host] VARCHAR(50), 
  [Type] INT, 
  [LogContent] TEXT, 
  [CreateTime] DATETIME);

CREATE INDEX [{0}_index_createtime] ON [{0}] ([CreateTime] DESC);", table);
            sql.Execute(DB);
            _mTables.Add(table, table);
        }
    }
}