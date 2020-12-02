using System;
using DAL;
using LatihanBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaksiController : ControllerBase
    {
        private IConfiguration _config;
        private TransaksiDAL _transaksiDAL;
        public TransaksiController(IConfiguration config)
        {
             _config = config;
             _transaksiDAL = new TransaksiDAL(_config);
        }

        public IActionResult Post(TransaksiPenjualan jual){
            try{
                int noNota = _transaksiDAL.InsetTransaksi(jual);

                var myOutput = new MyOutput
                {
                    Status = "SUCCESS",
                    Pesan = $"Data Transaksi dengan no nota {noNota} berhasil ditambahkan"
                }; 

                return Ok(myOutput);
            }
            catch(Exception ex){
                var myOutput = new MyOutput
                {
                    Status = "ERROR",
                    Pesan = $"{ex.Message}"
                };
                return BadRequest(myOutput);
            }
        }
    }
}