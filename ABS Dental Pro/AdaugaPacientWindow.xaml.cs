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
using System.IO;

namespace ABS_Dental_Pro
{
    /// <summary>
    /// Interaction logic for AdaugaPacientWindow.xaml
    /// </summary>
    public partial class AdaugaPacientWindow : Window
    {
        public Pacient pacientDeAdaugat = new Pacient();

        public delegate void AdaugaPacientDelegate(Pacient value);
        public AdaugaPacientDelegate AdaugaPacientCallback;
        public Action<Pacient> SendPacientToAdaugaProgramareWindowCallback;
        public bool adaugaPacientDinProgramari = false;

        public AdaugaPacientWindow()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            try
            {
                XDocument documentXmlPacienti = XDocument.Load("pacienti.xml");
                int numarPacienti = documentXmlPacienti.Descendants("pacient").Count();
                tbNumarFisa.Text = (numarPacienti + 1).ToString();
            }
            catch (FileNotFoundException )
            {
                int numarPacienti = 0;
                tbNumarFisa.Text = (numarPacienti + 1).ToString();
            }
            tbNume.Focus();
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
        } 

        private void tbCnp_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            long cnp;
            
            if ((tbCnp.Text != null) && (long.TryParse(tbCnp.Text, out cnp)))
            {
                if (tbCnp.Text.Length != 13)
                {
                    MessageBoxCustom.Show("Numărul CNP nu are dimensiunea corectă!", "CNP Incorect");
                    tbCnp.Foreground = Brushes.Red;
                }
                else
                {
                    // CNP valid, extrage varsta si sex
                    string dn = tbCnp.Text.Substring(0, 7);
                    string S = dn.Substring(0, 1);
                    string an = dn.Substring(1, 2);
                    string luna = dn.Substring(3, 2);
                    string zi = dn.Substring(5, 2);

                    int SInt, ziInt, lunaInt, anInt, anPatruCifre;
                    int.TryParse(S, out SInt);
                    int.TryParse(zi, out ziInt);
                    int.TryParse(luna, out lunaInt);
                    int.TryParse(an, out anInt);

                    if (anInt > (DateTime.Now.Year % 100))
                        anPatruCifre = anInt + 1900;
                    else
                        anPatruCifre = anInt + 2000;
                    tbVarsta.Text = (DateTime.Now.Year - anPatruCifre).ToString();


                    //determina sex
                    if ((SInt == 1) || (SInt == 5) || (SInt == 3) || (SInt == 7))
                        rbMasculin.IsChecked = true;
                    else
                    if ((SInt == 2) || (SInt == 6) || (SInt == 4) || (SInt == 8))
                        rbFeminin.IsChecked = true;
                }
                 
            }
            else
            {
                MessageBoxCustom.Show("Numărul CNP nu este număr natural!", "CNP Incorect");
                tbCnp.Foreground = Brushes.Red;
            }
        }

        internal void BoolPacientDinAdaugaProgramareWindow(bool pacientNou)
        {
            adaugaPacientDinProgramari = pacientNou;
        }

        private void tbCnp_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            tbCnp.Foreground = Brushes.Black;
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            pacientDeAdaugat.NumarFisa = tbNumarFisa.Text;
            pacientDeAdaugat.Medic = cbMedic.Text;
            pacientDeAdaugat.Nume = EliminateBeginEndSpaces(tbNume.Text);
            pacientDeAdaugat.Prenume = EliminateBeginEndSpaces(tbPrenume.Text);
            pacientDeAdaugat.Cnp = tbCnp.Text;
            pacientDeAdaugat.SerieCi = tbSeriaCi.Text;
            pacientDeAdaugat.NumarCi = tbNumarCi.Text;
            pacientDeAdaugat.Varsta = tbVarsta.Text;
            if (rbMasculin.IsChecked.Value)
            {
                pacientDeAdaugat.Sex = "M";
            }
            else if (rbFeminin.IsChecked.Value)
            {
                pacientDeAdaugat.Sex = "F";
            }
            pacientDeAdaugat.Telefon = tbTelefon.Text;
            pacientDeAdaugat.Email = tbEmail.Text;
            pacientDeAdaugat.Observatii = tbObservatii.Text;

            if (!adaugaPacientDinProgramari)
            {
                AdaugaPacientCallback(pacientDeAdaugat); 
            }
            if (adaugaPacientDinProgramari)
            {
                SendPacientToAdaugaProgramareWindowCallback(pacientDeAdaugat); 
            }

            string pacientAdaugatCuSucces =
                string.Format("Pacientul {0} {1} a fost adăugat cu succes!",
                pacientDeAdaugat.Nume, pacientDeAdaugat.Prenume);

            MessageBoxCustom.Show(pacientAdaugatCuSucces, "Pacient adăugat");  

            this.Close();
        }

        private string EliminateBeginEndSpaces(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            char[] letters = s.ToCharArray();
            if ((letters[0] != ' ') && (letters[letters.Length - 1] != ' '))
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
                  (key == Key.Back) || (key == Key.Delete) || (key == Key.Left) || (key == Key.Right)))
            {
                e.Handled = true;
            }
        }
    }
}