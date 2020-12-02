using System;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using LatihanBackend.Models;
using Microsoft.Extensions.Configuration;

namespace DAL {
    public class TransaksiDAL {
        private IConfiguration _config;
        public TransaksiDAL (IConfiguration config) {
            _config = config;
        }

        private string GetConnStr () {
            return _config.GetConnectionString ("DefaultConnection");
        }

        public int InsetTransaksi (TransaksiPenjualan transaksiJual) {
            try {
                using (TransactionScope scope = new TransactionScope ()) {
                    using (SqlConnection conn = new SqlConnection (GetConnStr ())) {

                        string strSql = @"insert into NotaJual(PelangganID,Tanggal) 
                        values(@PelangganID,@Tanggal);select @@identity";
                        SqlCommand cmd1 = new SqlCommand (strSql, conn);
                        cmd1.Parameters.AddWithValue ("@PelangganID", transaksiJual.Nota.PelangganID);
                        cmd1.Parameters.AddWithValue ("@Tanggal", transaksiJual.Nota.Tanggal);
                        conn.Open ();
                        int notaID = Convert.ToInt32 (cmd1.ExecuteScalar ());

                        StringBuilder sb = new StringBuilder ();
                        sb.Append ("insert into ItemJual(NotaJualID,KodeBarang,Jumlah,HargaJual) values ");
                        foreach (var item in transaksiJual.Items) {
                            sb.Append ($"({notaID},'{item.KodeBarang}',{item.Jumlah},{item.HargaJual}),");
                        }
                        var strSql2 = sb.ToString ().Substring (0, sb.ToString ().Length - 1);
                        strSql2 += ";";

                        SqlCommand cmd2 = new SqlCommand (strSql2, conn);
                        cmd2.ExecuteNonQuery ();

                        scope.Complete();
                        return notaID;
                    }
                }
            } catch (SqlException sqlEx) {
                throw new Exception ($"Error Sql: {sqlEx.Message}");
            } catch (TransactionAbortedException ex) {
                throw new Exception ($"Transaction: {ex.Message}");
            }
        }
    }
}