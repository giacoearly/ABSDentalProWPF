using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ABS_Dental_Pro
{
    public class Istorie : INotifyPropertyChanged
    {
        private string medic;
        private string data;
        private string descriere;

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

        public Istorie()
        {

        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            string pacient = string.Format("{0}, {1}, {2}", Medic, Data, Descriere);
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

        public static bool operator ==(Istorie p1, Istorie p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Istorie p1, Istorie p2)
        {
            return !(p1 == p2);
        }
    }
}
