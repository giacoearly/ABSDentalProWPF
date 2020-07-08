using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ABS_Dental_Pro
{
    public class Zi : INotifyPropertyChanged
    {
        private string dela1;
        private string panala1;
        private string dela2;
        private string panala2;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DeLa1
        {
            get { return dela1; }
            set
            {
                if (dela1 != value)
                {
                    dela1 = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PanaLa1
        {
            get { return panala1; }
            set
            {
                if (panala1 != value)
                {
                    panala1 = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DeLa2
        {
            get { return dela2; }
            set
            {
                if (dela2 != value)
                {
                    dela2 = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PanaLa2
        {
            get { return panala2; }
            set
            {
                if (panala2 != value)
                {
                    panala2 = value;
                    OnPropertyChanged();
                }
            }
        }

        public Zi()
        {

        }

        public Zi(string d1, string p1, string d2, string p2)
        {
            dela1 = d1;
            panala1 = p1;
            dela2 = d2;
            panala2 = p2;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return dela1 + panala1 + dela2 + panala2;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.ToString() == obj.ToString();
        }

        public static bool operator ==(Zi p1, Zi p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Zi p1, Zi p2)
        {
            return !(p1 == p2);
        }
    }
    
    public class Medic : INotifyPropertyChanged
    {
        private string id;
        private string nume;
        private string prenume;
        private string telefon;
        private string email;
        private string observatii;
        private Zi luni = new Zi();
        private Zi marti = new Zi();
        private Zi miercuri = new Zi();
        private Zi joi = new Zi();
        private Zi vineri = new Zi();
        private Zi sambata = new Zi();

        public string ID
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Nume
        {
            get { return nume; }
            set
            {
                if (nume != value)
                {
                    nume = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Prenume
        {
            get { return prenume; }
            set
            {
                if (prenume != value)
                {
                    prenume = value;
                    OnPropertyChanged();
                }
            }
        }
 
        public string Telefon
        {
            get { return telefon; }
            set
            {
                if (telefon != value)
                {
                    telefon = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Observatii
        {
            get { return observatii; }
            set
            {
                if (observatii != value)
                {
                    observatii = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public Zi Luni
        {
            get { return luni; }
            set
            {
                if (luni != value)
                {
                    luni = value;
                    OnPropertyChanged();
                }
            }
        }

        public Zi Marti
        {
            get { return marti; }
            set
            {
                if (marti != value)
                {
                    marti = value;
                    OnPropertyChanged();
                }
            }
        }

        public Zi Miercuri
        {
            get { return miercuri; }
            set
            {
                if (miercuri != value)
                {
                    miercuri = value;
                    OnPropertyChanged();
                }
            }
        }

        public Zi Joi
        {
            get { return joi; }
            set
            {
                if (joi != value)
                {
                   joi  = value;
                   OnPropertyChanged();
                }
            }
        }

        public Zi Vineri
        {
            get { return vineri; }
            set
            {
                if (vineri != value)
                {
                    vineri = value;
                    OnPropertyChanged();
                }
            }
        }

        public Zi Sambata
        {
            get { return sambata; }
            set
            {
                if (sambata != value)
                {
                    sambata = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public Medic()
        {

        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            string pacient = string.Format("ID: {0}, Nume: {1}, Prenume: {2}, Telefon: {3}, Other: {4},{5}, Program: {6},{7},{8}{9},{10},{11}",
                                            ID, Nume, Prenume, Telefon, Email, Observatii, Luni, Marti, Miercuri, Joi, Vineri, Sambata);
            return pacient;
        }

        public override bool Equals(object obj)
        {
            return this.ToString() == obj.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public static bool operator ==(Medic p1, Medic p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Medic p1, Medic p2)
        {
            return !(p1 == p2);
        }
    }
}
