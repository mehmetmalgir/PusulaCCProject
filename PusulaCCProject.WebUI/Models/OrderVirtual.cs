using System;

namespace PusulaCCProject.WebUI.Models
{
    public class OrderVirtual
    {
        public int orderId { get; set; }
        public DateTime orderDate { get; set; }
        public Fishes fish { get; set; }
    }
}
