using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace AppWebApi.Models 
{ 
     [TableName("materi")] 
     public class materi:BaseNotifyProperty  
   {
          [PrimaryKey("IdMateri")] 
          [DbColumn("IdMateri")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value; 
                     OnPropertyChange("Id");
                     }
          } 

          [DbColumn("KodeMateri")] 
          public string KodeMateri 
          { 
               get{return _kodemateri;} 
               set{ 
                      _kodemateri=value; 
                     OnPropertyChange("KodeMateri");
                     }
          } 

          [DbColumn("JudulMateri")] 
          public string Judul 
          { 
               get{return _judul;} 
               set{ 
                      _judul=value; 
                     OnPropertyChange("Judul");
                     }
          } 

          private int  _id;
           private string  _kodemateri;
           private string  _judul;
      }
}


