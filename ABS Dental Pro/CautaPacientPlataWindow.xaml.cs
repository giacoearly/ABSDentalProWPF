using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CautaPacientPlataWindow.xaml
    /// </summary>
    public partial class CautaPacientPlataWindow : Window
    {
        public ObservableCollection<Pacient> listaPacienti = new ObservableCollection<Pacient>();

        public Action<Pacient> SendPacientToMainCallback;

        public ObservableCollection<Pacient> listaPacientiProp
        {
            get { return this.listaPacienti; }
            set
            {
                if (listaPacienti != value)
                {
                    listaPacienti = value;
                }
            }
        }

        public CautaPacientPlataWindow()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            tbCauta.Focus();
        }

        private void dataGridPacienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (btnIncarca.IsEnabled == false)
            {
                btnIncarca.IsEnabled = true;
            }
        }

        private void btnInchide_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCauta_Click(object sender, RoutedEventArgs e)
        {
            if (tbCauta.Text==String.Empty)
            {
                return; 
            }

            listaPacienti.Clear(); // for multiple searches, clear DataGrid first

            try
            {
                XDocument documentXmlPacienti = XDocument.Load("pacienti.xml");

                var pacientiCautati = from p in documentXmlPacienti.Root.Elements("pacient")
                                      where (
                                              //p.Element("nume").Value.Contains(tbCauta.Text) ||
                                              //p.Element("prenume").Value.Contains(tbCauta.Text) ||
                                              p.Element("nume").Value.ToLower().Contains(tbCauta.Text.ToLower()) ||
                                              p.Element("prenume").Value.ToLower().Contains(tbCauta.Text.ToLower()) ||
                                              //p.Element("nume").Value.Contains(tbCauta.Text.ToUpper()) ||
                                              //p.Element("prenume").Value.Contains(tbCauta.Text.ToUpper()) ||
                                              //p.Element("nume").Value.Contains(UppercaseFirstLetter(tbCauta.Text)) ||
                                              //p.Element("prenume").Value.Contains(UppercaseFirstLetter(tbCauta.Text)) ||
                                              //(p.Element("nume").Value + " " + p.Element("prenume").Value).Contains(tbCauta.Text) ||
                                              //(p.Element("nume").Value.ToLower() + " " + p.Element("prenume").Value.ToLower()).Contains(tbCauta.Text.ToLower()) ||
                                              (p.Element("nume").Value.ToUpper() + " " + p.Element("prenume").Value.ToUpper()).Contains(tbCauta.Text.ToUpper()) ||
                                              (p.Element("prenume").Value.ToUpper() + " " + p.Element("nume").Value.ToUpper()).Contains(tbCauta.Text.ToUpper())
                                              )
                                      select p;
                foreach (var pacient in pacientiCautati)
                {
                    listaPacienti.Insert(0, new Pacient()
                    {
                        NumarFisa = pacient.Element("numarfisa").Value,
                        Medic = pacient.Descendants("medic").First().Value,
                        Nume = pacient.Element("nume").Value,
                        Prenume = pacient.Element("prenume").Value,
                        Cnp = pacient.Element("cnp").Value,
                        SerieCi = pacient.Element("serieci").Value,
                        NumarCi = pacient.Element("numarci").Value,
                        Varsta = pacient.Element("varsta").Value,
                        Sex = pacient.Element("sex").Value,
                        Telefon = pacient.Element("telefon").Value,
                        Email = pacient.Element("email").Value,
                        Observatii = pacient.Element("observatii").Value
                    });
                }

                if (btnIncarca.IsEnabled == true)
                {
                    btnIncarca.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private string UppercaseFirstLetter(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        private void btnIncarca_Click(object sender, RoutedEventArgs e)
        {
            SendPacientToMainCallback((Pacient)dataGridPacienti.SelectedItem);
            this.Close();
        }
    }
}
