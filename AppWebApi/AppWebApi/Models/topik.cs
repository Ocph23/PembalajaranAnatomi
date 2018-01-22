using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebApi.Models
{
    [TableName("topik")]
    public class topik : BaseNotifyProperty
    {
        [PrimaryKey("IdTopik")]
        [DbColumn("IdTopik")]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange("Id");
            }
        }

        [DbColumn("JudulTopik")]
        public string Judul
        {
            get { return _judul; }
            set
            {
                _judul = value;
                OnPropertyChange("Judul");
            }
        }

        [DbColumn("PosisiMulai")]
        public TimeSpan PosisiMulai
        {
            get { return _posisimulai; }
            set
            {
                _posisimulai = value;
                OnPropertyChange("PosisiMulai");
            }
        }

        [DbColumn("PosisiAkhir")]
        public TimeSpan PosisiAkhir
        {
            get { return _posisiakhir; }
            set
            {
                _posisiakhir = value;
                OnPropertyChange("PosisiAkhir");
            }
        }

        [DbColumn("IdSubMateri")]
        public int SubMateriId
        {
            get { return _submateriid; }
            set
            {
                _submateriid = value;
                OnPropertyChange("SubMateriId");
            }
        }

        private int _id;
        private string _judul;
        private TimeSpan _posisimulai;
        private TimeSpan _posisiakhir;
        private int _submateriid;
    }

}