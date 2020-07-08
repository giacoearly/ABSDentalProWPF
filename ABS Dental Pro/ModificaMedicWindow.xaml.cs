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

namespace ABS_Dental_Pro
{
    /// <summary>
    /// Interaction logic for ModificaMedicWindow.xaml
    /// </summary>
    public partial class ModificaMedicWindow : Window
    {
        public Medic medicPrimit = new Medic();
        public Medic medicModificatDeTrimisInapoi = new Medic();
        public Action<Medic> SendMedicModificatCallback;

        public ModificaMedicWindow()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
        }

        public void SendMedicFunc(Medic medic)
        {
            medicPrimit = medic;

            tbID.Text = medicPrimit.ID;
            tbNume.Text = medicPrimit.Nume;
            tbPrenume.Text = medicPrimit.Prenume;
            tbTelefon.Text = medicPrimit.Telefon;
            tbEmail.Text = medicPrimit.Email;
            tbObservatii.Text = medicPrimit.Observatii;
            tbLuni1.Text = medicPrimit.Luni.DeLa1;
            tbLuni2.Text = medicPrimit.Luni.PanaLa1;
            tbLuni3.Text = medicPrimit.Luni.DeLa2;
            tbLuni4.Text = medicPrimit.Luni.PanaLa2;
            tbMarti1.Text = medicPrimit.Marti.DeLa1;
            tbMarti2.Text = medicPrimit.Marti.PanaLa1;
            tbMarti3.Text = medicPrimit.Marti.DeLa2;
            tbMarti4.Text = medicPrimit.Marti.PanaLa2;
            tbMiercuri1.Text = medicPrimit.Miercuri.DeLa1;
            tbMiercuri2.Text = medicPrimit.Miercuri.PanaLa1;
            tbMiercuri3.Text = medicPrimit.Miercuri.DeLa2;
            tbMiercuri4.Text = medicPrimit.Miercuri.PanaLa2;
            tbJoi1.Text = medicPrimit.Joi.DeLa1;
            tbJoi2.Text = medicPrimit.Joi.PanaLa1;
            tbJoi3.Text = medicPrimit.Joi.DeLa2;
            tbJoi4.Text = medicPrimit.Joi.PanaLa2;
            tbVineri1.Text = medicPrimit.Vineri.DeLa1;
            tbVineri2.Text = medicPrimit.Vineri.PanaLa1;
            tbVineri3.Text = medicPrimit.Vineri.DeLa2;
            tbVineri4.Text = medicPrimit.Vineri.PanaLa2;
            tbSambata1.Text = medicPrimit.Sambata.DeLa1;
            tbSambata2.Text = medicPrimit.Sambata.PanaLa1;
            tbSambata3.Text = medicPrimit.Sambata.DeLa2;
            tbSambata4.Text = medicPrimit.Sambata.PanaLa2;
        }

        private void btnAnuleaza_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            ModificaMedicInXml();
            TrimiteMedicModificatInapoi();

            string medicModificatCuSucces =
            string.Format("Medicul {0} {1} a fost modificat cu succes!",
              medicModificatDeTrimisInapoi.Nume, medicModificatDeTrimisInapoi.Prenume);

            MessageBoxCustom.Show(medicModificatCuSucces, "Medic modificat");

            this.Close();
        }

        private void TrimiteMedicModificatInapoi()
        {
            // trimite medic modificat inapoi

            medicModificatDeTrimisInapoi.ID = tbID.Text;
            medicModificatDeTrimisInapoi.Nume = tbNume.Text;
            medicModificatDeTrimisInapoi.Prenume = tbPrenume.Text;
            medicModificatDeTrimisInapoi.Telefon = tbTelefon.Text;
            medicModificatDeTrimisInapoi.Email = tbEmail.Text;
            medicModificatDeTrimisInapoi.Observatii = tbObservatii.Text;
            medicModificatDeTrimisInapoi.Luni = new Zi(tbLuni1.Text, tbLuni2.Text, tbLuni3.Text, tbLuni4.Text);
            medicModificatDeTrimisInapoi.Marti = new Zi(tbMarti1.Text, tbMarti2.Text, tbMarti3.Text, tbMarti4.Text);
            medicModificatDeTrimisInapoi.Miercuri = new Zi(tbMiercuri1.Text, tbMiercuri2.Text, tbMiercuri3.Text, tbMiercuri4.Text);
            medicModificatDeTrimisInapoi.Joi = new Zi(tbJoi1.Text, tbJoi2.Text, tbJoi3.Text, tbJoi4.Text);
            medicModificatDeTrimisInapoi.Vineri = new Zi(tbVineri1.Text, tbVineri2.Text, tbVineri3.Text, tbVineri4.Text);
            medicModificatDeTrimisInapoi.Sambata = new Zi(tbSambata1.Text, tbSambata2.Text, tbSambata3.Text, tbSambata4.Text);

            SendMedicModificatCallback(medicModificatDeTrimisInapoi);
        }

        private void ModificaMedicInXml()
        {
            // modifica fisier .xml 
            XDocument documentXmlMedici = XDocument.Load("medici.xml");
            var medicDeModificat = from medic in documentXmlMedici.Root.Elements("medic")
                                     where medic.Element("id").Value == medicPrimit.ID
                                     where medic.Element("nume").Value == medicPrimit.Nume
                                     where medic.Element("prenume").Value == medicPrimit.Prenume
                                     select medic;
            foreach (XElement medic in medicDeModificat)
            {
                medic.SetElementValue("id", tbID.Text);
                medic.SetElementValue("nume", tbNume.Text);
                medic.SetElementValue("prenume", tbPrenume.Text);
                medic.SetElementValue("telefon", tbTelefon.Text);
                medic.SetElementValue("email", tbEmail.Text);
                medic.SetElementValue("observatii", tbObservatii.Text);
                medic.SetElementValue("lunidela1", tbLuni1.Text);
                medic.SetElementValue("lunipanala1", tbLuni2.Text);
                medic.SetElementValue("lunidela2", tbLuni3.Text);
                medic.SetElementValue("lunipanala2", tbLuni4.Text);
                medic.SetElementValue("martidela1", tbMarti1.Text);
                medic.SetElementValue("martipanala1", tbMarti2.Text);
                medic.SetElementValue("martidela2", tbMarti3.Text);
                medic.SetElementValue("martipanala2", tbMarti4.Text);
                medic.SetElementValue("miercuridela1", tbMiercuri1.Text);
                medic.SetElementValue("miercuripanala1", tbMiercuri2.Text);
                medic.SetElementValue("miercuridela2", tbMiercuri3.Text);
                medic.SetElementValue("miercuripanala2", tbMiercuri4.Text);
                medic.SetElementValue("joidela1", tbJoi1.Text);
                medic.SetElementValue("joipanala1", tbJoi2.Text);
                medic.SetElementValue("joidela2", tbJoi3.Text);
                medic.SetElementValue("joipanala2", tbJoi4.Text);
                medic.SetElementValue("vineridela1", tbVineri1.Text);
                medic.SetElementValue("vineripanala1", tbVineri2.Text);
                medic.SetElementValue("vineridela2", tbVineri3.Text);
                medic.SetElementValue("vineripanala2", tbVineri4.Text);
                medic.SetElementValue("sambatadela1", tbSambata1.Text);
                medic.SetElementValue("sambatapanala1", tbSambata2.Text);
                medic.SetElementValue("sambatadela2", tbSambata3.Text);
                medic.SetElementValue("sambatapanala2", tbSambata4.Text);
            }
            documentXmlMedici.Save("medici.xml");
        }
    }
}
