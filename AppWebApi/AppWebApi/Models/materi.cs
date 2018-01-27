using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace AppWebApi.Models
{
    [TableName("materi")]
    public class materi : BaseNotifyProperty
    {
        [PrimaryKey("KodeMateri")]
        [DbColumn("KodeMateri")]
        public string KodeMateri
        {
            get { return _kodemateri; }
            set
            {
                SetProperty(ref _kodemateri, value);
            }
        }

        [DbColumn("JudulMateri")]
        public string JudulMateri
        {
            get { return _judulmateri; }
            set
            {
                SetProperty(ref _judulmateri, value);
            }
        }

        private string _kodemateri;
        private string _judulmateri;
    }
}


