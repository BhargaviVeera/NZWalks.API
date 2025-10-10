using Microsoft.AspNetCore.Mvc;
using NZWalks.Web.Models;
using NZWalks.Web.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZWalks.Web.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string apiBaseUrl = "https://localhost:7239/api/Region";

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ---------------- LIST ALL REGIONS ----------------
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> regions = new();

            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(apiBaseUrl);

                response.EnsureSuccessStatusCode();
                regions.AddRange(await response.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return View(regions);
        }

        // ---------------- ADD REGION ----------------
        [HttpGet]
        public IActionResult AddRegion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(RegionViewModel model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var request = new HttpRequestMessage(HttpMethod.Post, apiBaseUrl)
                {
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding region: {ex.Message}");
            }

            return RedirectToAction("Index");
        }

        // ---------------- EDIT REGION ----------------
        [HttpGet]
        public async Task<IActionResult> EditRegion(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{apiBaseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var region = await response.Content.ReadFromJsonAsync<RegionDTO>();
            if (region == null)
                return RedirectToAction("Index");

            var model = new RegionViewModel
            {
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            ViewBag.RegionId = region.Id;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRegion(Guid id, RegionViewModel model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var request = new HttpRequestMessage(HttpMethod.Put, $"{apiBaseUrl}/{id}")
                {
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating region: {ex.Message}");
            }

            return RedirectToAction("Index");
        }

        // ---------------- DELETE REGION ----------------
        [HttpGet]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.DeleteAsync($"{apiBaseUrl}/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting region: {ex.Message}");
            }

            return RedirectToAction("Index");
        }
    }
}
