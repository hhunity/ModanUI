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
    public enum TabType
    {
        Tab1,
        Tab2,
        Tab3
    }

    public partial class View1Model : ObservableValidator
    {
        [ObservableProperty]
        [NotifyDataErrorInfo] // 必須！
        [NotifyPropertyChangedFor(nameof(ErrorMessage))]
        [CustomValidation(typeof(View1Model), nameof(ValidateAge))]
        private string age = "";

        [ObservableProperty]
        [NotifyDataErrorInfo] // 必須！
        [NotifyPropertyChangedFor(nameof(ErrorMessage))]
        [CustomValidation(typeof(View1Model), nameof(ValidateScore))]
        private string score = "";

        [ObservableProperty]
        private string message = "";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsAdvancedOptionsEnabled))]
        private bool enableAdvanced;

        // AdvancedOption用
        [ObservableProperty]
        private bool advancedOption1;

        [ObservableProperty]
        private bool advancedOption2;

        // 子スイッチの有効/無効を制御するプロパティ
        public bool IsAdvancedOptionsEnabled => EnableAdvanced;


        public View1Model()
        {
            PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(Age))
                {
                    Debug.WriteLine($"[PropertyChanged] Name = {Score}");
                }
            };
        }

        public static DValidationResult? ValidateAge(string value, ValidationContext context)
        {
            if (!int.TryParse(value, out var num))
                return new DValidationResult("年齢は数字で入力してください");
            if (num < 1 || num > 100)
                return new DValidationResult("年齢は1～100の間で入力してください");
            return DValidationResult.Success;
        }

        public static DValidationResult? ValidateScore(string value, ValidationContext context)
        {
            if (!int.TryParse(value, out var num))
                return new DValidationResult("スコアは数字で入力してください");
            if (num < 0 || num > 10)
                return new DValidationResult("スコアは0～10の間で入力してください");
            return DValidationResult.Success;
        }

        public string ErrorMessage
        {
            get
            {
                var allErrors = new[] { nameof(Age), nameof(Score) }
                    .SelectMany(p => GetErrors(p)?.Cast<object>() ?? Enumerable.Empty<object>())
                    .Select(e => e?.ToString())
                    .Where(msg => !string.IsNullOrWhiteSpace(msg));

                return string.Join("\n", allErrors);
            }
        }

        [RelayCommand]
        private void Submit()
        {
            ValidateAllProperties(); // 手動でバリデーション
            OnPropertyChanged(nameof(ErrorMessage)); // エラーメッセージ更新

            if (!HasErrors)
            {
                Message = "送信成功！";
            }
            else
            {
                Message = ""; // 成功時だけ表示したい場合
            }
        }
    }

}