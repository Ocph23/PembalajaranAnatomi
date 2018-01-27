using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace AppWebApi.Models
{
    [TableName("kuis")]
    public class kuis : BaseNotifyProperty
    {
        [PrimaryKey("KodeKuis")]
        [DbColumn("KodeKuis")]
        public string KodeKuis
        {
            get { return _kodesoal; }
            set
            {
                SetProperty(ref _kodesoal, value);
            }
        }

        private string _noUrut;
        [DbColumn("NoUrut")]
        public string NoUrut
        {
            get { return _noUrut; }
            set { _noUrut = value; }
        }

        [DbColumn("Pertanyaan")]
        public string Pertanyaan
        {
            get { return _pertanyaan; }
            set
            {
                SetProperty(ref _pertanyaan, value);
            }
        }

        [DbColumn("KodeSubMateri")]
        public string KodeSubMateri
        {
            get { return _kodesubmateri; }
            set
            {
                SetProperty(ref _kodesubmateri, value);
            }
        }

        [DbColumn("JawabanA")]
        public string JawabanA
        {
            get { return _jawabana; }
            set
            {
                SetProperty(ref _jawabana, value);
            }
        }

        [DbColumn("JawabanB")]
        public string JawabanB
        {
            get { return _jawabanb; }
            set
            {
                SetProperty(ref _jawabanb, value);
            }
        }

        [DbColumn("JawabanC")]
        public string JawabanC
        {
            get { return _jawabanc; }
            set
            {
                SetProperty(ref _jawabanc, value);
            }
        }

        [DbColumn("JawabanD")]
        public string JawabanD
        {
            get { return _jawaband; }
            set
            {
                SetProperty(ref _jawaband, value);
            }
        }

        [DbColumn("JawabanBenar")]
        public string JawabanBenar
        {
            get { return _jawabanbenar; }
            set
            {
                SetProperty(ref _jawabanbenar, value);
            }
        }

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


