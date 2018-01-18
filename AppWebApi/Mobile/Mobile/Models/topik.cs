using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
   public class topik : NotifyBase
    {
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                SetProperty(ref _id, value);
            }
        }

        public string Judul
        {
            get { return _judul; }
            set
            {
                _judul = value;
                SetProperty(ref _judul, value);
            }
        }

        public TimeSpan PosisiMulai
        {
            get { return _posisimulai; }
            set
            {
                _posisimulai = value;
                SetProperty(ref _posisiakhir, value);
            }
        }

        public TimeSpan PosisiAkhir
        {
            get { return _posisiakhir; }
            set
            {
                _posisiakhir = value;
                SetProperty(ref _posisiakhir, value);
            }
        }

        public int SubMateriId
        {
            get { return _submateriid; }
            set
            {
                _submateriid = value;
                SetProperty(ref _submateriid, value);
            }
        }

        private int _id;
        private string _judul;
        private TimeSpan _posisimulai;
        private TimeSpan _posisiakhir;
        private int _submateriid;
    }
}
