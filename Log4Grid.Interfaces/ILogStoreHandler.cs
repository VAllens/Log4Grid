namespace Log4Grid.Interfaces
{
    public interface ILogStoreHandler
    {
        void Add(Models.LogModel e);
        void Clean(string app, string host);
        void Backup(string app, string host);
    }
}