using System;
using System.Collections.Generic;
using System.Linq;
using LatihanBackend.DAL;
using LatihanBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LatihanBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarangController: ControllerBase
    {

        private IConfiguration _config;
        private BarangDAL barangDAL;

        public BarangController(IConfiguration config)
        {
            _config = config;
            barangDAL = new BarangDAL(_config);
        }

        [HttpGet]
        public IEnumerable<Barang> Get(){
            var results = barangDAL.GetAll();
            return results;
        }

        [HttpGet("{KodeBarang}")]
        public Barang Get(string KodeBarang){
            var result = barangDAL.GetByKode(KodeBarang);
            return result;
        }

        [HttpPost]
        public IActionResult Post(Barang barang){
            try
            {
                barangDAL.Insert(barang);
                return Ok($"Data Barang dengan kode {barang.KodeBarang} berhasil ditambah");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{KodeBarang}")]
        public IActionResult Put(string KodeBarang,Barang barang){
            try
            {
                barangDAL.Update(KodeBarang,barang);
                return Ok($"Barang dengan kode {KodeBarang} berhasil diupdate");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{KodeBarang}")]
        public IActionResult Delete(string KodeBarang){
            try
            {
                barangDAL.Delete(KodeBarang);
                return Ok($"Barang Kode {KodeBarang} berhasil dihapus");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}