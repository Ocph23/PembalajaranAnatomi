using System;
using System.Collections.Generic;

namespace Mobile.Models
{
    public class kuis : NotifyBase
    {
        public string KodeKuis
        {
            get { return _kodesoal; }
            set
            {
                SetProperty(ref _kodesoal, value);
            }
        }

        private string _noUrut;
        public string NoUrut
        {
            get { return _noUrut; }
            set { _noUrut = value; }
        }

        public string Pertanyaan
        {
            get { return _pertanyaan; }
            set
            {
                SetProperty(ref _pertanyaan, value);
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

        public string JawabanA
        {
            get { return _jawabana; }
            set
            {
                SetProperty(ref _jawabana, value);
            }
        }

        public string JawabanB
        {
            get { return _jawabanb; }
            set
            {
                SetProperty(ref _jawabanb, value);
            }
        }

        public string JawabanC
        {
            get { return _jawabanc; }
            set
            {
                SetProperty(ref _jawabanc, value);
            }
        }

        public string JawabanD
        {
            get { return _jawaband; }
            set
            {
                SetProperty(ref _jawaband, value);
            }
        }

        public string JawabanBenar
        {
            get { return _jawabanbenar; }
            set
            {
                SetProperty(ref _jawabanbenar, value);
            }
        }

        public Option OptionSelected { get;  set; }
        public int Number { get; set; }
        public List<Option> Choices { get; set; }

        private string _kodesoal;
        private string _pertanyaan;
        private string _kodesubmateri;
        private string _jawabana;
        private string _jawabanb;
        private string _jawabanc;
        private string _jawaband;
        private string _jawabanbenar;
    }
}


