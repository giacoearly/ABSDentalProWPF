using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ABS_Dental_Pro
{
    /// <summary>
    /// Interaction logic for MessageBoxCustom.xaml
    /// </summary>
    public partial class MessageBoxFormOver : Window
    {
        public MessageBoxFormOver()
        {
            InitializeComponent();
        }

        public MessageBoxFormOver(string message)
        {
            InitializeComponent();

            this.Owner = Application.Current.MainWindow;
            Thickness marginiButonOK = new Thickness(300, 0, 0, 0);
            btnOK.Margin = marginiButonOK;
            btnOK.Focus();

            btnNu.Visibility = Visibility.Hidden;

            tbMessage.Text = message;

            AdjustWindowWidth(message.Length);
        }

        public MessageBoxFormOver(string message, string title)
        {
            InitializeComponent();

            //if (Application.Current != null)
            //{
                //this.Owner = Application.Current.MainWindow;
                //this.Owner = this;
                //this.Topmost = true;
            //}
            Thickness marginiButonOK = new Thickness(300, 0, 0, 0);
            btnOK.Margin = marginiButonOK;
            btnOK.Focus();

            btnNu.Visibility = Visibility.Hidden;

            tbMessage.Text = message;

            AdjustWindowWidth(message.Length);

            this.Title = title;
        }

        public MessageBoxFormOver(string message, string title, MessageBoxButton msgBtn)
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            Thickness marginiButonOK = new Thickness(200, 0, 0, 0);
            btnOK.Margin = marginiButonOK;
            btnOK.IsDefault = false;
            btnOK.IsCancel = false;

            btnNu.Visibility = Visibility.Visible;
            btnNu.IsDefault = true;
            btnNu.IsCancel = true;
            btnNu.Focus();
            if (msgBtn == MessageBoxButton.YesNo)
            {
                tbkOK.Text = "Da";
                tbkNu.Text = "Nu";
            }

            tbMessage.Text = message;

            AdjustWindowWidth(message.Length);

            this.Title = title;
        }

        private void AdjustWindowWidth(int length)
        {
            if (length <= 30)
            {
                Thickness marginiButonOK = new Thickness(150, 0, 0, 0);
                btnOK.Margin = marginiButonOK;
                return;
            }
            else
            if ((length > 55) && (length <= 60))
            {
                this.Width = 500;
            }
            else
            if ((length > 60) && (length <= 65))
            {
                this.Width = 550;
            }
            else
            if ((length > 65) && (length <= 70))
            {
                this.Width = 600;
            }
            else
            if ((length > 70) && (length <= 75))
            {
                this.Width = 650;
            }
            else
            if ((length > 75) && (length <= 80))
            {
                this.Width = 700;
            }
            else
            if ((length > 80) && (length <= 85))
            {
                this.Width = 750;
            }
            else
            if ((length > 85) && (length <= 90))
            {
                this.Width = 800;
            }
            else
            if ((length > 90) && (length <= 95))
            {
                this.Width = 850;
            }
            else
            {
                this.Width = 900;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnNu_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
