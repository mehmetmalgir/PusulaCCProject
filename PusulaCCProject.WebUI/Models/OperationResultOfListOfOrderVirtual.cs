using System.Collections.Generic;

namespace PusulaCCProject.WebUI.Models
{
    public class OperationResultOfListOfOrderVirtual
    {
        public bool state { get; set; }
        public string message { get; set; }
        public List<OrderVirtual> response { get; set; }
    }
}
