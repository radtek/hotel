using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckIn.UserControls
{
    /// <summary>
    /// CheckOutStep.xaml 的交互逻辑
    /// </summary>
    public partial class CheckOutStep : UserControl
    {
        public CheckOutStep()
        {
            InitializeComponent();
        }

        private Label[] _btnArray;

        public void BtInit()
        {
            _btnArray = new Label[] { this.button1, this.button2 };
        }
        public void SetStep(int n)
        {
            if (_btnArray == null)
            {
                return;
            }
            if (n < 0 || n > _btnArray.Length - 1)
            {
                return;
            }
            Label bt = _btnArray[n];
            Label lb = (Label)bt.Content;
            lb.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            this.UpdateLayout();
        }
    }
}
