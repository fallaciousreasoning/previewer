using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PreviewerPlugins.ImagePlugin
{
    /// <summary>
    /// Interaction logic for ImagePreviewer.xaml
    /// </summary>
    public partial class ImagePreviewer : UserControl
    {
        public ImagePreviewViewModel ViewModel { get { return DataContext as ImagePreviewViewModel; } }

        public ImagePreviewer()
        {
            InitializeComponent();
        }
    }
}
