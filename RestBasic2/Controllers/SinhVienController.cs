using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestBasic2.Models;

namespace RestBasic2.Controllers
{
    public class SinhVienController : ApiController
    {
        // GET api/sinhvien
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        public List<SinhVien> Get()
        {
            SinhVienPersistence sp = new SinhVienPersistence();
            return sp.getAllSv();
        }


        // GET api/sinhvien/5
        //public string Get(int id)
          public SinhVien Get(int id)
        {
            //return "value";
            //SinhVien sv = new SinhVien();
            //sv.ID = id;
            //sv.ten = "HoVaKho";
            //sv.gioitinh = "Nam";
            //sv.khoa = 3;
            SinhVienPersistence sp = new SinhVienPersistence();
            return sp.getSv(id);

        }

        // POST api/sinhvien
        //public void Post([FromBody]string value)
          public HttpResponseMessage Post([FromBody]SinhVien value)//sv obj in json
        {
            SinhVienPersistence sp = new SinhVienPersistence();
            int id = sp.addSV(value);
            value.mssv = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("{0}", id));
                
             return response;

        }

        // PUT api/sinhvien/5
        //public void Put(int id, [FromBody]string value)
          public HttpResponseMessage Put(int id, [FromBody]SinhVien sv)
        {
            SinhVienPersistence sp = new SinhVienPersistence();
            bool rowChanged = sp.updateSV(id,sv);
            HttpResponseMessage response;
            if (rowChanged)
                response = Request.CreateResponse(HttpStatusCode.OK);
            else
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            return response;
        }

        // DELETE api/sinhvien/5
        //public void Delete(int id)
            public HttpResponseMessage Delete(int id)
        {
            SinhVienPersistence sp = new SinhVienPersistence();
            bool rowsExisted = true;
            rowsExisted = sp.delSv(id);
            HttpResponseMessage response;
            if (rowsExisted)
                response = Request.CreateResponse(HttpStatusCode.OK);
            else
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            return response;
        }
    }
}
