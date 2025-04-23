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
using CommunityToolkit.Mvvm.Messaging;

namespace WpfApp.Views
{
    public class MousePositionMessage
    {
        public Point Position { get; }
        public bool IsDragging { get; set; } = false; // ドラッグ中かどうか

        public MousePositionMessage(Point position,bool bdrag)
        {
            Position = position;
            IsDragging = bdrag;
        }
    }

    /// <summary>
    /// View2.xaml の相互作用ロジック
    /// </summary>
    public partial class View2 : UserControl
    {
        //private View2Model ViewModel => (View2Model)DataContext;
        private bool isDragging = false;

        public View2(View2Model ViewModel)
        {
            InitializeComponent();
            DataContext = ViewModel;

        }
        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(MainGrid);
            WeakReferenceMessenger.Default.Send(new MousePositionMessage(pos,isDragging));
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
