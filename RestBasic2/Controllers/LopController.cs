using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestBasic2.Models;

namespace RestBasic2.Controllers
{
    public class LopController : ApiController
    {
        // GET api/lop
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        public List<Lop> Get() {
            SinhVienPersistence sp = new SinhVienPersistence();
            return sp.getAllLop();
        }

        // GET api/lop/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/lop
        public void Post([FromBody]string value)
        {
        }

        // PUT api/lop/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/lop/5
        public void Delete(int id)
        {
        }
    }
}
