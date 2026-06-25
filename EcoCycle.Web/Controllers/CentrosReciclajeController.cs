using System.Text.Json;
using EcoCycle.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.Web.Controllers
{
    public class CentrosReciclajeController : Controller
    {
        private readonly ApiService _apiService;

        public CentrosReciclajeController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);
            var centros = await _apiService.GetAsync<List<JsonElement>>("api/centrosreciclaje");

            ViewBag.EsAdmin = HttpContext.Session.GetString("Rol") == "Admin";
            return View(centros ?? new List<JsonElement>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
                return RedirectToAction("Login", "Auth");

            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string nombreCentro, string? direccion, decimal? latitud, decimal? longitud, string? telefono, int capacidad)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index");

            _apiService.SetToken(token);

            try
            {
                await _apiService.PostAsync("api/centrosreciclaje", new
                {
                    nombreCentro,
                    direccion,
                    latitud,
                    longitud,
                    telefono,
                    capacidad
                });

                TempData["Success"] = "Centro de reciclaje creado exitosamente";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Error al crear el centro";
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
                var centro = await _apiService.GetAsync<JsonElement>($"api/centrosreciclaje/{id}");
                return View(centro);
            }
            catch
            {
                TempData["Error"] = "Centro no encontrado";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int idCentro, string nombreCentro, string? direccion, decimal? latitud, decimal? longitud, string? telefono, int capacidad)
        {
            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index");

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);

            try
            {
                await _apiService.PutAsync($"api/centrosreciclaje/{idCentro}", new
                {
                    idCentro,
                    nombreCentro,
                    direccion,
                    latitud,
                    longitud,
                    telefono,
                    capacidad
                });

                TempData["Success"] = "Centro actualizado";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Error al actualizar el centro";
                return RedirectToAction("Edit", new { id = idCentro });
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
                await _apiService.DeleteAsync($"api/centrosreciclaje/{id}");
                TempData["Success"] = "Centro eliminado";
            }
            catch
            {
                TempData["Error"] = "Error al eliminar el centro";
            }

            return RedirectToAction("Index");
        }
    }
}
