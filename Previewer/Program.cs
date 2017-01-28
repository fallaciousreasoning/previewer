using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;
using TestApp;

namespace Previewer
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var manager = new SingletonInstanceManager();
            manager.Run(args);
        }
    }

    public class SingletonInstanceManager : WindowsFormsApplicationBase
    {
        private App instance;

        public SingletonInstanceManager()
        {
            IsSingleInstance = true;
        }

        protected override bool OnStartup(StartupEventArgs eventArgs)
        {
            instance = new App();
            instance.Run();

            return false;
        }

        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {
            base.OnStartupNextInstance(eventArgs);

            if (eventArgs.CommandLine.Count > 0)
                (instance.MainWindow as MainWindow).CurrentFile = eventArgs.CommandLine[0];

            instance.MainWindow.Activate();
        }
    }
}
