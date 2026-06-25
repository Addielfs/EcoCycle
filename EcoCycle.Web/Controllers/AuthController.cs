using System.Text.Json;
using EcoCycle.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;

        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contrasena)
        {
            try
            {
                var result = await _apiService.PostAsync<object, JsonElement>(
                    "api/auth/login", new { correo, contrasena });

                var token = result.GetProperty("token").GetString()!;
                var idUsuario = result.GetProperty("idUsuario").GetInt32();
                var correoUsuario = result.GetProperty("correo").GetString()!;
                var rol = result.GetProperty("rol").GetString()!;

                HttpContext.Session.SetString("Token", token);
                HttpContext.Session.SetString("IdUsuario", idUsuario.ToString());
                HttpContext.Session.SetString("Correo", correoUsuario);
                HttpContext.Session.SetString("Rol", rol);

                _apiService.SetToken(token);

                TempData["Success"] = "Inicio de sesión exitoso";
                return RedirectToAction("Index", "Dashboard");
            }
            catch (HttpRequestException)
            {
                TempData["Error"] = "Credenciales inválidas";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string correo, string contrasena, string? telefono, string? direccion)
        {
            try
            {
                var result = await _apiService.PostAsync<object, JsonElement>(
                    "api/auth/register", new { correo, contrasena, telefono, direccion });

                var token = result.GetProperty("token").GetString()!;
                var idUsuario = result.GetProperty("idUsuario").GetInt32();
                var correoUsuario = result.GetProperty("correo").GetString()!;
                var rol = result.GetProperty("rol").GetString()!;

                HttpContext.Session.SetString("Token", token);
                HttpContext.Session.SetString("IdUsuario", idUsuario.ToString());
                HttpContext.Session.SetString("Correo", correoUsuario);
                HttpContext.Session.SetString("Rol", rol);

                _apiService.SetToken(token);

                TempData["Success"] = "Registro exitoso";
                return RedirectToAction("Index", "Dashboard");
            }
            catch (HttpRequestException)
            {
                TempData["Error"] = "Error al registrarse. Verifica los datos.";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
