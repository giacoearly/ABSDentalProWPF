using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for IstoricPlatiWindow.xaml
    /// </summary>
    public partial class IstoricPlatiWindow : Window, INotifyPropertyChanged
    {
        int suma = 0;
        public ObservableCollection<Plata> listaIstorii = new ObservableCollection<Plata>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Plata> listaIstoriiProp
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

        public IstoricPlatiWindow()
        {
            InitializeComponent();
            this.Owner = System.Windows.Application.Current.MainWindow;
        }

        internal void SendNumePrenumeFunc(string nume, string prenume)
        {
            //tbPentru.Text += " " + nume + " " + prenume;
            this.Title += " " + nume + " " + prenume;

            ArataIstoricPlati(nume, prenume);
        }

        private void ArataIstoricPlati(string nume, string prenume)
        {
            try
            {
                XDocument documentXmlIstorii = XDocument.Load("transe.xml");

                var istoriiShow = documentXmlIstorii.Descendants("plata");
                var istorii = from p in istoriiShow
                              where p.Element("pacient").Value == (nume + " " + prenume)
                              select new Plata()
                              {
                                  Medic = p.Descendants("medic").First().Value,
                                  Transa = p.Descendants("transa").First().Value,
                                  Data = p.Descendants("data").First().Value,
                                  Descriere = p.Descendants("descriere").First().Value,
                              };
                foreach (var item in istorii)
                {
                    listaIstoriiProp.Insert(0, item);
                    suma += Convert.ToInt32(item.Transa);
                }
               tbTotal.Text += " " + suma.ToString() + " lei";
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBoxCustom.Show("Fișierul transe.xml lipsește!", "Fișier inexistent");
            }
        }
    }
}
