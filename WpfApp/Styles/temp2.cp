<!-- MainWindow.xaml -->
<Window x:Class="MyApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:MyApp"
        Title="UniformGrid Example" Height="400" Width="600">
    <Window.DataContext>
        <local:MainViewModel />
    </Window.DataContext>
    
    <StackPanel>
        <StackPanel Orientation="Horizontal" HorizontalAlignment="Center" Margin="0,10">
            <Button Content="Set A" Command="{Binding LoadSetACommand}" Margin="5"/>
            <Button Content="Set B" Command="{Binding LoadSetBCommand}" Margin="5"/>
        </StackPanel>

        <ItemsControl ItemsSource="{Binding ChildViewModels}">
            <ItemsControl.ItemsPanel>
                <ItemsPanelTemplate>
                    <UniformGrid Rows="2" Columns="3" />
                </ItemsPanelTemplate>
            </ItemsControl.ItemsPanel>
            <ItemsControl.ItemTemplate>
                <DataTemplate DataType="{x:Type local:ChildViewModel}">
                    <local:ChildView />
                </DataTemplate>
            </ItemsControl.ItemTemplate>
        </ItemsControl>
    </StackPanel>
</Window>

<!-- ChildView.xaml -->
<UserControl x:Class="MyApp.ChildView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:MyApp"
             Width="100" Height="100">
    <Border BorderBrush="Gray" BorderThickness="1" Margin="5">
        <TextBlock Text="{Binding Title}" 
                   HorizontalAlignment="Center" 
                   VerticalAlignment="Center" 
                   FontSize="16"/>
    </Border>
</UserControl>


// MainViewModel.cs
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MyApp
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<ChildViewModel> ChildViewModels { get; } = new();

        [RelayCommand]
        private void LoadSetA()
        {
            ChildViewModels.Clear();
            for (int i = 0; i < 6; i++)
                ChildViewModels.Add(new ChildViewModel($"Set A - {i+1}"));
        }

        [RelayCommand]
        private void LoadSetB()
        {
            ChildViewModels.Clear();
            for (int i = 0; i < 4; i++)
                ChildViewModels.Add(new ChildViewModel($"Set B - {i+1}"));
        }
    }
}

// ChildViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;

namespace MyApp
{
    public partial class ChildViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title;

        public ChildViewModel(string title)
        {
            Title = title;
        }
    }
}


