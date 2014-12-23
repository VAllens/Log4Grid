using Peanut;

namespace Log4Grid.MSSQL
{
    public class LogSearch4Mssql : DataAccess.LogSearchHandlerBase<Peanut.MSSQL>
    {
        protected override bool Exists(string table)
        {
            SQL sql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + table + "]') AND type in (N'U')";
            return sql.GetValue<long>(DB) > 0;
        }
    }
}