using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using RestBasic2.Models;
using MySql.Data;
using System.Net.Http;
using System.Data;

namespace RestBasic2
{
    public class SinhVienPersistence
    {
        private MySqlConnection conn;
        public SinhVienPersistence()
        {

            try
            {
                String connString = "server=localhost;userid=root;password=;database=qlsv1";
                conn = new MySqlConnection(connString);
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }

        }

        public int addSV(SinhVien sv)
        {
            //String set = "'" + sv.ID + "','" + sv.ten + "','" + sv.gioitinh + "','" + sv.khoa + "'";
            //String sql = "insert into sinhvien values(" + set + ")";
            String set = ",'" + sv.ten + "','" + sv.ngaysinh + "','" + sv.diemTB + "','" + sv.idlop + "'";
            String sql = "insert into sv values(NULL" + set + ")";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;
            return id;
        }

        public bool updateSV(int id, SinhVien sv) {
            String set = "hoten='{0}',ngaysinh='{1}',diemTB='{2}',id_lop='{3}'";
            set = String.Format(set, sv.ten.ToString(), sv.ngaysinh.ToString(), sv.diemTB.ToString(),sv.idlop.ToString());
            String sql = "update sv Set "+set+" where mssv = '" + id + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            int affect_row = cmd.ExecuteNonQuery();//=1 if row existed (changed or not),//=0 if not found row
            if (affect_row>0) return true;

            return false;
        }

        public List<SinhVien> getAllSv() {//using dataTable
            List<SinhVien> svs= new List<SinhVien>();
            String sql = "select * from sv";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.Fill(dt);
            da.Dispose();
            conn.Close();
            SinhVien svtmp;
            for (int i = 0; i < dt.Rows.Count; i++) {
                svtmp = new SinhVien();
                svtmp.mssv = Int32.Parse(dt.Rows[i]["mssv"].ToString());
                svtmp.ten = dt.Rows[i]["hoten"].ToString();
                svtmp.idlop = Int32.Parse(dt.Rows[i]["id_lop"].ToString());
                //svtmp.ngaysinh = dt.Rows[i]["ngaysinh"].ToString();//default: 2/14/1992 12:00:00 AM
                String ngaysinh = dt.Rows[i]["ngaysinh"].ToString(); svtmp.ngaysinh = DateTime.Parse(ngaysinh).ToString("yyyy-MM-dd");
                svtmp.diemTB = float.Parse(dt.Rows[i]["diemTB"].ToString());
                svs.Add(svtmp);
            }
            return svs;

        }

        public List<Lop> getAllLop()
        {//using dataTable
            List<Lop> lops = new List<Lop>();
            String sql = "select * from lop";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.Fill(dt);
            da.Dispose();
            conn.Close();
            Lop loptmp;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                loptmp = new Lop();
                loptmp.idlop = Int32.Parse(dt.Rows[i]["id_lop"].ToString());
                loptmp.tenlop = dt.Rows[i]["ten_lop"].ToString();

                lops.Add(loptmp);
            }
            return lops;

        }

        public SinhVien getSv(int id) {//using Reader
            
            String sql = "select * from sv where mssv='"+id+"'";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) {
                SinhVien sv = new SinhVien();
                sv.mssv = reader.GetInt32(0);
                sv.ten = reader.GetString(1);
                //sv.ngaysinh = reader.GetString(2);
                sv.ngaysinh = DateTime.Parse(reader.GetString(2)).ToString("yyyy-MM-dd");
                sv.diemTB = reader.GetFloat(3);
                sv.idlop = reader.GetInt32(4);
                return sv;
            }
            return null;
        }

        public bool delSv(int id) {
            SinhVien sv = new SinhVien();
             String sql = "select * from sv where mssv='"+id+"'";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) {//check existed row
                reader.Close();
                sql = "delete from sv where mssv='" + id + "'";
                cmd = new MySqlCommand(sql, conn);
                int num = cmd.ExecuteNonQuery();
                if (num > 0)
                    return true;
            
            
            }
            return false;
        }
    }
}