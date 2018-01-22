using AppWebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
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
                try
                {
                    var result = db.SubMateri.Where(O => O.MateriId == materiId);
                    foreach (var item in result)
                    {
                        item.Topiks = db.Topics.Where(O => O.SubMateriId == item.Id).ToList();
                    }
                    return result.ToList();
                }
                catch (Exception ex)
                {

                    throw;
                }
             
            }
        }

        // GET: api/Materi/5
        [HttpGet]
        public submateri Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                string uploadPath = HttpContext.Current.Server.MapPath("~/Uploads/");
                var result = db.SubMateri.Where(O => O.Id == id).FirstOrDefault();
                result.Topiks = db.Topics.Where(O => O.SubMateriId == result.Id).ToList();
                if (result != null && !string.IsNullOrEmpty(result.Gambar))
                {
                    var fi = new FileInfo(uploadPath + result.Gambar);
                    var s = fi.OpenRead();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        s.CopyTo(ms);
                        result.DataGambar = ms.ToArray();
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

                        var isUpdated = db.SubMateri.Update(O => new { O.Animasi }, sub, O => O.Id == Id);
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


   
        public HttpResponseMessage GetMediaVideo(string fileName)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    string uploadPath = HttpContext.Current.Server.MapPath("~/Uploads/");

                    var fi = new FileInfo(uploadPath + fileName);
                    var respon = new HttpResponseMessage(HttpStatusCode.OK);
                    var s = fi.OpenRead();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        s.CopyTo(ms);
                        respon.Content = new ByteArrayContent(ms.ToArray());
                        respon.Content.Headers.ContentDisposition =
                       new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                       {
                           FileName = fileName
                       };

                      //  respon.Content.Headers.ContentType = new MediaTypeHeaderValue("video/mp4");



                    }
                    s.Close();
                    return respon;
                }

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, ex.Message);
            }
          

        }


        [Route("api/media/{Id}/video1")]
        public IHttpActionResult GetLiveVideo(int id)
        {
            using (var db = new OcphDbContext())
            {
                string uploadPath = HttpContext.Current.Server.MapPath("~/Uploads/");
                var result = db.SubMateri.Where(O => O.Id == id).FirstOrDefault();

                return new VideoFileActionResult(uploadPath+result.Animasi);

            }
           
        }

    }


    public class VideoFileActionResult : IHttpActionResult
    {
        private const long BufferLength = 65536;
        public VideoFileActionResult(string videoFilePath)
        {
            this.Filepath = videoFilePath;
        }

        public string Filepath { get; private set; }

       
        Task<HttpResponseMessage> IHttpActionResult.ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            FileInfo fileInfo = new FileInfo(this.Filepath);
            long totalLength = fileInfo.Length;
            response.Content = new PushStreamContent((outputStream, httpContent, transportContext) =>
            {
                OnStreamConnected(outputStream, httpContent, transportContext);
            });

            response.Content.Headers.ContentLength = totalLength;
            return Task.FromResult(response);
        }

        private async void OnStreamConnected(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                var buffer = new byte[BufferLength];

                using (var nypdVideo = File.Open(this.Filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var videoLength = (int)nypdVideo.Length;
                    var videoBytesRead = 1;

                    while (videoLength > 0 && videoBytesRead > 0)
                    {
                        videoBytesRead = nypdVideo.Read(buffer, 0, Math.Min(videoLength, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, videoBytesRead);
                        videoLength -= videoBytesRead;
                    }
                }
            }
            catch (HttpException ex)
            {
                return;
            }
            finally
            {
                // Close output stream as we are done
                outputStream.Close();
            }
        }
    }
}