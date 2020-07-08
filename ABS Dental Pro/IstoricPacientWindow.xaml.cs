using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ABS_Dental_Pro
{
    /// <summary>
    /// Interaction logic for IstoricPacientWindow.xaml
    /// </summary>
    public partial class IstoricPacientWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Istorie> listaIstorii = new ObservableCollection<Istorie>();
        Istorie istorieOld = new Istorie();
        int indiceLaModificatDescrierea;

        public string NumePacient;
        public string PrenumePacient;

        public Action<string, Istorie> SendDescriereLaModificatCallback;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Istorie> listaIstoriiProp
        {
            get { return this.listaIstorii; }
            set
            {
                if (listaIstorii != value)
                {
                    listaIstorii = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IstoricPacientWindow()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
        }

        private void btnInchide_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            istorieOld = (Istorie)dataGridIstorii.SelectedItem;
            indiceLaModificatDescrierea = dataGridIstorii.SelectedIndex;

            try
            {
                XDocument documentXmlIstorii = XDocument.Load("istorii.xml");

                var istoriiShow = documentXmlIstorii.Descendants("istorie");
                var istorieVeche = from p in istoriiShow
                                   where p.Element("pacient").Value == (NumePacient + " " + PrenumePacient)
                                   where p.Element("medic").Value == istorieOld.Medic
                                   where p.Element("data").Value == istorieOld.Data
                                   where p.Element("descriere").Value == istorieOld.Descriere
                                   select new Istorie()
                                    {
                                        Medic = p.Element("medic").Value,
                                        Data = p.Element("data").Value,
                                        Descriere = p.Element("descriere").Value,
                                    };
                if (istorieVeche.Count()==1)
                {
                    ModificaDescriereWindow modificaDescriereWindow = new ModificaDescriereWindow();
                    this.SendDescriereLaModificatCallback += new Action<string, Istorie>(modificaDescriereWindow.SendDescriereLaModificatFunc);
                    SendDescriereLaModificatCallback((NumePacient + " " + PrenumePacient), ((Istorie[])istorieVeche.ToArray()).First());
                    modificaDescriereWindow.SendDescriereModificataLaIstoricCallback += new Action<string>(this.SendDescriereModificataLaIstoricFunc); 
                    modificaDescriereWindow.ShowDialog();
                }
                else
                {
                    MessageBoxCustom.Show("Există mai multe înregistrări identice în fișierul istorii.xml; modificare eșuată.");
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Fișierul istorii.xml lipsește!", "Fișier inexistent");
            } 
        }

        private void SendDescriereModificataLaIstoricFunc(string descriereNoua)
        {
            listaIstorii[indiceLaModificatDescrierea] = new Istorie() { Medic = istorieOld.Medic, Data = istorieOld.Data, Descriere = descriereNoua };
            ModificaIstorieInXml();
        }

        private void ModificaIstorieInXml()
        {
            // modifica fisier .xml 
            XDocument documentXmlIstorii = XDocument.Load("istorii.xml");
            var istorieVecheDeModificat = from p in documentXmlIstorii.Root.Elements("istorie")
                                          where p.Element("pacient").Value == (NumePacient + " " + PrenumePacient)
                                          where p.Element("medic").Value == istorieOld.Medic
                                          where p.Element("data").Value == istorieOld.Data
                                          where p.Element("descriere").Value == istorieOld.Descriere                
                                          select p;
            foreach (XElement istorie in istorieVecheDeModificat)
            {
                istorie.SetElementValue("descriere", listaIstorii[indiceLaModificatDescrierea].Descriere);
            }
            documentXmlIstorii.Save("istorii.xml");
        }

        internal void SendPacientFunc(string nume, string prenume)
        {
            NumePacient = nume;
            PrenumePacient = prenume;
            this.Title += " " + nume + " " + prenume;

            ArataIstoricPacient(nume, prenume);
        }

        private void ArataIstoricPacient(string nume, string prenume)
        {
            try
            {
                XDocument documentXmlIstorii = XDocument.Load("istorii.xml");

                var istoriiShow = documentXmlIstorii.Descendants("istorie");
                var istorii = from p in istoriiShow
                              where p.Element("pacient").Value == (nume + " " + prenume)
                              select new Istorie()
                              {
                                  Medic = p.Descendants("medic").First().Value,
                                  Data = p.Descendants("data").First().Value,
                                  Descriere = p.Descendants("descriere").First().Value,
                              };
                foreach (var item in istorii)
                {
                    listaIstorii.Insert(0, item);
                }
            }
            catch (FileNotFoundException)
            {
                MessageBoxCustom.Show("Fișierul istorii.xml lipsește!", "Fișier inexistent");
            }
        }

        private void dataGridIstorii_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (btnModifica.IsEnabled==false)
            {
                btnModifica.IsEnabled = true; 
            }
        }
    }
}
