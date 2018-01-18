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
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Topik/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Topik/5
        public void Delete(int id)
        {
        }
    }
}
