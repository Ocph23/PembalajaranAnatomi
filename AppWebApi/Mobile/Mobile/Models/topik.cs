using System;

namespace Mobile.Models
{
    public class topik :NotifyBase
    {
        public string KodeTopik
        {
            get { return _idtopik; }
            set
            {
                SetProperty(ref _idtopik, value);
            }
        }

        public string JudulTopik
        {
            get { return _judultopik; }
            set
            {
                SetProperty(ref _judultopik, value);
            }
        }

        public TimeSpan PosisiMulai
        {
            get { return _posisimulai; }
            set
            {
                SetProperty(ref _posisimulai, value);
            }
        }

        public TimeSpan PosisiAkhir
        {
            get { return _posisiakhir; }
            set
            {
                SetProperty(ref _posisiakhir, value);
            }
        }

        public string KodeSubMateri
        {
            get { return _submateri_kodesubmateri; }
            set
            {
                SetProperty(ref _submateri_kodesubmateri, value);
            }
        }

        private string _idtopik;
        private string _judultopik;
        private TimeSpan _posisimulai;
        private TimeSpan _posisiakhir;
        private int _idsubmateri;
        private string _submateri_kodesubmateri;
    }
}


