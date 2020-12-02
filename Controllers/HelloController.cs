using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace LatihanBackend.Controllers
{
    
    //set class sebagai api controller
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        //GET,POST,PUT,DELETE
        [HttpGet]
        public List<string> Get(){
            var listNama = new List<string>{
                "Erick","Budi","Bambang","Joko","Ani"
            };
            System.Console.WriteLine($"{listNama[0]}");
            return listNama;
        }
    }
}