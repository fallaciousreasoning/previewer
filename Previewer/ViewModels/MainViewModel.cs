using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Previewer.Annotations;
using Previewer.Core;
using TestApp;

namespace Previewer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public Action<Control> SetContent;

        private string filePath;

        public string FilePath
        {
            get { return filePath; }
            set
            {
                if (value == filePath) return;
                filePath = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            KeyInterceptor.Wait(App.ActivatorKey, HandleActivatePressed);
        }

        private void HandleActivatePressed(KeyStates k)
        {
            if (!Application.Current.MainWindow.IsActive)
            {
                if (!SelectionDetector.SelectedAndExplorerActive()) return;
                
                FilePath = SelectionDetector.SelectedPath();
                Application.Current.MainWindow.Show();

                var control = App.PluginRegistrar.GetPreviewerForFile(FilePath);
                SetContent?.Invoke(control);
            }
            else
                Application.Current.MainWindow.Hide();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
