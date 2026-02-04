using System;
using System.Collections.Generic;

namespace Models
{
    public class OrderHistoryViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
    }

    public class OrderViewModel
    {
        public int CartId { get; set; }
        public string Date { get; set; }
        public List<OrderItemViewModel> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderItemViewModel
    {
        public int ComputerId { get; set; }
        public string ComputerName { get; set; }
        public string ComputerPicture { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}