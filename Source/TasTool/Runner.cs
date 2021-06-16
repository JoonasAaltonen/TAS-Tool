using System;
using TasTool.Interfaces;

namespace TasTool
{
    public class Runner : IRunner
    {
        public Runner()
        {
            Console.WriteLine("Runner created");
        }
        public void Run()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}