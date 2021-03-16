using PathEditor.Models;
using PathEditor.ModelViews;
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
using System.Windows.Shapes;

namespace PathEditor
{
    /// <summary>
    /// Interaction logic for MainWindow1.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {
        public MainWindow1()
        {

            DataContext = new PathEditorViewModel(new PathRepository());

            Loaded += MainWindow1_Loaded;
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            txt.Text = @"e:\Documents\Visual Studio 2017\Projects\sunamo.cz\";
        }
    }
}