using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AppWebApi.Models;


namespace AppWebApi.Controllers
{
    public class MateriController : ApiController
    {
        // GET: api/Materi
        public IEnumerable<materi> Get()
        {
            using (var db = new OcphDbContext())
            {
                return db.Materi.Select();
            }
        }

        // GET: api/Materi/5
        public materi Get(string id)
        {
            using (var db = new OcphDbContext())
            {
                return db.Materi.Where(O => O.KodeMateri == id).FirstOrDefault();
            }
        }

        // POST: api/Materi
        public HttpResponseMessage Post([FromBody]materi value)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new SystemException("Lengkapi Data Anda");
                else
                {
                    using (var db = new OcphDbContext())
                    {
                        if (db.Materi.Insert(value))
                            return Request.CreateResponse(HttpStatusCode.OK, value);
                        else
                        {
                            throw new SystemException("Data Tidak Berhasil Ditambahkan");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
            }
        }

        // PUT: api/Materi/5
        public HttpResponseMessage Put(string id, [FromBody]materi value)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new SystemException("Lengkapi Data Anda");
                else
                {
                    using (var db = new OcphDbContext())
                    {
                        var isUpdated= db.Materi.Update(O=>new {O.JudulMateri},value, O=>O.KodeMateri==value.KodeMateri);
                        if (isUpdated)
                            return Request.CreateResponse(HttpStatusCode.OK, value);
                        else
                        {
                            throw new SystemException("Data Tidak Berhasil Diubah");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
            }

        }




        // DELETE: api/Materi/5
        public HttpResponseMessage Delete(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new SystemException("Lengkapi Data Anda");
                else
                {
                    using (var db = new OcphDbContext())
                    {
                        var isDeleted= db.Materi.Delete( O => O.KodeMateri == id);
                        if (isDeleted)
                            return Request.CreateResponse(HttpStatusCode.OK, id);
                        else
                        {
                            throw new SystemException("Data Tidak Berhasil Terhapus");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
            }
        }
    }
}
