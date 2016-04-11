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

namespace PreviewerPlugins.TextPlugin
{
    /// <summary>
    /// Interaction logic for TextPreviewer.xaml
    /// </summary>
    public partial class TextPreviewer : UserControl
    {
        public TextPreviewer()
        {
            InitializeComponent();
        }

        public TextPreviewerViewModel ViewModel { get { return DataContext as TextPreviewerViewModel; } }
    }
}
