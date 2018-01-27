using System;
using System.Collections.Generic;

namespace Mobile.Models
{
    public class submateri :NotifyBase
    {
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

        public string KodeMateri
        {
            get { return _kodemateri; }
            set
            {
                SetProperty(ref _kodemateri, value);
            }
        }

        public List<topik> Topiks { get; set; }
        public byte[] DataGambar { get; set; }
        public byte[] DataAnimasi { get; set; }

        private string _kodesubmateri;
        private string _judulsubmateri;
        private string _gambar;
        private string _animasi;
        private string _penjelasan;
        private string _kodemateri;
    }
}


