using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using PusulaCCProject.WebUI.Models;
using System;
using System.Linq;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace PusulaCCProject.WebUI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {

            HttpClient client = new HttpClient();            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://process.pusulacc.com.tr/testapi/api/Order/GetOrders");
            var stringToken = HttpContext.Session.GetString("token").ToString().Replace("\"", "");
            var keyOfApi = "X-Access-Token";
            request.Headers.Add(keyOfApi, stringToken);
                       
            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var objectResponse = response.Content.ReadAsStringAsync().Result;            
            var responseModel = JsonConvert.DeserializeObject<OperationResultOfListOfOrderVirtual>(objectResponse);
            var viewModel = responseModel.response;
            var orderedModel = viewModel.OrderByDescending(x => x.orderDate).ToList();
            return View(orderedModel);
        }

        [HttpPost]
        public IActionResult AddOrder(int id)
        {
            HttpClient client = new HttpClient();
            string strUrl = "https://process.pusulacc.com.tr/testapi/api/Order/AddOrder?fishId=";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{strUrl}+{id}");
            var stringToken = HttpContext.Session.GetString("token").ToString().Replace("\"", "");
            var keyOfApi = "X-Access-Token";
            request.Headers.Add(keyOfApi, stringToken);

            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var objectResponse = response.Content.ReadAsStringAsync().Result;
            var addedOrderModel = JsonConvert.DeserializeObject<OperationResultOfLoginResponse>(objectResponse);
            
            if(addedOrderModel.state == true)
            {
                return Json(new { isSuccess = true, message = "Siparişiniz alındı..." });
            }

            return Json(new { isSuccess = false, message = "Sipariş oluşturulamadı..." });
        }







    }
}
