using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AdaugaPlataWindow.xaml
    /// </summary>
    public partial class AdaugaPlataWindow : Window
    {
        string pacientNumePrenume;
        public Action<Plata> SendPlataToMainWindowCallback;

        public AdaugaPlataWindow()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            tbTotal.Focus();
            datePicker.Text = DateTime.Today.ToString();

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

        private void NumericOnlyPreviewKeyDown(object sender, KeyEventArgs e)
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

        internal void AdaugaPlataFunc(string arg1, string arg2, string arg3)
        {
            tbSituatie.Text += " " + arg1 + " " + arg2;
            cbMedic.Text = arg3;
            pacientNumePrenume = arg1 + " " + arg2;
        }

        private void tbTransa_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((tbTransa.Text != String.Empty)&&(tbTotal.Text != String.Empty))
            {
                int total = int.Parse(tbTotal.Text);
                int transa = int.Parse(tbTransa.Text);

                if (transa>total)
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

        private void btnAdaugaPlata_Click(object sender, RoutedEventArgs e)
        {
            int total = int.Parse(tbTotal.Text);
            int transa = int.Parse(tbTransa.Text);

            if (transa > total)
            {
                MessageBoxCustom.Show("Totalul nu poate fi mai mic decât tranșa!", "Valoare incorectă");
                return;
            }

            bool? adauga;
            string str = string.Format("Sigur vreți să adăugați plata în valoare de {0} de lei, din totalul de {1} de lei, pentru pacientul {2}?",
                                       transa, total, pacientNumePrenume);
            adauga = MessageBoxCustom.Show(str, "Adăugare plată", MessageBoxButton.YesNo);

            if ((bool)adauga)
            {
                int nrPlati = 0;

                try
                {

                    XDocument documentXmlPlati = XDocument.Load("plati.xml");

                    nrPlati = documentXmlPlati.Root.Elements("plata").Where(x => x.Element("pacient").Value == pacientNumePrenume)
                                                                     .Where(x => x.Element("rest").Value != "0").Count();
                }
                catch (FileNotFoundException)
                {

                }

                str = String.Format("Nu puteți adăuga a doua plată neachitată integral pentru un pacient!");
                if ((nrPlati == 1) && (tbRest.Text != "0"))
                {
                    MessageBoxCustom.Show(str, "Date invalide");
                    return;
                }

                int rest = int.Parse(tbRest.Text);

                if (total != (transa+rest))
                {
                    MessageBoxCustom.Show("Valoarea totală este incorectă!", "Valoare incorectă");
                    tbTotal.Focus();
                    return;
                }

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
                AdaugaPlataInXML(plata);
                this.Close();
            }
        }

        private void AdaugaPlataInXML(Plata value)
        {
            XElement xelem = new XElement("plata");

            xelem.Add(new XElement("pacient", value.NumePrenumePacient));
            xelem.Add(new XElement("medic", value.Medic));
            xelem.Add(new XElement("total", value.Total));
            xelem.Add(new XElement("transa", value.Transa));
            xelem.Add(new XElement("rest", value.Rest));
            xelem.Add(new XElement("data", value.Data));
            xelem.Add(new XElement("descriere", value.Descriere));
          
            try
            {
                XDocument documentXmlPlati = XDocument.Load("plati.xml");
                documentXmlPlati.Element("plati").Add(xelem);
                documentXmlPlati.Save("plati.xml");
            }
            catch (FileNotFoundException)
            {

                XDocument documentXmlPlati = new XDocument(new XElement("plati"));
                documentXmlPlati.Element("plati").Add(xelem);
                documentXmlPlati.Save("plati.xml");
            }
            // transe
            xelem.Element("total").Remove();
            xelem.Element("rest").Remove();

            try
            {
                XDocument documentXmlPlati = XDocument.Load("transe.xml");
                documentXmlPlati.Element("transe").Add(xelem);
                documentXmlPlati.Save("transe.xml");
            }
            catch (FileNotFoundException)
            {

                XDocument documentXmlPlati = new XDocument(new XElement("transe"));
                documentXmlPlati.Element("transe").Add(xelem);
                documentXmlPlati.Save("transe.xml");
            }
        }

        private void tbTransa_KeyUp(object sender, KeyEventArgs e)
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

        private void tbTotal_KeyUp(object sender, KeyEventArgs e)
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
    }
}
