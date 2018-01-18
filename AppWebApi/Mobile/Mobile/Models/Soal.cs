using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class soal
    {
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }

        public int SubMateriId
        {
            get { return _submateriid; }
            set
            {
                _submateriid = value;
            }
        }
        public List<Option> Choices { get; set; }
        private int _id;
        private string _value;
        private int _submateriid;
    }

}
