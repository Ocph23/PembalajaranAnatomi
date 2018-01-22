using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWebApi.Models
{

    public delegate void delChangeAnswer(bool answer,Option option);
    [TableName("Soal")]
   public class Soal:DAL.BaseNotifyProperty
    {
        private int id;
        [PrimaryKey("IdSoal")]
        [DbColumn("IdSoal")]
        public int Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChange("Id");
            }
        }

        private string _value;
        [DbColumn("IsiSoal")]
        public string Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChange("Value"); }
        }


        private int subMateriId;
        [DbColumn("IdSubMateri")]
        public int SubMateriId
        {
            get { return subMateriId; }
            set { subMateriId = value; OnPropertyChange("SubMateriId"); }
        }


        public bool? CorrectAnswer { get; private set; }

        public void AddOption(Option option)
        {
            option.delAnswer = new delChangeAnswer(SetAnswer);
            Choices.Add(option);
           // Options.Refresh();
        }

        private void SetAnswer(bool answer,Option option)
        {
            this.CorrectAnswer = answer;
           
        }

        public List<Option> Choices { get; set;  }
        //public CollectionView Options { get; set; }
        public int Number { get; internal set; }

        public Soal()
        {
            Choices = new List<Option>();
            //Random rnd = new Random();
          //  Options = (CollectionView)CollectionViewSource.GetDefaultView(list);
        }

    }
}
