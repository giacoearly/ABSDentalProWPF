using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ABS_Dental_Pro
{
    public class Plata : INotifyPropertyChanged
    {
        private string pacient;
        private string medic;
        private string total;
        private string transa;
        private string rest;
        private string data;
        private string descriere;

        public string NumePrenumePacient
        {
            get { return pacient; }
            set
            {
                if (pacient != value)
                {
                    pacient = value;
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

        public string Total
        {
            get { return total; }
            set
            {
                if (total != value)
                {
                    total = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Transa
        {
            get { return transa; }
            set
            {
                if (transa != value)
                {
                    transa = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Rest
        {
            get { return rest; }
            set
            {
                if (rest != value)
                {
                    rest = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Data
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Plata()
        {
                
        }
    }
}