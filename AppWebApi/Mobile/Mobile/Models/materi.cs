using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class materi : NotifyBase
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
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

        public string Judul
        {
            get { return _judul; }
            set
            {
                SetProperty(ref _judul, value);
            }
        }

        private int _id;
        private string _kodemateri;
        private string _judul;
    }
}


