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

namespace WpfApp.ViewModels
{

    public partial class MainViewModel : ObservableValidator
    {
        //public View2Model SharedView2Model { get; } = new View2Model();

        private readonly View2 _view2;
        private readonly View1 _view1;

        public MainViewModel(View1 view1, View2 view2)
        {
            // 保持しておきたい場合だけフィールドに保存
            _view1 = view1;
            _view2 = view2;

            // 最初の画面を表示
            CurrentView = view1;
        }

        public enum TabKey { View1, View2 , ViewSetting}

        // 今表示中のタブを表す列挙型
        [ObservableProperty]
        private TabKey selectedTab = TabKey.View1;

        [ObservableProperty]
        private UserControl currentView; // ← 最初にView1を表示

        [RelayCommand]
        private void ShowView1()
        {
            CurrentView = _view1;
            SelectedTab = TabKey.View1;
        }

        [RelayCommand]
        private void ShowView2()
        {
            CurrentView = _view2;
        }

        [RelayCommand]
        private void ShowSettingView()
        {
            CurrentView = new Views.ViewSetting();
            SelectedTab = TabKey.ViewSetting;
        }
    }

}