using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using log4net.Util;

namespace Log4Grid.Appender
{
    public class Log4GridAppender : log4net.Appender.AppenderSkeleton
    {
        private readonly byte[] _mBuffer = new Byte[1024*64];

        private readonly CpuCounter _mCpuCounter;

        private readonly Process _mProcess;

        private System.Threading.Timer _mTimer;

        public Log4GridAppender()
        {
            _mProcess = Process.GetCurrentProcess();
            _mCpuCounter = new CpuCounter(_mProcess.ProcessName);
            System.Threading.ThreadPool.QueueUserWorkItem(OnSend);
            _mTimer = new System.Threading.Timer(OnStat, null, 1000, 1000);
        }

        private IPEndPoint _mServerPoint;

        private Socket[] _mSockets;

        private long _mIndex;

        public Socket Client
        {
            get
            {
                if (_mSockets == null)
                {
                    _mServerPoint = new IPEndPoint(IPAddress.Parse(Host), Port);
                    _mSockets = new Socket[10];
                    for (int i = 0; i < 10; i++)
                    {
                        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 0);
                        this._mSockets[i] = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        this._mSockets[i].Bind(ipep);
                    }
                }
                long index = System.Threading.Interlocked.Increment(ref _mIndex);
                return _mSockets[index%_mSockets.Length];
            }
        }

        public string Host { get; set; }

        public int Port { get; set; }

        public string ServerName { get; set; }

        public string AppName { get; set; }

        private readonly Queue<object> _mLogQueue = new Queue<object>(1024*5);

        private void Push(object log)
        {
            lock (this)
            {
                _mLogQueue.Enqueue(log);
            }
        }

        private object Pop()
        {
            lock (this)
            {
                if (_mLogQueue.Count > 0)
                    return _mLogQueue.Dequeue();
                return null;
            }
        }

        private void OnSend(object state)
        {
            while (true)
            {
                try
                {
                    object message = Pop();
                    if (message != null)
                    {
                        ArraySegment<byte> data = Models.ProtobufPacket.Serialize(message, _mBuffer);
                        int start = data.Offset;
                        int sends = data.Count;
                        while (sends > 0)
                        {
                            int count = Client.SendTo(data.Array, start, sends, SocketFlags.None, _mServerPoint);
                            start += count;
                            sends -= count;
                        }
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }

                catch (Exception e)
                {
                    LogLog.Error(typeof (Log4GridAppender), e.Message);
                }
            }
        }

        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            try
            {
                Models.LogModel log = new Models.LogModel
                {
                    App = this.AppName,
                    Host = ServerName,
                    Content = loggingEvent.RenderedMessage,
                    CreateTime = DateTime.Now
                };
                if (loggingEvent.Level == log4net.Core.Level.Debug)
                {
                    log.Type = Models.LogType.Debug;
                }
                else if (loggingEvent.Level == log4net.Core.Level.Info)
                {
                    log.Type = Models.LogType.Info;
                }
                else if (loggingEvent.Level == log4net.Core.Level.Warn)
                {
                    log.Type = Models.LogType.Warn;
                }
                else if (loggingEvent.Level == log4net.Core.Level.Error)
                {
                    log.Type = Models.LogType.Error;
                }
                else if (loggingEvent.Level == log4net.Core.Level.Fatal)
                {
                    log.Type = Models.LogType.Fatal;
                }
                else
                {
                    log.Type = Models.LogType.Info;
                }

                Push(log);
            }
            catch (Exception e)
            {
                LogLog.Error(typeof (Log4GridAppender), e.Message);
            }
        }

        private void OnStat(object state)
        {
            Models.StatModel sm = new Models.StatModel();
            sm.MemoryUsage = string.Format("{0:0.00}MB", (double) _mProcess.WorkingSet64/1024/1024);
            sm.CpuUsage = string.Format("{0:00}%", _mCpuCounter.ProcessUsage(_mProcess.Id));
            sm.App = AppName;
            sm.Host = ServerName;
            Push(sm);
        }
    }

    internal class CpuCounter : IDisposable
    {
        public CpuCounter(string processName)
        {
            _mProcessName = System.IO.Path.GetFileNameWithoutExtension(processName);
            _mFileName = processName;
            _mTimer = new System.Threading.Timer(GetUsage, null, 1000, 1000);
        }

        private string _mFileName;

        private readonly System.Threading.Timer _mTimer;

        private readonly string _mProcessName;

        private readonly Dictionary<int, float> _mProcessCpuUsage = new Dictionary<int, float>();

        private readonly List<CounterItem> _mCounters = new List<CounterItem>();

        private readonly Dictionary<string, int> _mProcessIDs = new Dictionary<string, int>();

        private static readonly object Objs = new object();

        public float ProcessUsage(int pid)
        {
            float result = 0;
            _mProcessCpuUsage.TryGetValue(pid, out result);
            return result;
        }

        private CounterItem OnCreateCounter(string processname)
        {
            CounterItem item = _mCounters.Find(e => e.ProcessName == processname);
            if (item == null)
            {
                item = new CounterItem();
                item.ProcessName = processname;
                item.Counter = new PerformanceCounter("Process", "% Processor Time");
                item.Counter.InstanceName = processname;
                _mCounters.Add(item);
            }
            item.Enabled = true;
            return item;
        }

        private void GetUsage(object state)
        {
            _mTimer.Change(-1, -1);
            _mProcessIDs.Clear();
            Process[] ps = Process.GetProcessesByName(_mProcessName);
            List<CounterItem> disposeditems = new List<CounterItem>();
            if (ps.Length > 0)
            {
                lock (Objs)
                {
                    if (!_mProcessIDs.ContainsKey(ps[0].ProcessName))
                    {
                        _mProcessIDs.Add(ps[0].ProcessName, ps[0].Id);
                    }
                }
                OnCreateCounter(ps[0].ProcessName).Pid = ps[0].Id;
                for (int i = 1; i < ps.Length; i++)
                {
                    lock (Objs)
                    {
                        if (!_mProcessIDs.ContainsKey(ps[i].ProcessName))
                        {
                            _mProcessIDs.Add(ps[i].ProcessName + "#" + i, ps[i].Id);
                        }
                    }
                    OnCreateCounter(ps[i].ProcessName + "#" + i).Pid = ps[i].Id;
                }
            }
            foreach (CounterItem item in _mCounters)
            {
                if (item.Enabled)
                {
                    try
                    {
                        _mProcessCpuUsage[_mProcessIDs[item.ProcessName]] = item.Counter.NextValue();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    item.Enabled = false;
                }
            }
            _mTimer.Change(1000, 1000);
        }

        private class CounterItem
        {
            public string ProcessName { get; set; }

            public PerformanceCounter Counter { get; set; }

            public bool Enabled { get; set; }
            public int Pid { get; set; }
        }

        public void Dispose()
        {
            if (_mTimer != null)
                _mTimer.Dispose();
        }
    }
}