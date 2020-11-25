using System.Collections.Generic;
using LatihanBackend.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System;

namespace LatihanBackend.DAL
{
    public class BarangDAL
    {
        private IConfiguration _config;
        public BarangDAL(IConfiguration config)
        {
            _config = config;            
        }

        private string GetConnStr(){
            return _config.GetConnectionString("DefaultConnection");
        }

        //ADO.NET
        public IEnumerable<Barang> GetAll(){
            List<Barang> lstBarang = new List<Barang>();
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"select * from Barang order by NamaBarang asc";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows){
                    while(dr.Read()){
                        lstBarang.Add(new Barang{
                            KodeBarang = dr["KodeBarang"].ToString(),
                            NamaBarang = dr["NamaBarang"].ToString(),
                            Stok = Convert.ToInt32(dr["Stok"]),
                            HargaBeli = Convert.ToDecimal(dr["HargaBeli"]),
                            HargaJual = Convert.ToDecimal(dr["HargaJual"])
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return lstBarang;
        }

        public Barang GetByKode(string KodeBarang){
            Barang barang = new Barang();
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"select * from Barang where KodeBarang=@KodeBarang";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                cmd.Parameters.AddWithValue("@KodeBarang",KodeBarang);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows){
                    dr.Read();
                    barang.KodeBarang = dr["KodeBarang"].ToString();
                    barang.NamaBarang = dr["NamaBarang"].ToString();
                    barang.Stok = Convert.ToInt32(dr["Stok"]);
                    barang.HargaBeli = Convert.ToDecimal(dr["HargaBeli"]);
                    barang.HargaJual = Convert.ToDecimal(dr["HargaJual"]);
                }
                else {
                    return null;
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return barang;
        }

        /*public string GetLastKode(){
            string KodeBarang = string.Empty;
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"select KodeBarang from Barang order by KodeBarang desc";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows){
                    dr.Read();
                    KodeBarang = dr["KodeBarang"].ToString();
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            int count = Convert.ToInt32(KodeBarang.Substring(2,3));
            count++;
        }*/


        public void Insert(Barang barang){
            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"insert into Barang(KodeBarang,NamaBarang,Stok,HargaBeli,HargaJual) 
                values(@KodeBarang,@NamaBarang,@Stok,@HargaBeli,@HargaJual)";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                cmd.Parameters.AddWithValue("@KodeBarang",barang.KodeBarang);
                cmd.Parameters.AddWithValue("@NamaBarang",barang.NamaBarang);
                cmd.Parameters.AddWithValue("@Stok",barang.Stok);
                cmd.Parameters.AddWithValue("@HargaBeli",barang.HargaBeli);
                cmd.Parameters.AddWithValue("@HargaJual",barang.HargaJual);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Kesalahan: {sqlEx.Message}");
                }
                finally{
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void Update(string KodeBarang,Barang barang){
            var result = GetByKode(KodeBarang);
            if(result==null)
                throw new Exception($"Barang Kode {KodeBarang} tidak ditemukan");

            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"update Barang set NamaBarang=@NamaBarang,
                Stok=@Stok,HargaBeli=@HargaBeli,HargaJual=@HargaJual 
                where KodeBarang=@KodeBarang";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                cmd.Parameters.AddWithValue("@NamaBarang",barang.NamaBarang);
                cmd.Parameters.AddWithValue("@Stok",barang.Stok);
                cmd.Parameters.AddWithValue("@HargaBeli",barang.HargaBeli);
                cmd.Parameters.AddWithValue("@HargaJual",barang.HargaJual);
                cmd.Parameters.AddWithValue("@KodeBarang",KodeBarang);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                finally{
                    cmd.Dispose();
                    conn.Close();
                }
            }

        }

        public void Delete(string KodeBarang){
            var result = GetByKode(KodeBarang);
            if(result==null)
                throw new Exception($"Barang Kode {KodeBarang} tidak ditemukan");

            using(SqlConnection conn = new SqlConnection(GetConnStr())){
                string strSql = @"delete from Barang where KodeBarang=@KodeBarang";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                cmd.Parameters.AddWithValue("@KodeBarang",KodeBarang);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.Message}");
                }
                finally{
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
    }
}