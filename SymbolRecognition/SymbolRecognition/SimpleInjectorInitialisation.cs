using Caliburn.Micro;
using SimpleInjector;

namespace SymbolRecognition
{
    public static class SimpleInjectorInitialisation
    {
        public static Container Initialize()
        {
            var container = new Container();

            RegisterCaliburnMicro(container);
            RgisterServices(container);

            container.Verify();

            return container;
        }

        private static void RgisterServices(Container container)
        {
            container.Register<IUserService, UserService>();
        }

        private static void RegisterCaliburnMicro(Container container)
        {
            container.RegisterSingleton<IWindowManager, WindowManager>();
            container.RegisterSingleton<IEventAggregator, EventAggregator>();
        }
    }
}