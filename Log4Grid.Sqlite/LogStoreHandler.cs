using Peanut;

namespace Log4Grid.Sqlite
{
    public class LogStore4Sqlite : DataAccess.LogStoreHandlerBase<SqliteDriver>
    {
        protected override bool Exists(string table)
        {
            SQL sql = "SELECT count(*) FROM sqlite_master WHERE name =@p1 and type='table'";
            sql = sql["p1", table];
            return sql.GetValue<long>(DB) > 0;
        }

        protected override void OnCreateTable(string table)
        {
            SQL sql = string.Format(@"CREATE TABLE [{0}] (
  [ID] VARCHAR(50), 
  [App] VARCHAR(50), 
  [Host] VARCHAR(50), 
  [Type] INT, 
  [Content] TEXT, 
  [CreateTime] DATETIME);

CREATE INDEX [{0}_index_createtime] ON [{0}] ([CreateTime] DESC);", table);
            sql.Execute(DB);
        }
    }
}