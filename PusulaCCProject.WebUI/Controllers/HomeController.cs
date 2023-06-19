using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PusulaCCProject.WebUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PusulaCCProject.WebUI.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		
		public IActionResult Index()
		{
			var client = new HttpClient();

			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			client.DefaultRequestHeaders.Add("X-Access-Token", HttpContext.Session.GetString("token").ToString());
			

			var request = client.GetAsync("https://process.pusulacc.com.tr/testapi/api/Fish/GetFishes").Result;
			var response = request.Content.ReadAsStringAsync().Result;

			var viewModel = JsonConvert.DeserializeObject<List<Fishes>>(response);

			return View(viewModel);
		}

		
	}
}
