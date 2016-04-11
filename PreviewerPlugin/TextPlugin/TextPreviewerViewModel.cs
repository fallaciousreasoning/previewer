using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PreviewerPlugins.Annotations;

namespace PreviewerPlugins.TextPlugin
{
    public class TextPreviewerViewModel : INotifyPropertyChanged
    {
        private string text;
        private string filePath;

        public string Text
        {
            get { return text; }
            set
            {
                if (value == text) return;
                text = value;
                OnPropertyChanged();
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
