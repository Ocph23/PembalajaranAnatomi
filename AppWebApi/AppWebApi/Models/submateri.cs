using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace AppWebApi.Models 
{ 
     [TableName("submateri")] 
     public class submateri:BaseNotifyProperty  
   {
          [PrimaryKey("IdSubMateri")] 
          [DbColumn("IdSubMateri")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value; 
                     OnPropertyChange("Id");
                     }
          } 

          [DbColumn("KodeSubMateri")] 
          public string KodeSubMateri 
          { 
               get{return _kodesubmateri;} 
               set{ 
                      _kodesubmateri=value; 
                     OnPropertyChange("KodeSubMateri");
                     }
          } 

          [DbColumn("JudulSubMateri")] 
          public string JudulSubMateri 
          { 
               get{return _judulsubmateri;} 
               set{ 
                      _judulsubmateri=value; 
                     OnPropertyChange("JudulSubMateri");
                     }
          } 

          [DbColumn("Gambar")] 
          public string Gambar 
          { 
               get{return _gambar;} 
               set{ 
                      _gambar=value; 
                     OnPropertyChange("Gambar");
                     }
          } 

         

          [DbColumn("Animasi")] 
          public string Animasi 
          { 
               get{return _animasi;} 
               set{ 
                      _animasi=value; 
                     OnPropertyChange("Animasi");
                     }
          } 

          [DbColumn("Penjelasan")] 
          public string Penjelasan 
          { 
               get{return _penjelasan;} 
               set{ 
                      _penjelasan=value; 
                     OnPropertyChange("Penjelasan");
                     }
          } 

          [DbColumn("IdMateri")] 
          public int MateriId
        { 
               get{return _materiId;} 
               set{ 
                      _materiId=value; 
                     OnPropertyChange("MateriId");
                     }
          }

        public byte[] DataGambar { get; internal set; }
        public byte[] DataSound { get; internal set; }
        public byte[] DataAnimasi { get; internal set; }
        public List<topik> Topiks { get; internal set; }

        private int  _id;
           private string  _kodesubmateri;
           private string  _judulsubmateri;
           private string  _gambar;
           private string  _sound;
           private string  _animasi;
           private string  _penjelasan;
           private int  _materiId;
      }
}


