using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.IO;
using System.Threading;
using System.Globalization;

namespace ABS_Dental_Pro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Pacient> listaPacienti = new ObservableCollection<Pacient>();    

        public Action<Pacient> SendPacientLaModificatCallback;
        public Action<int> SendIndiceMedicToAdaugaProgramareWindowCallback;
        public Action<int> SendIndiceMedicToModificaProgramareWindowCallback;
        public Action<string, string> SendNumePrenumeCallback;
        public Action<string, string, string> SendNumePrenumeMedicCallback;
        public Action<Plata> SendPlataLaModificatCallback;

        Pacient pacientDeTrimisLaModificat = new Pacient();
        Programare programareDeModificatInitiala = new Programare();
        int indiceDeTrimisLaModificat;
        public int NumarDetaliiMedic = 0;

        // adauga zile
        int offsetDays1 = 0;
        int offsetDays2 = 0;
        int offsetDays3 = 0;
        int offsetDays4 = 0;
        int offsetDays5 = 0;
        int offsetDays6 = 0;

        public Dictionary<Programare, Grid> listaGridProgramariCanvas1 = new Dictionary<Programare, Grid>();
        public Dictionary<Programare, Grid> listaGridProgramariCanvas2 = new Dictionary<Programare, Grid>();
        public Dictionary<Programare, Grid> listaGridProgramariCanvas3 = new Dictionary<Programare, Grid>();
        public Dictionary<Programare, Grid> listaGridProgramariCanvas4 = new Dictionary<Programare, Grid>();
        public Dictionary<Programare, Grid> listaGridProgramariCanvas5 = new Dictionary<Programare, Grid>();
        public Dictionary<Programare, Grid> listaGridProgramariCanvas6 = new Dictionary<Programare, Grid>();

        public string primaZiProgramariPacienti1; public string primaZiProgramariPacienti2; public string primaZiProgramariPacienti3;
        public string primaZiProgramariPacienti4; public string primaZiProgramariPacienti5; public string primaZiProgramariPacienti6;
        public string ultimaZiProgramariPacienti1; public string ultimaZiProgramariPacienti2; public string ultimaZiProgramariPacienti3;
        public string ultimaZiProgramariPacienti4; public string ultimaZiProgramariPacienti5; public string ultimaZiProgramariPacienti6;

        // Pentru modificarea unui pacient din tabul plati
        private bool modificaPacientPlata = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Pacient> listaPacientiProp
        {
            get { return this.listaPacienti; }
            set
            {
                if (listaPacienti != value)
                {
                    listaPacienti = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        { 
            InitializeComponent();

            // activeaza btnAdaugaMedic, in caz ca este inactiv, atunci cand se sterge un medic
            ActiveazaBtnAdaugaMedic();
         
            PopulateInitDataGrid();
            PopulateDetaliiMedici();
            PopulateProgramariTabItemHeaders();  
            PutDatesOverProgramariPacienti();
            ShowProgramariInProgramariPacienti();
            DeleteProgramariVechiDinXml();
            AfiseazaDataCurenta();

            //if (tbNrPacienti.Text == "0")
            //{
            //    btnCautaPacient.IsEnabled = false;
            //}

            // Thread nou care la miezul noptii trebuie sa steagra programarile din ziua precedenta
            // si sa schimbe data afisata in tabul programari.
            CreatingNewThreadTimer();
        }

        private void AfiseazaDataCurenta()
        {
            this.Dispatcher.BeginInvoke(new Action(() => { tbDataCurenta1.Text = DateTime.Now.ToShortDateString(); }));
            this.Dispatcher.BeginInvoke(new Action(() => { tbDataCurenta2.Text = DateTime.Now.ToShortDateString(); }));
            this.Dispatcher.BeginInvoke(new Action(() => { tbDataCurenta3.Text = DateTime.Now.ToShortDateString(); }));
            this.Dispatcher.BeginInvoke(new Action(() => { tbDataCurenta4.Text = DateTime.Now.ToShortDateString(); }));
            this.Dispatcher.BeginInvoke(new Action(() => { tbDataCurenta5.Text = DateTime.Now.ToShortDateString(); }));
            this.Dispatcher.BeginInvoke(new Action(() => { tbDataCurenta6.Text = DateTime.Now.ToShortDateString(); }));
        }

        private void CreatingNewThreadTimer()
        {
            Thread thread = new Thread(new ThreadStart(MyTimer));
            thread.IsBackground = true;
            thread.Start();
        }

        private void MyTimer()
        {
            DateTime time;
            while (true)
            {
                time = DateTime.Now;
                int hour = time.Hour;
                int minute = time.Minute;
                //int second = time.Second;
                if (hour == 0 && minute == 0)
                {
                    MyMethodToExecute();
                }
                else
                {
                    Thread.Sleep(30000);
                }
            }
        }

        private void MyMethodToExecute()
        {
            DeleteProgramariVechiDinXml();
            AfiseazaDataCurenta();
        }
        
        private void ActiveazaBtnAdaugaMedic()
        {
            this.detaliiMedic1.MedicSters += new EventHandler(MyEventHandlerFunction_MedicSters);
            this.detaliiMedic2.MedicSters += new EventHandler(MyEventHandlerFunction_MedicSters);
            this.detaliiMedic3.MedicSters += new EventHandler(MyEventHandlerFunction_MedicSters);
            this.detaliiMedic4.MedicSters += new EventHandler(MyEventHandlerFunction_MedicSters);
            this.detaliiMedic5.MedicSters += new EventHandler(MyEventHandlerFunction_MedicSters);
            this.detaliiMedic6.MedicSters += new EventHandler(MyEventHandlerFunction_MedicSters);
        }

        private void MyEventHandlerFunction_MedicSters(object sender, EventArgs e)
        {
            if (btnAdaugaMedic.IsEnabled==false)
            {
                btnAdaugaMedic.IsEnabled = true; 
            }
        }

        private void DeleteProgramariVechiDinXml()
        {
            // pentru ToShortDateString
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");

            try
            {
                XDocument documentXmlProgramari = XDocument.Load("programari.xml");

                try
                {
                    XDocument documentXmlIstorii = XDocument.Load("istorii.xml");
                }
                catch (FileNotFoundException)
                {
                    XDocument documentXmlIstorii = new XDocument(new XElement("istorii"));
                    documentXmlIstorii.Save("istorii.xml");
                }

                int nrElemToRemove = documentXmlProgramari.Root.Elements("programare")
                  .Where(x => DateTime.Parse(x.Element("data").Value,
                         System.Globalization.CultureInfo.CreateSpecificCulture("ro-RO")) < DateTime.Today).Count();
                if (nrElemToRemove != 0)
                {
                    XDocument documentXmlIstorii = XDocument.Load("istorii.xml");

                    var programariShow = documentXmlProgramari.Descendants("programare");
                    var programari = from p in programariShow
                                        where DateTime.Parse(p.Element("data").Value,
                                                            System.Globalization.CultureInfo.CreateSpecificCulture("ro-RO")) < DateTime.Today
                                        select new Programare()
                                        {
                                            NumePacient = p.Descendants("numepacient").First().Value,
                                            PrenumePacient = p.Descendants("prenumepacient").First().Value,
                                            NumeMedic = p.Descendants("numemedic").First().Value,
                                            PrenumeMedic = p.Descendants("prenumemedic").First().Value,
                                            Data = DateTime.Parse(p.Descendants("data").First().Value,
                                                    System.Globalization.CultureInfo.CreateSpecificCulture("ro-RO")),
                                            Descriere = p.Descendants("descriere").First().Value,
                                    };
                    foreach (var item in programari)
                    {
                        XElement xelem = new XElement("istorie");

                        xelem.Add(new XElement("pacient", item.NumePacient + " " + item.PrenumePacient));
                        xelem.Add(new XElement("medic", "Dr. " + item.NumeMedic + " " + item.PrenumeMedic));
                        xelem.Add(new XElement("data", item.Data.ToShortDateString()));
                        xelem.Add(new XElement("descriere", item.Descriere));

                        documentXmlIstorii.Element("istorii").Add(xelem);
                        documentXmlIstorii.Save("istorii.xml");
                    }

                    // sterge programari din .xml, mai vechi decat azi
                    documentXmlProgramari.Root.Elements("programare").Where(x => DateTime.Parse(x.Element("data").Value,
                            System.Globalization.CultureInfo.CreateSpecificCulture("ro-RO")) < DateTime.Today).Remove();
                    documentXmlProgramari.Save("programari.xml");
                }             
            } 
            catch (FileNotFoundException)
            {
                //MessageBox.Show("Fișierul programari.xml lipsește!", "Fișier inexistent");
            }
        }

        private void ShowProgramariInProgramariPacienti()
        {
            try
            {
                XDocument documentXmlProgramari = XDocument.Load("programari.xml");
                var programariShow = documentXmlProgramari.Descendants("programare");
                var programari = from p in programariShow
                                 select new Programare()
                                 {
                                     NumePacient = p.Descendants("numepacient").First().Value,
                                     PrenumePacient = p.Descendants("prenumepacient").First().Value,
                                     NumeMedic = p.Descendants("numemedic").First().Value,
                                     PrenumeMedic = p.Descendants("prenumemedic").First().Value,
                                     Data = DateTime.Parse(p.Descendants("data").First().Value,
                                                System.Globalization.CultureInfo.CreateSpecificCulture("ro-RO")),
                                     Ora = p.Descendants("ora").First().Value,
                                     Durata = p.Descendants("durata").First().Value,
                                     Descriere = p.Descendants("descriere").First().Value,
                                     IndiceListaMedici = int.Parse(p.Descendants("indicelistamedici").First().Value),
                                 };
                foreach (var item in programari)
                {
                    AdaugaProgramareInProgramariPacienti(item);
                }
            }
            catch (FileNotFoundException)
            {
                //MessageBox.Show("Fișierul programari.xml lipsește!", "Fișier inexistent");
            }
        }

        private void PutDatesOverProgramariPacienti()
        { 
            DateTime day = DateTime.Today;

            // skip Sunday
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);        
            }
            programariPacienti1.oZiIntreaga1.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga1.ZiLuna.Text = programariPacienti1.oZiIntreaga1.ZiLuna.Text;
            programariPacienti3.oZiIntreaga1.ZiLuna.Text = programariPacienti1.oZiIntreaga1.ZiLuna.Text;
            programariPacienti4.oZiIntreaga1.ZiLuna.Text = programariPacienti1.oZiIntreaga1.ZiLuna.Text;
            programariPacienti5.oZiIntreaga1.ZiLuna.Text = programariPacienti1.oZiIntreaga1.ZiLuna.Text;
            programariPacienti6.oZiIntreaga1.ZiLuna.Text = programariPacienti1.oZiIntreaga1.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga2.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga2.ZiLuna.Text = programariPacienti1.oZiIntreaga2.ZiLuna.Text;
            programariPacienti3.oZiIntreaga2.ZiLuna.Text = programariPacienti1.oZiIntreaga2.ZiLuna.Text;
            programariPacienti4.oZiIntreaga2.ZiLuna.Text = programariPacienti1.oZiIntreaga2.ZiLuna.Text;
            programariPacienti5.oZiIntreaga2.ZiLuna.Text = programariPacienti1.oZiIntreaga2.ZiLuna.Text;
            programariPacienti6.oZiIntreaga2.ZiLuna.Text = programariPacienti1.oZiIntreaga2.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga3.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga3.ZiLuna.Text = programariPacienti1.oZiIntreaga3.ZiLuna.Text;
            programariPacienti3.oZiIntreaga3.ZiLuna.Text = programariPacienti1.oZiIntreaga3.ZiLuna.Text;
            programariPacienti4.oZiIntreaga3.ZiLuna.Text = programariPacienti1.oZiIntreaga3.ZiLuna.Text;
            programariPacienti5.oZiIntreaga3.ZiLuna.Text = programariPacienti1.oZiIntreaga3.ZiLuna.Text;
            programariPacienti6.oZiIntreaga3.ZiLuna.Text = programariPacienti1.oZiIntreaga3.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga4.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga4.ZiLuna.Text = programariPacienti1.oZiIntreaga4.ZiLuna.Text;
            programariPacienti3.oZiIntreaga4.ZiLuna.Text = programariPacienti1.oZiIntreaga4.ZiLuna.Text;
            programariPacienti4.oZiIntreaga4.ZiLuna.Text = programariPacienti1.oZiIntreaga4.ZiLuna.Text;
            programariPacienti5.oZiIntreaga4.ZiLuna.Text = programariPacienti1.oZiIntreaga4.ZiLuna.Text;
            programariPacienti6.oZiIntreaga4.ZiLuna.Text = programariPacienti1.oZiIntreaga4.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga5.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga5.ZiLuna.Text = programariPacienti1.oZiIntreaga5.ZiLuna.Text;
            programariPacienti3.oZiIntreaga5.ZiLuna.Text = programariPacienti1.oZiIntreaga5.ZiLuna.Text;
            programariPacienti4.oZiIntreaga5.ZiLuna.Text = programariPacienti1.oZiIntreaga5.ZiLuna.Text;
            programariPacienti5.oZiIntreaga5.ZiLuna.Text = programariPacienti1.oZiIntreaga5.ZiLuna.Text;
            programariPacienti6.oZiIntreaga5.ZiLuna.Text = programariPacienti1.oZiIntreaga5.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga6.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga6.ZiLuna.Text = programariPacienti1.oZiIntreaga6.ZiLuna.Text;
            programariPacienti3.oZiIntreaga6.ZiLuna.Text = programariPacienti1.oZiIntreaga6.ZiLuna.Text;
            programariPacienti4.oZiIntreaga6.ZiLuna.Text = programariPacienti1.oZiIntreaga6.ZiLuna.Text;
            programariPacienti5.oZiIntreaga6.ZiLuna.Text = programariPacienti1.oZiIntreaga6.ZiLuna.Text;
            programariPacienti6.oZiIntreaga6.ZiLuna.Text = programariPacienti1.oZiIntreaga6.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga7.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga7.ZiLuna.Text = programariPacienti1.oZiIntreaga7.ZiLuna.Text;
            programariPacienti3.oZiIntreaga7.ZiLuna.Text = programariPacienti1.oZiIntreaga7.ZiLuna.Text;
            programariPacienti4.oZiIntreaga7.ZiLuna.Text = programariPacienti1.oZiIntreaga7.ZiLuna.Text;
            programariPacienti5.oZiIntreaga7.ZiLuna.Text = programariPacienti1.oZiIntreaga7.ZiLuna.Text;
            programariPacienti6.oZiIntreaga7.ZiLuna.Text = programariPacienti1.oZiIntreaga7.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga8.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga8.ZiLuna.Text = programariPacienti1.oZiIntreaga8.ZiLuna.Text;
            programariPacienti3.oZiIntreaga8.ZiLuna.Text = programariPacienti1.oZiIntreaga8.ZiLuna.Text;
            programariPacienti4.oZiIntreaga8.ZiLuna.Text = programariPacienti1.oZiIntreaga8.ZiLuna.Text;
            programariPacienti5.oZiIntreaga8.ZiLuna.Text = programariPacienti1.oZiIntreaga8.ZiLuna.Text;
            programariPacienti6.oZiIntreaga8.ZiLuna.Text = programariPacienti1.oZiIntreaga8.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga9.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga9.ZiLuna.Text = programariPacienti1.oZiIntreaga9.ZiLuna.Text;
            programariPacienti3.oZiIntreaga9.ZiLuna.Text = programariPacienti1.oZiIntreaga9.ZiLuna.Text;
            programariPacienti4.oZiIntreaga9.ZiLuna.Text = programariPacienti1.oZiIntreaga9.ZiLuna.Text;
            programariPacienti5.oZiIntreaga9.ZiLuna.Text = programariPacienti1.oZiIntreaga9.ZiLuna.Text;
            programariPacienti6.oZiIntreaga9.ZiLuna.Text = programariPacienti1.oZiIntreaga9.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga10.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga10.ZiLuna.Text = programariPacienti1.oZiIntreaga10.ZiLuna.Text;
            programariPacienti3.oZiIntreaga10.ZiLuna.Text = programariPacienti1.oZiIntreaga10.ZiLuna.Text;
            programariPacienti4.oZiIntreaga10.ZiLuna.Text = programariPacienti1.oZiIntreaga10.ZiLuna.Text;
            programariPacienti5.oZiIntreaga10.ZiLuna.Text = programariPacienti1.oZiIntreaga10.ZiLuna.Text;
            programariPacienti6.oZiIntreaga10.ZiLuna.Text = programariPacienti1.oZiIntreaga10.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga11.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga11.ZiLuna.Text = programariPacienti1.oZiIntreaga11.ZiLuna.Text;
            programariPacienti3.oZiIntreaga11.ZiLuna.Text = programariPacienti1.oZiIntreaga11.ZiLuna.Text;
            programariPacienti4.oZiIntreaga11.ZiLuna.Text = programariPacienti1.oZiIntreaga11.ZiLuna.Text;
            programariPacienti5.oZiIntreaga11.ZiLuna.Text = programariPacienti1.oZiIntreaga11.ZiLuna.Text;
            programariPacienti6.oZiIntreaga11.ZiLuna.Text = programariPacienti1.oZiIntreaga11.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga12.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga12.ZiLuna.Text = programariPacienti1.oZiIntreaga12.ZiLuna.Text;
            programariPacienti3.oZiIntreaga12.ZiLuna.Text = programariPacienti1.oZiIntreaga12.ZiLuna.Text;
            programariPacienti4.oZiIntreaga12.ZiLuna.Text = programariPacienti1.oZiIntreaga12.ZiLuna.Text;
            programariPacienti5.oZiIntreaga12.ZiLuna.Text = programariPacienti1.oZiIntreaga12.ZiLuna.Text;
            programariPacienti6.oZiIntreaga12.ZiLuna.Text = programariPacienti1.oZiIntreaga12.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga13.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga13.ZiLuna.Text = programariPacienti1.oZiIntreaga13.ZiLuna.Text;
            programariPacienti3.oZiIntreaga13.ZiLuna.Text = programariPacienti1.oZiIntreaga13.ZiLuna.Text;
            programariPacienti4.oZiIntreaga13.ZiLuna.Text = programariPacienti1.oZiIntreaga13.ZiLuna.Text;
            programariPacienti5.oZiIntreaga13.ZiLuna.Text = programariPacienti1.oZiIntreaga13.ZiLuna.Text;
            programariPacienti6.oZiIntreaga13.ZiLuna.Text = programariPacienti1.oZiIntreaga13.ZiLuna.Text;
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga14.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            programariPacienti2.oZiIntreaga14.ZiLuna.Text = programariPacienti1.oZiIntreaga14.ZiLuna.Text;
            programariPacienti3.oZiIntreaga14.ZiLuna.Text = programariPacienti1.oZiIntreaga14.ZiLuna.Text;
            programariPacienti4.oZiIntreaga14.ZiLuna.Text = programariPacienti1.oZiIntreaga14.ZiLuna.Text;
            programariPacienti5.oZiIntreaga14.ZiLuna.Text = programariPacienti1.oZiIntreaga14.ZiLuna.Text;
            programariPacienti6.oZiIntreaga14.ZiLuna.Text = programariPacienti1.oZiIntreaga14.ZiLuna.Text;
        }

        private void PopulateProgramariTabItemHeaders()
        {
            if (detaliiMedic1.IsEnabled)
            {
                tabItemMedic1.Header = "Dr. " + detaliiMedic1.lbNume.Content.ToString() + " " + detaliiMedic1.lbPrenume.Content.ToString();
            }
            if (detaliiMedic2.IsEnabled)
            {
                tabItemMedic2.Header = "Dr. " + detaliiMedic2.lbNume.Content.ToString() + " " + detaliiMedic2.lbPrenume.Content.ToString();
            }
            if (detaliiMedic3.IsEnabled)
            {
                tabItemMedic3.Header = "Dr. " + detaliiMedic3.lbNume.Content.ToString() + " " + detaliiMedic3.lbPrenume.Content.ToString();
            }
            if (detaliiMedic4.IsEnabled)
            {
                tabItemMedic4.Header = "Dr. " + detaliiMedic4.lbNume.Content.ToString() + " " + detaliiMedic4.lbPrenume.Content.ToString();
            }
            if (detaliiMedic5.IsEnabled)
            {
                tabItemMedic5.Header = "Dr. " + detaliiMedic5.lbNume.Content.ToString() + " " + detaliiMedic5.lbPrenume.Content.ToString();
            }
            if (detaliiMedic6.IsEnabled)
            {
                tabItemMedic6.Header = "Dr. " + detaliiMedic6.lbNume.Content.ToString() + " " + detaliiMedic6.lbPrenume.Content.ToString(); 
            }
        }

        private void PopulateDetaliiMedici()
        {
            try
            {
                XDocument documentXmlMedici = XDocument.Load("medici.xml");
                var mediciInitDetaliiMedici = documentXmlMedici.Descendants("medic");
                var medici = from m in mediciInitDetaliiMedici
                             select new Medic()
                             {
                                 ID = m.Descendants("id").First().Value,
                                 Nume = m.Descendants("nume").First().Value,
                                 Prenume = m.Descendants("prenume").First().Value,
                                 Telefon = m.Descendants("telefon").First().Value,
                                 Email = m.Descendants("email").First().Value,
                                 Observatii = m.Descendants("observatii").First().Value,
                                 Luni = new Zi()
                                 {
                                     DeLa1 = m.Descendants("lunidela1").First().Value,
                                     PanaLa1 = m.Descendants("lunipanala1").First().Value,
                                     DeLa2 = m.Descendants("lunidela2").First().Value,
                                     PanaLa2 = m.Descendants("lunipanala2").First().Value
                                 },
                                 Marti = new Zi()
                                 {
                                     DeLa1 = m.Descendants("martidela1").First().Value,
                                     PanaLa1 = m.Descendants("martipanala1").First().Value,
                                     DeLa2 = m.Descendants("martidela2").First().Value,
                                     PanaLa2 = m.Descendants("martipanala2").First().Value
                                 },
                                 Miercuri = new Zi()
                                 {
                                     DeLa1 = m.Descendants("miercuridela1").First().Value,
                                     PanaLa1 = m.Descendants("miercuripanala1").First().Value,
                                     DeLa2 = m.Descendants("miercuridela2").First().Value,
                                     PanaLa2 = m.Descendants("miercuripanala2").First().Value
                                 },
                                 Joi = new Zi()
                                 {
                                     DeLa1 = m.Descendants("joidela1").First().Value,
                                     PanaLa1 = m.Descendants("joipanala1").First().Value,
                                     DeLa2 = m.Descendants("joidela2").First().Value,
                                     PanaLa2 = m.Descendants("joipanala2").First().Value
                                 },
                                 Vineri = new Zi()
                                 {
                                     DeLa1 = m.Descendants("vineridela1").First().Value,
                                     PanaLa1 = m.Descendants("vineripanala1").First().Value,
                                     DeLa2 = m.Descendants("vineridela2").First().Value,
                                     PanaLa2 = m.Descendants("vineripanala2").First().Value
                                 },
                                 Sambata = new Zi()
                                 {
                                     DeLa1 = m.Descendants("sambatadela1").First().Value,
                                     PanaLa1 = m.Descendants("sambatapanala1").First().Value,
                                     DeLa2 = m.Descendants("sambatadela2").First().Value,
                                     PanaLa2 = m.Descendants("sambatapanala2").First().Value
                                 }
                             };
                foreach (var item in medici)
                {
                    switch (NumarDetaliiMedic)
                    {
                        case 0:
                            detaliiMedic1.Visibility = Visibility.Visible;
                            detaliiMedic1.IsEnabled = true;
                            detaliiMedic1.lbID.Content = item.ID;
                            detaliiMedic1.lbNume.Content = item.Nume;
                            detaliiMedic1.lbPrenume.Content = item.Prenume;
                            detaliiMedic1.lbTelefon.Content = item.Telefon;
                            detaliiMedic1.lbEmail.Content = item.Email;
                            detaliiMedic1.lbObservatii.Content = item.Observatii;
                            detaliiMedic1.lbLuni1.Content = ParseTimeForMinutesAndPutColon(item.Luni.DeLa1);
                            detaliiMedic1.lbLuni2.Content = ParseTimeForMinutesAndPutColon(item.Luni.PanaLa1);
                            detaliiMedic1.lbLuni3.Content = ParseTimeForMinutesAndPutColon(item.Luni.DeLa2);
                            detaliiMedic1.lbLuni4.Content = ParseTimeForMinutesAndPutColon(item.Luni.PanaLa2);
                            detaliiMedic1.lbMarti1.Content = ParseTimeForMinutesAndPutColon(item.Marti.DeLa1);
                            detaliiMedic1.lbMarti2.Content = ParseTimeForMinutesAndPutColon(item.Marti.PanaLa1);
                            detaliiMedic1.lbMarti3.Content = ParseTimeForMinutesAndPutColon(item.Marti.DeLa2);
                            detaliiMedic1.lbMarti4.Content = ParseTimeForMinutesAndPutColon(item.Marti.PanaLa2);
                            detaliiMedic1.lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.DeLa1);
                            detaliiMedic1.lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.PanaLa1);
                            detaliiMedic1.lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.DeLa2);
                            detaliiMedic1.lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.PanaLa2);
                            detaliiMedic1.lbJoi1.Content = ParseTimeForMinutesAndPutColon(item.Joi.DeLa1);
                            detaliiMedic1.lbJoi2.Content = ParseTimeForMinutesAndPutColon(item.Joi.PanaLa1);
                            detaliiMedic1.lbJoi3.Content = ParseTimeForMinutesAndPutColon(item.Joi.DeLa2);
                            detaliiMedic1.lbJoi4.Content = ParseTimeForMinutesAndPutColon(item.Joi.PanaLa2);
                            detaliiMedic1.lbVineri1.Content = ParseTimeForMinutesAndPutColon(item.Vineri.DeLa1);
                            detaliiMedic1.lbVineri2.Content = ParseTimeForMinutesAndPutColon(item.Vineri.PanaLa1);
                            detaliiMedic1.lbVineri3.Content = ParseTimeForMinutesAndPutColon(item.Vineri.DeLa2);
                            detaliiMedic1.lbVineri4.Content = ParseTimeForMinutesAndPutColon(item.Vineri.PanaLa2);
                            detaliiMedic1.lbSambata1.Content = ParseTimeForMinutesAndPutColon(item.Sambata.DeLa1);
                            detaliiMedic1.lbSambata2.Content = ParseTimeForMinutesAndPutColon(item.Sambata.PanaLa1);
                            detaliiMedic1.lbSambata3.Content = ParseTimeForMinutesAndPutColon(item.Sambata.DeLa2);
                            detaliiMedic1.lbSambata4.Content = ParseTimeForMinutesAndPutColon(item.Sambata.PanaLa2);
                            NumarDetaliiMedic++;
                            break;
                        case 1:
                            detaliiMedic2.Visibility = Visibility.Visible;
                            detaliiMedic2.IsEnabled = true;
                            detaliiMedic2.lbID.Content = item.ID;
                            detaliiMedic2.lbNume.Content = item.Nume;
                            detaliiMedic2.lbPrenume.Content = item.Prenume;
                            detaliiMedic2.lbTelefon.Content = item.Telefon;
                            detaliiMedic2.lbEmail.Content = item.Email;
                            detaliiMedic2.lbObservatii.Content = item.Observatii;
                            detaliiMedic2.lbLuni1.Content = ParseTimeForMinutesAndPutColon(item.Luni.DeLa1);
                            detaliiMedic2.lbLuni2.Content = ParseTimeForMinutesAndPutColon(item.Luni.PanaLa1);
                            detaliiMedic2.lbLuni3.Content = ParseTimeForMinutesAndPutColon(item.Luni.DeLa2);
                            detaliiMedic2.lbLuni4.Content = ParseTimeForMinutesAndPutColon(item.Luni.PanaLa2);
                            detaliiMedic2.lbMarti1.Content = ParseTimeForMinutesAndPutColon(item.Marti.DeLa1);
                            detaliiMedic2.lbMarti2.Content = ParseTimeForMinutesAndPutColon(item.Marti.PanaLa1);
                            detaliiMedic2.lbMarti3.Content = ParseTimeForMinutesAndPutColon(item.Marti.DeLa2);
                            detaliiMedic2.lbMarti4.Content = ParseTimeForMinutesAndPutColon(item.Marti.PanaLa2);
                            detaliiMedic2.lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.DeLa1);
                            detaliiMedic2.lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.PanaLa1);
                            detaliiMedic2.lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.DeLa2);
                            detaliiMedic2.lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.PanaLa2);
                            detaliiMedic2.lbJoi1.Content = ParseTimeForMinutesAndPutColon(item.Joi.DeLa1);
                            detaliiMedic2.lbJoi2.Content = ParseTimeForMinutesAndPutColon(item.Joi.PanaLa1);
                            detaliiMedic2.lbJoi3.Content = ParseTimeForMinutesAndPutColon(item.Joi.DeLa2);
                            detaliiMedic2.lbJoi4.Content = ParseTimeForMinutesAndPutColon(item.Joi.PanaLa2);
                            detaliiMedic2.lbVineri1.Content = ParseTimeForMinutesAndPutColon(item.Vineri.DeLa1);
                            detaliiMedic2.lbVineri2.Content = ParseTimeForMinutesAndPutColon(item.Vineri.PanaLa1);
                            detaliiMedic2.lbVineri3.Content = ParseTimeForMinutesAndPutColon(item.Vineri.DeLa2);
                            detaliiMedic2.lbVineri4.Content = ParseTimeForMinutesAndPutColon(item.Vineri.PanaLa2);
                            detaliiMedic2.lbSambata1.Content = ParseTimeForMinutesAndPutColon(item.Sambata.DeLa1);
                            detaliiMedic2.lbSambata2.Content = ParseTimeForMinutesAndPutColon(item.Sambata.PanaLa1);
                            detaliiMedic2.lbSambata3.Content = ParseTimeForMinutesAndPutColon(item.Sambata.DeLa2);
                            detaliiMedic2.lbSambata4.Content = ParseTimeForMinutesAndPutColon(item.Sambata.PanaLa2);
                            NumarDetaliiMedic++;
                            break;
                        case 2:
                            detaliiMedic3.Visibility = Visibility.Visible;
                            detaliiMedic3.IsEnabled = true;
                            detaliiMedic3.lbID.Content = item.ID;
                            detaliiMedic3.lbNume.Content = item.Nume;
                            detaliiMedic3.lbPrenume.Content = item.Prenume;
                            detaliiMedic3.lbTelefon.Content = item.Telefon;
                            detaliiMedic3.lbEmail.Content = item.Email;
                            detaliiMedic3.lbObservatii.Content = item.Observatii;
                            detaliiMedic3.lbLuni1.Content = ParseTimeForMinutesAndPutColon(item.Luni.DeLa1);
                            detaliiMedic3.lbLuni2.Content = ParseTimeForMinutesAndPutColon(item.Luni.PanaLa1);
                            detaliiMedic3.lbLuni3.Content = ParseTimeForMinutesAndPutColon(item.Luni.DeLa2);
                            detaliiMedic3.lbLuni4.Content = ParseTimeForMinutesAndPutColon(item.Luni.PanaLa2);
                            detaliiMedic3.lbMarti1.Content = ParseTimeForMinutesAndPutColon(item.Marti.DeLa1);
                            detaliiMedic3.lbMarti2.Content = ParseTimeForMinutesAndPutColon(item.Marti.PanaLa1);
                            detaliiMedic3.lbMarti3.Content = ParseTimeForMinutesAndPutColon(item.Marti.DeLa2);
                            detaliiMedic3.lbMarti4.Content = ParseTimeForMinutesAndPutColon(item.Marti.PanaLa2);
                            detaliiMedic3.lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.DeLa1);
                            detaliiMedic3.lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.PanaLa1);
                            detaliiMedic3.lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.DeLa2);
                            detaliiMedic3.lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.PanaLa2);
                            detaliiMedic3.lbJoi1.Content = ParseTimeForMinutesAndPutColon(item.Joi.DeLa1);
                            detaliiMedic3.lbJoi2.Content = ParseTimeForMinutesAndPutColon(item.Joi.PanaLa1);
                            detaliiMedic3.lbJoi3.Content = ParseTimeForMinutesAndPutColon(item.Joi.DeLa2);
                            detaliiMedic3.lbJoi4.Content = ParseTimeForMinutesAndPutColon(item.Joi.PanaLa2);
                            detaliiMedic3.lbVineri1.Content = ParseTimeForMinutesAndPutColon(item.Vineri.DeLa1);
                            detaliiMedic3.lbVineri2.Content = ParseTimeForMinutesAndPutColon(item.Vineri.PanaLa1);
                            detaliiMedic3.lbVineri3.Content = ParseTimeForMinutesAndPutColon(item.Vineri.DeLa2);
                            detaliiMedic3.lbVineri4.Content = ParseTimeForMinutesAndPutColon(item.Vineri.PanaLa2);
                            detaliiMedic3.lbSambata1.Content = ParseTimeForMinutesAndPutColon(item.Sambata.DeLa1);
                            detaliiMedic3.lbSambata2.Content = ParseTimeForMinutesAndPutColon(item.Sambata.PanaLa1);
                            detaliiMedic3.lbSambata3.Content = ParseTimeForMinutesAndPutColon(item.Sambata.DeLa2);
                            detaliiMedic3.lbSambata4.Content = ParseTimeForMinutesAndPutColon(item.Sambata.PanaLa2);
                            NumarDetaliiMedic++;
                            break;
                        case 3:
                            detaliiMedic4.Visibility = Visibility.Visible;
                            detaliiMedic4.IsEnabled = true;
                            detaliiMedic4.lbID.Content = item.ID;
                            detaliiMedic4.lbNume.Content = item.Nume;
                            detaliiMedic4.lbPrenume.Content = item.Prenume;
                            detaliiMedic4.lbTelefon.Content = item.Telefon;
                            detaliiMedic4.lbEmail.Content = item.Email;
                            detaliiMedic4.lbObservatii.Content = item.Observatii;
                            detaliiMedic4.lbLuni1.Content = ParseTimeForMinutesAndPutColon(item.Luni.DeLa1);
                            detaliiMedic4.lbLuni2.Content = ParseTimeForMinutesAndPutColon(item.Luni.PanaLa1);
                            detaliiMedic4.lbLuni3.Content = ParseTimeForMinutesAndPutColon(item.Luni.DeLa2);
                            detaliiMedic4.lbLuni4.Content = ParseTimeForMinutesAndPutColon(item.Luni.PanaLa2);
                            detaliiMedic4.lbMarti1.Content = ParseTimeForMinutesAndPutColon(item.Marti.DeLa1);
                            detaliiMedic4.lbMarti2.Content = ParseTimeForMinutesAndPutColon(item.Marti.PanaLa1);
                            detaliiMedic4.lbMarti3.Content = ParseTimeForMinutesAndPutColon(item.Marti.DeLa2);
                            detaliiMedic4.lbMarti4.Content = ParseTimeForMinutesAndPutColon(item.Marti.PanaLa2);
                            detaliiMedic4.lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.DeLa1);
                            detaliiMedic4.lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.PanaLa1);
                            detaliiMedic4.lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.DeLa2);
                            detaliiMedic4.lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.PanaLa2);
                            detaliiMedic4.lbJoi1.Content = ParseTimeForMinutesAndPutColon(item.Joi.DeLa1);
                            detaliiMedic4.lbJoi2.Content = ParseTimeForMinutesAndPutColon(item.Joi.PanaLa1);
                            detaliiMedic4.lbJoi3.Content = ParseTimeForMinutesAndPutColon(item.Joi.DeLa2);
                            detaliiMedic4.lbJoi4.Content = ParseTimeForMinutesAndPutColon(item.Joi.PanaLa2);
                            detaliiMedic4.lbVineri1.Content = ParseTimeForMinutesAndPutColon(item.Vineri.DeLa1);
                            detaliiMedic4.lbVineri2.Content = ParseTimeForMinutesAndPutColon(item.Vineri.PanaLa1);
                            detaliiMedic4.lbVineri3.Content = ParseTimeForMinutesAndPutColon(item.Vineri.DeLa2);
                            detaliiMedic4.lbVineri4.Content = ParseTimeForMinutesAndPutColon(item.Vineri.PanaLa2);
                            detaliiMedic4.lbSambata1.Content = ParseTimeForMinutesAndPutColon(item.Sambata.DeLa1);
                            detaliiMedic4.lbSambata2.Content = ParseTimeForMinutesAndPutColon(item.Sambata.PanaLa1);
                            detaliiMedic4.lbSambata3.Content = ParseTimeForMinutesAndPutColon(item.Sambata.DeLa2);
                            detaliiMedic4.lbSambata4.Content = ParseTimeForMinutesAndPutColon(item.Sambata.PanaLa2);
                            NumarDetaliiMedic++;
                            break;
                        case 4:
                            detaliiMedic5.Visibility = Visibility.Visible;
                            detaliiMedic5.IsEnabled = true;
                            detaliiMedic5.lbID.Content = item.ID;
                            detaliiMedic5.lbNume.Content = item.Nume;
                            detaliiMedic5.lbPrenume.Content = item.Prenume;
                            detaliiMedic5.lbTelefon.Content = item.Telefon;
                            detaliiMedic5.lbEmail.Content = item.Email;
                            detaliiMedic5.lbObservatii.Content = item.Observatii;
                            detaliiMedic5.lbLuni1.Content = ParseTimeForMinutesAndPutColon(item.Luni.DeLa1);
                            detaliiMedic5.lbLuni2.Content = ParseTimeForMinutesAndPutColon(item.Luni.PanaLa1);
                            detaliiMedic5.lbLuni3.Content = ParseTimeForMinutesAndPutColon(item.Luni.DeLa2);
                            detaliiMedic5.lbLuni4.Content = ParseTimeForMinutesAndPutColon(item.Luni.PanaLa2);
                            detaliiMedic5.lbMarti1.Content = ParseTimeForMinutesAndPutColon(item.Marti.DeLa1);
                            detaliiMedic5.lbMarti2.Content = ParseTimeForMinutesAndPutColon(item.Marti.PanaLa1);
                            detaliiMedic5.lbMarti3.Content = ParseTimeForMinutesAndPutColon(item.Marti.DeLa2);
                            detaliiMedic5.lbMarti4.Content = ParseTimeForMinutesAndPutColon(item.Marti.PanaLa2);
                            detaliiMedic5.lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.DeLa1);
                            detaliiMedic5.lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.PanaLa1);
                            detaliiMedic5.lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.DeLa2);
                            detaliiMedic5.lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.PanaLa2);
                            detaliiMedic5.lbJoi1.Content = ParseTimeForMinutesAndPutColon(item.Joi.DeLa1);
                            detaliiMedic5.lbJoi2.Content = ParseTimeForMinutesAndPutColon(item.Joi.PanaLa1);
                            detaliiMedic5.lbJoi3.Content = ParseTimeForMinutesAndPutColon(item.Joi.DeLa2);
                            detaliiMedic5.lbJoi4.Content = ParseTimeForMinutesAndPutColon(item.Joi.PanaLa2);
                            detaliiMedic5.lbVineri1.Content = ParseTimeForMinutesAndPutColon(item.Vineri.DeLa1);
                            detaliiMedic5.lbVineri2.Content = ParseTimeForMinutesAndPutColon(item.Vineri.PanaLa1);
                            detaliiMedic5.lbVineri3.Content = ParseTimeForMinutesAndPutColon(item.Vineri.DeLa2);
                            detaliiMedic5.lbVineri4.Content = ParseTimeForMinutesAndPutColon(item.Vineri.PanaLa2);
                            detaliiMedic5.lbSambata1.Content = ParseTimeForMinutesAndPutColon(item.Sambata.DeLa1);
                            detaliiMedic5.lbSambata2.Content = ParseTimeForMinutesAndPutColon(item.Sambata.PanaLa1);
                            detaliiMedic5.lbSambata3.Content = ParseTimeForMinutesAndPutColon(item.Sambata.DeLa2);
                            detaliiMedic5.lbSambata4.Content = ParseTimeForMinutesAndPutColon(item.Sambata.PanaLa2);
                            NumarDetaliiMedic++;
                            break;
                        case 5:
                            btnAdaugaMedic.IsEnabled = false;

                            detaliiMedic6.Visibility = Visibility.Visible;
                            detaliiMedic6.IsEnabled = true;
                            detaliiMedic6.lbID.Content = item.ID;
                            detaliiMedic6.lbNume.Content = item.Nume;
                            detaliiMedic6.lbPrenume.Content = item.Prenume;
                            detaliiMedic6.lbTelefon.Content = item.Telefon;
                            detaliiMedic6.lbEmail.Content = item.Email;
                            detaliiMedic6.lbObservatii.Content = item.Observatii;
                            detaliiMedic6.lbLuni1.Content = ParseTimeForMinutesAndPutColon(item.Luni.DeLa1);
                            detaliiMedic6.lbLuni2.Content = ParseTimeForMinutesAndPutColon(item.Luni.PanaLa1);
                            detaliiMedic6.lbLuni3.Content = ParseTimeForMinutesAndPutColon(item.Luni.DeLa2);
                            detaliiMedic6.lbLuni4.Content = ParseTimeForMinutesAndPutColon(item.Luni.PanaLa2);
                            detaliiMedic6.lbMarti1.Content = ParseTimeForMinutesAndPutColon(item.Marti.DeLa1);
                            detaliiMedic6.lbMarti2.Content = ParseTimeForMinutesAndPutColon(item.Marti.PanaLa1);
                            detaliiMedic6.lbMarti3.Content = ParseTimeForMinutesAndPutColon(item.Marti.DeLa2);
                            detaliiMedic6.lbMarti4.Content = ParseTimeForMinutesAndPutColon(item.Marti.PanaLa2);
                            detaliiMedic6.lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.DeLa1);
                            detaliiMedic6.lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.PanaLa1);
                            detaliiMedic6.lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.DeLa2);
                            detaliiMedic6.lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(item.Miercuri.PanaLa2);
                            detaliiMedic6.lbJoi1.Content = ParseTimeForMinutesAndPutColon(item.Joi.DeLa1);
                            detaliiMedic6.lbJoi2.Content = ParseTimeForMinutesAndPutColon(item.Joi.PanaLa1);
                            detaliiMedic6.lbJoi3.Content = ParseTimeForMinutesAndPutColon(item.Joi.DeLa2);
                            detaliiMedic6.lbJoi4.Content = ParseTimeForMinutesAndPutColon(item.Joi.PanaLa2);
                            detaliiMedic6.lbVineri1.Content = ParseTimeForMinutesAndPutColon(item.Vineri.DeLa1);
                            detaliiMedic6.lbVineri2.Content = ParseTimeForMinutesAndPutColon(item.Vineri.PanaLa1);
                            detaliiMedic6.lbVineri3.Content = ParseTimeForMinutesAndPutColon(item.Vineri.DeLa2);
                            detaliiMedic6.lbVineri4.Content = ParseTimeForMinutesAndPutColon(item.Vineri.PanaLa2);
                            detaliiMedic6.lbSambata1.Content = ParseTimeForMinutesAndPutColon(item.Sambata.DeLa1);
                            detaliiMedic6.lbSambata2.Content = ParseTimeForMinutesAndPutColon(item.Sambata.PanaLa1);
                            detaliiMedic6.lbSambata3.Content = ParseTimeForMinutesAndPutColon(item.Sambata.DeLa2);
                            detaliiMedic6.lbSambata4.Content = ParseTimeForMinutesAndPutColon(item.Sambata.PanaLa2);
                            NumarDetaliiMedic++;
                            break;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                //MessageBox.Show("Fișierul medici.xml lipsește!", "Fișier inexistent");
            }
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

        private void PopulateInitDataGrid()
        {
            try
            {
                XDocument documentXmlPacienti = XDocument.Load("pacienti.xml");
                var pacientiInitDataGrid = documentXmlPacienti.Descendants("pacient");
                tbNrPacienti.Text = pacientiInitDataGrid.Count().ToString();
                var pacienti = from p in pacientiInitDataGrid.Skip(pacientiInitDataGrid.Count() - Constants.NrPacientiDataGrid)
                               select new Pacient()
                               {
                                   NumarFisa = p.Descendants("numarfisa").First().Value,
                                   Medic = p.Descendants("medic").First().Value,
                                   Nume = p.Descendants("nume").First().Value,
                                   Prenume = p.Descendants("prenume").First().Value,
                                   Cnp = p.Descendants("cnp").First().Value,
                                   SerieCi = p.Descendants("serieci").First().Value,
                                   NumarCi = p.Descendants("numarci").First().Value,
                                   Varsta = p.Descendants("varsta").First().Value,
                                   Sex = p.Descendants("sex").First().Value,
                                   Telefon = p.Descendants("telefon").First().Value,
                                   Email = p.Descendants("email").First().Value,
                                   Observatii = p.Descendants("observatii").First().Value
                               };
                listaPacienti.Clear();
                foreach (var item in pacienti)
                {
                    listaPacienti.Insert(0, item);
                }
            }
            catch (FileNotFoundException)
            {
                //MessageBox.Show("Fișierul pacienti.xml lipsește!", "Fișier inexistent");
                tbNrPacienti.Text = "0";
            }
        }

        private void ShowDespreWindow(object sender, RoutedEventArgs e)
        {
            DespreWindow despreWindow = new DespreWindow();
            despreWindow.ShowDialog();
        }

        private void btnAdaugaPacient_Click(object sender, RoutedEventArgs e)
        {
            AdaugaPacientWindow adaugaPacientWindow = new AdaugaPacientWindow();

            adaugaPacientWindow.AdaugaPacientCallback =
            new AdaugaPacientWindow.AdaugaPacientDelegate(
                this.AdaugaPacientFunction);

            adaugaPacientWindow.ShowDialog();
        }

        private void AdaugaPacientFunction(Pacient value)
        {
            listaPacienti.Insert(0, value);

            AdaugaPacientInXML(value);
        }

        private void AdaugaPacientInXML(Pacient value)
        {
            XElement xelem = new XElement("pacient");
            xelem.Add(new XElement("numarfisa", value.NumarFisa));
            xelem.Add(new XElement("medic", value.Medic));
            xelem.Add(new XElement("nume", value.Nume));
            xelem.Add(new XElement("prenume", value.Prenume));
            xelem.Add(new XElement("cnp", value.Cnp));
            xelem.Add(new XElement("serieci", value.SerieCi));
            xelem.Add(new XElement("numarci", value.NumarCi));
            xelem.Add(new XElement("varsta", value.Varsta));
            xelem.Add(new XElement("sex", value.Sex));
            xelem.Add(new XElement("telefon", value.Telefon));
            xelem.Add(new XElement("email", value.Email));
            xelem.Add(new XElement("observatii", value.Observatii));

            try
            {
                XDocument documentXmlPacienti = XDocument.Load("pacienti.xml");
                documentXmlPacienti.Element("pacienti").Add(xelem);
                documentXmlPacienti.Save("pacienti.xml");

                tbNrPacienti.Text = documentXmlPacienti.Descendants("pacient").Count().ToString();
            }
            catch (FileNotFoundException)
            {

                XDocument documentXmlPacienti = new XDocument(new XElement("pacienti"));
                documentXmlPacienti.Element("pacienti").Add(xelem);
                documentXmlPacienti.Save("pacienti.xml");

                tbNrPacienti.Text = documentXmlPacienti.Descendants("pacient").Count().ToString();
            }
        }

        private void btnCautaPacient_Click(object sender, RoutedEventArgs e)
        {
            CautaPacientWindow cautaPacientWindow = new CautaPacientWindow();
            cautaPacientWindow.SendPacientModificatToMainCallback +=
                new Action<Pacient, Pacient>(this.ModificaPacientInMainDataGrid);
            cautaPacientWindow.SendPacientStersToMainCallback +=
                new Action<Pacient>(this.StergePacientInMainDataGrid);
            cautaPacientWindow.ShowDialog();
        }

        private void StergePacientInMainDataGrid(Pacient obj)
        {
            listaPacienti.Remove(obj);
            PopulateInitDataGrid();
        }

        private void ModificaPacientInMainDataGrid(Pacient pacientOld, Pacient pacientNew)
        {
            for (int i = 0; i < listaPacienti.Count; i++)
            {
                if (listaPacienti[i] == pacientOld)
                {
                    listaPacienti[i] = pacientNew;
                    break;
                }
            }
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSterge_Click(object sender, RoutedEventArgs e)
        {
            bool? stergePacient;

            Pacient pacientDeSters = (Pacient)dataGridPacienti.SelectedItem;
            string stergePacientText = string.Format("Sigur vreți să ștergeți pacientul {0} {1}?",
                                               pacientDeSters.Nume, pacientDeSters.Prenume);
            stergePacient = MessageBoxCustom.Show(stergePacientText, "Ștergere pacient",
                                                  MessageBoxButton.YesNo);

            if ((bool)stergePacient)
            {
                // sterge pacient din .xml
                XDocument documentXmlPacienti = XDocument.Load("pacienti.xml");
                documentXmlPacienti.Root.Elements("pacient")
                  .Where(x => x.Element("numarfisa").Value == pacientDeSters.NumarFisa)
                  .Where(x => x.Element("nume").Value == pacientDeSters.Nume)
                  .Where(x => x.Element("prenume").Value == pacientDeSters.Prenume)
                  .Where(x => x.Element("cnp").Value == pacientDeSters.Cnp).Remove();

                documentXmlPacienti.Save("pacienti.xml");

                // sterge pacient din lista de cautare
                listaPacienti.Remove(pacientDeSters);

                if (btnModifica.IsEnabled == true)
                {
                    btnModifica.IsEnabled = false;
                    btnSterge.IsEnabled = false;
                    btnIstoricPacient.IsEnabled = false; 
                }

                PopulateInitDataGrid();
            }
        }

        private void btnAdaugaMedic_Click(object sender, RoutedEventArgs e)
        {
            if ((detaliiMedic1.IsEnabled) && (detaliiMedic2.IsEnabled) && (detaliiMedic3.IsEnabled) &&
                (detaliiMedic4.IsEnabled) && (detaliiMedic5.IsEnabled) && (detaliiMedic6.IsEnabled))
            {
                MessageBoxCustom.Show("Numărul maxim de medici a fost atins!", "Poziție indisponibilă");
            }
            else
            {
                AdaugaMedicComboWindow adaugaMedicWindow = new AdaugaMedicComboWindow();
                adaugaMedicWindow.SendMedicToMainCallback += new Action<Medic>(this.AdaugaMedicFunc);
                adaugaMedicWindow.ShowDialog(); 
            }
        }

        private void AdaugaMedicFunc(Medic value)
        {
            AdaugaMedicInXML(value);
            AdaugaMedicLaDetaliiMedic(value);
        }

        private void AdaugaMedicLaDetaliiMedic(Medic value)
        {
            if (detaliiMedic1.IsEnabled == false)
            {
                detaliiMedic1.Visibility = Visibility.Visible;
                detaliiMedic1.IsEnabled = true;
                detaliiMedic1.lbID.Content = value.ID;
                detaliiMedic1.lbNume.Content = value.Nume;
                detaliiMedic1.lbPrenume.Content = value.Prenume;
                detaliiMedic1.lbTelefon.Content = value.Telefon;
                detaliiMedic1.lbEmail.Content = value.Email;
                detaliiMedic1.lbObservatii.Content = value.Observatii;
                detaliiMedic1.lbLuni1.Content = ParseTimeForMinutesAndPutColon(value.Luni.DeLa1);
                detaliiMedic1.lbLuni2.Content = ParseTimeForMinutesAndPutColon(value.Luni.PanaLa1);
                detaliiMedic1.lbLuni3.Content = ParseTimeForMinutesAndPutColon(value.Luni.DeLa2);
                detaliiMedic1.lbLuni4.Content = ParseTimeForMinutesAndPutColon(value.Luni.PanaLa2);
                detaliiMedic1.lbMarti1.Content = ParseTimeForMinutesAndPutColon(value.Marti.DeLa1);
                detaliiMedic1.lbMarti2.Content = ParseTimeForMinutesAndPutColon(value.Marti.PanaLa1);
                detaliiMedic1.lbMarti3.Content = ParseTimeForMinutesAndPutColon(value.Marti.DeLa2);
                detaliiMedic1.lbMarti4.Content = ParseTimeForMinutesAndPutColon(value.Marti.PanaLa2);
                detaliiMedic1.lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.DeLa1);
                detaliiMedic1.lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.PanaLa1);
                detaliiMedic1.lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.DeLa2);
                detaliiMedic1.lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.PanaLa2);
                detaliiMedic1.lbJoi1.Content = ParseTimeForMinutesAndPutColon(value.Joi.DeLa1);
                detaliiMedic1.lbJoi2.Content = ParseTimeForMinutesAndPutColon(value.Joi.PanaLa1);
                detaliiMedic1.lbJoi3.Content = ParseTimeForMinutesAndPutColon(value.Joi.DeLa2);
                detaliiMedic1.lbJoi4.Content = ParseTimeForMinutesAndPutColon(value.Joi.PanaLa2);
                detaliiMedic1.lbVineri1.Content = ParseTimeForMinutesAndPutColon(value.Vineri.DeLa1);
                detaliiMedic1.lbVineri2.Content = ParseTimeForMinutesAndPutColon(value.Vineri.PanaLa1);
                detaliiMedic1.lbVineri3.Content = ParseTimeForMinutesAndPutColon(value.Vineri.DeLa2);
                detaliiMedic1.lbVineri4.Content = ParseTimeForMinutesAndPutColon(value.Vineri.PanaLa2);
                detaliiMedic1.lbSambata1.Content = ParseTimeForMinutesAndPutColon(value.Sambata.DeLa1);
                detaliiMedic1.lbSambata2.Content = ParseTimeForMinutesAndPutColon(value.Sambata.PanaLa1);
                detaliiMedic1.lbSambata3.Content = ParseTimeForMinutesAndPutColon(value.Sambata.DeLa2);
                detaliiMedic1.lbSambata4.Content = ParseTimeForMinutesAndPutColon(value.Sambata.PanaLa2);
            }
            else if (detaliiMedic2.IsEnabled == false)
            {
                detaliiMedic2.Visibility = Visibility.Visible;
                detaliiMedic2.IsEnabled = true;
                detaliiMedic2.lbID.Content = value.ID;
                detaliiMedic2.lbNume.Content = value.Nume;
                detaliiMedic2.lbPrenume.Content = value.Prenume;
                detaliiMedic2.lbTelefon.Content = value.Telefon;
                detaliiMedic2.lbEmail.Content = value.Email;
                detaliiMedic2.lbObservatii.Content = value.Observatii;
                detaliiMedic2.lbLuni1.Content = ParseTimeForMinutesAndPutColon(value.Luni.DeLa1);
                detaliiMedic2.lbLuni2.Content = ParseTimeForMinutesAndPutColon(value.Luni.PanaLa1);
                detaliiMedic2.lbLuni3.Content = ParseTimeForMinutesAndPutColon(value.Luni.DeLa2);
                detaliiMedic2.lbLuni4.Content = ParseTimeForMinutesAndPutColon(value.Luni.PanaLa2);
                detaliiMedic2.lbMarti1.Content = ParseTimeForMinutesAndPutColon(value.Marti.DeLa1);
                detaliiMedic2.lbMarti2.Content = ParseTimeForMinutesAndPutColon(value.Marti.PanaLa1);
                detaliiMedic2.lbMarti3.Content = ParseTimeForMinutesAndPutColon(value.Marti.DeLa2);
                detaliiMedic2.lbMarti4.Content = ParseTimeForMinutesAndPutColon(value.Marti.PanaLa2);
                detaliiMedic2.lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.DeLa1);
                detaliiMedic2.lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.PanaLa1);
                detaliiMedic2.lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.DeLa2);
                detaliiMedic2.lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.PanaLa2);
                detaliiMedic2.lbJoi1.Content = ParseTimeForMinutesAndPutColon(value.Joi.DeLa1);
                detaliiMedic2.lbJoi2.Content = ParseTimeForMinutesAndPutColon(value.Joi.PanaLa1);
                detaliiMedic2.lbJoi3.Content = ParseTimeForMinutesAndPutColon(value.Joi.DeLa2);
                detaliiMedic2.lbJoi4.Content = ParseTimeForMinutesAndPutColon(value.Joi.PanaLa2);
                detaliiMedic2.lbVineri1.Content = ParseTimeForMinutesAndPutColon(value.Vineri.DeLa1);
                detaliiMedic2.lbVineri2.Content = ParseTimeForMinutesAndPutColon(value.Vineri.PanaLa1);
                detaliiMedic2.lbVineri3.Content = ParseTimeForMinutesAndPutColon(value.Vineri.DeLa2);
                detaliiMedic2.lbVineri4.Content = ParseTimeForMinutesAndPutColon(value.Vineri.PanaLa2);
                detaliiMedic2.lbSambata1.Content = ParseTimeForMinutesAndPutColon(value.Sambata.DeLa1);
                detaliiMedic2.lbSambata2.Content = ParseTimeForMinutesAndPutColon(value.Sambata.PanaLa1);
                detaliiMedic2.lbSambata3.Content = ParseTimeForMinutesAndPutColon(value.Sambata.DeLa2);
                detaliiMedic2.lbSambata4.Content = ParseTimeForMinutesAndPutColon(value.Sambata.PanaLa2);
            }
            else if (detaliiMedic3.IsEnabled == false)
            {
                detaliiMedic3.Visibility = Visibility.Visible;
                detaliiMedic3.IsEnabled = true;
                detaliiMedic3.lbID.Content = value.ID;
                detaliiMedic3.lbNume.Content = value.Nume;
                detaliiMedic3.lbPrenume.Content = value.Prenume;
                detaliiMedic3.lbTelefon.Content = value.Telefon;
                detaliiMedic3.lbEmail.Content = value.Email;
                detaliiMedic3.lbObservatii.Content = value.Observatii;
                detaliiMedic3.lbLuni1.Content = ParseTimeForMinutesAndPutColon(value.Luni.DeLa1);
                detaliiMedic3.lbLuni2.Content = ParseTimeForMinutesAndPutColon(value.Luni.PanaLa1);
                detaliiMedic3.lbLuni3.Content = ParseTimeForMinutesAndPutColon(value.Luni.DeLa2);
                detaliiMedic3.lbLuni4.Content = ParseTimeForMinutesAndPutColon(value.Luni.PanaLa2);
                detaliiMedic3.lbMarti1.Content = ParseTimeForMinutesAndPutColon(value.Marti.DeLa1);
                detaliiMedic3.lbMarti2.Content = ParseTimeForMinutesAndPutColon(value.Marti.PanaLa1);
                detaliiMedic3.lbMarti3.Content = ParseTimeForMinutesAndPutColon(value.Marti.DeLa2);
                detaliiMedic3.lbMarti4.Content = ParseTimeForMinutesAndPutColon(value.Marti.PanaLa2);
                detaliiMedic3.lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.DeLa1);
                detaliiMedic3.lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.PanaLa1);
                detaliiMedic3.lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.DeLa2);
                detaliiMedic3.lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.PanaLa2);
                detaliiMedic3.lbJoi1.Content = ParseTimeForMinutesAndPutColon(value.Joi.DeLa1);
                detaliiMedic3.lbJoi2.Content = ParseTimeForMinutesAndPutColon(value.Joi.PanaLa1);
                detaliiMedic3.lbJoi3.Content = ParseTimeForMinutesAndPutColon(value.Joi.DeLa2);
                detaliiMedic3.lbJoi4.Content = ParseTimeForMinutesAndPutColon(value.Joi.PanaLa2);
                detaliiMedic3.lbVineri1.Content = ParseTimeForMinutesAndPutColon(value.Vineri.DeLa1);
                detaliiMedic3.lbVineri2.Content = ParseTimeForMinutesAndPutColon(value.Vineri.PanaLa1);
                detaliiMedic3.lbVineri3.Content = ParseTimeForMinutesAndPutColon(value.Vineri.DeLa2);
                detaliiMedic3.lbVineri4.Content = ParseTimeForMinutesAndPutColon(value.Vineri.PanaLa2);
                detaliiMedic3.lbSambata1.Content = ParseTimeForMinutesAndPutColon(value.Sambata.DeLa1);
                detaliiMedic3.lbSambata2.Content = ParseTimeForMinutesAndPutColon(value.Sambata.PanaLa1);
                detaliiMedic3.lbSambata3.Content = ParseTimeForMinutesAndPutColon(value.Sambata.DeLa2);
                detaliiMedic3.lbSambata4.Content = ParseTimeForMinutesAndPutColon(value.Sambata.PanaLa2);
            }
            else if (detaliiMedic4.IsEnabled == false)
            {
                detaliiMedic4.Visibility = Visibility.Visible;
                detaliiMedic4.IsEnabled = true;
                detaliiMedic4.lbID.Content = value.ID;
                detaliiMedic4.lbNume.Content = value.Nume;
                detaliiMedic4.lbPrenume.Content = value.Prenume;
                detaliiMedic4.lbTelefon.Content = value.Telefon;
                detaliiMedic4.lbEmail.Content = value.Email;
                detaliiMedic4.lbObservatii.Content = value.Observatii;
                detaliiMedic4.lbLuni1.Content = ParseTimeForMinutesAndPutColon(value.Luni.DeLa1);
                detaliiMedic4.lbLuni2.Content = ParseTimeForMinutesAndPutColon(value.Luni.PanaLa1);
                detaliiMedic4.lbLuni3.Content = ParseTimeForMinutesAndPutColon(value.Luni.DeLa2);
                detaliiMedic4.lbLuni4.Content = ParseTimeForMinutesAndPutColon(value.Luni.PanaLa2);
                detaliiMedic4.lbMarti1.Content = ParseTimeForMinutesAndPutColon(value.Marti.DeLa1);
                detaliiMedic4.lbMarti2.Content = ParseTimeForMinutesAndPutColon(value.Marti.PanaLa1);
                detaliiMedic4.lbMarti3.Content = ParseTimeForMinutesAndPutColon(value.Marti.DeLa2);
                detaliiMedic4.lbMarti4.Content = ParseTimeForMinutesAndPutColon(value.Marti.PanaLa2);
                detaliiMedic4.lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.DeLa1);
                detaliiMedic4.lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.PanaLa1);
                detaliiMedic4.lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.DeLa2);
                detaliiMedic4.lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.PanaLa2);
                detaliiMedic4.lbJoi1.Content = ParseTimeForMinutesAndPutColon(value.Joi.DeLa1);
                detaliiMedic4.lbJoi2.Content = ParseTimeForMinutesAndPutColon(value.Joi.PanaLa1);
                detaliiMedic4.lbJoi3.Content = ParseTimeForMinutesAndPutColon(value.Joi.DeLa2);
                detaliiMedic4.lbJoi4.Content = ParseTimeForMinutesAndPutColon(value.Joi.PanaLa2);
                detaliiMedic4.lbVineri1.Content = ParseTimeForMinutesAndPutColon(value.Vineri.DeLa1);
                detaliiMedic4.lbVineri2.Content = ParseTimeForMinutesAndPutColon(value.Vineri.PanaLa1);
                detaliiMedic4.lbVineri3.Content = ParseTimeForMinutesAndPutColon(value.Vineri.DeLa2);
                detaliiMedic4.lbVineri4.Content = ParseTimeForMinutesAndPutColon(value.Vineri.PanaLa2);
                detaliiMedic4.lbSambata1.Content = ParseTimeForMinutesAndPutColon(value.Sambata.DeLa1);
                detaliiMedic4.lbSambata2.Content = ParseTimeForMinutesAndPutColon(value.Sambata.PanaLa1);
                detaliiMedic4.lbSambata3.Content = ParseTimeForMinutesAndPutColon(value.Sambata.DeLa2);
                detaliiMedic4.lbSambata4.Content = ParseTimeForMinutesAndPutColon(value.Sambata.PanaLa2);
            }
            else if (detaliiMedic5.IsEnabled == false)
            {
                detaliiMedic5.Visibility = Visibility.Visible;
                detaliiMedic5.IsEnabled = true;
                detaliiMedic5.lbID.Content = value.ID;
                detaliiMedic5.lbNume.Content = value.Nume;
                detaliiMedic5.lbPrenume.Content = value.Prenume;
                detaliiMedic5.lbTelefon.Content = value.Telefon;
                detaliiMedic5.lbEmail.Content = value.Email;
                detaliiMedic5.lbObservatii.Content = value.Observatii;
                detaliiMedic5.lbLuni1.Content = ParseTimeForMinutesAndPutColon(value.Luni.DeLa1);
                detaliiMedic5.lbLuni2.Content = ParseTimeForMinutesAndPutColon(value.Luni.PanaLa1);
                detaliiMedic5.lbLuni3.Content = ParseTimeForMinutesAndPutColon(value.Luni.DeLa2);
                detaliiMedic5.lbLuni4.Content = ParseTimeForMinutesAndPutColon(value.Luni.PanaLa2);
                detaliiMedic5.lbMarti1.Content = ParseTimeForMinutesAndPutColon(value.Marti.DeLa1);
                detaliiMedic5.lbMarti2.Content = ParseTimeForMinutesAndPutColon(value.Marti.PanaLa1);
                detaliiMedic5.lbMarti3.Content = ParseTimeForMinutesAndPutColon(value.Marti.DeLa2);
                detaliiMedic5.lbMarti4.Content = ParseTimeForMinutesAndPutColon(value.Marti.PanaLa2);
                detaliiMedic5.lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.DeLa1);
                detaliiMedic5.lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.PanaLa1);
                detaliiMedic5.lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.DeLa2);
                detaliiMedic5.lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.PanaLa2);
                detaliiMedic5.lbJoi1.Content = ParseTimeForMinutesAndPutColon(value.Joi.DeLa1);
                detaliiMedic5.lbJoi2.Content = ParseTimeForMinutesAndPutColon(value.Joi.PanaLa1);
                detaliiMedic5.lbJoi3.Content = ParseTimeForMinutesAndPutColon(value.Joi.DeLa2);
                detaliiMedic5.lbJoi4.Content = ParseTimeForMinutesAndPutColon(value.Joi.PanaLa2);
                detaliiMedic5.lbVineri1.Content = ParseTimeForMinutesAndPutColon(value.Vineri.DeLa1);
                detaliiMedic5.lbVineri2.Content = ParseTimeForMinutesAndPutColon(value.Vineri.PanaLa1);
                detaliiMedic5.lbVineri3.Content = ParseTimeForMinutesAndPutColon(value.Vineri.DeLa2);
                detaliiMedic5.lbVineri4.Content = ParseTimeForMinutesAndPutColon(value.Vineri.PanaLa2);
                detaliiMedic5.lbSambata1.Content = ParseTimeForMinutesAndPutColon(value.Sambata.DeLa1);
                detaliiMedic5.lbSambata2.Content = ParseTimeForMinutesAndPutColon(value.Sambata.PanaLa1);
                detaliiMedic5.lbSambata3.Content = ParseTimeForMinutesAndPutColon(value.Sambata.DeLa2);
                detaliiMedic5.lbSambata4.Content = ParseTimeForMinutesAndPutColon(value.Sambata.PanaLa2);
            }
            else if (detaliiMedic6.IsEnabled == false)
            {
                btnAdaugaMedic.IsEnabled = false;

                detaliiMedic6.Visibility = Visibility.Visible;
                detaliiMedic6.IsEnabled = true;
                detaliiMedic6.lbID.Content = value.ID;
                detaliiMedic6.lbNume.Content = value.Nume;
                detaliiMedic6.lbPrenume.Content = value.Prenume;
                detaliiMedic6.lbTelefon.Content = value.Telefon;
                detaliiMedic6.lbEmail.Content = value.Email;
                detaliiMedic6.lbObservatii.Content = value.Observatii;
                detaliiMedic6.lbLuni1.Content = ParseTimeForMinutesAndPutColon(value.Luni.DeLa1);
                detaliiMedic6.lbLuni2.Content = ParseTimeForMinutesAndPutColon(value.Luni.PanaLa1);
                detaliiMedic6.lbLuni3.Content = ParseTimeForMinutesAndPutColon(value.Luni.DeLa2);
                detaliiMedic6.lbLuni4.Content = ParseTimeForMinutesAndPutColon(value.Luni.PanaLa2);
                detaliiMedic6.lbMarti1.Content = ParseTimeForMinutesAndPutColon(value.Marti.DeLa1);
                detaliiMedic6.lbMarti2.Content = ParseTimeForMinutesAndPutColon(value.Marti.PanaLa1);
                detaliiMedic6.lbMarti3.Content = ParseTimeForMinutesAndPutColon(value.Marti.DeLa2);
                detaliiMedic6.lbMarti4.Content = ParseTimeForMinutesAndPutColon(value.Marti.PanaLa2);
                detaliiMedic6.lbMiercuri1.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.DeLa1);
                detaliiMedic6.lbMiercuri2.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.PanaLa1);
                detaliiMedic6.lbMiercuri3.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.DeLa2);
                detaliiMedic6.lbMiercuri4.Content = ParseTimeForMinutesAndPutColon(value.Miercuri.PanaLa2);
                detaliiMedic6.lbJoi1.Content = ParseTimeForMinutesAndPutColon(value.Joi.DeLa1);
                detaliiMedic6.lbJoi2.Content = ParseTimeForMinutesAndPutColon(value.Joi.PanaLa1);
                detaliiMedic6.lbJoi3.Content = ParseTimeForMinutesAndPutColon(value.Joi.DeLa2);
                detaliiMedic6.lbJoi4.Content = ParseTimeForMinutesAndPutColon(value.Joi.PanaLa2);
                detaliiMedic6.lbVineri1.Content = ParseTimeForMinutesAndPutColon(value.Vineri.DeLa1);
                detaliiMedic6.lbVineri2.Content = ParseTimeForMinutesAndPutColon(value.Vineri.PanaLa1);
                detaliiMedic6.lbVineri3.Content = ParseTimeForMinutesAndPutColon(value.Vineri.DeLa2);
                detaliiMedic6.lbVineri4.Content = ParseTimeForMinutesAndPutColon(value.Vineri.PanaLa2);
                detaliiMedic6.lbSambata1.Content = ParseTimeForMinutesAndPutColon(value.Sambata.DeLa1);
                detaliiMedic6.lbSambata2.Content = ParseTimeForMinutesAndPutColon(value.Sambata.PanaLa1);
                detaliiMedic6.lbSambata3.Content = ParseTimeForMinutesAndPutColon(value.Sambata.DeLa2);
                detaliiMedic6.lbSambata4.Content = ParseTimeForMinutesAndPutColon(value.Sambata.PanaLa2);
            }
        }


        private void AdaugaMedicInXML(Medic value)
        {
            XElement xelem = new XElement("medic");
            xelem.Add(new XElement("id", value.ID));
            xelem.Add(new XElement("nume", value.Nume));
            xelem.Add(new XElement("prenume", value.Prenume));
            xelem.Add(new XElement("telefon", value.Telefon));
            xelem.Add(new XElement("email", value.Email));
            xelem.Add(new XElement("observatii", value.Observatii));

            xelem.Add(new XElement("lunidela1", value.Luni.DeLa1));
            xelem.Add(new XElement("lunipanala1", value.Luni.PanaLa1));
            xelem.Add(new XElement("lunidela2", value.Luni.DeLa2));
            xelem.Add(new XElement("lunipanala2", value.Luni.PanaLa2));

            xelem.Add(new XElement("martidela1", value.Marti.DeLa1));
            xelem.Add(new XElement("martipanala1", value.Marti.PanaLa1));
            xelem.Add(new XElement("martidela2", value.Marti.DeLa2));
            xelem.Add(new XElement("martipanala2", value.Marti.PanaLa2));

            xelem.Add(new XElement("miercuridela1", value.Miercuri.DeLa1));
            xelem.Add(new XElement("miercuripanala1", value.Miercuri.PanaLa1));
            xelem.Add(new XElement("miercuridela2", value.Miercuri.DeLa2));
            xelem.Add(new XElement("miercuripanala2", value.Miercuri.PanaLa2));

            xelem.Add(new XElement("joidela1", value.Joi.DeLa1));
            xelem.Add(new XElement("joipanala1", value.Joi.PanaLa1));
            xelem.Add(new XElement("joidela2", value.Joi.DeLa2));
            xelem.Add(new XElement("joipanala2", value.Joi.PanaLa2));

            xelem.Add(new XElement("vineridela1", value.Vineri.DeLa1));
            xelem.Add(new XElement("vineripanala1", value.Vineri.PanaLa1));
            xelem.Add(new XElement("vineridela2", value.Vineri.DeLa2));
            xelem.Add(new XElement("vineripanala2", value.Vineri.PanaLa2));

            xelem.Add(new XElement("sambatadela1", value.Sambata.DeLa1));
            xelem.Add(new XElement("sambatapanala1", value.Sambata.PanaLa1));
            xelem.Add(new XElement("sambatadela2", value.Sambata.DeLa2));
            xelem.Add(new XElement("sambatapanala2", value.Sambata.PanaLa2));

            try
            {
                XDocument documentXmlMedici = XDocument.Load("medici.xml");
                documentXmlMedici.Element("medici").Add(xelem);
                documentXmlMedici.Save("medici.xml");
            }
            catch (FileNotFoundException)
            {
                XDocument documentXmlMedici = new XDocument(new XElement("medici"));
                documentXmlMedici.Element("medici").Add(xelem);
                documentXmlMedici.Save("medici.xml");
            }
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            ModificaPacientWindow modificaPacientWindow = new ModificaPacientWindow();
            pacientDeTrimisLaModificat = (Pacient)dataGridPacienti.SelectedItem;
            indiceDeTrimisLaModificat = dataGridPacienti.SelectedIndex;
            this.SendPacientLaModificatCallback += new Action<Pacient>(modificaPacientWindow.SendPacientFunc);
            modificaPacientWindow.SendPacientModificatCallback += new Action<Pacient>(this.ModificaPacientInDataGrid);
            SendPacientLaModificatCallback(pacientDeTrimisLaModificat);
            modificaPacientWindow.ShowDialog();
        }

        private void ModificaPacientInDataGrid(Pacient pacientModificat)
        {
            listaPacienti[indiceDeTrimisLaModificat] = pacientModificat;

            if (btnModifica.IsEnabled == true)
            {
                btnModifica.IsEnabled = false;
                btnSterge.IsEnabled = false;
                btnIstoricPacient.IsEnabled = false;
            }
        }

        private void btnAdaugaProgramareMedic1_Click(object sender, RoutedEventArgs e)
        {
            AdaugaProgramareWindow adaugaProgramareWindow = new AdaugaProgramareWindow();

            adaugaProgramareWindow.SendPacientProgramareToMainWindowCallback +=
                new Action<Pacient>(this.AdaugaPacientFunction);
            adaugaProgramareWindow.SendProgramareToMainWindowCallback +=
                new Action<Programare>(this.AdaugaProgramareFunction);
            this.SendIndiceMedicToAdaugaProgramareWindowCallback +=
                new Action<int>(adaugaProgramareWindow.SendIndiceMedicToAdaugaProgramareWindowFunc);
            SendIndiceMedicToAdaugaProgramareWindowCallback(tabProgramari.SelectedIndex);
            adaugaProgramareWindow.ShowDialog();
        }

        private void AdaugaProgramareFunction(Programare programareAdaugata)
        {
            AdaugaProgramareInProgramariPacienti(programareAdaugata);

            AdaugaProgramareInXML(programareAdaugata);
        }

        private void AdaugaProgramareInXML(Programare programareAdaugata)
        {
            XElement xelem = new XElement("programare");

            xelem.Add(new XElement("numepacient", programareAdaugata.NumePacient));
            xelem.Add(new XElement("prenumepacient", programareAdaugata.PrenumePacient));
            xelem.Add(new XElement("numemedic", programareAdaugata.NumeMedic));
            xelem.Add(new XElement("prenumemedic", programareAdaugata.PrenumeMedic));
            xelem.Add(new XElement("data", programareAdaugata.Data.ToShortDateString()));
            xelem.Add(new XElement("ora", programareAdaugata.Ora));
            xelem.Add(new XElement("durata", programareAdaugata.Durata));
            xelem.Add(new XElement("descriere", programareAdaugata.Descriere));
            xelem.Add(new XElement("indicelistamedici", programareAdaugata.IndiceListaMedici));
 
            try
            {
                XDocument documentXmlProgramari = XDocument.Load("programari.xml");
                documentXmlProgramari.Element("programari").Add(xelem);
                documentXmlProgramari.Save("programari.xml");
            }
            catch (FileNotFoundException)
            {
                XDocument documentXmlProgramari = new XDocument(new XElement("programari"));
                documentXmlProgramari.Element("programari").Add(xelem);
                documentXmlProgramari.Save("programari.xml");
            }
        }

        private void AdaugaProgramareInProgramariPacienti(Programare programareAdaugata)
        {
            // pentru programarile decalate
            int offsetDays = 0;
            switch (programareAdaugata.IndiceListaMedici)
            {
                case 0: // tab 1
                    offsetDays = offsetDays1;
                    break;
                case 1: // tab 2
                    offsetDays = offsetDays2;
                    break;
                case 2: // tab 3
                    offsetDays = offsetDays3;
                    break;
                case 3: // tab 4
                    offsetDays = offsetDays4;
                    break;
                case 4: // tab 5
                    offsetDays = offsetDays5;
                    break;
                case 5: // tab 6
                    offsetDays = offsetDays6;
                    break;
            }
            int numarDuminiciPanaLaData = NumarDuminiciPanaLaData(programareAdaugata.Data, offsetDays);
            int ziOrar = (programareAdaugata.Data - DateTime.Today).Days - offsetDays - numarDuminiciPanaLaData;

            if (ziOrar <= 13 && ziOrar >= 0)
            {
               
                double ora;
                if (programareAdaugata.Ora.Length <= 2)
                {
                    ora = double.Parse(programareAdaugata.Ora);
                }
                else
                {
                    switch (programareAdaugata.Ora.Substring(programareAdaugata.Ora.Length - 2))
                    {
                        case "00":
                            ora = 0;
                            break;
                        case "15":
                            ora = 0.25;
                            break;
                        case "30":
                            ora = 0.5;
                            break;
                        case "45":
                            ora = 0.75;
                            break;
                        default:
                            ora = 0;
                            break;
                    }
                    if (programareAdaugata.Ora.Length == 3)
                    {
                        ora += double.Parse(programareAdaugata.Ora.Substring(0, 1));
                    }
                    else if (programareAdaugata.Ora.Length == 4)
                    {
                        ora += double.Parse(programareAdaugata.Ora.Substring(0, 2));
                    }
                }
                double durataFactor;
                switch (programareAdaugata.Durata)
                {
                    case "30 minute":
                        durataFactor = 0.5d;
                        break;
                    case "1 oră":
                        durataFactor = 1d;
                        break;
                    case "1,5 ore":
                        durataFactor = 1.5d;
                        break;
                    case "2 ore":
                        durataFactor = 2d;
                        break;
                    case "2,5 ore":
                        durataFactor = 2.5d;
                        break;
                    case "3 ore":
                        durataFactor = 3d;
                        break;
                    default:
                        durataFactor = 0d;
                        break;
                }

                Rectangle rectangleProgramare = new Rectangle();
                rectangleProgramare.Stroke = Brushes.Beige;
                rectangleProgramare.StrokeThickness = 0.5;
                rectangleProgramare.Fill = Brushes.LimeGreen;
                rectangleProgramare.Width = 100;
                rectangleProgramare.Height = 50 * durataFactor;
                rectangleProgramare.Margin = new Thickness(100 * ziOrar + 50, 25 * (ora - 9) * 2, 0, 0);

                Grid gridProgramare = new Grid();
                gridProgramare.Children.Add(rectangleProgramare);               

                if (programareAdaugata.Durata != "30 minute")
                {
                    // programareAdaugata.Durata > "30 minute"

                    TextBlock tbTextProgramare = new TextBlock();
                    tbTextProgramare.Text = programareAdaugata.NumePacient + " " +
                                            ShrinkPrenume(programareAdaugata.PrenumePacient, programareAdaugata.NumePacient);
                    tbTextProgramare.Foreground = Brushes.White;
                    tbTextProgramare.FontSize = 11;
                    tbTextProgramare.Margin = new Thickness(100 * ziOrar + 50 + 5, 25 * (ora - 9) * 2 + 5, 0, 0);
                    tbTextProgramare.HorizontalAlignment = HorizontalAlignment.Center;
                    tbTextProgramare.FontWeight = FontWeights.Bold;
                    gridProgramare.Children.Add(tbTextProgramare);

                    tbTextProgramare = new TextBlock();
                    tbTextProgramare.Text = ParseTimeForMinutesAndPutColon(programareAdaugata.Ora) + " - " +
                                            ParseTimeForMinutesAndPutColon(ParseTimeAndAddDurata(
                                                            programareAdaugata.Ora, programareAdaugata.Durata));
                    tbTextProgramare.Foreground = Brushes.White;
                    tbTextProgramare.FontSize = 11;
                    tbTextProgramare.Margin = new Thickness(100 * ziOrar + 50 + 5, 25 * (ora - 9) * 2 + 5 + 15,
                                                                                                        0, 0);
                    tbTextProgramare.HorizontalAlignment = HorizontalAlignment.Center;
                    tbTextProgramare.FontWeight = FontWeights.Bold;
                    gridProgramare.Children.Add(tbTextProgramare);
                }
                else
                {
                    // programareAdaugata.Durata = "30 minute"

                    TextBlock tbTextProgramare = new TextBlock();
                    tbTextProgramare.Text = programareAdaugata.NumePacient + " " +
                                            ShrinkPrenume(programareAdaugata.PrenumePacient, programareAdaugata.NumePacient);
                    tbTextProgramare.Foreground = Brushes.White;
                    tbTextProgramare.FontSize = 11;
                    tbTextProgramare.Margin = new Thickness(100 * ziOrar + 50 + 5, 25 * (ora - 9) * 2 + 0, 0, 0);
                    tbTextProgramare.HorizontalAlignment = HorizontalAlignment.Center;
                    tbTextProgramare.FontWeight = FontWeights.Bold;
                    gridProgramare.Children.Add(tbTextProgramare);

                    tbTextProgramare = new TextBlock();
                    tbTextProgramare.Text = ParseTimeForMinutesAndPutColon(programareAdaugata.Ora) + " - " +
                                            ParseTimeForMinutesAndPutColon(ParseTimeAndAddDurata(
                                                            programareAdaugata.Ora, programareAdaugata.Durata));
                    tbTextProgramare.Foreground = Brushes.White;
                    tbTextProgramare.FontSize = 11;
                    tbTextProgramare.Margin = new Thickness(100 * ziOrar + 50 + 5, 25 * (ora - 9) * 2 + 1 + 10,
                                                                                                        0, 0);
                    tbTextProgramare.HorizontalAlignment = HorizontalAlignment.Center;
                    tbTextProgramare.FontWeight = FontWeights.Bold;
                    gridProgramare.Children.Add(tbTextProgramare);
                }

                switch (programareAdaugata.IndiceListaMedici)
                {
                    case 0: // tab 1
                        programariPacientiCanvas1.Children.Add(gridProgramare);
                        if (listaGridProgramariCanvas1.ContainsKey(programareAdaugata) == false)
                        {
                            listaGridProgramariCanvas1.Add(programareAdaugata, gridProgramare); 
                        }
                        break;
                    case 1: // tab 2
                        programariPacientiCanvas2.Children.Add(gridProgramare);
                        if (listaGridProgramariCanvas2.ContainsKey(programareAdaugata) == false)
                        {
                            listaGridProgramariCanvas2.Add(programareAdaugata, gridProgramare);
                        }
                        break;
                    case 2: // tab 3
                        programariPacientiCanvas3.Children.Add(gridProgramare);
                        if (listaGridProgramariCanvas3.ContainsKey(programareAdaugata) == false)
                        {
                            listaGridProgramariCanvas3.Add(programareAdaugata, gridProgramare);
                        }
                        break;
                    case 3: // tab 4
                        programariPacientiCanvas4.Children.Add(gridProgramare);
                        if (listaGridProgramariCanvas4.ContainsKey(programareAdaugata) == false)
                        {
                            listaGridProgramariCanvas4.Add(programareAdaugata, gridProgramare);
                        }
                        break;
                    case 4: // tab 5
                        programariPacientiCanvas5.Children.Add(gridProgramare);
                        if (listaGridProgramariCanvas5.ContainsKey(programareAdaugata) == false)
                        {
                            listaGridProgramariCanvas5.Add(programareAdaugata, gridProgramare);
                        }
                        break;
                    case 5: // tab 6
                        programariPacientiCanvas6.Children.Add(gridProgramare);
                        if (listaGridProgramariCanvas6.ContainsKey(programareAdaugata) == false)
                        {
                            listaGridProgramariCanvas6.Add(programareAdaugata, gridProgramare);
                        }
                        break;
                }                
            }
        }

        private string ShrinkPrenume(string prenumePacient, string numePacient)
        {
            if ((prenumePacient.Length + numePacient.Length) > Constants.LungimeMaximaNumePrenume)
            {
                string[] allPrenume = prenumePacient.Split(' ');
                string allPrenumeCuInitiale = String.Empty;

                for (int i = 0; i < allPrenume.Count(); i++)
                {
                    allPrenumeCuInitiale += allPrenume[i].Substring(0, 1) + ".";
                    if (i != allPrenume.Count()-1)
                    {
                        allPrenumeCuInitiale += " ";
                    }
                }
                return allPrenumeCuInitiale;
            }

            return prenumePacient;
        }

        private int NumarDuminiciPanaLaData(DateTime data, int offsetDays = 0)
        {
            int numarDuminici = 0;
            DateTime day = DateTime.Today.AddDays(offsetDays);

            if (day > data)
            {
                return numarDuminici;
            }

            while (day!=data)
            {
                if (day.DayOfWeek==DayOfWeek.Sunday)
                {
                    numarDuminici++;
                }
                day = day.AddDays(1);
            }
            
            return numarDuminici;
        }

        private string ParseTimeAndAddDurata(string ora, string durata)
        {
            int oraH = 0; int oraM = 0;
            int durataH = 0; int durataM = 0;
            // ora finala    
            int OraH = 0; int OraM = 0;

            if (ora == null)
            {
                return null;
            }
            if (ora.Length == 4)
            {
                oraH = int.Parse(ora.Substring(0, 2));
                oraM = int.Parse(ora.Substring(2, 2));
            }
            else if (ora.Length == 3)
            {
                oraH = int.Parse(ora.Substring(0, 1));
                oraM = int.Parse(ora.Substring(1, 2));
            }
            else
            {
                oraH = int.Parse(ora);
            }

            switch (durata)
            {
                case "30 minute":
                    durataM = 30;
                    break;
                case "1 oră":
                    durataH = 1;
                    break;
                case "1,5 ore":
                    durataH = 1;
                    durataM = 30;
                    break;
                case "2 ore":
                    durataH = 2;
                    break;
                case "2,5 ore":
                    durataH = 2;
                    durataM = 30;
                    break;
                case "3 ore":
                    durataH = 3;
                    break;
                default:
                    break;
            }
            OraH = oraH + durataH;
            OraM = oraM + durataM;
            int numberOfHours = OraM / 60; // ore in minute; OraM>60
            if (numberOfHours != 0)
            {
                OraH += numberOfHours;
                OraM -= 60 * numberOfHours; 
            }

            if (OraM==0)
            {
                return OraH.ToString();
            }
            return OraH.ToString() + OraM.ToString();
        }

        private void btnAdaugaProgramareMedic2_Click(object sender, RoutedEventArgs e)
        {
            AdaugaProgramareWindow adaugaProgramareWindow = new AdaugaProgramareWindow();

            adaugaProgramareWindow.SendPacientProgramareToMainWindowCallback +=
                new Action<Pacient>(this.AdaugaPacientFunction);
            adaugaProgramareWindow.SendProgramareToMainWindowCallback +=
                new Action<Programare>(this.AdaugaProgramareFunction);
            this.SendIndiceMedicToAdaugaProgramareWindowCallback +=
                new Action<int>(adaugaProgramareWindow.SendIndiceMedicToAdaugaProgramareWindowFunc);
            SendIndiceMedicToAdaugaProgramareWindowCallback(tabProgramari.SelectedIndex);
            adaugaProgramareWindow.ShowDialog();
        }

        private void btnAdaugaProgramareMedic3_Click(object sender, RoutedEventArgs e)
        {
            AdaugaProgramareWindow adaugaProgramareWindow = new AdaugaProgramareWindow();

            adaugaProgramareWindow.SendPacientProgramareToMainWindowCallback +=
                new Action<Pacient>(this.AdaugaPacientFunction);
            adaugaProgramareWindow.SendProgramareToMainWindowCallback +=
                new Action<Programare>(this.AdaugaProgramareFunction);
            this.SendIndiceMedicToAdaugaProgramareWindowCallback +=
                new Action<int>(adaugaProgramareWindow.SendIndiceMedicToAdaugaProgramareWindowFunc);
            SendIndiceMedicToAdaugaProgramareWindowCallback(tabProgramari.SelectedIndex);
            adaugaProgramareWindow.ShowDialog();
        }

        private void btnAdaugaProgramareMedic4_Click(object sender, RoutedEventArgs e)
        {
            AdaugaProgramareWindow adaugaProgramareWindow = new AdaugaProgramareWindow();

            adaugaProgramareWindow.SendPacientProgramareToMainWindowCallback +=
                new Action<Pacient>(this.AdaugaPacientFunction);
            adaugaProgramareWindow.SendProgramareToMainWindowCallback +=
                new Action<Programare>(this.AdaugaProgramareFunction);
            this.SendIndiceMedicToAdaugaProgramareWindowCallback +=
                new Action<int>(adaugaProgramareWindow.SendIndiceMedicToAdaugaProgramareWindowFunc);
            SendIndiceMedicToAdaugaProgramareWindowCallback(tabProgramari.SelectedIndex);
            adaugaProgramareWindow.ShowDialog();
        }

        private void btnAdaugaProgramareMedic5_Click(object sender, RoutedEventArgs e)
        {
            AdaugaProgramareWindow adaugaProgramareWindow = new AdaugaProgramareWindow();

            adaugaProgramareWindow.SendPacientProgramareToMainWindowCallback +=
                new Action<Pacient>(this.AdaugaPacientFunction);
            adaugaProgramareWindow.SendProgramareToMainWindowCallback +=
                new Action<Programare>(this.AdaugaProgramareFunction);
            this.SendIndiceMedicToAdaugaProgramareWindowCallback +=
                new Action<int>(adaugaProgramareWindow.SendIndiceMedicToAdaugaProgramareWindowFunc);
            SendIndiceMedicToAdaugaProgramareWindowCallback(tabProgramari.SelectedIndex);
            adaugaProgramareWindow.ShowDialog();
        }

        private void btnAdaugaProgramareMedic6_Click(object sender, RoutedEventArgs e)
        {
            AdaugaProgramareWindow adaugaProgramareWindow = new AdaugaProgramareWindow();

            adaugaProgramareWindow.SendPacientProgramareToMainWindowCallback +=
                new Action<Pacient>(this.AdaugaPacientFunction);
            adaugaProgramareWindow.SendProgramareToMainWindowCallback +=
                new Action<Programare>(this.AdaugaProgramareFunction);
            this.SendIndiceMedicToAdaugaProgramareWindowCallback +=
                new Action<int>(adaugaProgramareWindow.SendIndiceMedicToAdaugaProgramareWindowFunc);
            SendIndiceMedicToAdaugaProgramareWindowCallback(tabProgramari.SelectedIndex);
            adaugaProgramareWindow.ShowDialog();
        }

        private void detaliiMedic1_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (detaliiMedic1.Visibility == Visibility.Visible)
            {
                tabItemMedic1.Header = "Dr. " + detaliiMedic1.lbNume.Content.ToString() + " " + detaliiMedic1.lbPrenume.Content.ToString();
            }
        }

        private void detaliiMedic2_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (detaliiMedic2.Visibility == Visibility.Visible)
            {
                tabItemMedic2.Header = "Dr. " + detaliiMedic2.lbNume.Content.ToString() + " " + detaliiMedic2.lbPrenume.Content.ToString();
            }
        }

        private void detaliiMedic3_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (detaliiMedic3.Visibility == Visibility.Visible)
            {
                tabItemMedic3.Header = "Dr. " + detaliiMedic3.lbNume.Content.ToString() + " " + detaliiMedic3.lbPrenume.Content.ToString();
            }
        }

        private void detaliiMedic4_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (detaliiMedic4.Visibility == Visibility.Visible)
            {
                tabItemMedic4.Header = "Dr. " + detaliiMedic4.lbNume.Content.ToString() + " " + detaliiMedic4.lbPrenume.Content.ToString();
            }
        }

        private void detaliiMedic5_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (detaliiMedic5.Visibility == Visibility.Visible)
            {
                tabItemMedic5.Header = "Dr. " + detaliiMedic5.lbNume.Content.ToString() + " " + detaliiMedic5.lbPrenume.Content.ToString();
            }
        }

        private void detaliiMedic6_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (detaliiMedic6.Visibility==Visibility.Visible)
            {
                tabItemMedic6.Header = "Dr. " + detaliiMedic6.lbNume.Content.ToString() + " " + detaliiMedic6.lbPrenume.Content.ToString();
            }
        }

        private void btnModificaProgramareMedic1_Click(object sender, RoutedEventArgs e)
        {
            ModificaProgramareWindow modificaProgramareWindow = new ModificaProgramareWindow();

            modificaProgramareWindow.SendProgramareToMainWindowToDeleteCallback +=
                new Action<Programare>(this.StergeProgramareFunction1);
            modificaProgramareWindow.SendProgramareLaModificatInMainWindowCallback +=
                new Action<Programare, Programare>(this.ModificaProgramareFunction);
            this.SendIndiceMedicToModificaProgramareWindowCallback +=
                new Action<int>(modificaProgramareWindow.SendIndiceMedicToModificaProgramareWindowCallbackFunc);
            SendIndiceMedicToModificaProgramareWindowCallback(tabProgramari.SelectedIndex);
            modificaProgramareWindow.ShowDialog();
        }

        private void ModificaProgramareFunction(Programare programareVeche, Programare programareNoua)
        {
            programareDeModificatInitiala = programareVeche;

            // alegem Canvas-ul corect
            Dictionary<Programare, Grid> dictionary = AlegemCanvasulCorect(programareVeche.IndiceListaMedici);

            // sterge programare veche din ProgramariPacienti
            StergeProgramareVeche(programareVeche, dictionary);

            // adauga programare noua 
            AdaugaProgramareInProgramariPacienti(programareNoua);

            // modifica programare in .xml
            ModificaProgramareInXml(programareNoua, programareVeche);
        }

        private void StergeProgramareFunction1(Programare programare)
        {
            // alegem Canvas-ul corect
            Dictionary<Programare, Grid> dictionary = AlegemCanvasulCorect(programare.IndiceListaMedici);

            // sterge programare veche din ProgramariPacienti
            StergeProgramareVeche(programare, dictionary);

            // sterge programare din .xml
            StergeProgramareDinXml(programare);
        }        

        private void btnModificaProgramareMedic2_Click(object sender, RoutedEventArgs e)
        {
            ModificaProgramareWindow modificaProgramareWindow = new ModificaProgramareWindow();

            modificaProgramareWindow.SendProgramareToMainWindowToDeleteCallback +=
               new Action<Programare>(this.StergeProgramareFunction2);
            modificaProgramareWindow.SendProgramareLaModificatInMainWindowCallback +=
               new Action<Programare, Programare>(this.ModificaProgramareFunction);
            this.SendIndiceMedicToModificaProgramareWindowCallback +=
               new Action<int>(modificaProgramareWindow.SendIndiceMedicToModificaProgramareWindowCallbackFunc);
            SendIndiceMedicToModificaProgramareWindowCallback(tabProgramari.SelectedIndex);
            modificaProgramareWindow.ShowDialog();
        }

        private void StergeProgramareFunction2(Programare programare)
        {
            // alegem Canvas-ul corect
            Dictionary<Programare, Grid> dictionary = AlegemCanvasulCorect(programare.IndiceListaMedici);

              // sterge programare veche din ProgramariPacienti
            StergeProgramareVeche(programare, dictionary);     

            // sterge programare din .xml
            StergeProgramareDinXml(programare);  
        }

        private void StergeProgramareDinXml(Programare programare)
        {
            // sterge programare din .xml
            XDocument documentXmlProgramari = XDocument.Load("programari.xml");
            documentXmlProgramari.Root.Elements("programare")
                  .Where(x => x.Element("numepacient").Value == programare.NumePacient)
                  .Where(x => x.Element("prenumepacient").Value == programare.PrenumePacient)
                  .Where(x => x.Element("numemedic").Value == programare.NumeMedic)
                  .Where(x => x.Element("prenumemedic").Value == programare.PrenumeMedic)
                  .Where(x => x.Element("data").Value == programare.Data.ToShortDateString())
                  .Where(x => x.Element("ora").Value == programare.Ora)
                  .Where(x => x.Element("durata").Value == programare.Durata)
                  .Where(x => x.Element("descriere").Value == programare.Descriere)
                  .Where(x => x.Element("indicelistamedici").Value == programare.IndiceListaMedici.ToString()).Remove();

            documentXmlProgramari.Save("programari.xml");
        }

        private void ModificaProgramareInXml(Programare programareNoua, Programare programareVeche)
        {
            // modifica programare in .xml
            XDocument documentXmlProgramari = XDocument.Load("programari.xml");
            var programareDeModificat = from p in documentXmlProgramari.Root.Elements("programare")
                                        where p.Element("numepacient").Value == programareVeche.NumePacient
                                        where p.Element("prenumepacient").Value == programareVeche.PrenumePacient
                                        where p.Element("numemedic").Value == programareVeche.NumeMedic
                                        where p.Element("prenumemedic").Value == programareVeche.PrenumeMedic
                                        where p.Element("data").Value == programareVeche.Data.ToShortDateString()
                                        where p.Element("ora").Value == programareVeche.Ora
                                        where p.Element("durata").Value == programareVeche.Durata
                                        where p.Element("descriere").Value == programareVeche.Descriere
                                        where p.Element("indicelistamedici").Value == programareVeche.IndiceListaMedici.ToString()
                                        select p;
            foreach (XElement p in programareDeModificat)
            {
                p.SetElementValue("numepacient", programareNoua.NumePacient);
                p.SetElementValue("prenumepacient", programareNoua.PrenumePacient);
                p.SetElementValue("numemedic", programareNoua.NumeMedic);
                p.SetElementValue("prenumemedic", programareNoua.PrenumeMedic);
                p.SetElementValue("data", programareNoua.Data.ToShortDateString());
                p.SetElementValue("ora", programareNoua.Ora);
                p.SetElementValue("durata", programareNoua.Durata);
                p.SetElementValue("descriere", programareNoua.Descriere);
                p.SetElementValue("indicelistamedici", programareNoua.IndiceListaMedici);
            }
            documentXmlProgramari.Save("programari.xml");
        }

        private void btnModificaProgramareMedic3_Click(object sender, RoutedEventArgs e)
        {
            ModificaProgramareWindow modificaProgramareWindow = new ModificaProgramareWindow();
        
            modificaProgramareWindow.SendProgramareToMainWindowToDeleteCallback +=
               new Action<Programare>(this.StergeProgramareFunction3);
            modificaProgramareWindow.SendProgramareLaModificatInMainWindowCallback +=
              new Action<Programare, Programare>(this.ModificaProgramareFunction);
            this.SendIndiceMedicToModificaProgramareWindowCallback +=
              new Action<int>(modificaProgramareWindow.SendIndiceMedicToModificaProgramareWindowCallbackFunc);
            SendIndiceMedicToModificaProgramareWindowCallback(tabProgramari.SelectedIndex);
            modificaProgramareWindow.ShowDialog();
        }

        private void StergeProgramareFunction3(Programare programare)
        {
            // alegem Canvas-ul corect
            Dictionary<Programare, Grid> dictionary = AlegemCanvasulCorect(programare.IndiceListaMedici);

            // sterge programare veche din ProgramariPacienti
            StergeProgramareVeche(programare, dictionary);

            // sterge programare din .xml
            StergeProgramareDinXml(programare);
        }
        
       private void btnModificaProgramareMedic4_Click(object sender, RoutedEventArgs e)
        {
            ModificaProgramareWindow modificaProgramareWindow = new ModificaProgramareWindow();

            modificaProgramareWindow.SendProgramareToMainWindowToDeleteCallback +=
               new Action<Programare>(this.StergeProgramareFunction4);
            modificaProgramareWindow.SendProgramareLaModificatInMainWindowCallback +=
               new Action<Programare, Programare>(this.ModificaProgramareFunction);
            this.SendIndiceMedicToModificaProgramareWindowCallback +=
               new Action<int>(modificaProgramareWindow.SendIndiceMedicToModificaProgramareWindowCallbackFunc);
            SendIndiceMedicToModificaProgramareWindowCallback(tabProgramari.SelectedIndex);
            modificaProgramareWindow.ShowDialog();
        }

        private void StergeProgramareFunction4(Programare programare)
        {
            // alegem Canvas-ul corect
            Dictionary<Programare, Grid> dictionary = AlegemCanvasulCorect(programare.IndiceListaMedici);

            // sterge programare veche din ProgramariPacienti
            StergeProgramareVeche(programare, dictionary);

            // sterge programare din .xml
            StergeProgramareDinXml(programare);
        }
        
        private void btnModificaProgramareMedic5_Click(object sender, RoutedEventArgs e)
        {
            ModificaProgramareWindow modificaProgramareWindow = new ModificaProgramareWindow();

            modificaProgramareWindow.SendProgramareToMainWindowToDeleteCallback +=
               new Action<Programare>(this.StergeProgramareFunction5);
            modificaProgramareWindow.SendProgramareLaModificatInMainWindowCallback +=
               new Action<Programare, Programare>(this.ModificaProgramareFunction);
            this.SendIndiceMedicToModificaProgramareWindowCallback +=
               new Action<int>(modificaProgramareWindow.SendIndiceMedicToModificaProgramareWindowCallbackFunc);
            SendIndiceMedicToModificaProgramareWindowCallback(tabProgramari.SelectedIndex);
            modificaProgramareWindow.ShowDialog();
        }

        private void StergeProgramareFunction5(Programare programare)
        {
            // alegem Canvas-ul corect
            Dictionary<Programare, Grid> dictionary = AlegemCanvasulCorect(programare.IndiceListaMedici);

            // sterge programare veche din ProgramariPacienti
            StergeProgramareVeche(programare, dictionary);

            // sterge programare din .xml
            StergeProgramareDinXml(programare);
        }
        
        private void btnModificaProgramareMedic6_Click(object sender, RoutedEventArgs e)
        {
            ModificaProgramareWindow modificaProgramareWindow = new ModificaProgramareWindow();

            modificaProgramareWindow.SendProgramareToMainWindowToDeleteCallback +=
               new Action<Programare>(this.StergeProgramareFunction6);
            modificaProgramareWindow.SendProgramareLaModificatInMainWindowCallback +=
               new Action<Programare, Programare>(this.ModificaProgramareFunction);
            this.SendIndiceMedicToModificaProgramareWindowCallback +=
               new Action<int>(modificaProgramareWindow.SendIndiceMedicToModificaProgramareWindowCallbackFunc);
            SendIndiceMedicToModificaProgramareWindowCallback(tabProgramari.SelectedIndex);
            modificaProgramareWindow.ShowDialog();
        }

        private void StergeProgramareFunction6(Programare programare)
        {
            // alegem Canvas-ul corect
            Dictionary<Programare, Grid> dictionary = AlegemCanvasulCorect(programare.IndiceListaMedici);

            // sterge programare veche din ProgramariPacienti
            StergeProgramareVeche(programare, dictionary);

            // sterge programare din .xml
            StergeProgramareDinXml(programare);
        }
        
        private void StergeProgramareVeche(Programare programare, Dictionary<Programare, Grid> dictionary)
        {
            Programare programareDeSters = new Programare();
            bool foundProgramare = false;

            // cautam in programarile tuturor medicilor
            foreach (var item in dictionary)
            {
                if ((item.Key.NumePacient == programare.NumePacient) && (item.Key.PrenumePacient == programare.PrenumePacient) &&
                    (item.Key.NumeMedic == programare.NumeMedic) && (item.Key.PrenumeMedic == programare.PrenumeMedic) &&
                    (item.Key.Data == programare.Data) && (item.Key.Ora == programare.Ora) &&
                    (item.Key.Durata == programare.Durata) && (item.Key.Descriere == programare.Descriere) &&   
                    (item.Key.IndiceListaMedici == programare.IndiceListaMedici))
                {
                    // sterge  programare veche
                    switch (programare.IndiceListaMedici)
                    {
                        case 0:
                            programariPacientiCanvas1.Children.Remove(item.Value);
                            programareDeSters = item.Key;
                            foundProgramare = true;
                            break;
                        case 1:
                            programariPacientiCanvas2.Children.Remove(item.Value);
                            programareDeSters = item.Key;
                            foundProgramare = true;
                            break;
                        case 2:
                            programariPacientiCanvas3.Children.Remove(item.Value);
                            programareDeSters = item.Key;
                            foundProgramare = true;
                            break;
                        case 3:
                            programariPacientiCanvas4.Children.Remove(item.Value);
                            programareDeSters = item.Key;
                            foundProgramare = true;
                            break;
                        case 4:
                            programariPacientiCanvas5.Children.Remove(item.Value);
                            programareDeSters = item.Key;
                            foundProgramare = true;
                            break;
                        case 5:
                            programariPacientiCanvas6.Children.Remove(item.Value);
                            programareDeSters = item.Key;
                            foundProgramare = true;
                            break;
                        default:
                            break;
                    }
                    if(foundProgramare == true)
                    {
                        break;
                    }
                }
            }

            // sterge  programare veche din listaGridProgramariCanvas
            switch (programare.IndiceListaMedici)
            {
                case 0:
                    listaGridProgramariCanvas1.Remove(programareDeSters);
                    break;
                case 1:
                    listaGridProgramariCanvas2.Remove(programareDeSters);
                    break;
                case 2:
                    listaGridProgramariCanvas3.Remove(programareDeSters);
                    break;
                case 3:
                    listaGridProgramariCanvas4.Remove(programareDeSters);
                    break;
                case 4:
                    listaGridProgramariCanvas5.Remove(programareDeSters);
                    break;
                case 5:
                    listaGridProgramariCanvas6.Remove(programareDeSters);
                    break;
                default:
                    break;
            }
        }

        private Dictionary<Programare, Grid> AlegemCanvasulCorect(int indiceListaMedici)
        {
            Dictionary<Programare, Grid> dictionary = new Dictionary<Programare, Grid>();

            // alegem Canvas-ul corect
            switch (indiceListaMedici)
            {
                case 0: // tab 1
                    dictionary = listaGridProgramariCanvas1;
                    break;
                case 1: // tab 2
                    dictionary = listaGridProgramariCanvas2;
                    break;
                case 2: // tab 3
                    dictionary = listaGridProgramariCanvas3;
                    break;
                case 3: // tab 4
                    dictionary = listaGridProgramariCanvas4;
                    break;
                case 4: // tab 5
                    dictionary = listaGridProgramariCanvas5;
                    break;
                case 5: // tab 6
                    dictionary = listaGridProgramariCanvas6;
                    break;
            }

            return dictionary;
        }

        private void btnInainteProgramareMedic1_Click(object sender, RoutedEventArgs e)
        {
            PutDatesOverProgramariPacientiOffset1(1);
            // sterge toate programarile 
            var grids = programariPacientiCanvas1.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas1.Children.Remove(item);
            }
            // adauga aproape toate programarile, decalate
            var temp = listaGridProgramariCanvas1.Keys.ToList();
            listaGridProgramariCanvas1.Clear();
            foreach (var item in temp)
            {
                AdaugaProgramareInProgramariPacienti(item);
            }
            // adauga programarile zilei noi din ProgramariPacienti
            AdaugaZiProgramariPacienti(detaliiMedic1.lbNume.Content.ToString(), detaliiMedic1.lbPrenume.Content.ToString(),
                                             ultimaZiProgramariPacienti1);
        }

        private void AdaugaZiProgramariPacienti(string numeMedic, string prenumeMedic, string ziProgramare)
        {
            try
            {
                XDocument documentXmlProgramari = XDocument.Load("programari.xml");
                var programariShow = documentXmlProgramari.Descendants("programare");
                var programari = from p in programariShow
                                 where p.Element("numemedic").Value == numeMedic
                                 where p.Element("prenumemedic").Value == prenumeMedic
                                 where p.Element("data").Value == ziProgramare
                                 select new Programare()
                                 {
                                     NumePacient = p.Descendants("numepacient").First().Value,
                                     PrenumePacient = p.Descendants("prenumepacient").First().Value,
                                     NumeMedic = p.Descendants("numemedic").First().Value,
                                     PrenumeMedic = p.Descendants("prenumemedic").First().Value,
                                     Data = DateTime.Parse(p.Descendants("data").First().Value,
                                                System.Globalization.CultureInfo.CreateSpecificCulture("ro-RO")),
                                     Ora = p.Descendants("ora").First().Value,
                                     Durata = p.Descendants("durata").First().Value,
                                     Descriere = p.Descendants("descriere").First().Value,
                                     IndiceListaMedici = int.Parse(p.Descendants("indicelistamedici").First().Value),
                                 };
                foreach (var item in programari)
                {
                    AdaugaProgramareInProgramariPacienti(item);
                }
            }
            catch (FileNotFoundException)
            {
                //MessageBox.Show("Fișierul programari.xml lipsește!", "Fișier inexistent");
            }
        }

        private void PutDatesOverProgramariPacientiOffset1(int offset)
        {
            // pentru ToShortDateString
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");
        
            DateTime day = DateTime.Today;
            // adauga offset
            offsetDays1 += offset;
            day = day.AddDays(offsetDays1);

            // skip Sunday
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
                // Se mai incrementeaza aici o data pentru ca trecerea peste duminica 
                // sa fie promovata si pentru urmatoarele duminici
                offsetDays1 += 1;
            }
            programariPacienti1.oZiIntreaga1.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            primaZiProgramariPacienti1 = day.ToShortDateString();

            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga2.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];       
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga3.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];         
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga4.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                            ", " + day.Day.ToString() + " " +
                                                            Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga5.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                            ", " + day.Day.ToString() + " " +
                                                            Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga6.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga7.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                                ", " + day.Day.ToString() + " " +
                                                                Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga8.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                             ", " + day.Day.ToString() + " " +
                                                             Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga9.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga10.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga11.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                               ", " + day.Day.ToString() + " " +
                                                               Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga12.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga13.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti1.oZiIntreaga14.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                             ", " + day.Day.ToString() + " " +
                                                             Constants.LunileAnului[day.Month];
            ultimaZiProgramariPacienti1 = day.ToShortDateString();
        }

        private void btnInapoiProgramareMedic1_Click(object sender, RoutedEventArgs e)
        {
            // daca este Luni, parcurgem 2 zile, peste Duminica  
            if (programariPacienti1.oZiIntreaga1.ZiLuna.Text.Substring(0, 2)=="Lu")
            {
                PutDatesOverProgramariPacientiOffset1(-2);
            }
            else
            {
                PutDatesOverProgramariPacientiOffset1(-1); 
            }
            // sterge toate programarile 
            var grids = programariPacientiCanvas1.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas1.Children.Remove(item);
            }
            // adauga aproape toate programarile, decalate
            var temp = listaGridProgramariCanvas1.Keys.ToList();
            listaGridProgramariCanvas1.Clear();
            foreach (var item in temp)
            {
                AdaugaProgramareInProgramariPacienti(item);
            }
            // adauga programarile zilei noi din ProgramariPacienti
            AdaugaZiProgramariPacienti(detaliiMedic1.lbNume.Content.ToString(), detaliiMedic1.lbPrenume.Content.ToString(),
                                             primaZiProgramariPacienti1);
        }

        private void btnRepedeInainteProgramareMedic1_Click(object sender, RoutedEventArgs e)
        {
            // repede = două săptămâni
            PutDatesOverProgramariPacientiOffset1(14);
            // sterge toate programarile 
            var grids = programariPacientiCanvas1.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas1.Children.Remove(item);
            }
            // adauga programarile noi din ProgramariPacienti
            AdaugaZileProgramariPacienti(detaliiMedic1.lbNume.Content.ToString(), detaliiMedic1.lbPrenume.Content.ToString(),
                                           primaZiProgramariPacienti1, ultimaZiProgramariPacienti1);
        }

        private void AdaugaZileProgramariPacienti(string numeMedic, string prenumeMedic, string primaZi, string ultimaZi)
        {
            try
            {
                XDocument documentXmlProgramari = XDocument.Load("programari.xml");
                var programariShow = documentXmlProgramari.Descendants("programare");
                var programari = from p in programariShow
                                 where p.Element("numemedic").Value == numeMedic
                                 where p.Element("prenumemedic").Value == prenumeMedic
                                 where (DateTime.Parse(p.Descendants("data").First().Value, System.Globalization.CultureInfo.CreateSpecificCulture("ro-RO")) 
                                    >= DateTime.Parse(primaZi, System.Globalization.CultureInfo.CreateSpecificCulture("ro-RO"))) &&
                                    (DateTime.Parse(p.Descendants("data").First().Value, System.Globalization.CultureInfo.CreateSpecificCulture("ro-RO"))
                                    <= DateTime.Parse(ultimaZi, System.Globalization.CultureInfo.CreateSpecificCulture("ro-RO")))
                                 select new Programare()
                                 {
                                     NumePacient = p.Descendants("numepacient").First().Value,
                                     PrenumePacient = p.Descendants("prenumepacient").First().Value,
                                     NumeMedic = p.Descendants("numemedic").First().Value,
                                     PrenumeMedic = p.Descendants("prenumemedic").First().Value,
                                     Data = DateTime.Parse(p.Descendants("data").First().Value,
                                                System.Globalization.CultureInfo.CreateSpecificCulture("ro-RO")),
                                     Ora = p.Descendants("ora").First().Value,
                                     Durata = p.Descendants("durata").First().Value,
                                     Descriere = p.Descendants("descriere").First().Value,
                                     IndiceListaMedici = int.Parse(p.Descendants("indicelistamedici").First().Value),
                                 };
                foreach (var item in programari)
                {
                    AdaugaProgramareInProgramariPacienti(item);
                }
            }
            catch (FileNotFoundException)
            {
                //MessageBox.Show("Fișierul programari.xml lipsește!", "Fișier inexistent");
            }
        }

        private void btnRepedeInapoiProgramareMedic1_Click(object sender, RoutedEventArgs e)
        {
            // repede = două săptămâni
            PutDatesOverProgramariPacientiOffset1(-14);
            // sterge toate programarile 
            var grids = programariPacientiCanvas1.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas1.Children.Remove(item);
            }
            // adauga programarile noi din ProgramariPacienti
            AdaugaZileProgramariPacienti(detaliiMedic1.lbNume.Content.ToString(), detaliiMedic1.lbPrenume.Content.ToString(),
                                           primaZiProgramariPacienti1, ultimaZiProgramariPacienti1);
        }

        private void btnInainteProgramareMedic2_Click(object sender, RoutedEventArgs e)
        {
            PutDatesOverProgramariPacientiOffset2(1);
            // sterge toate programarile 
            var grids = programariPacientiCanvas2.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas2.Children.Remove(item);
            }
            // adauga aproape toate programarile, decalate
            var temp = listaGridProgramariCanvas2.Keys.ToList();
            listaGridProgramariCanvas2.Clear();
            foreach (var item in temp)
            {
                AdaugaProgramareInProgramariPacienti(item);
            }
            // adauga programarile zilei noi din ProgramariPacienti
            AdaugaZiProgramariPacienti(detaliiMedic2.lbNume.Content.ToString(), detaliiMedic2.lbPrenume.Content.ToString(),
                                             ultimaZiProgramariPacienti2);
        }

        private void PutDatesOverProgramariPacientiOffset2(int offset)
        {
            // pentru ToShortDateString
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");

            DateTime day = DateTime.Today;
            // adauga offset
            offsetDays2 += offset;
            day = day.AddDays(offsetDays2);

            // skip Sunday
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
                // Se mai incrementeaza aici o data pentru ca trecerea peste duminica 
                // sa fie promovata si pentru urmatoarele duminici
                offsetDays2 += 1;
            }
            programariPacienti2.oZiIntreaga1.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            primaZiProgramariPacienti2 = day.ToShortDateString();

            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga2.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga3.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga4.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                            ", " + day.Day.ToString() + " " +
                                                            Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga5.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                            ", " + day.Day.ToString() + " " +
                                                            Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga6.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga7.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                                ", " + day.Day.ToString() + " " +
                                                                Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga8.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                             ", " + day.Day.ToString() + " " +
                                                             Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga9.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga10.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga11.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                               ", " + day.Day.ToString() + " " +
                                                               Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga12.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga13.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti2.oZiIntreaga14.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                             ", " + day.Day.ToString() + " " +
                                                             Constants.LunileAnului[day.Month];
            ultimaZiProgramariPacienti2 = day.ToShortDateString();
        }

        private void btnInapoiProgramareMedic2_Click(object sender, RoutedEventArgs e)
        {
            // daca este Luni, parcurgem 2 zile, peste Duminica  
            if (programariPacienti2.oZiIntreaga1.ZiLuna.Text.Substring(0, 2) == "Lu")
            {
                PutDatesOverProgramariPacientiOffset2(-2);
            }
            else
            {
                PutDatesOverProgramariPacientiOffset2(-1);
            }
            // sterge toate programarile 
            var grids = programariPacientiCanvas2.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas2.Children.Remove(item);
            }
            // adauga aproape toate programarile, decalate
            var temp = listaGridProgramariCanvas2.Keys.ToList();
            listaGridProgramariCanvas2.Clear();
            foreach (var item in temp)
            {
                AdaugaProgramareInProgramariPacienti(item);
            }
            // adauga programarile zilei noi din ProgramariPacienti
            AdaugaZiProgramariPacienti(detaliiMedic2.lbNume.Content.ToString(), detaliiMedic2.lbPrenume.Content.ToString(),
                                             primaZiProgramariPacienti2);
        }

        private void btnRepedeInainteProgramareMedic2_Click(object sender, RoutedEventArgs e)
        {
            // repede = două săptămâni
            PutDatesOverProgramariPacientiOffset2(14);
            // sterge toate programarile 
            var grids = programariPacientiCanvas2.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas2.Children.Remove(item);
            }
            // adauga programarile noi din ProgramariPacienti
            AdaugaZileProgramariPacienti(detaliiMedic2.lbNume.Content.ToString(), detaliiMedic2.lbPrenume.Content.ToString(),
                                           primaZiProgramariPacienti2, ultimaZiProgramariPacienti2);
        }

        private void btnRepedeInapoiProgramareMedic2_Click(object sender, RoutedEventArgs e)
        {
            // repede = două săptămâni
            PutDatesOverProgramariPacientiOffset2(-14);
            // sterge toate programarile 
            var grids = programariPacientiCanvas2.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas2.Children.Remove(item);
            }
            // adauga programarile noi din ProgramariPacienti
            AdaugaZileProgramariPacienti(detaliiMedic2.lbNume.Content.ToString(), detaliiMedic2.lbPrenume.Content.ToString(),
                                           primaZiProgramariPacienti2, ultimaZiProgramariPacienti2);
        }

        private void btnInainteProgramareMedic3_Click(object sender, RoutedEventArgs e)
        {
            PutDatesOverProgramariPacientiOffset3(1);
            // sterge toate programarile 
            var grids = programariPacientiCanvas3.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas3.Children.Remove(item);
            }
            // adauga aproape toate programarile, decalate
            var temp = listaGridProgramariCanvas3.Keys.ToList();
            listaGridProgramariCanvas3.Clear();
            foreach (var item in temp)
            {
                AdaugaProgramareInProgramariPacienti(item);
            }
            // adauga programarile zilei noi din ProgramariPacienti
            AdaugaZiProgramariPacienti(detaliiMedic3.lbNume.Content.ToString(), detaliiMedic3.lbPrenume.Content.ToString(),
                                             ultimaZiProgramariPacienti3);
        }

        private void PutDatesOverProgramariPacientiOffset3(int offset)
        {
            // pentru ToShortDateString
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");

            DateTime day = DateTime.Today;
            // adauga offset
            offsetDays3 += offset;
            day = day.AddDays(offsetDays3);

            // skip Sunday
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
                // Se mai incrementeaza aici o data pentru ca trecerea peste duminica 
                // sa fie promovata si pentru urmatoarele duminici
                offsetDays3 += 1;
            }
            programariPacienti3.oZiIntreaga1.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            primaZiProgramariPacienti3 = day.ToShortDateString();

            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga2.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga3.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga4.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                            ", " + day.Day.ToString() + " " +
                                                            Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga5.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                            ", " + day.Day.ToString() + " " +
                                                            Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga6.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga7.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                                ", " + day.Day.ToString() + " " +
                                                                Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga8.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                             ", " + day.Day.ToString() + " " +
                                                             Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga9.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga10.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga11.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                               ", " + day.Day.ToString() + " " +
                                                               Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga12.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga13.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti3.oZiIntreaga14.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                             ", " + day.Day.ToString() + " " +
                                                             Constants.LunileAnului[day.Month];
            ultimaZiProgramariPacienti3 = day.ToShortDateString();
        }

        private void btnInapoiProgramareMedic3_Click(object sender, RoutedEventArgs e)
        {
            // daca este Luni, parcurgem 2 zile, peste Duminica  
            if (programariPacienti3.oZiIntreaga1.ZiLuna.Text.Substring(0, 2) == "Lu")
            {
                PutDatesOverProgramariPacientiOffset3(-2);
            }
            else
            {
                PutDatesOverProgramariPacientiOffset3(-1);
            }
            // sterge toate programarile 
            var grids = programariPacientiCanvas3.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas3.Children.Remove(item);
            }
            // adauga aproape toate programarile, decalate
            var temp = listaGridProgramariCanvas3.Keys.ToList();
            listaGridProgramariCanvas3.Clear();
            foreach (var item in temp)
            {
                AdaugaProgramareInProgramariPacienti(item);
            }
            // adauga programarile zilei noi din ProgramariPacienti
            AdaugaZiProgramariPacienti(detaliiMedic3.lbNume.Content.ToString(), detaliiMedic3.lbPrenume.Content.ToString(),
                                             primaZiProgramariPacienti3);
        }

        private void btnRepedeInainteProgramareMedic3_Click(object sender, RoutedEventArgs e)
        {
            // repede = două săptămâni
            PutDatesOverProgramariPacientiOffset3(14);
            // sterge toate programarile 
            var grids = programariPacientiCanvas3.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas3.Children.Remove(item);
            }
            // adauga programarile noi din ProgramariPacienti
            AdaugaZileProgramariPacienti(detaliiMedic3.lbNume.Content.ToString(), detaliiMedic3.lbPrenume.Content.ToString(),
                                           primaZiProgramariPacienti3, ultimaZiProgramariPacienti3);
        }

        private void btnRepedeInapoiProgramareMedic3_Click(object sender, RoutedEventArgs e)
        {
            // repede = două săptămâni
            PutDatesOverProgramariPacientiOffset3(-14);
            // sterge toate programarile 
            var grids = programariPacientiCanvas3.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas3.Children.Remove(item);
            }
            // adauga programarile noi din ProgramariPacienti
            AdaugaZileProgramariPacienti(detaliiMedic3.lbNume.Content.ToString(), detaliiMedic3.lbPrenume.Content.ToString(),
                                           primaZiProgramariPacienti3, ultimaZiProgramariPacienti3);
        }

        private void btnInainteProgramareMedic4_Click(object sender, RoutedEventArgs e)
        {
            PutDatesOverProgramariPacientiOffset4(1);
            // sterge toate programarile 
            var grids = programariPacientiCanvas4.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas4.Children.Remove(item);
            }
            // adauga aproape toate programarile, decalate
            var temp = listaGridProgramariCanvas4.Keys.ToList();
            listaGridProgramariCanvas4.Clear();
            foreach (var item in temp)
            {
                AdaugaProgramareInProgramariPacienti(item);
            }
            // adauga programarile zilei noi din ProgramariPacienti
            AdaugaZiProgramariPacienti(detaliiMedic4.lbNume.Content.ToString(), detaliiMedic4.lbPrenume.Content.ToString(),
                                             ultimaZiProgramariPacienti4);
        }

        private void PutDatesOverProgramariPacientiOffset4(int offset)
        {
            // pentru ToShortDateString
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");

            DateTime day = DateTime.Today;
            // adauga offset
            offsetDays4 += offset;
            day = day.AddDays(offsetDays4);

            // skip Sunday
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
                // Se mai incrementeaza aici o data pentru ca trecerea peste duminica 
                // sa fie promovata si pentru urmatoarele duminici
                offsetDays4 += 1;
            }
            programariPacienti4.oZiIntreaga1.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            primaZiProgramariPacienti4 = day.ToShortDateString();

            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga2.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga3.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga4.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                            ", " + day.Day.ToString() + " " +
                                                            Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga5.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                            ", " + day.Day.ToString() + " " +
                                                            Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga6.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga7.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                                ", " + day.Day.ToString() + " " +
                                                                Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga8.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                             ", " + day.Day.ToString() + " " +
                                                             Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga9.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga10.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga11.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                               ", " + day.Day.ToString() + " " +
                                                               Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga12.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga13.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti4.oZiIntreaga14.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                             ", " + day.Day.ToString() + " " +
                                                             Constants.LunileAnului[day.Month];
            ultimaZiProgramariPacienti4 = day.ToShortDateString();
        }

        private void btnInapoiProgramareMedic4_Click(object sender, RoutedEventArgs e)
        {
            // daca este Luni, parcurgem 2 zile, peste Duminica  
            if (programariPacienti4.oZiIntreaga1.ZiLuna.Text.Substring(0, 2) == "Lu")
            {
                PutDatesOverProgramariPacientiOffset4(-2);
            }
            else
            {
                PutDatesOverProgramariPacientiOffset4(-1);
            }
            // sterge toate programarile 
            var grids = programariPacientiCanvas4.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas4.Children.Remove(item);
            }
            // adauga aproape toate programarile, decalate
            var temp = listaGridProgramariCanvas4.Keys.ToList();
            listaGridProgramariCanvas4.Clear();
            foreach (var item in temp)
            {
                AdaugaProgramareInProgramariPacienti(item);
            }
            // adauga programarile zilei noi din ProgramariPacienti
            AdaugaZiProgramariPacienti(detaliiMedic4.lbNume.Content.ToString(), detaliiMedic4.lbPrenume.Content.ToString(),
                                             primaZiProgramariPacienti4);
        }

        private void btnRepedeInainteProgramareMedic4_Click(object sender, RoutedEventArgs e)
        {
            // repede = două săptămâni
            PutDatesOverProgramariPacientiOffset4(14);
            // sterge toate programarile 
            var grids = programariPacientiCanvas4.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas4.Children.Remove(item);
            }
            // adauga programarile noi din ProgramariPacienti
            AdaugaZileProgramariPacienti(detaliiMedic4.lbNume.Content.ToString(), detaliiMedic4.lbPrenume.Content.ToString(),
                                           primaZiProgramariPacienti4, ultimaZiProgramariPacienti4);
        }

        private void btnRepedeInapoiProgramareMedic4_Click(object sender, RoutedEventArgs e)
        {
            // repede = două săptămâni
            PutDatesOverProgramariPacientiOffset4(-14);
            // sterge toate programarile 
            var grids = programariPacientiCanvas4.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas4.Children.Remove(item);
            }
            // adauga programarile noi din ProgramariPacienti
            AdaugaZileProgramariPacienti(detaliiMedic4.lbNume.Content.ToString(), detaliiMedic4.lbPrenume.Content.ToString(),
                                           primaZiProgramariPacienti4, ultimaZiProgramariPacienti4);
        }

        private void btnInainteProgramareMedic5_Click(object sender, RoutedEventArgs e)
        {
            PutDatesOverProgramariPacientiOffset5(1);
            // sterge toate programarile 
            var grids = programariPacientiCanvas5.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas5.Children.Remove(item);
            }
            // adauga aproape toate programarile, decalate
            var temp = listaGridProgramariCanvas5.Keys.ToList();
            listaGridProgramariCanvas5.Clear();
            foreach (var item in temp)
            {
                AdaugaProgramareInProgramariPacienti(item);
            }
            // adauga programarile zilei noi din ProgramariPacienti
            AdaugaZiProgramariPacienti(detaliiMedic5.lbNume.Content.ToString(), detaliiMedic5.lbPrenume.Content.ToString(),
                                             ultimaZiProgramariPacienti5);
        }

        private void PutDatesOverProgramariPacientiOffset5(int offset)
        {
            // pentru ToShortDateString
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");

            DateTime day = DateTime.Today;
            // adauga offset
            offsetDays5 += offset;
            day = day.AddDays(offsetDays5);

            // skip Sunday
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
                // Se mai incrementeaza aici o data pentru ca trecerea peste duminica 
                // sa fie promovata si pentru urmatoarele duminici
                offsetDays5 += 1;
            }
            programariPacienti5.oZiIntreaga1.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            primaZiProgramariPacienti5 = day.ToShortDateString();

            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga2.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga3.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga4.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                            ", " + day.Day.ToString() + " " +
                                                            Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga5.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                            ", " + day.Day.ToString() + " " +
                                                            Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga6.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga7.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                                ", " + day.Day.ToString() + " " +
                                                                Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga8.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                             ", " + day.Day.ToString() + " " +
                                                             Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga9.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga10.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga11.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                               ", " + day.Day.ToString() + " " +
                                                               Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga12.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga13.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti5.oZiIntreaga14.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                             ", " + day.Day.ToString() + " " +
                                                             Constants.LunileAnului[day.Month];
            ultimaZiProgramariPacienti5 = day.ToShortDateString();
        }

        private void btnInapoiProgramareMedic5_Click(object sender, RoutedEventArgs e)
        {
            // daca este Luni, parcurgem 2 zile, peste Duminica  
            if (programariPacienti5.oZiIntreaga1.ZiLuna.Text.Substring(0, 2) == "Lu")
            {
                PutDatesOverProgramariPacientiOffset5(-2);
            }
            else
            {
                PutDatesOverProgramariPacientiOffset5(-1);
            }
            // sterge toate programarile 
            var grids = programariPacientiCanvas5.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas5.Children.Remove(item);
            }
            // adauga aproape toate programarile, decalate
            var temp = listaGridProgramariCanvas5.Keys.ToList();
            listaGridProgramariCanvas5.Clear();
            foreach (var item in temp)
            {
                AdaugaProgramareInProgramariPacienti(item);
            }
            // adauga programarile zilei noi din ProgramariPacienti
            AdaugaZiProgramariPacienti(detaliiMedic5.lbNume.Content.ToString(), detaliiMedic5.lbPrenume.Content.ToString(),
                                             primaZiProgramariPacienti5);
        }

        private void btnRepedeInainteProgramareMedic5_Click(object sender, RoutedEventArgs e)
        {
            // repede = două săptămâni
            PutDatesOverProgramariPacientiOffset5(14);
            // sterge toate programarile 
            var grids = programariPacientiCanvas5.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas5.Children.Remove(item);
            }
            // adauga programarile noi din ProgramariPacienti
            AdaugaZileProgramariPacienti(detaliiMedic5.lbNume.Content.ToString(), detaliiMedic5.lbPrenume.Content.ToString(),
                                           primaZiProgramariPacienti5, ultimaZiProgramariPacienti5);
        }

        private void btnRepedeInapoiProgramareMedic5_Click(object sender, RoutedEventArgs e)
        {
            // repede = două săptămâni
            PutDatesOverProgramariPacientiOffset5(-14);
            // sterge toate programarile 
            var grids = programariPacientiCanvas5.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas5.Children.Remove(item);
            }
            // adauga programarile noi din ProgramariPacienti
            AdaugaZileProgramariPacienti(detaliiMedic5.lbNume.Content.ToString(), detaliiMedic5.lbPrenume.Content.ToString(),
                                           primaZiProgramariPacienti5, ultimaZiProgramariPacienti5);
        }

        private void btnInainteProgramareMedic6_Click(object sender, RoutedEventArgs e)
        {
            PutDatesOverProgramariPacientiOffset6(1);
            // sterge toate programarile 
            var grids = programariPacientiCanvas6.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas6.Children.Remove(item);
            }
            // adauga aproape toate programarile, decalate
            var temp = listaGridProgramariCanvas6.Keys.ToList();
            listaGridProgramariCanvas6.Clear();
            foreach (var item in temp)
            {
                AdaugaProgramareInProgramariPacienti(item);
            }
            // adauga programarile zilei noi din ProgramariPacienti
            AdaugaZiProgramariPacienti(detaliiMedic6.lbNume.Content.ToString(), detaliiMedic6.lbPrenume.Content.ToString(),
                                             ultimaZiProgramariPacienti6);
        }

        private void PutDatesOverProgramariPacientiOffset6(int offset)
        {
            // pentru ToShortDateString
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");

            DateTime day = DateTime.Today;
            // adauga offset
            offsetDays6 += offset;
            day = day.AddDays(offsetDays6);

            // skip Sunday
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
                // Se mai incrementeaza aici o data pentru ca trecerea peste duminica 
                // sa fie promovata si pentru urmatoarele duminici
                offsetDays6 += 1;
            }
            programariPacienti6.oZiIntreaga1.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            primaZiProgramariPacienti6 = day.ToShortDateString();

            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga2.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga3.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga4.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                            ", " + day.Day.ToString() + " " +
                                                            Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga5.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                            ", " + day.Day.ToString() + " " +
                                                            Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga6.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga7.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                                ", " + day.Day.ToString() + " " +
                                                                Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga8.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                             ", " + day.Day.ToString() + " " +
                                                             Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga9.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga10.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga11.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                               ", " + day.Day.ToString() + " " +
                                                               Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga12.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga13.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                           ", " + day.Day.ToString() + " " +
                                                           Constants.LunileAnului[day.Month];
            day = day.AddDays(1);  //////////////
            if ((int)day.DayOfWeek == 0)
            {
                day = day.AddDays(1);
            }
            programariPacienti6.oZiIntreaga14.ZiLuna.Text = Constants.ZileleSaptamanii[(int)day.DayOfWeek] +
                                                             ", " + day.Day.ToString() + " " +
                                                             Constants.LunileAnului[day.Month];
            ultimaZiProgramariPacienti6 = day.ToShortDateString();
        }

        private void btnInapoiProgramareMedic6_Click(object sender, RoutedEventArgs e)
        {
            // daca este Luni, parcurgem 2 zile, peste Duminica  
            if (programariPacienti6.oZiIntreaga1.ZiLuna.Text.Substring(0, 2) == "Lu")
            {
                PutDatesOverProgramariPacientiOffset6(-2);
            }
            else
            {
                PutDatesOverProgramariPacientiOffset6(-1);
            }
            // sterge toate programarile 
            var grids = programariPacientiCanvas6.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas6.Children.Remove(item);
            }
            // adauga aproape toate programarile, decalate
            var temp = listaGridProgramariCanvas6.Keys.ToList();
            listaGridProgramariCanvas6.Clear();
            foreach (var item in temp)
            {
                AdaugaProgramareInProgramariPacienti(item);
            }
            // adauga programarile zilei noi din ProgramariPacienti
            AdaugaZiProgramariPacienti(detaliiMedic6.lbNume.Content.ToString(), detaliiMedic6.lbPrenume.Content.ToString(),
                                             primaZiProgramariPacienti6);
        }

        private void btnRepedeInainteProgramareMedic6_Click(object sender, RoutedEventArgs e)
        {
            // repede = două săptămâni
            PutDatesOverProgramariPacientiOffset6(14);
            // sterge toate programarile 
            var grids = programariPacientiCanvas6.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas6.Children.Remove(item);
            }
            // adauga programarile noi din ProgramariPacienti
            AdaugaZileProgramariPacienti(detaliiMedic6.lbNume.Content.ToString(), detaliiMedic6.lbPrenume.Content.ToString(),
                                           primaZiProgramariPacienti6, ultimaZiProgramariPacienti6);
        }

        private void btnRepedeInapoiProgramareMedic6_Click(object sender, RoutedEventArgs e)
        {
            // repede = două săptămâni
            PutDatesOverProgramariPacientiOffset6(-14);
            // sterge toate programarile 
            var grids = programariPacientiCanvas6.Children.OfType<Grid>().ToList();
            foreach (var item in grids)
            {
                programariPacientiCanvas6.Children.Remove(item);
            }
            // adauga programarile noi din ProgramariPacienti
            AdaugaZileProgramariPacienti(detaliiMedic6.lbNume.Content.ToString(), detaliiMedic6.lbPrenume.Content.ToString(),
                                           primaZiProgramariPacienti6, ultimaZiProgramariPacienti6);
        }

        private void btnIstoricPacient_Click(object sender, RoutedEventArgs e)
        {
            Pacient pacient = new Pacient();
            pacient = (Pacient)dataGridPacienti.SelectedItem;

            try
            {
                XDocument documentXmlIstorii = XDocument.Load("istorii.xml");

                var istoriiShow = documentXmlIstorii.Descendants("istorie");
                var istorii = from p in istoriiShow
                              where p.Element("pacient").Value == (pacient.Nume + " " + pacient.Prenume)
                              select new Istorie()
                              {
                                  Medic = p.Descendants("medic").First().Value,
                                  Data = p.Descendants("data").First().Value,
                                  Descriere = p.Descendants("descriere").First().Value,
                              };
                // Daca pacientul nu are istoric, nu afisa fereastra de istoric, ci doar un mesaj de informare
                if (istorii.Count() == 0)
                {
                    string str = string.Format("Pacientul {0} {1} nu are istoric!", pacient.Nume, pacient.Prenume);
                    MessageBoxCustom.Show(str, "Istoric lipsă");
                    return;
                }

                IstoricPacientWindow istoricPacientWindow = new IstoricPacientWindow();
                this.SendNumePrenumeCallback += new Action<string, string>(istoricPacientWindow.SendPacientFunc);
                SendNumePrenumeCallback(pacient.Nume, pacient.Prenume);
                istoricPacientWindow.ShowDialog();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Fișierul istorii.xml lipsește!", "Fișier inexistent");
            }
        }

        private void dataGridPacienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (btnModifica.IsEnabled == false)
            {
                btnModifica.IsEnabled = true;
                btnSterge.IsEnabled = true;
                btnIstoricPacient.IsEnabled = true;
            }
        }

        private void btnCautaPacientPlata_Click(object sender, RoutedEventArgs e)
        {
            CautaPacientPlataWindow cautaPacientPlataWindow = new CautaPacientPlataWindow();
            cautaPacientPlataWindow.SendPacientToMainCallback += new Action<Pacient>(CautaPacientPlataFunc); 
            cautaPacientPlataWindow.ShowDialog(); 
        }

        private void CautaPacientPlataFunc(Pacient pacient)
        {
            btnAdaugaPlata.IsEnabled = true;
            btnModificaPacient.IsEnabled = true;

            // detalii pacient
            tbInformatii.Text = "Informații pacient " + pacient.Nume + " " + pacient.Prenume;
            tbSituatie.Text = "Situație plăți " + pacient.Nume + " " + pacient.Prenume;

            tbNumarFisa.Text = pacient.NumarFisa;
            tbMedic.Text = pacient.Medic;
            tbNume.Text = pacient.Nume;
            tbPrenume.Text = pacient.Prenume;
            tbCnp.Text = pacient.Cnp;
            tbSeriaCi.Text = pacient.SerieCi;
            tbNumarCi.Text = pacient.NumarCi;
            tbVarsta.Text = pacient.Varsta;
            tbSex.Text = pacient.Sex;
            tbTelefon.Text = pacient.Telefon;
            tbEmail.Text = pacient.Email;
            tbObservatii.Text = pacient.Observatii;

            // detalii ultima plata
            tbMedic2.Text = "";
            tbTotal.Text = "";
            tbTransa.Text = "";
            tbRest.Text = "0";
            tbData.Text = "";
            tbDescriere.Text = "";
            try
            {
                XDocument documentXmlPlati = XDocument.Load("plati.xml");
                var platiAll = documentXmlPlati.Descendants("plata");
                var plati = from p in platiAll
                            where p.Element("pacient").Value == pacient.Nume + " " + pacient.Prenume
                            where int.Parse(p.Element("rest").Value) > 0
                            select new Plata()
                               {
                                   //NumePrenumePacient = p.Descendants("pacient").First().Value,
                                   Medic = p.Descendants("medic").First().Value,
                                   Total = p.Descendants("total").First().Value,
                                   Transa  = p.Descendants("transa").First().Value,
                                   Rest = p.Descendants("rest").First().Value,
                                   Data = p.Descendants("data").First().Value,
                                   Descriere = p.Descendants("descriere").First().Value,
                               };
                int nrPlati = documentXmlPlati.Root.Elements("plata")
                                                   .Where( x => x.Element("pacient").Value == (pacient.Nume + " " + pacient.Prenume)).Count();
                foreach (var item in plati)
                {
                    tbMedic2.Text = item.Medic;
                    tbTotal.Text = item.Total;
                    tbTransa.Text = item.Transa;
                    tbRest.Text = item.Rest;
                    tbData.Text = item.Data;
                    tbDescriere.Text=item.Descriere; 
                }
                if (plati.Count() != 0)
                {
                    btnAdaugaPlataDoi.IsEnabled = true;
                    btnModificaPlata.IsEnabled = true;
                    btnIstoricPlati.IsEnabled = true;
                }
                if (nrPlati != 0)
                {
                    if (btnIstoricPlati.IsEnabled == false)
                    {
                        btnIstoricPlati.IsEnabled = true;
                    }
                }
                else // (nrPlati == 0)
                {
                    btnAdaugaPlataDoi.IsEnabled = false;
                    btnModificaPlata.IsEnabled = false;
                    btnIstoricPlati.IsEnabled = false;
                }
            }
            catch (FileNotFoundException)
            {
                //MessageBox.Show("Fișierul plati.xml lipsește!", "Fișier inexistent");
            }
        }

        private void btnAdaugaPlata_Click(object sender, RoutedEventArgs e)
        {
            AdaugaPlataWindow adaugaPlataWindow = new AdaugaPlataWindow();
            this.SendNumePrenumeMedicCallback += new Action<string, string, string>(adaugaPlataWindow.AdaugaPlataFunc);
            adaugaPlataWindow.SendPlataToMainWindowCallback += new Action<Plata>(ReceivePlataFunc);
            SendNumePrenumeMedicCallback(tbNume.Text, tbPrenume.Text, tbMedic.Text);
            adaugaPlataWindow.ShowDialog();
        }

        private void ReceivePlataFunc(Plata plata)
        {
            tbMedic2.Text = plata.Medic;
            tbTotal.Text = plata.Total;
            tbTransa.Text = plata.Transa;
            tbRest.Text = plata.Rest;
            tbData.Text = plata.Data;
            tbDescriere.Text = plata.Descriere;

            btnModificaPlata.IsEnabled = true;
            btnIstoricPlati.IsEnabled = true;

            if (plata.Rest != "0")
            {
                if (btnAdaugaPlataDoi.IsEnabled==false)
                {
                    btnAdaugaPlataDoi.IsEnabled = true;  
                }
            }
        }

        private void btnAdaugaPlataDoi_Click(object sender, RoutedEventArgs e)
        {
            AdaugaPlataDoiWindow adaugaPlataDoiWindow = new AdaugaPlataDoiWindow();
            this.SendNumePrenumeMedicCallback += new Action<string, string, string>(adaugaPlataDoiWindow.AdaugaPlataFunc);
            adaugaPlataDoiWindow.SendPlataToMainWindowCallback += new Action<Plata>(ReceivePlataFunc);
            SendNumePrenumeMedicCallback(tbNume.Text, tbPrenume.Text, tbMedic.Text);
            adaugaPlataDoiWindow.ShowDialog();
        }

        private void btnModificaPlata_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxCustom.Show("Folosiți această funcție doar pentru a corecta datele introduse!", "Avertisment", MessageBoxButton.OK,
                                  System.Windows.Forms.MessageBoxIcon.Warning);
            
            ModificaPlataWindow modificaPlataWindow = new ModificaPlataWindow();
            this.SendPlataLaModificatCallback += new Action<Plata>(modificaPlataWindow.AdaugaPlataFunc);
            modificaPlataWindow.SendPlataToMainWindowCallback += new Action<Plata>(ReceivePlataFunc);
            SendPlataLaModificatCallback(new Plata
                                                {
                                                    NumePrenumePacient = tbNume.Text + " " + tbPrenume.Text,
                                                    Medic = tbMedic2.Text,
                                                    Total = tbTotal.Text,
                                                    Transa = tbTransa.Text,
                                                    Rest = tbRest.Text,
                                                    Data = tbData.Text,
                                                    Descriere = tbDescriere.Text
                                                });
            modificaPlataWindow.ShowDialog();
        }

        private void btnIstoricPlati_Click(object sender, RoutedEventArgs e)
        {

            IstoricPlatiWindow istoricPlatiWindow = new IstoricPlatiWindow();
            this.SendNumePrenumeCallback += new Action<string, string>(istoricPlatiWindow.SendNumePrenumeFunc);
            SendNumePrenumeCallback(tbNume.Text, tbPrenume.Text);
            istoricPlatiWindow.ShowDialog();
        }

        private void btnModificaPacient_Click(object sender, RoutedEventArgs e)
        {
            ModificaPacientWindow modificaPacientWindow = new ModificaPacientWindow();
            pacientDeTrimisLaModificat = new Pacient() { NumarFisa = tbNumarFisa.Text, Medic = tbMedic.Text, Nume = tbNume.Text, Prenume = tbPrenume.Text,
                                                         Cnp = tbCnp.Text, SerieCi = tbSeriaCi.Text, NumarCi = tbNumarCi.Text, Varsta = tbVarsta.Text,
                                                         Sex = tbSex.Text, Telefon = tbTelefon.Text, Email = tbEmail.Text, Observatii = tbObservatii.Text, };
            for (int i = 0; i < listaPacienti.Count; i++)
            {
                if (listaPacienti[i] == pacientDeTrimisLaModificat)
                {
                    indiceDeTrimisLaModificat = i;
                    modificaPacientPlata = true;
                    break;
                }
            }
            this.SendPacientLaModificatCallback += new Action<Pacient>(modificaPacientWindow.SendPacientFunc);
            modificaPacientWindow.SendPacientModificatPlataCallback += new Action<Pacient>(this.ModificaPacientInDataGridPlata);
            SendPacientLaModificatCallback(pacientDeTrimisLaModificat);
            modificaPacientWindow.ShowDialog();
        }

        private void ModificaPacientInDataGridPlata(Pacient pacient)
        {
            if (modificaPacientPlata == true)
            {
                listaPacienti[indiceDeTrimisLaModificat] = pacient; 
            }

            tbNumarFisa.Text = pacient.NumarFisa;
            tbMedic.Text = pacient.Medic;
            tbNume.Text = pacient.Nume;
            tbPrenume.Text = pacient.Prenume;
            tbCnp.Text = pacient.Cnp;
            tbSeriaCi.Text = pacient.SerieCi;
            tbNumarCi.Text = pacient.NumarCi;
            tbVarsta.Text = pacient.Varsta;
            tbSex.Text = pacient.Sex; 
            tbTelefon.Text = pacient.Telefon;
            tbEmail.Text = pacient.Email;
            tbObservatii.Text = pacient.Observatii;
        }
    }
}