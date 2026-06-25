using System.Text.Json;
using EcoCycle.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApiService _apiService;

        public DashboardController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
                return RedirectToAction("Login", "Auth");

            try
            {
                var token = HttpContext.Session.GetString("Token")!;
                _apiService.SetToken(token);

                var materiales = await _apiService.GetAsync<List<JsonElement>>("api/materiales");
                var publicaciones = await _apiService.GetAsync<List<JsonElement>>("api/publicaciones");
                var centros = await _apiService.GetAsync<List<JsonElement>>("api/centrosreciclaje");

                ViewBag.TotalMateriales = materiales?.Count ?? 0;
                ViewBag.TotalPublicaciones = publicaciones?.Count ?? 0;
                ViewBag.TotalCentros = centros?.Count ?? 0;

                var esAdmin = HttpContext.Session.GetString("Rol") == "Admin";
                ViewBag.EsAdmin = esAdmin;

                if (esAdmin)
                {
                    var reporte = await _apiService.GetAsync<JsonElement>("api/reportes/general");
                    if (reporte.ValueKind != JsonValueKind.Undefined)
                    {
                        ViewBag.TotalUsuarios = reporte.GetProperty("totalUsuarios").GetInt32();
                        ViewBag.TotalPuntos = reporte.GetProperty("totalPuntos").GetInt32();
                    }
                }
                else
                {
                    var idUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario")!);
                    var puntos = await _apiService.GetAsync<JsonElement>(
                        $"api/recompensas/usuario/{idUsuario}");
                    if (puntos.ValueKind != JsonValueKind.Undefined)
                        ViewBag.TotalPuntosUsuario = puntos.GetProperty("totalPuntos").GetInt32();
                }

                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
