using System.Text.Json;
using EcoCycle.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.Web.Controllers
{
    public class PublicacionesController : Controller
    {
        private readonly ApiService _apiService;

        public PublicacionesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);
            var publicaciones = await _apiService.GetAsync<List<JsonElement>>("api/publicaciones");

            ViewBag.IdUsuario = HttpContext.Session.GetString("IdUsuario");
            ViewBag.EsAdmin = HttpContext.Session.GetString("Rol") == "Admin";
            return View(publicaciones ?? new List<JsonElement>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);
            var materiales = await _apiService.GetAsync<List<JsonElement>>("api/materiales");
            ViewBag.Materiales = materiales ?? new List<JsonElement>();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int idMaterial, string? descripcion, decimal cantidad, string? ubicacion, decimal? latitud, decimal? longitud, IFormFile? imagenFile)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);
            var idUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario")!);

            try
            {
                var result = await _apiService.PostAsync<object, JsonElement>("api/publicaciones", new
                {
                    idUsuario,
                    idMaterial,
                    descripcion,
                    cantidad,
                    ubicacion,
                    latitud,
                    longitud
                });

                if (imagenFile != null && imagenFile.Length > 0)
                {
                    var idPublicacion = result.GetProperty("idPublicacion").GetInt32();
                    await _apiService.PostMultipartAsync($"api/publicaciones/{idPublicacion}/imagen", imagenFile);
                }

                TempData["Success"] = "Publicación creada exitosamente";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Error al crear la publicación";
                var materiales = await _apiService.GetAsync<List<JsonElement>>("api/materiales");
                ViewBag.Materiales = materiales ?? new List<JsonElement>();
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            _apiService.SetToken(token);

            try
            {
                await _apiService.DeleteAsync($"api/publicaciones/{id}");
                TempData["Success"] = "Publicación eliminada";
            }
            catch
            {
                TempData["Error"] = "Error al eliminar la publicación";
            }

            return RedirectToAction("Index");
        }
    }
}
