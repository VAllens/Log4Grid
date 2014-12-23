using System;

namespace Log4Grid.Service.ConsoleApp
{
    internal class Program
    {
        private static LogServer _mServer;

        private static void Main(string[] args)
        {
            _mServer = new LogServer();
            _mServer.Open();
            Console.Read();
        }
    }
}