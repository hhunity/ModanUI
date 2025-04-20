using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using CommunityToolkit.Mvvm.ComponentModel;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// View2.xaml の相互作用ロジック
    /// </summary>
    public partial class View2 : UserControl
    {
        public View2Model ViewModel { get; } = new View2Model();
        private bool isDragging = false;

        public View2()
        {
            InitializeComponent();
            DataContext = ViewModel;

        }
        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(MainGrid);
            ViewModel.MouseX = pos.X;
            ViewModel.MouseY = pos.Y;
            if (isDragging)
            {
                var pos2 = e.GetPosition(MainGrid);
                ViewModel.DragX = pos2.X;
                ViewModel.DragY = pos2.Y;
            }
        }

        private void MainGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            MainGrid.CaptureMouse(); // マウスキャプチャ
        }

        private void MainGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            MainGrid.ReleaseMouseCapture(); // キャプチャ解除
        }
    }


}
