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
    /// Interaction logic for ModificaDescriereWindow.xaml
    /// </summary>
    public partial class ModificaDescriereWindow : Window
    {
        public Action<string> SendDescriereModificataLaIstoricCallback;

        public ModificaDescriereWindow()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
        }

        internal void SendDescriereLaModificatFunc(string numeprenume, Istorie istorie)
        {
            this.Title += " " + numeprenume + ", la " + istorie.Medic + ", din " + istorie.Data;
            tbDescriereVeche.Text = istorie.Descriere;
            tbDescriereNoua.Text = istorie.Descriere;
            tbDescriereNoua.Focus();
            // du cursorul la sfarsit
            tbDescriereNoua.CaretIndex = tbDescriereNoua.Text.Length;
        }

        private void btnModificaDescriere_Click(object sender, RoutedEventArgs e)
        {
            SendDescriereModificataLaIstoricCallback(tbDescriereNoua.Text);
            this.Close();
        }

        private void btnInchide_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
