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

        private FolderItem folderItem;

        public string FilePath => SelectedFolderItem?.Path;

        public FolderItem SelectedFolderItem
        {
            get { return folderItem; }
            set
            {
                if (folderItem == value) return;
                
                folderItem = value;

                OnPropertyChanged(nameof(FilePath));
                OnPropertyChanged();
            }
        }

        public readonly ObservableCollection<FolderItem> ItemsInFolder = new ObservableCollection<FolderItem>();

        public MainViewModel()
        {
            KeyInterceptor.Wait(App.ActivatorKey, HandleActivatePressed);
        }

        private void HandleActivatePressed(KeyStates k)
        {
            if (!Application.Current.MainWindow.IsActive)
            {
                if (!SelectionDetector.SelectedAndExplorerActive()) return;
                
                SelectedFolderItem = SelectionDetector.SelectedPath();
                ItemsInFolder.Clear();
                SelectionDetector.GetItemsInSelectedPath().ForEach(ItemsInFolder.Add);

                Application.Current.MainWindow.Show();

                var control = App.PluginRegistrar.GetPreviewerForFile(FilePath);
                SetContent?.Invoke(control);

                Application.Current.MainWindow.Activate();
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
