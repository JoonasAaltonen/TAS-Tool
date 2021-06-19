using System;
using System.Configuration;
using System.IO;
using Autofac;
using TasTool.Autofac;
using TasTool.ConfigElements;
using TasTool.Interfaces;

namespace TasTool
{
    public class TasFactory
    {
        public IInitializer CreateTasInitializer()
        {
            var container = ContainerConfig.Configure();
            var scope = container.BeginLifetimeScope();
            var app = scope.Resolve<IInitializer>();
            return app;
        }
    }
}
