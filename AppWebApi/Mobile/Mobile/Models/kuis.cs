using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class kuis : NotifyBase
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public string Pertanyaan
        {
            get { return _pertanyaan; }
            set
            {
                SetProperty(ref _pertanyaan, value);
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

        public string Jawaban
        {
            get { return _jawaban; }
            set
            {
                SetProperty(ref _jawaban, value);
            }
        }

        public int SubMateriId
        {
            get { return _submateriid; }
            set
            {
                SetProperty(ref _submateriid, value);
            }
        }

        private int _id;
        private string _pertanyaan;
        private string _jawabana;
        private string _jawabanb;
        private string _jawabanc;
        private string _jawaband;
        private string _jawaban;
        private int _submateriid;
    }
}


