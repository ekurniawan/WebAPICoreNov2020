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
                _transaksiDAL.InsetTransaksi(jual);
                return Ok("Transaksi berhasil ditambahkan");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}