using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ABS_Dental_Pro
{
    public static class MessageBoxCustom
    {
        public static void Show(string message)
        {
            MessageBoxForm messageBoxForm = new MessageBoxForm(message);
            messageBoxForm.ShowDialog();
        }

        public static void Show(string message, string title)
        {
            MessageBoxForm messageBoxForm = new MessageBoxForm(message, title);
            messageBoxForm.ShowDialog();
        }

        public static bool? Show(string message, string title, MessageBoxButton msgBtn)
        {
            MessageBoxForm messageBoxForm = new MessageBoxForm(message, title, msgBtn);
            return messageBoxForm.ShowDialog();
        }

        public static void Show(string message, string title, MessageBoxButton msgBtn, MessageBoxIcon icon)
        {
            MessageBoxForm messageBoxForm = new MessageBoxForm(message, title, msgBtn, icon);
            messageBoxForm.ShowDialog();
        }
    }
}
