using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ABS_Dental_Pro
{
    public class Pacient : INotifyPropertyChanged
    {
        private string numarFisa;
        private string medic;
        private string nume;
        private string prenume;
        private string cnp;
        private string serieCi;
        private string numarCi;
        private string varsta;
        private string sex;
        private string telefon;
        private string email;
        private string observatii;

        public string NumarFisa
        {
            get { return numarFisa; }
            set
            {
                if (numarFisa!=value)
                {
                    numarFisa = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Medic
        {
            get { return medic; }
            set
            {
                if (medic != value)
                {
                    medic = value;
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

        public string Cnp
        {
            get { return cnp; }
            set
            {
                if (cnp != value)
                {
                    cnp = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SerieCi
        {
            get { return serieCi; }
            set
            {
                if (serieCi != value)
                {
                    serieCi = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NumarCi
        {
            get { return  numarCi; }
            set
            {
                if (numarCi != value)
                {
                    numarCi = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Varsta
        {
            get { return varsta; }
            set
            {
                if (varsta != value)
                {
                    varsta = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Sex
        {
            get { return sex; }
            set
            {
                if (sex != value)
                {
                    sex = value;
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


        public event PropertyChangedEventHandler PropertyChanged;

        public Pacient()
        {

        }

        private void OnPropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }   
                        
        public override string ToString()
        {
            string pacient = string.Format("Număr Fișă: {0}, Medic: {1}, Nume: {2}, Prenume: {3}, CNP: {4}, Other: {5},{6},{7},{8},{9},{10},{11}",
                                            NumarFisa, Medic, Nume, Prenume, Cnp, serieCi, numarCi, Varsta, Sex, Telefon, Email, Observatii);
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

        public static bool operator==(Pacient p1, Pacient p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator!=(Pacient p1, Pacient p2)
        {
            return !(p1 == p2);
        }
    }
}
