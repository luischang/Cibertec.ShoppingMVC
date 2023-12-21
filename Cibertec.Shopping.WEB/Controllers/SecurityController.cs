using Cibertec.Shopping.WEB.Models;
using Cibertec.Shopping.WEB.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Cibertec.Shopping.WEB.Controllers
{
    public class SecurityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel signInViewModel)
        {
            if (ModelState.IsValid)
            {
                var loginEndpoint = "http://localhost:5261/api/User/SignIn";
                using var httpClient = new HttpClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(signInViewModel)
                   , Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(loginEndpoint, jsonContent);

                if (response.IsSuccessStatusCode) 
                {
                    var userResult = await response.Content.ReadFromJsonAsync<UserResultViewModel>();

                    TempData.Put("UserResult", userResult);
                   

                    return RedirectToAction("Index", "Home");
                }            
                                  
                else               
                    ModelState.AddModelError(string.Empty, "Las credenciales son inválidas");               

            }
            return View(signInViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel) 
        {
            if (ModelState.IsValid)
            {
                var registerEndpoint = "http://localhost:5261/api/User";

                using var httpClient = new HttpClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(registerViewModel)
                    , Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(registerEndpoint, jsonContent);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Login", "Security");
                else
                    ModelState.AddModelError(string.Empty, "Error al registrar el usuario, inténtelo nuevamente");
            }

            return View();
        }
    }
}
