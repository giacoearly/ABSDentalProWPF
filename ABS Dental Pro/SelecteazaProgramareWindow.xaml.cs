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
    /// Interaction logic for SelecteazaProgramareWindow.xaml
    /// </summary>
    public partial class SelecteazaProgramareWindow : Window
    {
        public Action<string> SendDataToModificaProgramareWindowCallback;
        public Action<int> SendDataIndiceToModificaProgramareWindowCallback;

        public SelecteazaProgramareWindow()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
        }

        private void btnSelecteaza_Click(object sender, RoutedEventArgs e)
        {
            var checkedButton = grid.Children.OfType<RadioButton>()
                                    .FirstOrDefault(r => (bool)r.IsChecked);

            if (checkedButton != null)
            {
                SendDataToModificaProgramareWindowCallback(checkedButton.Content.ToString());

                if ((bool)rbProgramare1.IsChecked)
                {
                    SendDataIndiceToModificaProgramareWindowCallback(0);
                }
                else
                {
                    if ((bool)rbProgramare2.IsChecked)
                    {
                        SendDataIndiceToModificaProgramareWindowCallback(1);
                    }
                    else
                    {
                        if ((bool)rbProgramare3.IsChecked)
                        {
                            SendDataIndiceToModificaProgramareWindowCallback(2);
                        }
                        else
                        {
                            if ((bool)rbProgramare4.IsChecked)
                            {
                                SendDataIndiceToModificaProgramareWindowCallback(3);
                            }
                            else
                            {
                                if ((bool)rbProgramare5.IsChecked)
                                {
                                    SendDataIndiceToModificaProgramareWindowCallback(4);
                                }
                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBoxFormOver messageBoxFormOver = new MessageBoxFormOver("Selectați o dată!", "Dată lipsă");
                messageBoxFormOver.Owner = this; 
                messageBoxFormOver.ShowDialog();
            }
        }
     }
}
