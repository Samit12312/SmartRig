namespace Models
{
    public class ComputerCatalogViewModel
    {
        public int Id { get; set; }   // internal use (details, cart)

        public string ComputerName { get; set; }
        public string ComputerPicture { get; set; }

        public string Cpu { get; set; }
        public string Gpu { get; set; }
        public string Ram { get; set; }
        public string Storage { get; set; }
        public string OperatingSystem { get; set; }

        public int Price { get; set; }
    }
}
