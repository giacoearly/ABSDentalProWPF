using System;
using System.Collections.Generic;
using System.IO;
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

namespace ABS_Dental_Pro
{
    /// <summary>
    /// Interaction logic for AdaugaMedicComboWindow.xaml
    /// </summary>
    public partial class AdaugaMedicComboWindow : Window
    {
        public Medic medicDeAdaugat = new Medic();
        public Action<Medic> SendMedicToMainCallback;

        public AdaugaMedicComboWindow()
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
            medicDeAdaugat.Luni = new Zi(cbLuni11.Text+cbLuni12.Text, cbLuni21.Text+cbLuni22.Text,
                                         cbLuni31.Text+cbLuni32.Text, cbLuni41.Text+cbLuni42.Text);
            medicDeAdaugat.Marti = new Zi(cbMarti11.Text+cbMarti12.Text, cbMarti21.Text+cbMarti22.Text,
                                          cbMarti31.Text+cbMarti32.Text, cbMarti41.Text+cbMarti42.Text);
            medicDeAdaugat.Miercuri = new Zi(cbMiercuri11.Text+cbMiercuri12.Text, cbMiercuri21.Text+cbMiercuri22.Text,
                                             cbMiercuri31.Text+cbMiercuri32.Text, cbMiercuri41.Text+cbMiercuri42.Text);
            medicDeAdaugat.Joi = new Zi(cbJoi11.Text+cbJoi12.Text, cbJoi21.Text+cbJoi22.Text,
                                        cbJoi31.Text+cbJoi32.Text, cbJoi41.Text+cbJoi42.Text);
            medicDeAdaugat.Vineri = new Zi(cbVineri11.Text+cbVineri12.Text, cbVineri21.Text+cbVineri22.Text,
                                           cbVineri31.Text+cbVineri32.Text, cbVineri41.Text+cbVineri42.Text);
            medicDeAdaugat.Sambata = new Zi(cbSambata11.Text+cbSambata12.Text, cbSambata21.Text+cbSambata22.Text,
                                            cbSambata31.Text+cbSambata32.Text, cbSambata41.Text+cbSambata42.Text);

            SendMedicToMainCallback(medicDeAdaugat);

            string medicAdaugatCuSucces =
               string.Format("Medicul {0} {1} a fost adăugat cu succes!",
                medicDeAdaugat.Nume, medicDeAdaugat.Prenume);

            MessageBoxCustom.Show(medicAdaugatCuSucces, "Medic adăugat");

            this.Close();
        }
    }
}
