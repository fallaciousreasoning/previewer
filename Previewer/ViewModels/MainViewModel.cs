using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private FolderHelper folder;

        public FolderHelper Folder
        {
            get { return folder; }
            private set
            {
                if (Equals(value, folder)) return;
                folder = value;
                OnPropertyChanged();
            }
        }

        public string SelectedPath { get { return Folder?.SelectedItem.Path; } set { SetSelectedFile(value); } }

        public MainViewModel()
        {
            KeyInterceptor.Wait(App.ActivatorKey, HandleActivatePressed);
        }

        private void HandleActivatePressed(KeyStates k)
        {
            if (!Application.Current.MainWindow.IsActive)
            {
                if (!SelectionDetector.SelectedAndExplorerActive()) return;

                var selected = SelectionDetector.SelectedPath();
                
                SetSelectedFile(selected?.Path);
                Application.Current.MainWindow.Show();

                Application.Current.MainWindow.Activate();
            }
            else
                Application.Current.MainWindow.Hide();
        }

        public void SetSelectedFile(string path)
        {
            if (path == null) return;

            Folder = new FolderHelper(path);

            var control = App.PluginRegistrar.GetPreviewerForFile(Folder.SelectedItem.Path);
            SetContent?.Invoke(control);

            OnPropertyChanged(nameof(SelectedPath));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
