using System.Collections.Generic;

namespace LatihanBackend.Models
{
    public class TransaksiPenjualan
    {
        public NotaJual Nota { get; set; }
        public IEnumerable<ItemJual> Items { get; set; }
    }
}