namespace PusulaCCProject.WebUI.Models
{
    public class OperationResultOfLoginResponse
    {
        public bool state { get; set; }
        public string message { get; set; }
        public LoginResponseModel LoginResponseModel { get; set; }

    }
}
