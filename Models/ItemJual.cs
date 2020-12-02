namespace LatihanBackend.Models
{
    public class ItemJual
    {
        public int ItemJualID { get; set; }
        public int NotaJualID { get; set; }
        public string KodeBarang { get; set; }
        public int Jumlah { get; set; }
        public decimal HargaJual { get; set; }
    }
}