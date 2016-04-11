using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PreviewerPlugins.Annotations;

namespace PreviewerPlugins.ImagePlugin
{
    public class ImagePreviewViewModel : INotifyPropertyChanged
    {
        private string imageUri;

        public string ImageUri
        {
            get { return imageUri; }
            set
            {
                if (value == imageUri) return;
                imageUri = value;
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
