using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestBasic2.Models
{
    public class SinhVien
    {
        public int mssv { get; set; }
        public String ten { get; set; }
        public String ngaysinh { get; set; }
        public float diemTB { get; set; }
        public int idlop { get; set; }
    }
}