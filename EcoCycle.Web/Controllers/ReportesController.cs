using System.Text.Json;
using EcoCycle.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.Web.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApiService _apiService;

        public ReportesController(ApiService apiService)
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
                var reporte = await _apiService.GetAsync<JsonElement>("api/reportes/general");
                return View(reporte);
            }
            catch
            {
                TempData["Error"] = "Error al obtener el reporte";
                return View();
            }
        }
    }
}
