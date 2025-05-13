using NLog;
using NLog.Targets;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

[Target("RichTextBox")]
public sealed class RichTextBoxTarget : TargetWithLayout
{
    public static RichTextBox RichTextBoxControl { get; set; }

    protected override void Write(LogEventInfo logEvent)
    {
        if (RichTextBoxControl == null)
            return;

        string logMessage = this.Layout.Render(logEvent);

        RichTextBoxControl.Dispatcher.Invoke(() =>
        {
            RichTextBoxControl.AppendText(logMessage + "\n");
            RichTextBoxControl.ScrollToEnd();
        });
    }
}

using NLog;
using NLog.Config;
using NLog.Targets;

// 例えば MainWindow.xaml.cs の Loaded イベントで
private void Window_Loaded(object sender, RoutedEventArgs e)
{
    // RichTextBox をターゲットに関連付け
    RichTextBoxTarget.RichTextBoxControl = this.MyRichTextBox;

    // NLogの設定をコードで行う
    var config = new LoggingConfiguration();

    var rtbTarget = new RichTextBoxTarget
    {
        Name = "rtb",
        Layout = "${longdate} [${level}] ${message}"
    };

    config.AddTarget(rtbTarget);
    config.AddRuleForAllLevels(rtbTarget);

    LogManager.Configuration = config;
}

