using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using SimpleInjector;
using View.ViewModels;

namespace View.Configuration
{
    public class AppBootstrapper : BootstrapperBase
    {
        private Container _container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = SimpleInjectorInitialisation.Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e) {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            IServiceProvider provider = _container;
            Type collectionType = typeof(IEnumerable<>).MakeGenericType(service);
            var services = (IEnumerable<object>) provider.GetService(collectionType);

            return services ?? Enumerable.Empty<object>();
        }

        protected override void BuildUp(object instance)
        {
            var instanceProducer = _container.GetRegistration(instance.GetType(), true);
            instanceProducer.Registration.InitializeInstance(instance);
        }
    }
}