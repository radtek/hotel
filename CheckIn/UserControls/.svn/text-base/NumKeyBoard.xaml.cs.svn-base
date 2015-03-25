using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// NumKeyBoard.xaml 的交互逻辑
    /// </summary>
    public partial class NumKeyBoard : UserControl
    {
         public NumKeyBoard()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const uint KEYEVENTF_KEYUP = 0x2;
        private const byte VK_BACK = 0x8;
        private const byte VK_0 = 0x30;
        private const byte VK_1 = 0x31;
        private const byte VK_2 = 0x32;
        private const byte VK_3 = 0x33;
        private const byte VK_4 = 0x34;
        private const byte VK_5 = 0x35;
        private const byte VK_6 = 0x36;
        private const byte VK_7 = 0x37;
        private const byte VK_8 = 0x38;
        private const byte VK_9 = 0x39;

        private void bt_1_Click(object sender, RoutedEventArgs e)
        {
            keybd_event(VK_1, 0, 0, (UIntPtr)0);
            keybd_event(VK_1, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
        }

       

        private void bt_2_Click(object sender, RoutedEventArgs e)
        {
            keybd_event(VK_2, 0, 0, (UIntPtr)0);
            keybd_event(VK_2, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
        }

        private void bt_3_Click(object sender, RoutedEventArgs e)
        {
            keybd_event(VK_3, 0, 0, (UIntPtr)0);
            keybd_event(VK_3, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
        }

        private void bt_4_Click(object sender, RoutedEventArgs e)
        {
            keybd_event(VK_4, 0, 0, (UIntPtr)0);
            keybd_event(VK_4, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
        }

        private void bt_5_Click(object sender, RoutedEventArgs e)
        {
            keybd_event(VK_5, 0, 0, (UIntPtr)0);
            keybd_event(VK_5, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
        }

        private void bt_6_Click(object sender, RoutedEventArgs e)
        {
            keybd_event(VK_6, 0, 0, (UIntPtr)0);
            keybd_event(VK_6, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
        }

        private void bt_7_Click(object sender, RoutedEventArgs e)
        {
            keybd_event(VK_7, 0, 0, (UIntPtr)0);
            keybd_event(VK_7, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
        }

        private void bt_8_Click(object sender, RoutedEventArgs e)
        {
            keybd_event(VK_8, 0, 0, (UIntPtr)0);
            keybd_event(VK_8, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
        }

        private void bt_9_Click(object sender, RoutedEventArgs e)
        {
            keybd_event(VK_9, 0, 0, (UIntPtr)0);
            keybd_event(VK_9, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
        }

        private void bt_cancel_Click(object sender, RoutedEventArgs e)
        {
            keybd_event(VK_BACK, 0, 0, (UIntPtr)0);
            keybd_event(VK_BACK, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
        }

        private void bt_0_Click(object sender, RoutedEventArgs e)
        {
            keybd_event(VK_0, 0, 0, (UIntPtr)0);
            keybd_event(VK_0, 0, KEYEVENTF_KEYUP, (UIntPtr)0);
        }

        private void bt_ok_Click(object sender, RoutedEventArgs e)
        {
           this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
