using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PusulaCCProject.WebUI.Models;
using PusulaCCProject.WebUI.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;

namespace PusulaCCProject.WebUI.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(LoginRegisterModel loginModel)
        {
            LoginRegisterModelValidator validator = new LoginRegisterModelValidator();
            FluentValidation.Results.ValidationResult validationResult = validator.Validate(loginModel);

            if (validationResult.IsValid)
            {
                HttpClient client = new HttpClient();

                string jsonContent = JsonConvert.SerializeObject(loginModel);
                HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var request = client.PostAsync("https://process.pusulacc.com.tr/testapi/api/User/Login", httpContent).Result;
                var response = request.Content.ReadAsStringAsync().Result;

                var responseModel = JsonConvert.DeserializeObject<OperationResultOfLoginResponse>(response);

                if(responseModel.state == true)
                {
                    string stringAccessToken = JsonConvert.SerializeObject(responseModel.response.accessToken);
                    HttpContext.Session.SetString("token", stringAccessToken);
                    return Json(new { isSuccess = true });
                }
                else                
                    return Json(new { isSuccess = false, Message = "Kullanıcı Bulunamadı!" });

                      

            }
            else
            {
                string errorMessages = "<div class = 'alert alert-warning'>";
                foreach (ValidationFailure item in validationResult.Errors)
                {
                    errorMessages += $"<div>{item.ErrorMessage}</div>";
                }

                errorMessages += "</div>";

                return Json(new { isSuccess = false, Message = errorMessages });
            }


        }

        [HttpPost]
        public IActionResult Register(LoginRegisterModel loginRegisterModel)
        {
            LoginRegisterModelValidator validator = new LoginRegisterModelValidator();
            FluentValidation.Results.ValidationResult result = validator.Validate(loginRegisterModel);

            if (result.IsValid)
            {
                HttpClient client = new HttpClient();

                string jsonContent = JsonConvert.SerializeObject(loginRegisterModel);
                HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var request = client.PostAsync("https://process.pusulacc.com.tr/testapi/api/User/Register", httpContent).Result;
                var response = request.Content.ReadAsStringAsync().Result;

                var responseModel = JsonConvert.DeserializeObject<OperationResultOfLoginResponse>(response);

                if (responseModel.state == true)
                {                   
                    return Json(new { isSuccess = true });
                }
                else
                    return Json(new { isSuccess = false, Message = "Bu kullanıcı adı ile bir kullanıcı mevcut" });

            }
            else 
            {
                string errorMessages = "<div class = 'alert alert-warning'>";
                foreach (ValidationFailure item in result.Errors)
                {
                    errorMessages += $"<div>{item.ErrorMessage}</div>";
                }

                errorMessages += "</div>";

                return Json(new { isSuccess = false, Message = errorMessages });
            }




        }



    }
}
