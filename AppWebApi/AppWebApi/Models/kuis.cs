using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace AppWebApi.Models 
{ 
     [TableName("kuis")] 
     public class kuis:BaseNotifyProperty  
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value; 
                     OnPropertyChange("Id");
                     }
          } 

          [DbColumn("Pertanyaan")] 
          public string Pertanyaan 
          { 
               get{return _pertanyaan;} 
               set{ 
                      _pertanyaan=value; 
                     OnPropertyChange("Pertanyaan");
                     }
          } 

          [DbColumn("JawabanA")] 
          public string JawabanA 
          { 
               get{return _jawabana;} 
               set{ 
                      _jawabana=value; 
                     OnPropertyChange("JawabanA");
                     }
          } 

          [DbColumn("JawabanB")] 
          public string JawabanB 
          { 
               get{return _jawabanb;} 
               set{ 
                      _jawabanb=value; 
                     OnPropertyChange("JawabanB");
                     }
          } 

          [DbColumn("JawabanC")] 
          public string JawabanC 
          { 
               get{return _jawabanc;} 
               set{ 
                      _jawabanc=value; 
                     OnPropertyChange("JawabanC");
                     }
          } 

          [DbColumn("JawabanD")] 
          public string JawabanD 
          { 
               get{return _jawaband;} 
               set{ 
                      _jawaband=value; 
                     OnPropertyChange("JawabanD");
                     }
          } 

          [DbColumn("Jawaban")] 
          public string Jawaban 
          { 
               get{return _jawaban;} 
               set{ 
                      _jawaban=value; 
                     OnPropertyChange("Jawaban");
                     }
          } 

          [DbColumn("SubMateriId")] 
          public int SubMateriId 
          { 
               get{return _submateriid;} 
               set{ 
                      _submateriid=value; 
                     OnPropertyChange("SubMateriId");
                     }
          } 

          private int  _id;
           private string  _pertanyaan;
           private string  _jawabana;
           private string  _jawabanb;
           private string  _jawabanc;
           private string  _jawaband;
           private string  _jawaban;
           private int  _submateriid;
      }
}


