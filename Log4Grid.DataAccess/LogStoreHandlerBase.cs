﻿using System.Security.Cryptography;
using System.Text;
using Log4Grid.DBModels;
using Peanut;

namespace Log4Grid.DataAccess
{
    public abstract class LogStoreHandlerBase<T> : Interfaces.ILogStoreHandler where T : IDriver, new()
    {
        public virtual DB DB
        {
            get { return DB.DB104; }
        }

        private string _mConnectionString;

        public string ConnectionString
        {
            get { return _mConnectionString; }
            set
            {
                _mConnectionString = value;
                DBContext.LoadEntityByAssembly(typeof (DBHost).Assembly);
                DBContext.SetConnectionDriver<T>(DB);
                DBContext.SetConnectionString(DB, value);
            }
        }

        protected abstract bool Exists(string table);
        protected abstract void OnCreateTable(string table);
        private static readonly object MLockCreateTable = new object();

        private void CreateTabel(string name)
        {
            lock (MLockCreateTable)
            {
                if (!Exists(name))
                {
                    OnCreateTable(name);
                }
            }
        }

        public string MD5Encoding(string rawPass)
        {
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(rawPass);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hs)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public void Add(Models.LogModel e)
        {
            string name = "tbl_" + MD5Encoding(e.App);
            CreateTabel(name);
            using (DBContext.ChangeTable<DBLog>(name))
            {
                DBLog log = new DBLog();
                log.App = e.App;
                log.Content = e.Content;
                log.CreateTime = e.CreateTime;
                log.Host = e.Host;
                log.Type = e.Type;
                log.Save(DB);
            }
        }

        public void Clean(string app, string host)
        {
            string name = "tbl_" + MD5Encoding(app);
            if (!Exists(name))
                return;
            SQL sql = "delete from " + name + " where App=@p1";
            sql = sql["p1", app];
            if (!string.IsNullOrEmpty(host))
            {
                sql += " and Host=@p2";
                sql = sql["p2", host];
            }
            sql.Execute(DB);
        }

        public void Backup(string app, string host)
        {
        }
    }
}