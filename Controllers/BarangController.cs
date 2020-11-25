using System.Collections.Generic;
using LatihanBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace LatihanBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarangController: ControllerBase
    {
        private List<Barang> lstBarang;
        public BarangController()
        {
            var barang1 = new Barang {
             KodeBarang="KK123",
             NamaBarang="Keyboard Logitech AA",
             Stok = 10,
             HargaBeli = 500000,
             HargaJual = 550000
            };
            
            var barang2 = new Barang{
                KodeBarang = "KK222",
                NamaBarang = "Keyboard 222",
                Stok = 20,
                HargaBeli = 750000,
                HargaJual = 800000
            };
            lstBarang = new List<Barang>();
            lstBarang.Add(barang1);
            lstBarang.Add(barang2);
        }

        [HttpGet]
        public IEnumerable<Barang> Get(){
            return lstBarang;
        }
    }
}