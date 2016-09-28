using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;

namespace SymbolRecognition
{
    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper() {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e) {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}