using System;
using Peanut.Mappings;

namespace Log4Grid.DBModels
{
    ///<summary>
    ///Peanut Generator Copyright @ FanJianHan 2010-2013
    ///website:http://www.ikende.com
    ///</summary>
    [Table("TBL_Application")]
    public partial class DBApplication : DataObject
    {
        private string _mID;
        public static Peanut.FieldInfo<string> iD = new Peanut.FieldInfo<string>("TBL_Application", "ID");
        private string _mName;
        public static Peanut.FieldInfo<string> name = new Peanut.FieldInfo<string>("TBL_Application", "Name");
        private string _mRemark;
        public static Peanut.FieldInfo<string> remark = new Peanut.FieldInfo<string>("TBL_Application", "Remark");

        ///<summary>
        ///Type:string
        ///</summary>
        [ID()]
        [UID]
        public virtual string ID
        {
            get { return _mID; }
            set
            {
                _mID = value;
                EntityState.FieldChange("ID");
            }
        }

        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string Name
        {
            get { return _mName; }
            set
            {
                _mName = value;
                EntityState.FieldChange("Name");
            }
        }

        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string Remark
        {
            get { return _mRemark; }
            set
            {
                _mRemark = value;
                EntityState.FieldChange("Remark");
            }
        }
    }

    ///<summary>
    ///Peanut Generator Copyright @ FanJianHan 2010-2013
    ///website:http://www.ikende.com
    ///</summary>
    [Table("TBL_Host")]
    public partial class DBHost : DataObject
    {
        private string _mID;
        public static Peanut.FieldInfo<string> iD = new Peanut.FieldInfo<string>("TBL_Host", "ID");
        private string _mAppID;
        public static Peanut.FieldInfo<string> appID = new Peanut.FieldInfo<string>("TBL_Host", "AppID");
        private string _mName;
        public static Peanut.FieldInfo<string> name = new Peanut.FieldInfo<string>("TBL_Host", "Name");
        private string _mCpuUsage;
        public static Peanut.FieldInfo<string> cpuUsage = new Peanut.FieldInfo<string>("TBL_Host", "CpuUsage");
        private string _mMemoryUsage;
        public static Peanut.FieldInfo<string> memoryUsage = new Peanut.FieldInfo<string>("TBL_Host", "MemoryUsage");
        private DateTime _mLastActiveTime;
        public static Peanut.FieldInfo<DateTime> lastActiveTime = new Peanut.FieldInfo<DateTime>("TBL_Host", "LastActiveTime");

        ///<summary>
        ///Type:string
        ///</summary>
        [ID()]
        [UID]
        public virtual string ID
        {
            get { return _mID; }
            set
            {
                _mID = value;
                EntityState.FieldChange("ID");
            }
        }

        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string AppID
        {
            get { return _mAppID; }
            set
            {
                _mAppID = value;
                EntityState.FieldChange("AppID");
            }
        }

        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string Name
        {
            get { return _mName; }
            set
            {
                _mName = value;
                EntityState.FieldChange("Name");
            }
        }

        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string CpuUsage
        {
            get { return _mCpuUsage; }
            set
            {
                _mCpuUsage = value;
                EntityState.FieldChange("CpuUsage");
            }
        }

        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string MemoryUsage
        {
            get { return _mMemoryUsage; }
            set
            {
                _mMemoryUsage = value;
                EntityState.FieldChange("MemoryUsage");
            }
        }

        ///<summary>
        ///Type:DateTime
        ///</summary>
        [Column()]
        public virtual DateTime LastActiveTime
        {
            get { return _mLastActiveTime; }
            set
            {
                _mLastActiveTime = value;
                EntityState.FieldChange("LastActiveTime");
            }
        }
    }

    ///<summary>
    ///Peanut Generator Copyright @ FanJianHan 2010-2013
    ///website:http://www.ikende.com
    ///</summary>
    [Table("TBL_Log")]
    public partial class DBLog : DataObject
    {
        private string _mID;
        public static Peanut.FieldInfo<string> iD = new Peanut.FieldInfo<string>("TBL_Log", "ID");
        private string _mHost;
        public static Peanut.FieldInfo<string> host = new Peanut.FieldInfo<string>("TBL_Log", "Host");
        private string _mApp;
        public static Peanut.FieldInfo<string> app = new Peanut.FieldInfo<string>("TBL_Log", "App");
        private DateTime _mCreateTime;
        public static Peanut.FieldInfo<DateTime> createTime = new Peanut.FieldInfo<DateTime>("TBL_Log", "CreateTime");
        private string _mContent;
        public static Peanut.FieldInfo<string> content = new Peanut.FieldInfo<string>("TBL_Log", "LogContent");
        private Models.LogType _mType;
        public static Peanut.FieldInfo<Models.LogType> type = new Peanut.FieldInfo<Models.LogType>("TBL_Log", "Type");

        ///<summary>
        ///Type:string
        ///</summary>
        [ID()]
        [UID]
        public virtual string ID
        {
            get { return _mID; }
            set
            {
                _mID = value;
                EntityState.FieldChange("ID");
            }
        }

        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string Host
        {
            get { return _mHost; }
            set
            {
                _mHost = value;
                EntityState.FieldChange("Host");
            }
        }

        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string App
        {
            get { return _mApp; }
            set
            {
                _mApp = value;
                EntityState.FieldChange("App");
            }
        }

        ///<summary>
        ///Type:DateTime
        ///</summary>
        [Column()]
        public virtual DateTime CreateTime
        {
            get { return _mCreateTime; }
            set
            {
                _mCreateTime = value;
                EntityState.FieldChange("CreateTime");
            }
        }

        ///<summary>
        ///Type:string
        ///</summary>
        [Column("LogContent")]
        public virtual string Content
        {
            get { return _mContent; }
            set
            {
                _mContent = value;
                EntityState.FieldChange("Content");
            }
        }

        ///<summary>
        ///Type:Log4Grid.Models.LogType
        ///</summary>
        [Column()]
        [EnumToInt]
        public virtual Models.LogType Type
        {
            get { return _mType; }
            set
            {
                _mType = value;
                EntityState.FieldChange("Type");
            }
        }
    }

    ///<summary>
    ///Peanut Generator Copyright @ FanJianHan 2010-2013
    ///website:http://www.ikende.com
    ///</summary>
    [Table("TBL_User")]
    public partial class DBUser : DataObject
    {
        private string _mName;
        public static Peanut.FieldInfo<string> name = new Peanut.FieldInfo<string>("TBL_User", "Name");
        private string _mPassword;
        public static Peanut.FieldInfo<string> password = new Peanut.FieldInfo<string>("TBL_User", "User_PWD");
        private string _mEmail;
        public static Peanut.FieldInfo<string> email = new Peanut.FieldInfo<string>("TBL_User", "Email");
        private bool _mEnabled;
        public static Peanut.FieldInfo<bool> enabled = new Peanut.FieldInfo<bool>("TBL_User", "Enabled");

        ///<summary>
        ///Type:string
        ///</summary>
        [ID()]
        public virtual string Name
        {
            get { return _mName; }
            set
            {
                _mName = value;
                EntityState.FieldChange("Name");
            }
        }

        ///<summary>
        ///Type:string
        ///</summary>
        [Column("User_PWD")]
        [StringCrypto("log4grid")]
        public virtual string Password
        {
            get { return _mPassword; }
            set
            {
                _mPassword = value;
                EntityState.FieldChange("Password");
            }
        }

        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string Email
        {
            get { return _mEmail; }
            set
            {
                _mEmail = value;
                EntityState.FieldChange("Email");
            }
        }

        ///<summary>
        ///Type:bool
        ///</summary>
        [Column()]
        [BoolToInt]
        public virtual bool Enabled
        {
            get { return _mEnabled; }
            set
            {
                _mEnabled = value;
                EntityState.FieldChange("Enabled");
            }
        }
    }
}