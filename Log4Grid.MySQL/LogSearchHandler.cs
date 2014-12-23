using Peanut;

namespace Log4Grid.MySQL
{
    public class LogSearch4MySQL : DataAccess.LogSearchHandlerBase<MySqlDriver>
    {
        protected override bool Exists(string table)
        {
            SQL sql = @"SHOW TABLES LIKE @p1";
            sql = sql["p1", table];
            return sql.GetValue<string>(DB) != null;
        }
    }
}