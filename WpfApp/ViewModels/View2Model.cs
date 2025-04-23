using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Windows;
using WpfApp.Views;
using CommunityToolkit.Mvvm.Messaging;
using DValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using System.Windows.Input;
using System.Windows.Interop;
using WpfApp;

namespace WpfApp.ViewModels
{
    public partial class View2Model : ObservableValidator
    {

        [ObservableProperty]
        private double mouseX;

        [ObservableProperty]
        private double mouseY;

        [ObservableProperty]
        private double dragX;

        [ObservableProperty]
        private double dragY;

        public View2Model()
        {
            WeakReferenceMessenger.Default.Register<MousePositionMessage>(this, (r, msg) =>
            {
                Console.WriteLine($"Received X={msg.Position.X}, Y={msg.Position.Y}");

                MouseX =  msg.Position.X;
                MouseY =  msg.Position.X;

                if(msg.IsDragging)
                {
                    DragX = msg.Position.X;
                    DragY = msg.Position.Y;
                }

                Settings.Default.point = msg.Position.X;
                ((App)Application.Current).AppSettings.point = msg.Position.X;
            });
        }
    }
}