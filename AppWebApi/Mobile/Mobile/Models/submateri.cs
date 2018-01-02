using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class submateri : NotifyBase
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public string KodeSubMateri
        {
            get { return _kodesubmateri; }
            set
            {
                SetProperty(ref _kodesubmateri, value);
            }
        }

        public string JudulSubMateri
        {
            get { return _judulsubmateri; }
            set
            {
                SetProperty(ref _judulsubmateri, value);
            }
        }

        public string Gambar
        {
            get { return _gambar; }
            set
            {
                SetProperty(ref _gambar, value);
            }
        }

        public string Sound
        {
            get { return _sound; }
            set
            {
                SetProperty(ref _sound, value);
            }
        }

        public string Animasi
        {
            get { return _animasi; }
            set
            {
                SetProperty(ref _animasi, value);
            }
        }

        public string Penjelasan
        {
            get { return _penjelasan; }
            set
            {
                SetProperty(ref _penjelasan, value);
            }
        }

        public int MateriId
        {
            get { return _materiId; }
            set
            {
                SetProperty(ref _materiId, value);
            }
        }

        public byte[] DataGambar { get; internal set; }
        public byte[] DataSound { get; internal set; }
        public byte[] DataAnimasi { get; internal set; }

        private int _id;
        private string _kodesubmateri;
        private string _judulsubmateri;
        private string _gambar;
        private string _sound;
        private string _animasi;
        private string _penjelasan;
        private int _materiId;
    }
}


