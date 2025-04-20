using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using WpfApp.Views;

using DValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using System.Windows.Input;

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
    }
}