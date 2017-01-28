using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using PreviewerPlugins.Annotations;

namespace Previewer.Core
{
    public class FolderHelper : INotifyPropertyChanged
    {
        private FolderItem selectedItem;

        public string FolderPath { get; private set; }

        public FolderItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (Equals(value, selectedItem)) return;
                
                Load(SelectedItem.Path);
            }
        }

        public ObservableCollection<FolderItem> FolderContents { get; } = new ObservableCollection<FolderItem>();

        public FolderHelper(string filePath)
        {
            Load(filePath);
        }

        private void Load(string filePath)
        {
            if (filePath == null) return;
            
            FolderPath = Path.GetDirectoryName(filePath);
            this.selectedItem = FromPath(filePath);

            foreach (var entry in Directory.GetFileSystemEntries(FolderPath))
            {
                FolderContents.Add(FromPath(entry));
            }

            OnPropertyChanged(nameof(FolderPath));
            OnPropertyChanged(nameof(SelectedItem));
        }

        private FolderItem FromPath(string path)
        {
            if (path == null) return null;

            if (Directory.Exists(path))
            {
                var dInfo = new DirectoryInfo(path);
                return new FolderItem()
                {
                    IsFolder = true,
                    ModifiedDate = dInfo.LastWriteTime,
                    Name = dInfo.Name,
                    Path = path,
                };
            }

            var fInfo = new FileInfo(path);
            return new FolderItem()
            {
                IsFolder = false,
                ModifiedDate = fInfo.LastWriteTime,
                Name = fInfo.Name,
                Path = fInfo.FullName,
                Size = fInfo.Length             
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
