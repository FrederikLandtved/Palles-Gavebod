using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PallesGavebodWeb.Models;
using PallesGavebodAPI1.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace PallesGavebodWeb.Controllers
{
	public class GiftController : Controller
	{
		private readonly GiftDbContext _context;
		private readonly HttpClient _httpClient;
		private Uri BaseEndPoint { get; set; }
		private Uri BaseEndPointGirls { get; set; }
		private Uri BaseEndPointBoys { get; set; }

		public GiftController(GiftDbContext context)
		{
			BaseEndPoint = new Uri("http://localhost:56852/api/gifts");
			BaseEndPointGirls = new Uri("http://localhost:56852/api/gifts/girls");
			BaseEndPointBoys = new Uri("http://localhost:56852/api/gifts/boys");
			_context = context;
			_httpClient = new HttpClient();
		}

		public async Task<IActionResult> Index()
		{
			var response = await _httpClient.GetAsync(BaseEndPoint, HttpCompletionOption.ResponseHeadersRead);
			response.EnsureSuccessStatusCode();
			var data = await response.Content.ReadAsStringAsync();
			return View(JsonConvert.DeserializeObject<List<Gift>>(data));
		}

		public async Task<IActionResult> GirlGifts()
		{
			var response = await _httpClient.GetAsync(BaseEndPointGirls, HttpCompletionOption.ResponseHeadersRead);
			response.EnsureSuccessStatusCode();
			var data = await response.Content.ReadAsStringAsync();
			return View(JsonConvert.DeserializeObject<List<Gift>>(data));
		}

		public async Task<IActionResult> BoyGifts()
		{
			var response = await _httpClient.GetAsync(BaseEndPointBoys, HttpCompletionOption.ResponseHeadersRead);
			response.EnsureSuccessStatusCode();
			var data = await response.Content.ReadAsStringAsync();
			return View(JsonConvert.DeserializeObject<List<Gift>>(data));
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Gift gift)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:56852/api/gifts");

				var postTask = client.PostAsJsonAsync<Gift>("gifts", gift);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
			}

			ModelState.AddModelError(string.Empty, "Fejl.");

			return View(gift);
		}


	}
}
