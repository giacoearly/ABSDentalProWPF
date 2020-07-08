using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
    /// Interaction logic for AdaugaProgramareWindow.xaml
    /// </summapy>
    public partial class AdaugaProgramareWindow : Window, INotifyPropertyChanged
    {
        public Programare programareDeAdaugat = new Programare();
        public Pacient pacientAdaugatDinAdaugaProgramareWindow = new Pacient();

        public Action<Pacient> SendPacientProgramareToMainWindowCallback;
        public Action<bool> SendBoolToAdaugaPacientWindowCallback;
        public Action<Programare> SendProgramareToMainWindowCallback;
      
        int indiceMedicSelectat;
        private int indicePacientSelectat;
        bool avemPacientNou = true;

        private ObservableCollection<string> listaPacientiProgramare = new ObservableCollection<string>();
        private ObservableCollection<string> listaMedici = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> listaPacientiPropProgramare
        {
            get { return this.listaPacientiProgramare; }
            set
            {
                if (listaPacientiProgramare != value)
                {
                    listaPacientiProgramare = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> listaMediciProp
        {
            get { return this.listaMedici; }
            set
            {
                if (listaMedici != value)
                {
                    listaMedici = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AdaugaProgramareWindow()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            this.DataContext = this; // pentru DataBinding pt lista pacienti 

            // block some dates
            DatePickerSetBlackOutDates();
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            lbMedici.SelectedIndex = indiceMedicSelectat; 
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

                if (day.DayOfWeek==DayOfWeek.Sunday)
                {
                    datePicker.BlackoutDates.Add(new CalendarDateRange(day));
                }
            }
        }

        private void btnAdaugaPacient_Click(object sender, RoutedEventArgs e)
        {
            AdaugaPacientWindow adaugaPacientWindow = new AdaugaPacientWindow();
            this.SendBoolToAdaugaPacientWindowCallback +=
                new Action<bool>(adaugaPacientWindow.BoolPacientDinAdaugaProgramareWindow);
            SendBoolToAdaugaPacientWindowCallback(avemPacientNou);
            adaugaPacientWindow.SendPacientToAdaugaProgramareWindowCallback += 
                new Action<Pacient>(this.AdaugaPacientToListBox); 
            adaugaPacientWindow.ShowDialog();
        }

        private void AdaugaPacientToListBox(Pacient pacient)
        {
            listaPacientiProgramare.Insert(0, pacient.Nume + " " + pacient.Prenume);
            lbPacienti.SelectedItem = lbPacienti.Items.GetItemAt(0);
            lbPacienti.ScrollIntoView(lbPacienti.Items[0]);

            SendPacientProgramareToMainWindowCallback(pacient);
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (lbMedici.SelectedItem==null)
            {
                MessageBoxCustom.Show("Selectați un medic!", "Medic lipsă");
                return;
            }
            if (lbPacienti.SelectedItem == null)
            {
                MessageBoxCustom.Show("Selectați un pacient!", "Pacient lipsă");
                return;
            }
            if (datePicker.SelectedDate == null)
            {
                MessageBoxCustom.Show("Selectați o dată!", "Dată lipsă");
                return;
            }
            if (cbOra.SelectedItem == null)
            {
                MessageBoxCustom.Show("Selectați o oră!", "Oră lipsă");
                return;
            }
            if (cbDurata.SelectedItem == null)
            {
                MessageBoxCustom.Show("Selectați o durată!", "Durată lipsă");
                return;
            }

            // Verifica daca exista două programări, pentru un pacient, la același medic, în aceeași zi!
            bool aceeasiZi = false;
            aceeasiZi = VerificaProgramariInAceeasiZi();
            if (aceeasiZi)
            {
                MessageBoxCustom.Show("Nu pot exista două programări, pentru un pacient, la același medic, în aceeași zi!",
                                      "Programare invalidă");
                return;
            }

            //  Pot exista maxim 5 programari pentru un pacient la acelasi medic in zile diferite 
            if (VerificaCinciProgramari() == true)
            {
                return;
            }

            // scoate "Dr. " din nume medic
            int len = lbMedici.SelectedItem.ToString().Length;
            string str = lbMedici.SelectedItem.ToString().Substring(4, len - 4);
            string[] medic = str.Split(new char[] { ' ' }, 2);
            string[] pacient = lbPacienti.SelectedItem.ToString().Split(new char[] { ' ' }, 2);
            programareDeAdaugat.NumeMedic = medic[0];
            programareDeAdaugat.PrenumeMedic = medic[1];
            programareDeAdaugat.NumePacient = pacient[0];
            programareDeAdaugat.PrenumePacient = pacient[1];
            programareDeAdaugat.Data = datePicker.SelectedDate.Value;
            programareDeAdaugat.Ora = cbOra.Text + cbMinute.Text;
            programareDeAdaugat.Durata = cbDurata.Text;
            programareDeAdaugat.Descriere = tbDescriere.Text;
            programareDeAdaugat.IndiceListaMedici = lbMedici.SelectedIndex;

            SendProgramareToMainWindowCallback(programareDeAdaugat);

            string programareAdaugataCuSucces =
               string.Format("Programarea pentru {0} {1}, la medicul {2} {3}, a fost adăugată cu succes!",
                             programareDeAdaugat.NumePacient, programareDeAdaugat.PrenumePacient,
                             programareDeAdaugat.NumeMedic, programareDeAdaugat.PrenumeMedic);

            MessageBoxCustom.Show(programareAdaugataCuSucces, "Programare adăugată");

            this.Close();
        }

        private bool VerificaCinciProgramari()
        {
            // scoate "Dr. " din nume medic
            int len = lbMedici.SelectedItem.ToString().Length;
            string str = lbMedici.SelectedItem.ToString().Substring(4, len - 4);
            string[] medic = str.Split(new char[] { ' ' }, 2);
            string[] pacient = lbPacienti.SelectedItem.ToString().Split(new char[] { ' ' }, 2);

            try
            {
                XDocument documentXmlProgramari = XDocument.Load("programari.xml");
                var programari = documentXmlProgramari.Descendants("programare");
                var programareDeModificatSauSters = from p in programari
                                                    where p.Element("numepacient").Value == pacient[0]
                                                    where p.Element("prenumepacient").Value == pacient[1]
                                                    where p.Element("numemedic").Value == medic[0]
                                                    where p.Element("prenumemedic").Value == medic[1]
                                                    select new Programare();
                int progCount = programareDeModificatSauSters.Count();
                if (progCount == 5)
                {
                    MessageBoxCustom.Show("Nu pot exista mai mult de 5 programări, pentru un pacient, la același medic!",
                                          "Programare invalidă");
                    return true;
                }
            }
            catch (FileNotFoundException)
            {
                //MessageBox.Show("Fișierul programari.xml lipsește!", "Fișier inexistent");
            }

            return false;
        }

        public bool VerificaProgramariInAceeasiZi()
        {
            // scoate "Dr. " din nume medic
            int len = lbMedici.SelectedItem.ToString().Length;
            string str = lbMedici.SelectedItem.ToString().Substring(4, len - 4);
            string[] medic = str.Split(new char[] { ' ' }, 2);
            string[] pacient = lbPacienti.SelectedItem.ToString().Split(new char[] { ' ' }, 2);

            try
            {
                XDocument documentXmlProgramari = XDocument.Load("programari.xml");
                var programari = documentXmlProgramari.Descendants("programare");
                var programareDeSters = from p in programari
                                        where p.Element("numepacient").Value == pacient[0]
                                        where p.Element("prenumepacient").Value == pacient[1]
                                        where p.Element("numemedic").Value == medic[0]
                                        where p.Element("prenumemedic").Value == medic[1]
                                        where p.Element("data").Value == datePicker.SelectedDate.Value.ToShortDateString()
                                        select new Programare()
                                        {
                                            NumePacient = p.Descendants("numepacient").First().Value,
                                            PrenumePacient = p.Descendants("prenumepacient").First().Value,
                                            NumeMedic = p.Descendants("numemedic").First().Value,
                                            PrenumeMedic = p.Descendants("prenumemedic").First().Value, 
                                            Data = Convert.ToDateTime(p.Descendants("data").First().Value),
                                        };
                var prog = programareDeSters.ToArray();

                //  verifica daca exista deja programare in aceasta zi
                if (prog.Count() != 0)
                {
                    return true;
                }
            }
            catch (FileNotFoundException)
            {
                //MessageBox.Show("Fișierul programari.xml lipsește!", "Fișier inexistent");
            }

            return false; 
        }

        internal void SendIndiceMedicToAdaugaProgramareWindowFunc(int indice)
        {
            lbMedici.SelectedIndex = indice;
            indiceMedicSelectat = indice;
        }

        private void chkbMedicSelectat_Unchecked(object sender, RoutedEventArgs e)
        {
            listaPacientiProgramare.Clear();

            XDocument documentXmlPacienti = XDocument.Load("pacienti.xml");
            var pacientiLista = documentXmlPacienti.Descendants("pacient");
            var pacienti = from p in pacientiLista
                           select new Pacient()
                           {
                               Nume = p.Descendants("nume").First().Value,
                               Prenume = p.Descendants("prenume").First().Value,
                           };
            List<string> lista = new List<string>();
            foreach (var item in pacienti)
            {
                lista.Add(item.Nume + " " + item.Prenume);
            }
            lista = new List<string>(lista.OrderBy(i => i));
            foreach (var item in lista)
            {
                listaPacientiProgramare.Add(item);
            }
        }

        private void chkbMedicSelectat_Checked(object sender, RoutedEventArgs e)
        {
            MedicSelecteazaPacientii();       
        }

        private void MedicSelecteazaPacientii()
        {
            listaPacientiProgramare.Clear();
      
            if (listaMedici.Count == 0)
            {
                try
                {
                    XDocument documentXmlMedici = XDocument.Load("medici.xml");
                    var mediciLista = documentXmlMedici.Descendants("medic");
                    var medici = from m in mediciLista
                                 select new Medic()
                                 {
                                     Nume = m.Descendants("nume").First().Value,
                                     Prenume = m.Descendants("prenume").First().Value,
                                 };
                    foreach (var item in medici)
                    {
                        listaMedici.Add("Dr. " + item.Nume + " " + item.Prenume);
                    }
                    lbMedici.SelectedIndex = indiceMedicSelectat;
                }
                catch (Exception)
                {
                    return; 
                }
            }

            try
            {
                XDocument documentXmlPacienti = XDocument.Load("pacienti.xml");
                var pacientiLista = documentXmlPacienti.Descendants("pacient");
                var pacienti = from p in pacientiLista
                               select new Pacient()
                               {
                                   Nume = p.Descendants("nume").First().Value,
                                   Prenume = p.Descendants("prenume").First().Value,
                                   Medic = p.Descendants("medic").First().Value,
                               };
                List<string> lista = new List<string>();
                foreach (var item in pacienti)
                {
                    if (item.Medic == (listaMedici[indiceMedicSelectat]))
                    {
                        lista.Add(item.Nume + " " + item.Prenume);
                    }
                }
                lista = new List<string>(lista.OrderBy(i => i));
                foreach (var item in lista)
                {
                    listaPacientiProgramare.Add(item);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void lbMedici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMedici.SelectedIndex != -1)
            {
                indiceMedicSelectat = lbMedici.SelectedIndex; 
            }

            if ((bool)chkbMedicSelectat.IsChecked)
            {
                MedicSelecteazaPacientii();
            }
        }

        //private void btnModifica_Click(object sender, RoutedEventArgs e)
        //{
        //    string[] pacient = lbPacienti.SelectedItem.ToString().Split(new char[] { ' ' }, 2);
        //    indicePacientSelectat = lbPacienti.SelectedIndex;
        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {
                
        //    }
        //}

        //private void lbPacienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (btnModifica.IsEnabled == false)
        //    {
        //        btnModifica.IsEnabled = true;
        //    }
        //}
    }
 } 
