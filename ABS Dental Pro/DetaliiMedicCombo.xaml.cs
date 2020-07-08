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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ABS_Dental_Pro
{
    /// <summary>
    /// Interaction logic for DetaliiMedicCombo.xaml
    /// </summary>
    public partial class DetaliiMedicCombo : UserControl
    {
        Medic medicDeTrimis = new Medic();
        public Action<Medic> SendMedicLaModificatCallback;
        public event EventHandler MedicSters;

        public DetaliiMedicCombo()
        {
            InitializeComponent();
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            ModificaMedicComboWindow modificaMedicWindow = new ModificaMedicComboWindow();
            UmpleMedicDeTrimis();
            this.SendMedicLaModificatCallback += new Action<Medic>(modificaMedicWindow.SendMedicFunc);
            modificaMedicWindow.SendMedicModificatCallback +=
                new Action<Medic>(this.ModificaMedicInDetaliiMedic);
            SendMedicLaModificatCallback(medicDeTrimis);
            modificaMedicWindow.ShowDialog();
        }

        private void UmpleMedicDeTrimis()
        {
            medicDeTrimis.ID = (lbID.Content ?? "").ToString();
            medicDeTrimis.Nume = (lbNume.Content ?? "").ToString();
            medicDeTrimis.Prenume = (lbPrenume.Content ?? "").ToString();
            medicDeTrimis.Telefon = (lbTelefon.Content ?? "").ToString();
            medicDeTrimis.Email = (lbEmail.Content ?? "").ToString();
            medicDeTrimis.Observatii = (lbObservatii.Content ?? "").ToString();
            medicDeTrimis.Luni = new Zi((lbLuni1.Content ?? "").ToString(), (lbLuni2.Content ?? "").ToString(),
              (lbLuni3.Content ?? "").ToString(), (lbLuni4.Content ?? "").ToString());
            medicDeTrimis.Marti = new Zi((lbMarti1.Content ?? "").ToString(), (lbMarti2.Content ?? "").ToString(),
                (lbMarti3.Content ?? "").ToString(), (lbMarti4.Content ?? "").ToString());
            medicDeTrimis.Miercuri = new Zi((lbMiercuri1.Content ?? "").ToString(), (lbMiercuri2.Content ?? "").ToString(),
                (lbMiercuri3.Content ?? "").ToString(), (lbMiercuri4.Content ?? "").ToString());
            medicDeTrimis.Joi = new Zi((lbJoi1.Content ?? "").ToString(), (lbJoi2.Content ?? "").ToString(),
                (lbJoi3.Content ?? "").ToString(), (lbJoi4.Content ?? "").ToString());
            medicDeTrimis.Vineri = new Zi((lbVineri1.Content ?? "").ToString(), (lbVineri2.Content ?? "").ToString(),
                (lbVineri3.Content ?? "").ToString(), (lbVineri4.Content ?? "").ToString());
            medicDeTrimis.Sambata = new Zi((lbSambata1.Content ?? "").ToString(), (lbSambata2.Content ?? "").ToString(),
                (lbSambata3.Content ?? "").ToString(), (lbSambata4.Content ?? "").ToString());
        }

        private void ModificaMedicInDetaliiMedic(Medic medic)
        {
            lbID.Content = (medic.ID ?? "").ToString();
            lbNume.Content = (medic.Nume ?? "").ToString();
            lbPrenume.Content = (medic.Prenume ?? "").ToString();
            lbTelefon.Content = (medic.Telefon ?? "").ToString();
            lbEmail.Content = (medic.Email ?? "").ToString();
            lbObservatii.Content = (medic.Observatii ?? "").ToString();
            lbID.Content = (medic.ID ?? "").ToString();
            lbNume.Content = (medic.Nume ?? "").ToString();
            lbPrenume.Content = (medic.Prenume ?? "").ToString();
            lbTelefon.Content = (medic.Telefon ?? "").ToString();
            lbEmail.Content = (medic.Email ?? "").ToString();
            lbObservatii.Content = (medic.Observatii ?? "").ToString();
            lbLuni1.Content = ParseTimeForMinutesAndPutColon(medic.Luni.DeLa1);
            lbLuni2.Content = ParseTimeForMinutesAndPutColon(medic.Luni.PanaLa1);
            lbLuni3.Content = ParseTimeForMinutesAndPutColon(medic.Luni.DeLa2);
            lbLuni4.Content = ParseTimeForMinutesAndPutColon(medic.Luni.PanaLa2);
            lbMarti1.Content = ParseTimeForMinutesAndPutColon(medic.Marti.DeLa1);
            lbMarti2.Content = ParseTimeForMinutesAndPutColon(medic.Marti.PanaLa1);
            lbMarti3.Content = ParseTimeForMinutesAndPutColon(medic.Marti.DeLa2);
            lbMarti4.Content = ParseTimeForMinutesAndPutColon(medic.Marti.PanaLa2);
            lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(medic.Miercuri.DeLa1);
            lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(medic.Miercuri.PanaLa1);
            lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(medic.Miercuri.DeLa2);
            lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(medic.Miercuri.PanaLa2);
            lbJoi1.Content = ParseTimeForMinutesAndPutColon(medic.Joi.DeLa1);
            lbJoi2.Content = ParseTimeForMinutesAndPutColon(medic.Joi.PanaLa1);
            lbJoi3.Content = ParseTimeForMinutesAndPutColon(medic.Joi.DeLa2);
            lbJoi4.Content = ParseTimeForMinutesAndPutColon(medic.Joi.PanaLa2);
            lbVineri1.Content = ParseTimeForMinutesAndPutColon(medic.Vineri.DeLa1);
            lbVineri2.Content = ParseTimeForMinutesAndPutColon(medic.Vineri.PanaLa1);
            lbVineri3.Content = ParseTimeForMinutesAndPutColon(medic.Vineri.DeLa2);
            lbVineri4.Content = ParseTimeForMinutesAndPutColon(medic.Vineri.PanaLa2);
            lbSambata1.Content = ParseTimeForMinutesAndPutColon(medic.Sambata.DeLa1);
            lbSambata2.Content = ParseTimeForMinutesAndPutColon(medic.Sambata.PanaLa1);
            lbSambata3.Content = ParseTimeForMinutesAndPutColon(medic.Sambata.DeLa2);
            lbSambata4.Content = ParseTimeForMinutesAndPutColon(medic.Sambata.PanaLa2);
        }

        private string ParseTimeForMinutesAndPutColon(string time)
        {
            if (time == null)
            {
                return null;
            }

            if (time.Length == 4)
            {
                return time.Substring(0, 2) + ":" + time.Substring(2, 2);
            }
            else if (time.Length == 3)
            {
                return time.Substring(0, 1) + ":" + time.Substring(1, 2);
            }

            return time;
        }

        private void btnSterge_Click(object sender, RoutedEventArgs e)
        {
            bool? stergeMedic;

            string stergeMedicText = string.Format("Sigur vreți să ștergeți medicul {0} {1}?",
                                               lbNume.Content.ToString(), lbPrenume.Content.ToString());
            stergeMedic = MessageBoxCustom.Show(stergeMedicText, "Ștergere medic",
                                                  MessageBoxButton.YesNo);


            if ((bool)stergeMedic)
            {
                if (this.MedicSters != null)
                    this.MedicSters(this, new EventArgs());

                // sterge medic din .xml
                XDocument documentXmlMedici = XDocument.Load("medici.xml");
                documentXmlMedici.Root.Elements("medic")
                  .Where(x => x.Element("id").Value == lbID.Content.ToString())
                  .Where(x => x.Element("nume").Value == lbNume.Content.ToString())
                  .Where(x => x.Element("prenume").Value == lbPrenume.Content.ToString()).Remove();

                documentXmlMedici.Save("medici.xml");

                // sterge medic din GUI
                this.Visibility = Visibility.Hidden;
                this.IsEnabled = false;

                this.lbID.Content = "";
                this.lbNume.Content = "";
                this.lbPrenume.Content = "";
                this.lbTelefon.Content = "";
                this.lbEmail.Content = "";
                this.lbObservatii.Content = "";
                this.lbLuni1.Content = "";
                this.lbLuni2.Content = "";
                this.lbLuni3.Content = "";
                this.lbLuni4.Content = "";
                this.lbMarti1.Content = "";
                this.lbMarti2.Content = "";
                this.lbMarti3.Content = "";
                this.lbMarti4.Content = "";
                this.lbMiercuri1.Content = "";
                this.lbMiercuri2.Content = "";
                this.lbMiercuri3.Content = "";
                this.lbMiercuri4.Content = "";
                this.lbJoi1.Content = "";
                this.lbJoi2.Content = "";
                this.lbJoi3.Content = "";
                this.lbJoi4.Content = "";
                this.lbVineri1.Content = "";
                this.lbVineri2.Content = "";
                this.lbVineri3.Content = "";
                this.lbVineri4.Content = "";
                this.lbSambata1.Content = "";
                this.lbSambata2.Content = "";
                this.lbSambata3.Content = "";
                this.lbSambata4.Content = "";
            }
        }
    }
}
