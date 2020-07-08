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
using System.Xml.Linq;
using System.IO;

namespace ABS_Dental_Pro
{
    /// <summary>
    /// Interaction logic for AdaugaMedicWindow.xaml
    /// </summary>
    public partial class AdaugaMedicWindow : Window
    {
        public Medic medicDeAdaugat = new Medic();
        public Action<Medic> SendMedicToMainCallback;

        public AdaugaMedicWindow()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;

            try
            {
                XDocument documentXmlMedici = XDocument.Load("medici.xml");
                int numarMedici = documentXmlMedici.Descendants("medic").Count();
                tbID.Text = (numarMedici + 1).ToString();
            }
            catch (FileNotFoundException)
            {

                int numarMedici = 0;
                tbID.Text = (numarMedici + 1).ToString();
            }
            tbNume.Focus();
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            medicDeAdaugat.ID = tbID.Text;
            medicDeAdaugat.Nume = tbNume.Text;
            medicDeAdaugat.Prenume = tbPrenume.Text;
            medicDeAdaugat.Telefon = tbTelefon.Text;
            medicDeAdaugat.Email = tbEmail.Text;
            medicDeAdaugat.Observatii = tbObservatii.Text;
            medicDeAdaugat.Luni = new Zi(tbLuni1.Text, tbLuni2.Text, tbLuni3.Text, tbLuni4.Text);
            medicDeAdaugat.Marti = new Zi(tbMarti1.Text, tbMarti2.Text, tbMarti3.Text, tbMarti4.Text);
            medicDeAdaugat.Miercuri = new Zi(tbMiercuri1.Text, tbMiercuri2.Text, tbMiercuri3.Text, tbMiercuri4.Text);
            medicDeAdaugat.Joi = new Zi(tbJoi1.Text, tbJoi2.Text, tbJoi3.Text, tbJoi4.Text);
            medicDeAdaugat.Vineri = new Zi(tbVineri1.Text, tbVineri2.Text, tbVineri3.Text, tbVineri4.Text);
            medicDeAdaugat.Sambata = new Zi(tbSambata1.Text, tbSambata2.Text, tbSambata3.Text, tbSambata4.Text);

            SendMedicToMainCallback(medicDeAdaugat);

            string medicAdaugatCuSucces =
               string.Format("Medicul {0} {1} a fost adăugat cu succes!",
                medicDeAdaugat.Nume, medicDeAdaugat.Prenume);

            MessageBoxCustom.Show(medicAdaugatCuSucces, "Medic adăugat");

            this.Close();
        }
    }
}
