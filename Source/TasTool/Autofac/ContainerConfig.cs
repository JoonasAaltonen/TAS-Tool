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
            builder.RegisterType<Runner>().As<IRunner>();
            builder.RegisterType<TrackParser>().As<ITrackParser>();


            return builder.Build();
        }
    }
}