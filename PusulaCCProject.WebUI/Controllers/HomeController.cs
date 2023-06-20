using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PusulaCCProject.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace PusulaCCProject.WebUI.Controllers
{

    public class HomeController : Controller
    {

        [Authorize]
        public IActionResult Index()
        {            
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://process.pusulacc.com.tr/testapi/api/Fish/GetFishes");
            var stringToken = HttpContext.Session.GetString("token").ToString().Replace("\"","");             
            var keyOfApi = "X-Access-Token";          
            request.Headers.Add(keyOfApi, stringToken);
            
            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var objectResponse = response.Content.ReadAsStringAsync().Result;
            var viewModel = JsonConvert.DeserializeObject<OperationResultOfObject>(objectResponse);
            
            return View(viewModel.response);

        }


    }
}
