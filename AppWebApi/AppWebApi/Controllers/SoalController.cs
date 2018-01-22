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
        public IEnumerable<Soal> Get()
        {

            using (var db = new OcphDbContext())
            {
                return db.Soals.Select().ToList();
            }
        }

        // GET: api/Soal/5
        public IEnumerable<Soal> Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Soals.Where(O => O.Id == id)
                             join b in db.Options.Select() on a.Id equals b.SoalId into ba
                             from b in ba.DefaultIfEmpty()
                             select new Soal { Id = a.Id, Value = a.Value, Choices = ba.ToList() };
                return result.ToList();
            }
        }

        [Route("api/{materiId}/soal")]
        [HttpGet]
        public IEnumerable<Soal> GetBySubMateri(int materiId)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Soals.Where(O => O.SubMateriId== materiId);
                foreach(var item in result)
                {
                    item.Choices = db.Options.Where(O => O.SoalId == item.Id).ToList();
                }
                return result.ToList();

            }
        }

        // POST: api/Soal
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Soal value)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    value.Id = db.Soals.InsertAndGetLastID(value);
                    if(value.Id>0)
                    {
                        foreach(var item in value.Choices)
                        {
                            item.SoalId = value.Id;
                            item.Id = db.Options.InsertAndGetLastID(item);
                        }
                        trans.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK, value);
                    }else
                    {
                        throw new SystemException("Data tidak tersipan !");
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                   return Request.CreateErrorResponse(HttpStatusCode.NotModified, ex.Message);
                }
            }
          
        }

     //   [Route("api/{soal}/EditSoal")]
        [HttpPut]
        public HttpResponseMessage PutSoal([FromBody]Soal value)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    var isSaved = db.Soals.Update(O => new { O.Value }, value, O => O.Id == value.Id);
                    if (isSaved)
                    {
                        foreach(var item in value.Choices)
                        {
                          if(!db.Options.Update(O => new { O.IsTrueAnswer, O.Value }, item, O => O.Id == item.Id))
                            {
                                throw new SystemException("Data tidak tersimpan !");
                            }

                        }
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

        [Route("api/{soal}/EditOption")]
        [HttpPut]
        public HttpResponseMessage PutOption([FromBody]Option value)
        {

            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    var updated = db.Options.Update(O => new { O.IsTrueAnswer }, new Option { IsTrueAnswer = false }, O => O.SoalId == value.SoalId);
                    var isSaved = db.Options.Update(O => new {O.Value, O.IsTrueAnswer }, value, O => O.Id == value.Id);
                    if (isSaved && updated)
                    {
                        trans.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK, isSaved);
                    }
                    else
                    {
                        throw new SystemException("Data tidak tersimpan !");
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.NotModified, ex.Message);
                }
            }
        }


        // DELETE: api/Soal/5

        [Route("api/{soalId}/DeleteSoal")]
        [HttpPut]
        public HttpResponseMessage Delete(int id)
        {

            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    var OptionDelete = db.Options.Delete(O=>O.SoalId == id);
                    var soalDelete = db.Options.Delete( O => O.Id == id);
                    if (OptionDelete && soalDelete)
                    {
                        trans.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK, "Data Telah Dihapus");
                    }
                    else
                    {
                        throw new SystemException("Data Tidak terhapus!");
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.NotModified, ex.Message);
                }
            }
        }

        [Route("api/{soalId}/DeleteOption")]
        [HttpPut]
        public HttpResponseMessage DeleteOption(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var soalDelete = db.Options.Delete(O => O.Id == id);
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
