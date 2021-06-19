using Autofac;
using TasTool.Handlers;
using TasTool.Interfaces;
using TasTool.Track;

namespace TasTool.Autofac
{
    internal static class ContainerConfig
    {
        public static IContainer Configure()
        {
            // Tsek https://www.youtube.com/watch?v=mCUNrRtVVWY for info

            var builder = new ContainerBuilder();

            builder.RegisterType<WindowHandler>().As<IWindowHandler>();
            // Singleton ?? builder.RegisterType<WindowHandler>().As<IWindowHandler>().SingleInstance();
            builder.RegisterType<Initializer>().As<IInitializer>();
            builder.RegisterType<TrackParser>().As<ITrackParser>();
            builder.RegisterType<TasConfig>().As<ITasConfig>();


            return builder.Build();
        }
    }
}