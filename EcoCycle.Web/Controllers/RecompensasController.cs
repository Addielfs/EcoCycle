using System.Text.Json;
using EcoCycle.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.Web.Controllers
{
    public class RecompensasController : Controller
    {
        private readonly ApiService _apiService;

        public RecompensasController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);
            var esAdmin = HttpContext.Session.GetString("Rol") == "Admin";

            try
            {
                if (esAdmin)
                {
                    var todas = await _apiService.GetAsync<List<JsonElement>>("api/recompensas");
                    ViewBag.EsAdmin = true;
                    return View("AdminIndex", todas ?? new List<JsonElement>());
                }
                else
                {
                    var idUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario")!);
                    var puntos = await _apiService.GetAsync<JsonElement>(
                        $"api/recompensas/usuario/{idUsuario}");
                    ViewBag.EsAdmin = false;
                    return View("UserIndex", puntos);
                }
            }
            catch
            {
                TempData["Error"] = "Error al obtener las recompensas";
                return View(esAdmin ? "AdminIndex" : "UserIndex");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int idUsuario, int puntos)
        {
            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index");

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);

            try
            {
                await _apiService.PostAsync("api/recompensas", new { idUsuario, puntos });
                TempData["Success"] = "Recompensa creada exitosamente";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Error al crear la recompensa";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index");

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);

            try
            {
                var todas = await _apiService.GetAsync<List<JsonElement>>("api/recompensas");
                var recompensa = todas?.FirstOrDefault(r =>
                    r.GetProperty("idRecompensa").GetInt32() == id);
                if (recompensa == null)
                {
                    TempData["Error"] = "Recompensa no encontrada";
                    return RedirectToAction("Index");
                }
                return View(recompensa);
            }
            catch
            {
                TempData["Error"] = "Error al obtener la recompensa";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int idRecompensa, int puntos)
        {
            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index");

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);

            try
            {
                await _apiService.PutAsync($"api/recompensas/{idRecompensa}",
                    new { idRecompensa, puntos });
                TempData["Success"] = "Recompensa actualizada";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Error al actualizar la recompensa";
                return RedirectToAction("Edit", new { id = idRecompensa });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index");

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);

            try
            {
                await _apiService.DeleteAsync($"api/recompensas/{id}");
                TempData["Success"] = "Recompensa eliminada";
            }
            catch
            {
                TempData["Error"] = "Error al eliminar la recompensa";
            }

            return RedirectToAction("Index");
        }
    }
}
