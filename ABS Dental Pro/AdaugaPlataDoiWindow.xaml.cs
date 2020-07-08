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
    /// Interaction logic for AdaugaPlataDoiWindow.xaml
    /// </summary>
    public partial class AdaugaPlataDoiWindow : Window
    {
        string pacientNumePrenume;
        string numeMedic;
        int restInitial = 0;
        internal Action<Plata> SendPlataToMainWindowCallback;

        public AdaugaPlataDoiWindow()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            tbTransa.Focus();
            datePicker.Text = DateTime.Today.ToString();

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

        private void btnAdaugaPlata_Click(object sender, RoutedEventArgs e)
        {
            int transa = int.Parse(tbTransa.Text);
            int avans = int.Parse(tbAvans.Text);

            bool? adauga;
            string str = string.Format("Sigur vreți să adăugați plata în valoare de {0} de lei pentru pacientul {1}?", 
                                        transa, pacientNumePrenume);
            adauga = MessageBoxCustom.Show(str, "Adăugare plată", MessageBoxButton.YesNo);

            if ((bool)adauga)
            {
                Plata plata = new Plata
                {
                    NumePrenumePacient = pacientNumePrenume,
                    Medic = tbMedic.Text,
                    Total = tbTotal.Text,
                    Transa = (transa+avans).ToString(), 
                    Rest = tbRest.Text,
                    Data = datePicker.Text,
                    Descriere = tbDescriere.Text
                };
                SendPlataToMainWindowCallback(plata);
                AdaugaPlataDoiInXML();
                this.Close();  
            }
        }

        private void AdaugaPlataDoiInXML()
        {
            int transa = int.Parse(tbTransa.Text);
            int avans = int.Parse(tbAvans.Text);

            // modifica fisier .xml 
            XDocument documentXmlPlati = XDocument.Load("plati.xml");
            var plataDeModificat = from plata in documentXmlPlati.Root.Elements("plata")
                                     where plata.Element("pacient").Value == pacientNumePrenume
                                     where plata.Element("medic").Value == numeMedic
                                     where int.Parse(plata.Element("rest").Value) > 0
                                     select plata;
            foreach (XElement plata in plataDeModificat)
            {
                plata.SetElementValue("transa", (transa + avans).ToString());
                plata.SetElementValue("rest", tbRest.Text);
                plata.SetElementValue("data", datePicker.Text);
                plata.SetElementValue("descriere", tbDescriere.Text);
            }
            documentXmlPlati.Save("plati.xml");

            // transe
            XElement xelem = new XElement("plata");

            xelem.Add(new XElement("pacient", pacientNumePrenume));
            xelem.Add(new XElement("medic", tbMedic.Text));
            xelem.Add(new XElement("transa", tbTransa.Text));
            xelem.Add(new XElement("data", datePicker.Text));
            xelem.Add(new XElement("descriere", tbDescriere.Text));

            try
            {
                XDocument documentXmlTranse = XDocument.Load("transe.xml");
                documentXmlTranse.Element("transe").Add(xelem);
                documentXmlTranse.Save("transe.xml");
            }
            catch (FileNotFoundException)
            {

                XDocument documentXmlTranse = new XDocument(new XElement("transe"));
                documentXmlTranse.Element("transe").Add(xelem);
                documentXmlTranse.Save("transe.xml");
            }
        }

        internal void AdaugaPlataFunc(string arg1, string arg2, string arg3)
        {
            tbSituatie.Text += " " + arg1 + " " + arg2;
            tbMedic.Text = arg3;
            numeMedic = arg3;
            pacientNumePrenume = arg1 + " " + arg2;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string transeString = String.Empty;
            int sumaTranse = 0;

            // detalii ultima plata
            try
            {
                XDocument documentXmlPlati = XDocument.Load("plati.xml");
                var platiAll = documentXmlPlati.Descendants("plata");
                var plati = from p in platiAll
                            where p.Element("pacient").Value == pacientNumePrenume
                            where p.Element("medic").Value == numeMedic
                            where int.Parse(p.Element("rest").Value) > 0
                            select new Plata()
                            {
                                Total = p.Descendants("total").First().Value,
                                Transa = p.Descendants("transa").First().Value,
                                Rest = p.Descendants("rest").First().Value,
                                Data = p.Descendants("data").First().Value,
                                Descriere = p.Descendants("descriere").First().Value,
                            };
                foreach (var item in plati)
                {
                    tbTotal.Text = item.Total;
                    transeString = item.Transa;
                    tbRest.Text = item.Rest;
                    restInitial = int.Parse(item.Rest);
                }
                string[] transe = transeString.Split(',');
                for (int i = 0; i < transe.Count(); i++)
                {
                    if (transe[i] != String.Empty)
                    {
                        sumaTranse += int.Parse(transe[i]); 
                    }
                }
                tbAvans.Text = sumaTranse.ToString();
            }
            catch (FileNotFoundException)
            {
                //MessageBox.Show("Fișierul plati.xml lipsește!", "Fișier inexistent");
            }
        }

        private void tbTransa_KeyUp(object sender, KeyEventArgs e)
        {
            if ((tbTransa.Text != String.Empty) && (tbRest.Text != String.Empty))
            {
                int rest = int.Parse(tbRest.Text);
                int transa = int.Parse(tbTransa.Text);

                if (transa > restInitial)
                {
                    MessageBoxCustom.Show("Tranșa nu poate fi mai mare decât restul!", "Valoare incorectă");
                    tbTransa.Text = String.Empty;
                }
                else
                {
                    tbRest.Text = (restInitial - transa).ToString();
                }
            }

            if (tbTransa.Text == String.Empty)
            {
                tbRest.Text = restInitial.ToString();
            }
        }
    }
}
