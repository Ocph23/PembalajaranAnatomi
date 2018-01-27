using AppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppWebApi.Controllers
{
    public class SoalController : ApiController
    {
        // GET: api/Soal
        public IEnumerable<kuis> Get()
        {

            using (var db = new OcphDbContext())
            {
                return db.Soals.Select().ToList();
            }
        }

        // GET: api/Soal/5
        public IEnumerable<kuis> Get(string id)
        {
            using (var db = new OcphDbContext())
            {
                var result =db.Soals.Where(O => O.KodeKuis == id).ToList();
                return result.ToList();
            }
        }

        [Route("api/{materiId}/soal")]
        [HttpGet]
        public IEnumerable<kuis> GetBySubMateri(string materiId)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Soals.Where(O => O.KodeSubMateri== materiId).ToList();
                return result.ToList();

            }
        }

        // POST: api/Soal
        [HttpPost]
        public HttpResponseMessage Post([FromBody]kuis value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if(db.Soals.Insert(value))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, value);
                    }else
                    {
                        throw new SystemException("Data tidak tersipan !");
                    }
                }
                catch (Exception ex)
                {
                   return Request.CreateErrorResponse(HttpStatusCode.NotModified, ex.Message);
                }
            }
          
        }

     //   [Route("api/{soal}/EditSoal")]
        [HttpPut]
        public HttpResponseMessage PutSoal([FromBody]kuis value)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    var isSaved = db.Soals.Update(O => new { O.JawabanA,O.JawabanB,O.JawabanC,O.JawabanBenar,O.JawabanD ,O.Pertanyaan}, value, O => O.KodeKuis== value.KodeKuis);
                    if (isSaved)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, isSaved);
                    }
                       
                    else
                    {
                        throw new SystemException("Data tidak tersimpan !");
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotModified, ex.Message);
                }
            }
        }


        // DELETE: api/Soal/5

        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                   
                    var soalDelete = db.Soals.Delete( O => O.KodeKuis == id);
                    if (soalDelete)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Data Telah Dihapus");
                    }
                    else
                    {
                        throw new SystemException("Data Tidak terhapus!");
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotModified, ex.Message);
                }
            }
        }

    }
}
