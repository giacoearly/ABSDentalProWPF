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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ABS_Dental_Pro
{
    /// <summary>
    /// Interaction logic for ModificaPlataWindow.xaml
    /// </summary>
    public partial class ModificaPlataWindow : Window
    {
        String pacientNumePrenume;
        Plata plataOld = new Plata();

        public Action<Plata> SendPlataToMainWindowCallback;

        public ModificaPlataWindow()
        {
            InitializeComponent();
            this.Owner = System.Windows.Application.Current.MainWindow;

            // adauga medici in cbMedic
            try
            {
                XDocument documentXmlMedici = XDocument.Load("medici.xml");
                var mediciInitDetaliiMedici = documentXmlMedici.Descendants("medic");
                var medici = from m in mediciInitDetaliiMedici
                             select new Medic()
                             {
                                 Nume = m.Descendants("nume").First().Value,
                                 Prenume = m.Descendants("prenume").First().Value,
                             };
                foreach (var item in medici)
                {
                    cbMedic.Items.Add("Dr. " + item.Nume + " " + item.Prenume);
                }
            }
            catch (FileNotFoundException)
            {

            }

            // block some dates
            DatePickerSetBlackOutDates();
        }

        private void DatePickerSetBlackOutDates()
        {
            // block dates: 1.1.1900 -> yesterday
            datePicker.BlackoutDates.Add(new CalendarDateRange(
                new DateTime(1900, 1, 1),
                DateTime.Today.AddDays(-1)));

            // block dates: all sundays for next 100 years
            DateTime today = DateTime.Today;
            for (int i = 1; i < 365 * 100; i++)
            {
                DateTime day = today.AddDays(i);

                if (day.DayOfWeek == DayOfWeek.Sunday)
                {
                    datePicker.BlackoutDates.Add(new CalendarDateRange(day));
                }
            }
        }

        internal void AdaugaPlataFunc(Plata plata)
        {
            plataOld = plata;

            this.Title += " " + plata.NumePrenumePacient;
            pacientNumePrenume = plata.NumePrenumePacient;
            cbMedic.SelectedItem = plata.Medic;
            tbTotal.Text = plata.Total;
            tbTransa.Text = plata.Transa;
            tbRest.Text = plata.Rest;
            datePicker.Text = plata.Data;
            tbDescriere.Text = plata.Descriere; 
        }

        private void NumericOnlyPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Key key = e.Key;
            if (!((key == Key.D0) || (key == Key.D1) || (key == Key.D2) || (key == Key.D3) || (key == Key.D4) ||
                  (key == Key.D5) || (key == Key.D6) || (key == Key.D7) || (key == Key.D8) || (key == Key.D9) ||
                  (key == Key.NumPad0) || (key == Key.NumPad1) || (key == Key.NumPad2) || (key == Key.NumPad3) || (key == Key.NumPad4) ||
                  (key == Key.NumPad5) || (key == Key.NumPad6) || (key == Key.NumPad7) || (key == Key.NumPad8) || (key == Key.NumPad9) ||
                  (key == Key.Back) || (key == Key.Delete) || (key == Key.Left) || (key == Key.Right) || (key == Key.Tab)))
            {
                e.Handled = true;
            }
        }

        private void tbTransa_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((tbTransa.Text != String.Empty) && (tbTotal.Text != String.Empty))
            {
                int total = int.Parse(tbTotal.Text);
                int transa = int.Parse(tbTransa.Text);

                if (transa > total)
                {
                    MessageBoxCustom.Show("Tranșa nu poate fi mai mare decât totalul!", "Valoare incorectă");
                }
                else
                {
                    tbRest.Text = (total - transa).ToString();
                }
            }
        }

        private void tbTotal_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((tbTransa.Text != String.Empty) && (tbTotal.Text != String.Empty))
            {
                int total = int.Parse(tbTotal.Text);
                int transa = int.Parse(tbTransa.Text);

                if (transa > total)
                {
                    MessageBoxCustom.Show("Tranșa nu poate fi mai mare decât totalul!", "Valoare incorectă");
                }
                else
                {
                    tbRest.Text = (total - transa).ToString();
                }
            }
        }

        private void tbTransa_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((tbTransa.Text != String.Empty) && (tbTotal.Text != String.Empty))
            {
                int total = int.Parse(tbTotal.Text);
                int transa = int.Parse(tbTransa.Text);

                if (transa > total)
                {
                    MessageBoxCustom.Show("Tranșa nu poate fi mai mare decât totalul!", "Valoare incorectă");
                    tbTransa.Text = String.Empty;
                }
                else
                {
                    tbRest.Text = (total - transa).ToString();
                }
            }
        }

        private void tbTotal_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((tbTransa.Text != String.Empty) && (tbTotal.Text != String.Empty))
            {
                int total = int.Parse(tbTotal.Text);
                int transa = int.Parse(tbTransa.Text);

                if (transa < total)
                {
                    tbRest.Text = (total - transa).ToString();
                }
            }
        }

        private void btnModificaPlata_Click(object sender, RoutedEventArgs e)
        {
            int total = int.Parse(tbTotal.Text);
            int transa = int.Parse(tbTransa.Text);

            if (transa > total)
            {
                MessageBoxCustom.Show("Totalul nu poate fi mai mic decât tranșa!", "Valoare incorectă");
                return;
            }

            bool? modifica;
            string str = string.Format("Sigur vreți să modificați plata pentru pacientul {0}?", pacientNumePrenume);
            modifica = MessageBoxCustom.Show(str, "Modificare plată", MessageBoxButton.YesNo);

            if ((bool)modifica)
            {
                XDocument documentXmlPlati = XDocument.Load("plati.xml");

                Plata plata = new Plata
                {
                    NumePrenumePacient = pacientNumePrenume,
                    Medic = cbMedic.SelectedItem.ToString(),
                    Total = tbTotal.Text,
                    Transa = tbTransa.Text,
                    Rest = tbRest.Text,
                    Data = datePicker.Text,
                    Descriere = tbDescriere.Text
                };
                SendPlataToMainWindowCallback(plata);
                ModificaPlataInXML(plata);
                this.Close();
            }
        }

        private void ModificaPlataInXML(Plata plata)
        {
            // modifica plata in .xml
            XDocument documentXmlPlati = XDocument.Load("plati.xml");
            var plataDeModificat = from p in documentXmlPlati.Root.Elements("plata")
                                        //where p.Element("pacient").Value == plataOld.NumePrenumePacient
                                        where p.Element("medic").Value == plataOld.Medic
                                        where p.Element("total").Value == plataOld.Total
                                        where p.Element("transa").Value == plataOld.Transa
                                        where p.Element("rest").Value == plataOld.Rest
                                        where p.Element("data").Value == plataOld.Data
                                        where p.Element("descriere").Value == plataOld.Descriere
                                        select p;
            foreach (XElement p in plataDeModificat)
            {
                //p.SetElementValue("pacient", plata.NumePrenumePacient);
                p.SetElementValue("medic", plata.Medic);
                p.SetElementValue("total", plata.Total);
                p.SetElementValue("transa", plata.Transa);
                p.SetElementValue("rest", plata.Rest);
                p.SetElementValue("data", plata.Data);
                p.SetElementValue("descriere", plata.Descriere);
            }
            documentXmlPlati.Save("plati.xml");

            // modifica transa in .xml
            XDocument documentXmlTranse = XDocument.Load("transe.xml");
            var transaDeModificat = from p in documentXmlTranse.Root.Elements("plata")
                                       where p.Element("pacient").Value == plataOld.NumePrenumePacient
                                       where p.Element("medic").Value == plataOld.Medic
                                       where p.Element("transa").Value == plataOld.Transa
                                       where p.Element("data").Value == plataOld.Data
                                       where p.Element("descriere").Value == plataOld.Descriere
                                   select p;
            foreach (XElement p in transaDeModificat)
            {
                p.SetElementValue("medic", plata.Medic);
                p.SetElementValue("transa", plata.Transa);
                p.SetElementValue("data", plata.Data);
                p.SetElementValue("descriere", plata.Descriere);
            }
            documentXmlTranse.Save("transe.xml");
        }
    }
}
