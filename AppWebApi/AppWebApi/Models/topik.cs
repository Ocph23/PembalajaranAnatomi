using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace AppWebApi.Models
{
    [TableName("topik")]
    public class topik : BaseNotifyProperty
    {
        [PrimaryKey("KodeTopik")]
        [DbColumn("KodeTopik")]
        public string KodeTopik
        {
            get { return _idtopik; }
            set
            {
                SetProperty(ref _idtopik, value);
            }
        }

        [DbColumn("JudulTopik")]
        public string JudulTopik
        {
            get { return _judultopik; }
            set
            {
                SetProperty(ref _judultopik, value);
            }
        }

        [DbColumn("PosisiMulai")]
        public TimeSpan PosisiMulai
        {
            get { return _posisimulai; }
            set
            {
                SetProperty(ref _posisimulai, value);
            }
        }

        [DbColumn("PosisiAkhir")]
        public TimeSpan PosisiAkhir
        {
            get { return _posisiakhir; }
            set
            {
                SetProperty(ref _posisiakhir, value);
            }
        }

        [DbColumn("KodeSubMateri")]
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


