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
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// View1.xaml の相互作用ロジック
    /// </summary>
    public partial class View1 : UserControl
    {
        public View1Model ViewModel { get; } = new View1Model();

        public View1()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
