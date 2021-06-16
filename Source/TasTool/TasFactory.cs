using System;
using Autofac;
using TasTool.Autofac;
using TasTool.Interfaces;

namespace TasTool
{
    public class TasFactory
    {
        public IRunner CreateTasRunner()
        {
            var container = ContainerConfig.Configure();
            var scope = container.BeginLifetimeScope();
            var app = scope.Resolve<IRunner>();
            return app;
        }
    }
}
