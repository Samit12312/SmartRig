namespace Models
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public string Date { get; set; }
        public bool IsPayed { get; set; }
        public List<CartComputer> Computers { get; set; }
        public int Total { get; set; }
    }
}