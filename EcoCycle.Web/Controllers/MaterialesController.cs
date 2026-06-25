using System.Text.Json;
using EcoCycle.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.Web.Controllers
{
    public class MaterialesController : Controller
    {
        private readonly ApiService _apiService;

        public MaterialesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);
            var materiales = await _apiService.GetAsync<List<JsonElement>>("api/materiales");

            ViewBag.EsAdmin = HttpContext.Session.GetString("Rol") == "Admin";
            return View(materiales ?? new List<JsonElement>());
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
        public async Task<IActionResult> Create(string nombreMaterial, string? descripcion)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index");

            _apiService.SetToken(token);

            try
            {
                await _apiService.PostAsync("api/materiales", new { nombreMaterial, descripcion });
                TempData["Success"] = "Material creado exitosamente";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Error al crear el material";
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
                var material = await _apiService.GetAsync<JsonElement>($"api/materiales/{id}");
                return View(material);
            }
            catch
            {
                TempData["Error"] = "Material no encontrado";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int idMaterial, string nombreMaterial, string? descripcion)
        {
            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index");

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);

            try
            {
                await _apiService.PutAsync($"api/materiales/{idMaterial}", new
                {
                    idMaterial,
                    nombreMaterial,
                    descripcion
                });

                TempData["Success"] = "Material actualizado";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Error al actualizar el material";
                return RedirectToAction("Edit", new { id = idMaterial });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (HttpContext.Session.GetString("Rol") != "Admin")
                return RedirectToAction("Index");

            _apiService.SetToken(token);

            try
            {
                await _apiService.DeleteAsync($"api/materiales/{id}");
                TempData["Success"] = "Material eliminado";
            }
            catch
            {
                TempData["Error"] = "Error al eliminar el material";
            }

            return RedirectToAction("Index");
        }
    }
}
