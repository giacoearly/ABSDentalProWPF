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
    /// Interaction logic for StergePacientWindow.xaml
    /// </summary>
    public partial class CautaPacientWindow : Window
    {
        public ObservableCollection<Pacient> listaPacienti = new ObservableCollection<Pacient>();
        public event PropertyChangedEventHandler PropertyChanged;
        public Action<Pacient> SendPacientCallback;
        public Action<Pacient, Pacient> SendPacientModificatToMainCallback;
        public Action<Pacient> SendPacientStersToMainCallback;
        public Action<string, string> SendNumePrenumeCallback;

        Pacient pacientDeTrimis = new Pacient();
        int indiceDeTrimis;
            
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

        public CautaPacientWindow()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            tbCauta.Focus();
        }

        private void btnCauta_Click(object sender, RoutedEventArgs e)
        {
            if (tbCauta.Text == String.Empty)
            {
                return;
            }

            listaPacienti.Clear(); // for multiple searches, clear DataGrid first

            try
            {
                XDocument documentXmlPacienti = XDocument.Load("pacienti.xml");

                if (rbNumarFisa.IsChecked.Value)
                {

                    var pacientiCautati = from p in documentXmlPacienti.Root.Elements("pacient")
                                          where p.Element("numarfisa").Value.Contains(tbCauta.Text)
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
                }
                else if (rbNumePrenume.IsChecked.Value)
                {

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
                }
                else // if (rbCnp.IsChecked.Value)
                {

                    var pacientiCautati = from p in documentXmlPacienti.Root.Elements("pacient")
                                          where p.Element("cnp").Value.Contains(tbCauta.Text)
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
                }

                if (btnModifica.IsEnabled == true)
                {
                    btnModifica.IsEnabled = false;
                    btnSterge.IsEnabled = false;
                    btnIstoricPacient.IsEnabled = false;
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

                // sterge pacient din main, daca exista 
                SendPacientStersToMainCallback(pacientDeSters);
            }
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            ModificaPacientWindow modificaPacientWindow = new ModificaPacientWindow();
            pacientDeTrimis = (Pacient)dataGridPacienti.SelectedItem;
            indiceDeTrimis = dataGridPacienti.SelectedIndex;
            this.SendPacientCallback += new Action<Pacient>(modificaPacientWindow.SendPacientFunc);
            modificaPacientWindow.SendPacientModificatCallback +=
                new Action<Pacient>(this.ModificaPacientInCautaDataGrid);
            SendPacientCallback(pacientDeTrimis);
            modificaPacientWindow.ShowDialog();
        }

        private void ModificaPacientInCautaDataGrid(Pacient pacientModificat)
        {
            SendPacientModificatToMainCallback(pacientDeTrimis, pacientModificat);
            listaPacienti[indiceDeTrimis] = pacientModificat;

            if (btnModifica.IsEnabled == true)
            {
                btnModifica.IsEnabled = false;
                btnSterge.IsEnabled = false;
                btnIstoricPacient.IsEnabled = false; 
            }
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
    }
}
