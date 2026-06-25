using System.Text.Json;
using EcoCycle.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.Web.Controllers
{
    public class ConfiguracionController : Controller
    {
        private readonly ApiService _apiService;

        public ConfiguracionController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index", "Dashboard");

            _apiService.SetToken(token);

            try
            {
                var configs = await _apiService.GetAsync<List<JsonElement>>("api/configuracion");
                return View(configs ?? new List<JsonElement>());
            }
            catch
            {
                TempData["Error"] = "Error al obtener la configuración";
                return View(new List<JsonElement>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int idConfiguracion, string valor)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index", "Dashboard");

            _apiService.SetToken(token);

            try
            {
                await _apiService.PutAsync($"api/configuracion/{idConfiguracion}",
                    new { idConfiguracion, valor });
                TempData["Success"] = "Configuración actualizada";
            }
            catch
            {
                TempData["Error"] = "Error al actualizar la configuración";
            }

            return RedirectToAction("Index");
        }
    }
}
