using System.Collections.Generic;

namespace PusulaCCProject.WebUI.Models
{
    public class OperationResultOfObject
    {
        public bool state {  get; set; }
        public string message { get; set; }
        public List<Fishes> response { get; set; }
    }
}
