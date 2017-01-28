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
using Previewer.Core;
using Previewer.ViewModels;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainViewModel ViewModel => DataContext as MainViewModel;

        public string CurrentFile
        {
            get { return ViewModel.SelectedPath; }
            set { ViewModel.SelectedPath = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            ViewModel.SetContent = SetContent;
        }

        private void SetContent(Control control)
        {
            content.Children.Clear();

            if (control == null) return;
            content.Children.Add(control);
        }
    }
}
