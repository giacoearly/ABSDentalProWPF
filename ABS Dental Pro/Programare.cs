using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ABS_Dental_Pro
{
    public class Programare : INotifyPropertyChanged, IComparable
    {
        private string numeMedic;
        private string prenumeMedic;
        private string numePacient;
        private string prenumePacient;
        private DateTime data = new DateTime();
        private string ora;
        private string durata; 
        private string descriere;
        private int indiceListaMedici;

        public string NumeMedic
        {
            get { return numeMedic; }
            set
            {
                if (numeMedic != value)
                {
                    numeMedic = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PrenumeMedic
        {
            get { return prenumeMedic; }
            set
            {
                if (prenumeMedic != value)
                {
                    prenumeMedic = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NumePacient
        {
            get { return numePacient; }
            set
            {
                if (numePacient != value)
                {
                    numePacient = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PrenumePacient
        {
            get { return prenumePacient; }
            set
            {
                if (prenumePacient != value)
                {
                    prenumePacient = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Data
        {
            get { return data; }
            set
            {
                if (data != value)
                {
                    data = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Ora
        {
            get { return ora; }
            set
            {
                if (ora != value)
                {
                    ora = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Durata
        {
            get { return durata; }
            set
            {
                if (durata != value)
                {
                    durata = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Descriere
        {
            get { return descriere; }
            set
            {
                if (descriere != value)
                {
                    descriere = value;
                    OnPropertyChanged();
                }
            }
        }

        public int IndiceListaMedici
        {
            get { return indiceListaMedici; }
            set
            {
                if (indiceListaMedici != value)
                {
                    indiceListaMedici = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Programare()
        {

        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            string programare = string.Format("NumeMedic: {0}, PrenumeMedic: {1}, NumePacient: {2}, PrenumePacient: {3} Data: {4}, Ora: {5}, Durata: {6}, Descriere: {7}",
                                      numeMedic, prenumeMedic, numePacient, prenumePacient, data.ToShortDateString(), ora, durata, descriere);
            return programare;
        }

        //public override bool Equals(object obj)
        //{
        //    return (this.ToString() == obj.ToString());
        //}

        //public override int GetHashCode()
        //{
        //    return this.ToString().GetHashCode();
        //}

        public int CompareTo(object obj)
        {
            return data.CompareTo(((Programare)obj).data);
        }

        //public static bool operator ==(Programare p1, Programare p2)
        //{
        //    return p1.Equals(p2);
        //}

        //public static bool operator !=(Programare p1, Programare p2)
        //{
        //    return !(p1 == p2);
        //}
    }
}
