using AppWebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AppWebApi.Controllers
{
    public class SubMateriController : ApiController
    {
        // GET: api/Materi
        public IEnumerable<submateri> Get()
        {
            using (var db = new OcphDbContext())
            {
                return db.SubMateri.Select();
            }
        }


        [Route("api/{materiId}/submateri")]
        [HttpGet]
        public IEnumerable<submateri> GetById(int materiId)
        {
            using (var db = new OcphDbContext())
            {
                return db.SubMateri.Where(O => O.MateriId == materiId);
            }
        }

        // GET: api/Materi/5
        public submateri Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                string uploadPath = HttpContext.Current.Server.MapPath("~/Uploads/");
                var result= db.SubMateri.Where(O => O.Id == id).FirstOrDefault();
                if(result!=null && !string.IsNullOrEmpty(result.Gambar))
                {
                    var fi = new FileInfo(uploadPath+result.Gambar);
                    var s = fi.OpenRead();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        s.CopyTo(ms);
                        result.DataGambar = ms.ToArray();
                    }
                    s.Close();
                }

                if (result != null && !string.IsNullOrEmpty(result.Sound))
                {
                    var fi = new FileInfo(uploadPath + result.Sound);
                    var s = fi.OpenRead();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        s.CopyTo(ms);
                        result.DataSound = ms.ToArray();
                    }
                    s.Close();
                }

                if (result != null && !string.IsNullOrEmpty(result.Animasi))
                {
                    var fi = new FileInfo(uploadPath + result.Animasi);
                    var s = fi.OpenRead();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        s.CopyTo(ms);
                        result.DataAnimasi = ms.ToArray();
                    }
                    s.Close();
                }


                return result;

            }
        }


        [Route("api/{Id}/image")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post(int Id)
        {
            string uploadPath = HttpContext.Current.Server.MapPath("~/Uploads");
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                "This request is not properly formatted"));
            else
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    var provider = new MultipartFormDataStreamProvider(uploadPath);
                    await Request.Content.ReadAsMultipartAsync(provider);
                    var sub = new Models.submateri();
                    FileInfo fi = null;
                    foreach (var file in provider.FileData)
                    {
                        fi = new FileInfo(file.LocalFileName);
                        sub.Gambar = fi.Name;
                        var s = fi.OpenRead();
                        using (MemoryStream ms = new MemoryStream())
                        {
                            s.CopyTo(ms);
                            sub.DataGambar = ms.ToArray();
                        }
                        s.Close();
                    }

                    var isUpdated = db.SubMateri.Update(O => new { O.Gambar }, sub, O => O.Id == Id);
                        if (isUpdated)
                        {
                            trans.Commit();
                            return Request.CreateResponse(HttpStatusCode.OK, sub);
                        }
                        else
                            throw new SystemException("Gambar Gagal Disimpan");

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                }
            }
        }

        [Route("api/{Id}/sound")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostSound(int Id)
        {
            string uploadPath = HttpContext.Current.Server.MapPath("~/Uploads");
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                "This request is not properly formatted"));
            else
                using (var db = new OcphDbContext())
                {
                    var trans = db.Connection.BeginTransaction();
                    try
                    {
                        var provider = new MultipartFormDataStreamProvider(uploadPath);
                        await Request.Content.ReadAsMultipartAsync(provider);
                        var sub = new Models.submateri();
                        FileInfo fi = null;
                        foreach (var file in provider.FileData)
                        {
                            fi = new FileInfo(file.LocalFileName);
                            sub.Sound = fi.Name;
                            var s = fi.OpenRead();
                            using (MemoryStream ms = new MemoryStream())
                            {
                                s.CopyTo(ms);
                                sub.DataSound = ms.ToArray();
                            }
                            s.Close();
                        }

                        var isUpdated = db.SubMateri.Update(O => new { O.Sound }, sub, O => O.Id == Id);
                        if (isUpdated)
                        {
                            trans.Commit();
                            return Request.CreateResponse(HttpStatusCode.OK, sub);
                        }
                        else
                            throw new SystemException("Gambar Gagal Disimpan");

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                    }
                }
        }

        [Route("api/{Id}/animation")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostAnimation(int Id)
        {
            string uploadPath = HttpContext.Current.Server.MapPath("~/Uploads");
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                "This request is not properly formatted"));
            else
                using (var db = new OcphDbContext())
                {
                    var trans = db.Connection.BeginTransaction();
                    try
                    {
                        var provider = new MultipartFormDataStreamProvider(uploadPath);
                        await Request.Content.ReadAsMultipartAsync(provider);
                        var sub = new Models.submateri();
                        FileInfo fi = null;
                        foreach (var file in provider.FileData)
                        {
                            fi = new FileInfo(file.LocalFileName);
                            sub.Animasi = fi.Name;
                            var s = fi.OpenRead();
                            using (MemoryStream ms = new MemoryStream())
                            {
                                s.CopyTo(ms);
                                sub.DataAnimasi = ms.ToArray();
                            }
                            s.Close();
                        }

                        var isUpdated = db.SubMateri.Update(O => new { O.Animasi}, sub, O => O.Id == Id);
                        if (isUpdated)
                        {
                            trans.Commit();
                            return Request.CreateResponse(HttpStatusCode.OK, sub);
                        }
                        else
                            throw new SystemException("Gambar Gagal Disimpan");

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                    }
                }
        }



        // POST: api/Materi
        public HttpResponseMessage Post([FromBody]submateri value)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new SystemException("Lengkapi Data Anda");
                else
                {
                    using (var db = new OcphDbContext())
                    {
                        value.Id = db.SubMateri.InsertAndGetLastID(value);
                        if (value.Id > 0)
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
        public HttpResponseMessage Put(int id, [FromBody]submateri value)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new SystemException("Lengkapi Data Anda");
                else
                {
                    using (var db = new OcphDbContext())
                    {
                        var isUpdated = db.SubMateri.Update(O => new { O.JudulSubMateri, O.Penjelasan }, value, O => O.Id == value.Id);
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
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new SystemException("Lengkapi Data Anda");
                else
                {
                    using (var db = new OcphDbContext())
                    {
                        var isDeleted = db.SubMateri.Delete(O => O.Id == id);
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
