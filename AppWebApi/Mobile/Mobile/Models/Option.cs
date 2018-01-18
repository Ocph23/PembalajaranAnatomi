using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class Option:NotifyBase
    {
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                SetProperty(ref  _id, value);
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                SetProperty(ref _value, value);
            }
        }

        public string IsTrue
        {
            get { return _istrue; }
            set
            {
                _istrue = value;
                SetProperty(ref _istrue, value);
            }
        }

        public int SoalId
        {
            get { return _soalid; }
            set
            {
                _soalid = value;
                SetProperty(ref _soalid, value);
            }
        }

        private int _id;
        private string _value;
        private string _istrue;
        private int _soalid;
    }

}
