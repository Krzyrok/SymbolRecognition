using Caliburn.Micro;
using SimpleInjector;

namespace View.Configuration
{
    public static class SimpleInjectorInitialisation
    {
        public static Container Initialize()
        {
            var container = new Container();

            RegisterCaliburnMicro(container);

            container.Verify();

            return container;
        }

        private static void RegisterCaliburnMicro(Container container)
        {
            container.RegisterSingleton<IWindowManager, WindowManager>();
            container.RegisterSingleton<IEventAggregator, EventAggregator>();
        }
    }
}