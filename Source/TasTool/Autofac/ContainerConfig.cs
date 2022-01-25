using Autofac;
using TasTool.Handlers;
using TasTool.InputRecording;
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
            builder.RegisterType<TasMediator>().As<ITasMediator>();
            builder.RegisterType<TrackParserJson>().As<ITrackParserJson>();
            builder.RegisterType<TrackParser>().As<ITrackParser>();
            builder.RegisterType<TasConfig>().As<ITasConfig>();
            builder.RegisterType<InputMapper>().As<IInputMapper>();
            builder.RegisterType<InputWriter>().As<IInputWriter>();
            builder.RegisterType<InputHandler>().As<IInputHandler>();


            return builder.Build();
        }
    }
}