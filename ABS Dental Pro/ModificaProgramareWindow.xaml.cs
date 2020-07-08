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
    /// Interaction logic for ModificaProgramareWindow.xaml
    /// </summary>
    public partial class ModificaProgramareWindow : Window
    {
        public Programare programareDeModificatSauSters = new Programare();
        public Programare programareDeModificatInitiala = new Programare();

        private ObservableCollection<string> listaPacientiProgramare = new ObservableCollection<string>();
        private ObservableCollection<string> listaMedici = new ObservableCollection<string>();

        public Action<Programare, Programare> SendProgramareLaModificatInMainWindowCallback;
        public Action<Programare> SendProgramareToMainWindowToDeleteCallback;

        public event PropertyChangedEventHandler PropertyChanged;

        int indiceMedicSelectat;

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

        public ModificaProgramareWindow()
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

                if (day.DayOfWeek == DayOfWeek.Sunday)
                {
                    datePicker.BlackoutDates.Add(new CalendarDateRange(day));
                }
            }
        }

        internal void SendIndiceMedicToModificaProgramareWindowCallbackFunc(int indice)
        {
            lbMedici.SelectedIndex = indice;
            indiceMedicSelectat = indice;
        }

        private void btnModificaProgramare_Click(object sender, RoutedEventArgs e)
        {
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
            if (datePicker.IsEnabled == false)
            {
                MessageBoxCustom.Show("Programarea selectată este inexistentă!", "Programare lipsă");
                return;
            }

            string[] medic = lbMedici.SelectedItem.ToString().Split(new char[] { ' ' }, 2);
            string[] pacient = lbPacienti.SelectedItem.ToString().Split(new char[] { ' ' }, 2);
            programareDeModificatSauSters.NumeMedic = medic[0];
            programareDeModificatSauSters.PrenumeMedic = medic[1];
            programareDeModificatSauSters.NumePacient = pacient[0];
            programareDeModificatSauSters.PrenumePacient = pacient[1];
            programareDeModificatSauSters.Data = datePicker.SelectedDate.Value;
            programareDeModificatSauSters.Ora = cbOra.Text + cbMinute.Text;
            programareDeModificatSauSters.Durata = cbDurata.Text;
            programareDeModificatSauSters.Descriere = tbDescriere.Text;
            programareDeModificatSauSters.IndiceListaMedici = lbMedici.SelectedIndex;
         
            SendProgramareLaModificatInMainWindowCallback(programareDeModificatInitiala, programareDeModificatSauSters);

            string programareModificataCuSucces =
            string.Format("Programarea pentru {0} {1}, la medicul {2} {3}, a fost modificată cu succes!",
                          programareDeModificatSauSters.NumePacient, programareDeModificatSauSters.PrenumePacient,
                          programareDeModificatSauSters.NumeMedic, programareDeModificatSauSters.PrenumeMedic);

            MessageBoxCustom.Show(programareModificataCuSucces, "Programare modificată");

            this.Close();
        }

        private void btnCautaProgramare_Click(object sender, RoutedEventArgs e)
        {
            if (lbMedici.SelectedItem == null)
            {
                MessageBoxCustom.Show("Selectați un medic!", "Medic lipsă");
                return;
            }

            if (lbPacienti.SelectedItem == null)
            {
                MessageBoxCustom.Show("Selectați un pacient!", "Pacient lipsă");
            }
            else
            {
                // in caz ca se fac mai multe cautari 
                datePicker.IsEnabled = false;
                cbOra.IsEnabled = false;
                cbMinute.IsEnabled = false;
                cbDurata.IsEnabled = false;
                tbDescriere.IsEnabled = false;

                string[] medic = lbMedici.SelectedItem.ToString().Split(new char[] { ' ' }, 2);
                string[] pacient = lbPacienti.SelectedItem.ToString().Split(new char[] { ' ' }, 2);

                XDocument documentXmlProgramari = XDocument.Load("programari.xml");
                var programari = documentXmlProgramari.Descendants("programare");
                var programareDeModificatSauSters = from p in programari
                                            where p.Element("numepacient").Value == pacient[0]
                                            where p.Element("prenumepacient").Value == pacient[1]
                                            where p.Element("numemedic").Value == medic[0]
                                            where p.Element("prenumemedic").Value == medic[1]
                                            select new Programare()
                                            {
                                                NumePacient = p.Descendants("numepacient").First().Value,
                                                PrenumePacient = p.Descendants("prenumepacient").First().Value,
                                                NumeMedic = p.Descendants("numemedic").First().Value,
                                                PrenumeMedic = p.Descendants("prenumemedic").First().Value,
                                                Data = Convert.ToDateTime(p.Descendants("data").First().Value),
                                                Ora = p.Descendants("ora").First().Value,
                                                Durata = p.Descendants("durata").First().Value,
                                                Descriere = p.Descendants("descriere").First().Value,
                                                IndiceListaMedici = int.Parse(p.Descendants("indicelistamedici").First().Value),
                                            };
                Programare[] prog = programareDeModificatSauSters.ToArray();           
                Array.Sort(prog);

                if (prog.Count() != 0)
                {
                    switch (prog.Length)
                    {
                        case 1:
                            datePicker.IsEnabled = true;
                            cbOra.IsEnabled = true;
                            cbMinute.IsEnabled = true;
                            cbDurata.IsEnabled = true;
                            tbDescriere.IsEnabled = true;

                            foreach (Programare item in programareDeModificatSauSters)
                            {
                                datePicker.Text = item.Data.ToShortDateString();
                                FillComboboxesToModify(item.Ora, cbOra, cbMinute);
                                cbDurata.Text = item.Durata;
                                tbDescriere.Text = item.Descriere;
                            }
                            // seteaza programarea initiala de mdificat
                            programareDeModificatInitiala.NumeMedic = medic[0];
                            programareDeModificatInitiala.PrenumeMedic = medic[1];
                            programareDeModificatInitiala.NumePacient = pacient[0];
                            programareDeModificatInitiala.PrenumePacient = pacient[1];
                            programareDeModificatInitiala.Data = datePicker.SelectedDate.Value;
                            programareDeModificatInitiala.Ora = cbOra.Text + cbMinute.Text;
                            programareDeModificatInitiala.Durata = cbDurata.Text;
                            programareDeModificatInitiala.Descriere = tbDescriere.Text;
                            programareDeModificatInitiala.IndiceListaMedici = lbMedici.SelectedIndex;
                            break;
                        case 2:
                            SelecteazaProgramareWindow selecteazaProgramareWindow2 = new SelecteazaProgramareWindow();

                            selecteazaProgramareWindow2.SendDataToModificaProgramareWindowCallback +=
                                                                 new Action<string>(this.AdaugaDataToProgramareModificataFunction);
                            selecteazaProgramareWindow2.SendDataIndiceToModificaProgramareWindowCallback +=
                                                    new Action<int>(this.AdaugaIndiceToProgramareModificataFunction);
                            selecteazaProgramareWindow2.rbProgramare1.Content = prog[0].Data.ToShortDateString();
                            selecteazaProgramareWindow2.rbProgramare2.Content = prog[1].Data.ToShortDateString();
                            selecteazaProgramareWindow2.rbProgramare1.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow2.rbProgramare2.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow2.ShowDialog();
                            break;
                        case 3:
                            SelecteazaProgramareWindow selecteazaProgramareWindow3 = new SelecteazaProgramareWindow();

                            selecteazaProgramareWindow3.SendDataToModificaProgramareWindowCallback +=
                                                                 new Action<string>(this.AdaugaDataToProgramareModificataFunction);
                            selecteazaProgramareWindow3.SendDataIndiceToModificaProgramareWindowCallback +=
                                                    new Action<int>(this.AdaugaIndiceToProgramareModificataFunction);
                            selecteazaProgramareWindow3.rbProgramare1.Content = prog[0].Data.ToShortDateString();
                            selecteazaProgramareWindow3.rbProgramare2.Content = prog[1].Data.ToShortDateString();
                            selecteazaProgramareWindow3.rbProgramare3.Content = prog[2].Data.ToShortDateString(); 
                            selecteazaProgramareWindow3.rbProgramare1.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow3.rbProgramare2.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow3.rbProgramare3.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow3.ShowDialog();
                            break;
                        case 4:
                            SelecteazaProgramareWindow selecteazaProgramareWindow4 = new SelecteazaProgramareWindow();

                            selecteazaProgramareWindow4.SendDataToModificaProgramareWindowCallback +=
                                                                 new Action<string>(this.AdaugaDataToProgramareModificataFunction);
                            selecteazaProgramareWindow4.SendDataIndiceToModificaProgramareWindowCallback +=
                                                    new Action<int>(this.AdaugaIndiceToProgramareModificataFunction);
                            selecteazaProgramareWindow4.rbProgramare1.Content = prog[0].Data.ToShortDateString();
                            selecteazaProgramareWindow4.rbProgramare2.Content = prog[1].Data.ToShortDateString();
                            selecteazaProgramareWindow4.rbProgramare3.Content = prog[2].Data.ToShortDateString();
                            selecteazaProgramareWindow4.rbProgramare4.Content = prog[3].Data.ToShortDateString();
                            selecteazaProgramareWindow4.rbProgramare1.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow4.rbProgramare2.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow4.rbProgramare3.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow4.rbProgramare4.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow4.ShowDialog();
                            break;
                        case 5:
                            SelecteazaProgramareWindow selecteazaProgramareWindow5 = new SelecteazaProgramareWindow();

                            selecteazaProgramareWindow5.SendDataToModificaProgramareWindowCallback +=
                                                                 new Action<string>(this.AdaugaDataToProgramareModificataFunction);
                            selecteazaProgramareWindow5.SendDataIndiceToModificaProgramareWindowCallback +=
                                                    new Action<int>(this.AdaugaIndiceToProgramareModificataFunction);
                            selecteazaProgramareWindow5.rbProgramare1.Content = prog[0].Data.ToShortDateString();
                            selecteazaProgramareWindow5.rbProgramare2.Content = prog[1].Data.ToShortDateString();
                            selecteazaProgramareWindow5.rbProgramare3.Content = prog[2].Data.ToShortDateString();
                            selecteazaProgramareWindow5.rbProgramare4.Content = prog[3].Data.ToShortDateString();
                            selecteazaProgramareWindow5.rbProgramare5.Content = prog[4].Data.ToShortDateString();
                            selecteazaProgramareWindow5.rbProgramare1.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow5.rbProgramare2.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow5.rbProgramare3.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow5.rbProgramare4.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow5.rbProgramare5.Visibility = Visibility.Visible;
                            selecteazaProgramareWindow5.ShowDialog();
                            break;
                    }

                }
                else
                {
                    // nu exista programari
                    string str =
                    string.Format("Pacientul {0} {1} nu are programare la doctorul {2} {3}!",
                      pacient[0], pacient[1], medic[0], medic[1]);

                    MessageBoxCustom.Show(str, "Programare lipsă");
                }
            }
        }

        private void AdaugaIndiceToProgramareModificataFunction(int indice)
        {
            cbOra.IsEnabled = true;
            cbMinute.IsEnabled = true;
            cbDurata.IsEnabled = true;
            tbDescriere.IsEnabled = true;

            string[] medic = lbMedici.SelectedItem.ToString().Split(new char[] { ' ' }, 2);
            string[] pacient = lbPacienti.SelectedItem.ToString().Split(new char[] { ' ' }, 2);
            XDocument documentXmlProgramari = XDocument.Load("programari.xml");
            var programari = documentXmlProgramari.Descendants("programare");
            var programareDeModificatSauSters = from p in programari
                                                where p.Element("numepacient").Value == pacient[0]
                                                where p.Element("prenumepacient").Value == pacient[1]
                                                where p.Element("numemedic").Value == medic[0]
                                                where p.Element("prenumemedic").Value == medic[1]
                                                select new Programare()
                                                {
                                                    NumePacient = p.Descendants("numepacient").First().Value,
                                                    PrenumePacient = p.Descendants("prenumepacient").First().Value,
                                                    NumeMedic = p.Descendants("numemedic").First().Value,
                                                    PrenumeMedic = p.Descendants("prenumemedic").First().Value,
                                                    Data = Convert.ToDateTime(p.Descendants("data").First().Value),
                                                    Ora = p.Descendants("ora").First().Value,
                                                    Durata = p.Descendants("durata").First().Value,
                                                    Descriere = p.Descendants("descriere").First().Value,
                                                    IndiceListaMedici = int.Parse(p.Descendants("indicelistamedici").First().Value),
                                                };
            Programare[] prog = programareDeModificatSauSters.ToArray();
            Array.Sort(prog);

            FillComboboxesToModify(prog[indice].Ora, cbOra, cbMinute);
            cbDurata.Text = prog[indice].Durata;
            tbDescriere.Text = prog[indice].Descriere;

            // seteaza programarea initiala de mdificat
            programareDeModificatInitiala.NumeMedic = medic[0];
            programareDeModificatInitiala.PrenumeMedic = medic[1];
            programareDeModificatInitiala.NumePacient = pacient[0];
            programareDeModificatInitiala.PrenumePacient = pacient[1];
            programareDeModificatInitiala.Data = datePicker.SelectedDate.Value;
            programareDeModificatInitiala.Ora = cbOra.Text + cbMinute.Text;
            programareDeModificatInitiala.Durata = cbDurata.Text;
            programareDeModificatInitiala.Descriere = tbDescriere.Text;
            programareDeModificatInitiala.IndiceListaMedici = lbMedici.SelectedIndex;
        }

        private void AdaugaDataToProgramareModificataFunction(string data)
        {
            datePicker.IsEnabled = true;
            datePicker.Text = data;
        }

        private void FillComboboxesToModify(string hm, ComboBox cb1, ComboBox cb2)
        {
            if (hm != null)
            {
                switch (hm.Length)
                {
                    case 1:
                        cb1.Text = hm;
                        break;
                    case 2:
                        cb1.Text = hm;
                        break;
                    case 3:
                        cb1.Text = hm.Substring(0, 1);
                        cb2.Text = hm.Substring(1, 2);
                        break;
                    case 4:
                        cb1.Text = hm.Substring(0, 2);
                        cb2.Text = hm.Substring(2, 2);
                        break;
                    default:
                        break;
                }
            }
        }

        private void lbPacienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // in caz ca se fac mai multe cautari 
            if (datePicker.IsEnabled==true)
            {
                datePicker.IsEnabled = false;
                cbOra.IsEnabled = false;
                cbMinute.IsEnabled = false;
                cbDurata.IsEnabled = false;
                tbDescriere.IsEnabled = false; 
            }
        }

        private void lbMedici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // in caz ca se fac mai multe cautari, cu alt medic
            if (datePicker.IsEnabled == true)
            {
                datePicker.IsEnabled = false;
                cbOra.IsEnabled = false;
                cbMinute.IsEnabled = false;
                cbDurata.IsEnabled = false;
                tbDescriere.IsEnabled = false;
            }

            if (lbMedici.SelectedIndex != -1)
            {
                indiceMedicSelectat = lbMedici.SelectedIndex;
            }

            if ((bool)chkbMedicSelectat.IsChecked)
            {
                MedicSelecteazaPacientii();
            }
        }

        private void btnStergeProgramare_Click(object sender, RoutedEventArgs e)
        {
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
            if (datePicker.IsEnabled == false)
            {
                MessageBoxCustom.Show("Programarea selectată este inexistentă!", "Programare lipsă");
                return;
            }

            string[] medic = lbMedici.SelectedItem.ToString().Split(new char[] { ' ' }, 2);
            string[] pacient = lbPacienti.SelectedItem.ToString().Split(new char[] { ' ' }, 2);

            bool? stergeProgramare;
            string str = string.Format("Sigur vreți să ștergeți programarea pentru pacientul {0} {1}, la medicul {2} {3}?",
                                               pacient[0], pacient[1], medic[0], medic[1]);
            stergeProgramare = MessageBoxCustom.Show(str, "Ștergere programare", MessageBoxButton.YesNo);

            try
            {
                if ((bool)stergeProgramare)
                {
                    XDocument documentXmlProgramari = XDocument.Load("programari.xml");
                    var programari = documentXmlProgramari.Descendants("programare");
                    var programareDeSters = from p in programari
                                            where p.Element("numepacient").Value == pacient[0]
                                            where p.Element("prenumepacient").Value == pacient[1]
                                            where p.Element("numemedic").Value == medic[0]
                                            where p.Element("prenumemedic").Value == medic[1]
                                            where p.Element("data").Value == datePicker.SelectedDate.Value.ToShortDateString()
                                            where p.Element("ora").Value == cbOra.Text + cbMinute.Text
                                            where p.Element("durata").Value == cbDurata.Text
                                            where p.Element("descriere").Value == tbDescriere.Text
                                            where p.Element("indicelistamedici").Value == lbMedici.SelectedIndex.ToString()
                                            select new Programare()
                                            {
                                                NumePacient = p.Descendants("numepacient").First().Value,
                                                PrenumePacient = p.Descendants("prenumepacient").First().Value,
                                                NumeMedic = p.Descendants("numemedic").First().Value,
                                                PrenumeMedic = p.Descendants("prenumemedic").First().Value,
                                                Data = Convert.ToDateTime(p.Descendants("data").First().Value),
                                                Ora = p.Descendants("ora").First().Value,
                                                Durata = p.Descendants("durata").First().Value,
                                                Descriere = p.Descendants("descriere").First().Value,
                                                IndiceListaMedici = int.Parse(p.Descendants("indicelistamedici").First().Value),
                                            };
                    var prog = programareDeSters.ToArray();

                    if (prog.Count() != 0)
                    {
                        datePicker.IsEnabled = false;
                        cbOra.IsEnabled = false;
                        cbMinute.IsEnabled = false;
                        cbDurata.IsEnabled = false;
                        tbDescriere.IsEnabled = false;

                        programareDeModificatSauSters.NumeMedic = medic[0];
                        programareDeModificatSauSters.PrenumeMedic = medic[1];
                        programareDeModificatSauSters.NumePacient = pacient[0];
                        programareDeModificatSauSters.PrenumePacient = pacient[1];
                        programareDeModificatSauSters.Data = datePicker.SelectedDate.Value;
                        programareDeModificatSauSters.Ora = cbOra.Text + cbMinute.Text;
                        programareDeModificatSauSters.Durata = cbDurata.Text;
                        programareDeModificatSauSters.Descriere = tbDescriere.Text;
                        programareDeModificatSauSters.IndiceListaMedici = lbMedici.SelectedIndex;

                        SendProgramareToMainWindowToDeleteCallback(programareDeModificatSauSters);

                        string strProgramare =
                        string.Format("Programarea pentru {0} {1}, la medicul {2} {3}, a fost ștearsă cu succes!",
                                      programareDeModificatSauSters.NumePacient, programareDeModificatSauSters.PrenumePacient,
                                      programareDeModificatSauSters.NumeMedic, programareDeModificatSauSters.PrenumeMedic);

                        MessageBoxCustom.Show(strProgramare, "Programare ștearsă");

                        this.Close();
                    }
                    else
                    {
                        string strLipsa =
                        string.Format("Programarea pacientului {0} {1}, la medicul {2} {3}, nu există!",
                            pacient[0], pacient[1], medic[0], medic[1]);

                        MessageBoxCustom.Show(strLipsa, "Programare lipsă");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Fișierul programari.xml lipsește!", "Fișier inexistent");
            }
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
                    listaMedici.Add(item.Nume + " " + item.Prenume);
                }
                lbMedici.SelectedIndex = indiceMedicSelectat;
            }

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
                if (item.Medic == ("Dr. " + listaMedici[indiceMedicSelectat]))
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
    }
}
