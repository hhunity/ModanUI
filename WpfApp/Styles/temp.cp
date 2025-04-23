MainWindow.xaml：ボタンと複数のグラフビュー
	•	MainViewModel.cs：状態を管理（例：Syncモードかどうか）
	•	SubPlotView.xaml（UserControl）：個々のグラフビュー
	•	SubPlotViewModel.cs：各グラフの個別ロジック（共通 ViewModel を参照）


① MainViewModel.cs（状態の中心）
public enum InteractionMode { None, SyncScroll, Zoom }

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private InteractionMode currentMode = InteractionMode.None;

    // SubPlotViewModel にも共有させたい場合：
    public ObservableCollection<SubPlotViewModel> SubPlots { get; } = new();

    public MainViewModel()
    {
        for (int i = 0; i < 3; i++)
            SubPlots.Add(new SubPlotViewModel(this)); // this を渡すことで状態共有
    }

    [RelayCommand]
    private void EnableSyncMode()
    {
        CurrentMode = InteractionMode.SyncScroll;
    }

    [RelayCommand]
    private void EnableZoomMode()
    {
        CurrentMode = InteractionMode.Zoom;
    }
}

② MainWindow.xaml（上にボタン・下に SubPlotView を並べる）
<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:views="clr-namespace:WpfApp.Views"
        xmlns:vm="clr-namespace:WpfApp.ViewModels"
        Title="MainWindow">

    <Window.DataContext>
        <vm:MainViewModel />
    </Window.DataContext>

    <DockPanel>
        <StackPanel Orientation="Horizontal" DockPanel.Dock="Top">
            <Button Content="同期スクロール"
                    Command="{Binding EnableSyncModeCommand}" />
            <Button Content="ズーム"
                    Command="{Binding EnableZoomModeCommand}" />
        </StackPanel>

        <ItemsControl ItemsSource="{Binding SubPlots}">
            <ItemsControl.ItemTemplate>
                <DataTemplate>
                    <views:SubPlotView />
                </DataTemplate>
            </ItemsControl.ItemTemplate>
        </ItemsControl>
    </DockPanel>
</Window>

③ SubPlotView.xaml（グラフビュー本体）

<UserControl x:Class="WpfApp.Views.SubPlotView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:scott="clr-namespace:ScottPlot.WPF;assembly=ScottPlot.WPF">
    <Grid>
        <scott:WpfPlot x:Name="MyPlot"
                       MouseMove="MyPlot_MouseMove"/>
    </Grid>
</UserControl>


④ SubPlotView.xaml.cs（ここで ViewModel を利用）
public partial class SubPlotView : UserControl
{
    private SubPlotViewModel ViewModel => (SubPlotViewModel)DataContext;

    public SubPlotView()
    {
        InitializeComponent();
        this.DataContextChanged += (s, e) =>
        {
            ViewModel.Plot = MyPlot.Plot;
            ViewModel.Render(); // 初期描画
        };
    }

    private void MyPlot_MouseMove(object sender, MouseEventArgs e)
    {
        if (ViewModel?.Parent?.CurrentMode == InteractionMode.SyncScroll)
        {
            var mouse = e.GetPosition(MyPlot);
            double mouseX = MyPlot.Plot.GetCoordinateX((float)mouse.X);
            ViewModel.HandleMouseMove(mouseX);
        }
    }
}

⑤ SubPlotViewModel.cs（1つのプロット用のロジック）

public class SubPlotViewModel : ObservableObject
{
    public InteractionMode CurrentMode => Parent.CurrentMode;

    public MainViewModel Parent { get; }
    public ScottPlot.Plot Plot { get; set; }

    public SubPlotViewModel(MainViewModel parent)
    {
        Parent = parent;
    }

    public void HandleMouseMove(double x)
    {
        // 例えば縦線移動など
        Plot.Clear();
        Plot.AddVerticalLine(x, color: System.Drawing.Color.Red);
        Plot.Render();
    }

    public void Render()
    {
        double[] xs = Enumerable.Range(0, 100).Select(i => (double)i).ToArray();
        double[] ys = xs.Select(x => Math.Sin(x * 0.1)).ToArray();
        Plot.AddScatter(xs, ys);
        Plot.Render();
    }
}




