using System;
using System.Collections.Generic;
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
    /// Interaction logic for ModificaPacientWindow.xaml
    /// </summary>
    public partial class ModificaPacientWindow : Window
    {
        public Pacient pacientPrimit = new Pacient();
        public Pacient pacientModificatDeTrimisInapoi = new Pacient();

        public Action<Pacient> SendPacientModificatCallback;

        public Action<Pacient> SendPacientModificatPlataCallback { get; internal set; }

        public ModificaPacientWindow()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            // adauga medici in cbMedic
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

        public void SendPacientFunc(Pacient pacient)
        {
            pacientPrimit = pacient;

            tbNumarFisa.Text = pacientPrimit.NumarFisa;
            cbMedic.Text = pacient.Medic;
            tbNume.Text = pacientPrimit.Nume;
            tbPrenume.Text = pacientPrimit.Prenume;
            tbCnp.Text = pacientPrimit.Cnp;
            tbSeriaCi.Text = pacientPrimit.SerieCi;
            tbNumarCi.Text = pacientPrimit.NumarCi;
            tbVarsta.Text = pacientPrimit.Varsta;
            if (pacientPrimit.Sex !=  String.Empty)
            {
                if (pacientPrimit.Sex == "M")
                {
                    rbMasculin.IsChecked = true;
                }
                else
                {
                    if (pacientPrimit.Sex == "F")
                    {
                        rbFeminin.IsChecked = true;
                    } 
                }
            }
            tbTelefon.Text = pacientPrimit.Telefon;
            tbEmail.Text = pacientPrimit.Email;
            tbObservatii.Text = pacientPrimit.Observatii;
        }

        private void btnModificaPacient_Click(object sender, RoutedEventArgs e)
        {
            ModificaPacientInXml();
            TrimitePacientModificatInapoi();

            string str =
            string.Format("Pacientul {0} {1} a fost modificat cu succes!",
              pacientModificatDeTrimisInapoi.Nume, pacientModificatDeTrimisInapoi.Prenume);
            
            MessageBoxCustom.Show(str, "Pacient modificat");

            this.Close();
        }

        private void TrimitePacientModificatInapoi()
        {
            // trimite pacient modificat inapoi
           
            pacientModificatDeTrimisInapoi.NumarFisa = tbNumarFisa.Text;
            pacientModificatDeTrimisInapoi.Medic = cbMedic.Text;
            pacientModificatDeTrimisInapoi.Nume = EliminateBeginEndSpaces(tbNume.Text);
            pacientModificatDeTrimisInapoi.Prenume = EliminateBeginEndSpaces(tbPrenume.Text);
            pacientModificatDeTrimisInapoi.Cnp = tbCnp.Text;
            pacientModificatDeTrimisInapoi.SerieCi = tbSeriaCi.Text;
            pacientModificatDeTrimisInapoi.NumarCi = tbNumarCi.Text;
            pacientModificatDeTrimisInapoi.Varsta = tbVarsta.Text;
            if (rbMasculin.IsChecked.Value)
            {
                pacientModificatDeTrimisInapoi.Sex = "M";
            }
            else if (rbFeminin.IsChecked.Value)
            {
                pacientModificatDeTrimisInapoi.Sex = "F";
            }
            pacientModificatDeTrimisInapoi.Telefon = tbTelefon.Text;
            pacientModificatDeTrimisInapoi.Email = tbEmail.Text;
            pacientModificatDeTrimisInapoi.Observatii = tbObservatii.Text;

            if (SendPacientModificatCallback != null)
            {
                SendPacientModificatCallback(pacientModificatDeTrimisInapoi); 
            }

            if (SendPacientModificatPlataCallback != null)
            {
                SendPacientModificatPlataCallback(pacientModificatDeTrimisInapoi);
            }
        }

        private void ModificaPacientInXml()
        {
            // modifica fisier .xml 
            XDocument documentXmlPacienti = XDocument.Load("pacienti.xml");
            var pacientDeModificat = from pacient in documentXmlPacienti.Root.Elements("pacient")
                                     where pacient.Element("numarfisa").Value == pacientPrimit.NumarFisa
                                     where pacient.Element("nume").Value == pacientPrimit.Nume
                                     where pacient.Element("prenume").Value == pacientPrimit.Prenume
                                     where pacient.Element("cnp").Value == pacientPrimit.Cnp
                                     select pacient;
            foreach (XElement pacient in pacientDeModificat)
            {
                pacient.SetElementValue("numarfisa", tbNumarFisa.Text);
                pacient.SetElementValue("medic", cbMedic.Text);
                pacient.SetElementValue("nume", EliminateBeginEndSpaces(tbNume.Text));
                pacient.SetElementValue("prenume", EliminateBeginEndSpaces(tbPrenume.Text));
                pacient.SetElementValue("cnp", tbCnp.Text);
                pacient.SetElementValue("serieci", tbSeriaCi.Text);
                pacient.SetElementValue("numarci", tbNumarCi.Text);
                pacient.SetElementValue("varsta", tbVarsta.Text);
                if (rbMasculin.IsChecked.Value)
                    pacient.SetElementValue("sex", "M");
                else if (rbFeminin.IsChecked.Value)
                    pacient.SetElementValue("sex", "F");
                pacient.SetElementValue("telefon", tbTelefon.Text);
                pacient.SetElementValue("email", tbEmail.Text);
                pacient.SetElementValue("observatii", tbObservatii.Text);
            }
            documentXmlPacienti.Save("pacienti.xml");
        }

        private string EliminateBeginEndSpaces(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            char[] letters = s.ToCharArray();
            if ((letters[0]!=' ') && (letters[letters.Length-1]!=' '))
            {
                return s;
            }
            else
            {
                if ((letters[0] == ' ') && (letters[letters.Length - 1] == ' '))
                {
                    return s.Substring(1, s.Length - 2);
                }
                else
                {
                    if (letters[letters.Length - 1] == ' ')
                    {
                        return s.Substring(0, s.Length - 1);
                    }
                    else
                    {
                        if (letters[0] == ' ')
                        {
                            return s.Substring(1, s.Length - 1);
                        }
                        else
                        {
                            return null;
                        }
                    }            
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
    }
}
