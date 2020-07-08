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
using System.Windows.Forms;

namespace ABS_Dental_Pro
{
    /// <summary>
    /// Interaction logic for MessageBoxCustom.xaml
    /// </summary>
    public partial class MessageBoxForm : Window
    {
        public MessageBoxForm()
        {
            InitializeComponent();
        }

        public MessageBoxForm(string message)
        {
            InitializeComponent();

            this.Owner = System.Windows.Application.Current.MainWindow;
            Thickness marginiButonOK = new Thickness(300, 0, 0, 0);
            btnOK.Margin = marginiButonOK;
            btnOK.Focus();

            btnNu.Visibility = Visibility.Hidden;

            tbMessage.Text = message;

            AdjustWindowWidth(message.Length);
            btnOK.Margin = new Thickness(this.Width/2 - btnOK.Width/2, 0, 0, 0);
        }

        public MessageBoxForm(string message, string title)
        {
            InitializeComponent();

            if (System.Windows.Application.Current!=null)
            {
                this.Owner = System.Windows.Application.Current.MainWindow; 
            }
            Thickness marginiButonOK = new Thickness(300, 0, 0, 0);
            btnOK.Margin = marginiButonOK;
            btnOK.Focus();

            btnNu.Visibility = Visibility.Hidden;

            tbMessage.Text = message;

            AdjustWindowWidth(message.Length);
            btnOK.Margin = new Thickness(this.Width/2 - btnOK.Width/2, 0, 0, 0);

            this.Title = title;
        }

        public MessageBoxForm(string message, string title, MessageBoxButton msgBtn)
        {
            InitializeComponent();
            this.Owner = System.Windows.Application.Current.MainWindow;
            Thickness marginiButonOK = new Thickness(200, 0, 0, 0);
            btnOK.Margin = marginiButonOK;
            btnOK.IsDefault = false;
            btnOK.IsCancel = false;

            btnNu.Visibility = Visibility.Visible; 
            btnNu.IsDefault = true; 
            btnNu.IsCancel = true ;
            btnNu.Focus();
            if (msgBtn==MessageBoxButton.YesNo)
            {
                tbkOK.Text = "Da";
                tbkNu.Text = "Nu";
            }

            tbMessage.Text = message;

            AdjustWindowWidth(message.Length);
            btnOK.Margin = new Thickness(this.Width/2 - btnOK.Width - btnOK.Width/2, 0, 0, 0);

            this.Title = title;
        }

        public MessageBoxForm(string message, string title, MessageBoxButton msgBtn, MessageBoxIcon icon)
        {
            InitializeComponent();

            if (icon == MessageBoxIcon.Warning)
            {
                //imgOK.Source = new BitmapImage(new Uri(@"/Images/Exclamation-mark-icon.png", UriKind.Relative));
                iconExclamation.Visibility = Visibility.Visible;
            }
           
            this.Owner = System.Windows.Application.Current.MainWindow;
            Thickness marginiButonOK = new Thickness(200, 0, 0, 0);
            btnOK.Margin = marginiButonOK;
            btnOK.Focus();
            btnOK.IsDefault = false;
            btnOK.IsCancel = false;

            btnNu.Visibility = Visibility.Hidden;
           
            //btnNu.Visibility = Visibility.Visible;
            //btnNu.IsDefault = true;
            //btnNu.IsCancel = true;
            //btnNu.Focus();
            if (msgBtn == MessageBoxButton.YesNo)
            {
                tbkOK.Text = "Da";
                tbkNu.Text = "Nu";
            }

            tbMessage.Text = message;

            AdjustWindowWidth(message.Length);
            //btnOK.Margin = new Thickness(this.Width / 2 - btnOK.Width - btnOK.Width / 2, 0, 0, 0);

            this.Title = title;
        }

        private void AdjustWindowWidth(int length)
        {
            if (length <= 30)
            {
                this.Width = 400;
            }
            else
            if ((length > 30) && (length <= 55))
            {
                this.Width = 450;
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
