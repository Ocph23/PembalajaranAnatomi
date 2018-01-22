using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWebApi.Models
{
    [TableName("Pilihan")]
  public  class Option:DAL.BaseNotifyProperty
    {
              
        private int? _id;
        private string _value;
        private int _soalId;

        [PrimaryKey("IdPilihan")]
        [DbColumn("IdPilihan")]

        public int? Id
        {
            get { return _id; }
            set { _id = value.Value;OnPropertyChange("Id"); }
        }

       
        [DbColumn("IdSoal")]

        public int SoalId
        {
            get { return _soalId; }
            set { _soalId = value; OnPropertyChange("SoalId"); }
        }

        
        [DbColumn("IsiJawaban")]
        public string Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChange(Value); }
        }


        private bool _isTrue;
        private bool _userSelected;
        internal delChangeAnswer delAnswer;

        [DbColumn("StatusJawaban")]
        public bool IsTrueAnswer
        {
            get { return _isTrue; }
            set { _isTrue = value;OnPropertyChange("IsTrue"); }
        }


        public bool UserSelected
        {
            get { return _userSelected; }
            set
            {
                
                if (value && IsTrueAnswer)
                {
                    delAnswer(true,this);
                }
                else if(value)
                    delAnswer(false,this);

                _userSelected = value;
                OnPropertyChange("UserSelected");
            }
        }
    }
}
