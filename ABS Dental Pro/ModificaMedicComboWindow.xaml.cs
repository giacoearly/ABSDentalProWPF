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
    /// Interaction logic for ModificaMedicComboWindow.xaml
    /// </summary>
    public partial class ModificaMedicComboWindow : Window
    {
        public Medic medicPrimit = new Medic();
        public Medic medicModificatDeTrimisInapoi = new Medic();
        public Action<Medic> SendMedicModificatCallback;

        public ModificaMedicComboWindow()
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
            FillComboboxesToModify(medicPrimit.Luni.DeLa1, cbLuni11, cbLuni12);
            FillComboboxesToModify(medicPrimit.Luni.PanaLa1, cbLuni21, cbLuni22);
            FillComboboxesToModify(medicPrimit.Luni.DeLa2, cbLuni31, cbLuni32);
            FillComboboxesToModify(medicPrimit.Luni.PanaLa2, cbLuni41, cbLuni42);
            FillComboboxesToModify(medicPrimit.Marti.DeLa1, cbMarti11, cbMarti12);
            FillComboboxesToModify(medicPrimit.Marti.PanaLa1, cbMarti21, cbMarti22);
            FillComboboxesToModify(medicPrimit.Marti.DeLa2, cbMarti31, cbMarti32);
            FillComboboxesToModify(medicPrimit.Marti.PanaLa2, cbMarti41, cbMarti42);
            FillComboboxesToModify(medicPrimit.Miercuri.DeLa1, cbMiercuri11, cbMiercuri12);
            FillComboboxesToModify(medicPrimit.Miercuri.PanaLa1, cbMiercuri21, cbMiercuri22);
            FillComboboxesToModify(medicPrimit.Miercuri.DeLa2, cbMiercuri31, cbMiercuri32);
            FillComboboxesToModify(medicPrimit.Miercuri.PanaLa2, cbMiercuri41, cbMiercuri42);
            FillComboboxesToModify(medicPrimit.Joi.DeLa1, cbJoi11, cbJoi12);
            FillComboboxesToModify(medicPrimit.Joi.PanaLa1, cbJoi21, cbJoi22);
            FillComboboxesToModify(medicPrimit.Joi.DeLa2, cbJoi31, cbJoi32);
            FillComboboxesToModify(medicPrimit.Joi.PanaLa2, cbJoi41, cbJoi42);
            FillComboboxesToModify(medicPrimit.Vineri.DeLa1, cbVineri11, cbVineri12);
            FillComboboxesToModify(medicPrimit.Vineri.PanaLa1, cbVineri21, cbVineri22);
            FillComboboxesToModify(medicPrimit.Vineri.DeLa2, cbVineri31, cbVineri32);
            FillComboboxesToModify(medicPrimit.Vineri.PanaLa2, cbVineri41, cbVineri42);
            FillComboboxesToModify(medicPrimit.Sambata.DeLa1, cbSambata11, cbSambata12);
            FillComboboxesToModify(medicPrimit.Sambata.PanaLa1, cbSambata21, cbSambata22);
            FillComboboxesToModify(medicPrimit.Sambata.DeLa2, cbSambata31, cbSambata32);
            FillComboboxesToModify(medicPrimit.Sambata.PanaLa2, cbSambata41, cbSambata42);
        }

        private void FillComboboxesToModify(string hm, ComboBox cb1, ComboBox cb2)
        {
            if (hm!=null)
            {
                switch (hm.Length)
                {
                    case 1:
                        cb1.Text = hm;
                        break;
                    case 2:
                        cb1.Text = hm;
                        break;
                    case 4:
                        cb1.Text = hm.Substring(0, 1);
                        cb2.Text = hm.Substring(2, 2);
                        break;
                    case 5:
                        cb1.Text = hm.Substring(0, 2);
                        cb2.Text = hm.Substring(3, 2);
                        break;
                    default:
                        break;
                } 
            }
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
            medicModificatDeTrimisInapoi.Luni = new Zi(cbLuni11.Text+cbLuni12.Text, 
                                                       cbLuni21.Text+cbLuni22.Text, 
                                                       cbLuni31.Text+cbLuni32.Text, 
                                                       cbLuni41.Text+cbLuni42.Text);
            medicModificatDeTrimisInapoi.Marti = new Zi(cbMarti11.Text+cbMarti12.Text,
                                                        cbMarti21.Text+cbMarti22.Text, 
                                                        cbMarti31.Text+cbMarti32.Text, 
                                                        cbMarti41.Text+cbMarti42.Text);
            medicModificatDeTrimisInapoi.Miercuri = new Zi(cbMiercuri11.Text+cbMiercuri12.Text,
                                                           cbMiercuri21.Text+cbMiercuri22.Text, 
                                                           cbMiercuri31.Text+cbMiercuri32.Text, 
                                                           cbMiercuri41.Text+cbMiercuri42.Text);
            medicModificatDeTrimisInapoi.Joi = new Zi(cbJoi11.Text+cbJoi12.Text,
                                                      cbJoi21.Text+cbJoi22.Text,
                                                      cbJoi31.Text+cbJoi32.Text,
                                                      cbJoi41.Text+cbJoi42.Text);
            medicModificatDeTrimisInapoi.Vineri = new Zi(cbVineri11.Text+cbVineri12.Text,
                                                         cbVineri21.Text+cbVineri22.Text, 
                                                         cbVineri31.Text+cbVineri32.Text, 
                                                         cbVineri41.Text+cbVineri42.Text);
            medicModificatDeTrimisInapoi.Sambata = new Zi(cbSambata11.Text+cbSambata12.Text, 
                                                          cbSambata21.Text+cbSambata22.Text, 
                                                          cbSambata31.Text+cbSambata32.Text, 
                                                          cbSambata41.Text+cbSambata42.Text);

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
                medic.SetElementValue("lunidela1", cbLuni11.Text+cbLuni12.Text);
                medic.SetElementValue("lunipanala1", cbLuni21.Text+cbLuni22.Text);
                medic.SetElementValue("lunidela2", cbLuni31.Text+cbLuni32.Text);
                medic.SetElementValue("lunipanala2", cbLuni41.Text+cbLuni42.Text);
                medic.SetElementValue("martidela1", cbMarti11.Text+cbMarti12.Text);
                medic.SetElementValue("martipanala1", cbMarti21.Text+cbMarti22.Text);
                medic.SetElementValue("martidela2", cbMarti31.Text+cbMarti32.Text);
                medic.SetElementValue("martipanala2", cbMarti41.Text+cbMarti42.Text);
                medic.SetElementValue("miercuridela1", cbMiercuri11.Text+cbMiercuri12.Text);
                medic.SetElementValue("miercuripanala1", cbMiercuri21.Text+cbMiercuri22.Text);
                medic.SetElementValue("miercuridela2", cbMiercuri31.Text+cbMiercuri32.Text);
                medic.SetElementValue("miercuripanala2", cbMiercuri41.Text+cbMiercuri42.Text);
                medic.SetElementValue("joidela1", cbJoi11.Text+cbJoi12.Text);
                medic.SetElementValue("joipanala1", cbJoi21.Text+cbJoi22.Text);
                medic.SetElementValue("joidela2", cbJoi31.Text+cbJoi32.Text);
                medic.SetElementValue("joipanala2", cbJoi41.Text+cbJoi42.Text);
                medic.SetElementValue("vineridela1", cbVineri11.Text+cbVineri12.Text);
                medic.SetElementValue("vineripanala1", cbVineri21.Text+cbVineri22.Text);
                medic.SetElementValue("vineridela2", cbVineri31.Text+cbVineri32.Text);
                medic.SetElementValue("vineripanala2", cbVineri41.Text+cbVineri42.Text);
                medic.SetElementValue("sambatadela1", cbSambata11.Text+cbSambata12.Text);
                medic.SetElementValue("sambatapanala1", cbSambata21.Text+cbSambata22.Text);
                medic.SetElementValue("sambatadela2", cbSambata31.Text+cbSambata32.Text);
                medic.SetElementValue("sambatapanala2", cbSambata41.Text+cbSambata42.Text);
            }
            documentXmlMedici.Save("medici.xml");
        }
    }
}
