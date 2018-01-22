using AppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppWebApi.Controllers
{
    public class TopikController : ApiController
    {
       [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Topik/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Topics.Where(O => O.SubMateriId == id).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        // POST: api/Topik
        public HttpResponseMessage Post([FromBody]topik value)
        {
            using (var db = new OcphDbContext())
            {
                value.Id = db.Topics.InsertAndGetLastID(value);
                if(value.Id>0)
                return Request.CreateResponse(HttpStatusCode.OK, value);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable,"Data Tidak Tersimpan");
            }
        }

        // PUT: api/Topik/5
        public HttpResponseMessage Put(int id, [FromBody]topik value)
        {
            using (var db = new OcphDbContext())
            {
                var isUpdated = db.Topics.Update(O=> new { O.Judul,O.PosisiMulai,O.PosisiAkhir},value,O=>O.Id==id);
                if (isUpdated)
                    return Request.CreateResponse(HttpStatusCode.OK, value);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Data Tidak Tersimpan");
            }
        }

        // DELETE: api/Topik/5
        public HttpResponseMessage Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                var isUpdated = db.Topics.Delete( O => O.Id == id);
                if (isUpdated)
                    return Request.CreateResponse(HttpStatusCode.OK, "Data Berhasil Dihapus");
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Data Tidak Terhapus");
            }
        }
    }
}
