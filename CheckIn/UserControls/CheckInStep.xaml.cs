using System.Windows.Controls;
using System.Windows.Media;

namespace CheckIn.UserControls
{
    /// <summary>
    /// CheckInStep.xaml 的交互逻辑
    /// </summary>
    public partial class CheckInStep : UserControl
    {
        public CheckInStep()
        {
            InitializeComponent();
        }
        private Label[] _btArry;

        public void BtInit()
        {
            _btArry = new Label[] { this.button2, this.button3, this.button4, this.button5, this.button6 };
        }

        public void SetStep(int n)
        {
            if (_btArry == null)
                return;
            if (n < 0 || n > _btArry.Length - 1)
                return;
            Label bt = _btArry[n];
            Label lb = (Label)bt.Content;
            lb.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            this.UpdateLayout();
        }
    }
}
