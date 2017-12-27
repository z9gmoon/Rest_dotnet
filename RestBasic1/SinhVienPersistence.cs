using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using RestBasic1.Models;
using MySql.Data;
using System.Net.Http;
using System.Data;

namespace RestBasic1
{
    public class SinhVienPersistence
    {
        private MySqlConnection conn;
        public SinhVienPersistence()
        {

            try
            {
                String connString = "server=localhost;userid=root;password=;database=restbasic1";
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
            String set = ",'" + sv.ten + "','" + sv.gioitinh + "','" + sv.khoa + "'";
            String sql = "insert into sinhvien values(NULL" + set + ")";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;
            return id;
        }

        public bool updateSV(int id, SinhVien sv) {
            String set = "ten='{0}',gioitinh='{1}',khoa='{2}'";
            set = String.Format(set, sv.ten.ToString(), sv.gioitinh.ToString(), sv.khoa.ToString());
            String sql = "update sinhvien Set "+set+" where id = '" + id + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            int affect_row = cmd.ExecuteNonQuery();//=1 if row existed (changed or not),//=0 if not found row
            if (affect_row>0) return true;

            return false;
        }

        public List<SinhVien> getAllSv() {//using dataTable
            List<SinhVien> svs= new List<SinhVien>();
            String sql = "select * from sinhvien";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.Fill(dt);
            da.Dispose();
            conn.Close();
            SinhVien svtmp;
            for (int i = 0; i < dt.Rows.Count; i++) {
                svtmp = new SinhVien();
                svtmp.ID = Int32.Parse(dt.Rows[i]["id"].ToString());
                svtmp.ten = dt.Rows[i]["ten"].ToString();
                svtmp.khoa = Int32.Parse(dt.Rows[i]["khoa"].ToString());
                svtmp.gioitinh = dt.Rows[i]["gioitinh"].ToString();
                svs.Add(svtmp);
            }
            return svs;

        }

        public SinhVien getSv(int id) {//using Reader
            
            String sql = "select * from sinhvien where id='"+id+"'";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) {
                SinhVien sv = new SinhVien();
                sv.ID = reader.GetInt32(0);
                sv.ten = reader.GetString(1);
                sv.gioitinh = reader.GetString(2);
                sv.khoa = reader.GetInt32(3);
                return sv;
            }
            return null;
        }

        public bool delSv(int id) {
            SinhVien sv = new SinhVien();
             String sql = "select * from sinhvien where id='"+id+"'";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) {//check existed row
                reader.Close();
                sql = "delete from sinhvien where id='" + id + "'";
                cmd = new MySqlCommand(sql, conn);
                int num = cmd.ExecuteNonQuery();
                if (num > 0)
                    return true;
            
            
            }
            return false;
        }
    }
}