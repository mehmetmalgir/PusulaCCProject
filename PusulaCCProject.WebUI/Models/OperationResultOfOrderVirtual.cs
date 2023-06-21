using System.Collections.Generic;

namespace PusulaCCProject.WebUI.Models
{
    public class OperationResultOfOrderVirtual
    {
        public bool state { get; set; }
        public string message { get; set; }
        public OrderVirtual response { get; set; }
    }
}
