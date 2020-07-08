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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ABS_Dental_Pro
{
    /// <summary>
    /// Interaction logic for DetaliiMedic.xaml
    /// </summary>
    public partial class DetaliiMedic : UserControl
    {
        Medic medicDeTrimis = new Medic();
        public Action<Medic> SendMedicLaModificatCallback;

        public DetaliiMedic()
        {
            InitializeComponent();
        }

        private void btnSterge_Click(object sender, RoutedEventArgs e)
        {
            bool? stergeMedic;

            string stergeMedicText = string.Format("Sigur vreți să ștergeți medicul {0} {1}?",
                                               lbNume.Content.ToString(), lbPrenume.Content.ToString());
            stergeMedic = MessageBoxCustom.Show(stergeMedicText, "Ștergere medic",
                                                  MessageBoxButton.YesNo);


            if ((bool)stergeMedic)
            {
                // sterge medic din .xml
                XDocument documentXmlMedici = XDocument.Load("medici.xml");
                documentXmlMedici.Root.Elements("medic")
                  .Where(x => x.Element("id").Value == lbID.Content.ToString())
                  .Where(x => x.Element("nume").Value == lbNume.Content.ToString())
                  .Where(x => x.Element("prenume").Value == lbPrenume.Content.ToString()).Remove();

                documentXmlMedici.Save("medici.xml");

                // sterge medic din GUI
                this.IsEnabled = false;

                this.lbID.Content = "";
                this.lbNume.Content = "";
                this.lbPrenume.Content = "";
                this.lbTelefon.Content = "";
                this.lbEmail.Content = "";
                this.lbObservatii.Content = "";
                this.lbLuni1.Content = "";
                this.lbLuni2.Content = "";
                this.lbLuni3.Content = "";
                this.lbLuni4.Content = "";
                this.lbMarti1.Content = "";
                this.lbMarti2.Content = "";
                this.lbMarti3.Content = "";
                this.lbMarti4.Content = "";
                this.lbMiercuri1.Content = "";
                this.lbMiercuri2.Content = "";
                this.lbMiercuri3.Content = "";
                this.lbMiercuri4.Content = "";
                this.lbJoi1.Content = "";
                this.lbJoi2.Content = "";
                this.lbJoi3.Content = "";
                this.lbJoi4.Content = "";
                this.lbVineri1.Content = "";
                this.lbVineri2.Content = "";
                this.lbVineri3.Content = "";
                this.lbVineri4.Content = "";
                this.lbSambata1.Content = "";
                this.lbSambata2.Content = "";
                this.lbSambata3.Content = "";
                this.lbSambata4.Content = ""; 
            }
        }

        public static void TraverseVisualTree(Visual myMainWindow)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(myMainWindow);
            for (int i = 0; i < childrenCount; i++)
            {
                var visualChild = (Visual)VisualTreeHelper.GetChild(myMainWindow, i);
                if (visualChild is Label)
                {
                    Label lb = (Label)visualChild;
                    lb.Content = "";
                }
                TraverseVisualTree(visualChild);
            }
        }

        private void btnModifica_Click(object sender, RoutedEventArgs e)
        {
            ModificaMedicWindow modificaMedicWindow = new ModificaMedicWindow();
            UmpleMedicDeTrimis();
            this.SendMedicLaModificatCallback += new Action<Medic>(modificaMedicWindow.SendMedicFunc);
            modificaMedicWindow.SendMedicModificatCallback +=
                new Action<Medic>(this.ModificaMedicInDetaliiMedic);
            SendMedicLaModificatCallback(medicDeTrimis);
            modificaMedicWindow.ShowDialog();
        }

        private void ModificaMedicInDetaliiMedic(Medic medic)
        {
           lbID.Content = (medic.ID ?? "").ToString();
           lbNume.Content = (medic.Nume ?? "").ToString();
           lbPrenume.Content = (medic.Prenume ?? "").ToString();
           lbTelefon.Content = (medic.Telefon ?? "").ToString();
           lbEmail.Content = (medic.Email ?? "").ToString();
           lbObservatii.Content = (medic.Observatii ?? "").ToString();
           lbLuni1.Content = (medic.Luni.DeLa1 ?? "").ToString();
           lbLuni2.Content = (medic.Luni.PanaLa1 ?? "").ToString();
           lbLuni3.Content = (medic.Luni.DeLa2 ?? "").ToString();
           lbLuni4.Content = (medic.Luni.PanaLa2 ?? "").ToString();
           lbMarti1.Content = (medic.Marti.DeLa1 ?? "").ToString();
           lbMarti2.Content = (medic.Marti.PanaLa1 ?? "").ToString();
           lbMarti3.Content = (medic.Marti.DeLa2 ?? "").ToString();
           lbMarti4.Content = (medic.Marti.PanaLa2 ?? "").ToString();
           lbMiercuri1.Content = (medic.Miercuri.DeLa1 ?? "").ToString();
           lbMiercuri2.Content = (medic.Miercuri.PanaLa1 ?? "").ToString();
           lbMiercuri3.Content = (medic.Miercuri.DeLa2 ?? "").ToString();
           lbMiercuri4.Content = (medic.Miercuri.PanaLa2 ?? "").ToString();
           lbJoi1.Content = (medic.Joi.DeLa1 ?? "").ToString();
           lbJoi2.Content = (medic.Joi.PanaLa1 ?? "").ToString();
           lbJoi3.Content = (medic.Joi.DeLa2 ?? "").ToString();
           lbJoi4.Content = (medic.Joi.PanaLa2 ?? "").ToString();
           lbVineri1.Content = (medic.Vineri.DeLa1 ?? "").ToString();
           lbVineri2.Content = (medic.Vineri.PanaLa1 ?? "").ToString();
           lbVineri3.Content = (medic.Vineri.DeLa2 ?? "").ToString();
           lbVineri4.Content = (medic.Vineri.PanaLa2 ?? "").ToString();
           lbSambata1.Content = (medic.Sambata.DeLa1 ?? "").ToString();
           lbSambata2.Content = (medic.Sambata.PanaLa1 ?? "").ToString();
           lbSambata3.Content = (medic.Sambata.DeLa2 ?? "").ToString();
           lbSambata4.Content = (medic.Sambata.PanaLa2 ?? "").ToString();
        }

        private void UmpleMedicDeTrimis()
        {
            medicDeTrimis.ID = (lbID.Content ?? "").ToString();
            medicDeTrimis.Nume = (lbNume.Content ?? "").ToString();
            medicDeTrimis.Prenume = (lbPrenume.Content ?? "").ToString();
            medicDeTrimis.Telefon = (lbTelefon.Content ?? "").ToString();
            medicDeTrimis.Email = (lbEmail.Content ?? "").ToString();
            medicDeTrimis.Observatii = (lbObservatii.Content ?? "").ToString();
            medicDeTrimis.Luni = new Zi((lbLuni1.Content ?? "").ToString(), (lbLuni2.Content ?? "").ToString(),
                (lbLuni3.Content ?? "").ToString(), (lbLuni4.Content ?? "").ToString());
            medicDeTrimis.Marti = new Zi((lbMarti1.Content ?? "").ToString(), (lbMarti2.Content ?? "").ToString(),
                (lbMarti3.Content ?? "").ToString(), (lbMarti4.Content ?? "").ToString());
            medicDeTrimis.Miercuri = new Zi((lbMiercuri1.Content ?? "").ToString(), (lbMiercuri2.Content ?? "").ToString(),
                (lbMiercuri3.Content ?? "").ToString(), (lbMiercuri4.Content ?? "").ToString());
            medicDeTrimis.Joi = new Zi((lbJoi1.Content ?? "").ToString(), (lbJoi2.Content ?? "").ToString(),
                (lbJoi3.Content ?? "").ToString(), (lbJoi4.Content ?? "").ToString());
            medicDeTrimis.Vineri = new Zi((lbVineri1.Content ?? "").ToString(), (lbVineri2.Content ?? "").ToString(),
                (lbVineri3.Content ?? "").ToString(), (lbVineri4.Content ?? "").ToString());
            medicDeTrimis.Sambata = new Zi((lbSambata1.Content ?? "").ToString(), (lbSambata2.Content ?? "").ToString(),
                (lbSambata3.Content ?? "").ToString(), (lbSambata4.Content ?? "").ToString());
        }
    }
}
