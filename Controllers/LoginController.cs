using Data.Base;
using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace TechOilFrontend.Controllers
{
	public class LoginController : Controller
	{
		private readonly System.Net.Http.IHttpClientFactory _httpClientFactory;
        public LoginController(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }
        public IActionResult Login()
		{
			return View();
		}
		public async Task<IActionResult> Ingresar(LoginDto login)
		{
			var baseApi = new BaseApi(_httpClientFactory);
			var token = await baseApi.PostToApi("Login", login);
			var resultadoLogin = token as OkObjectResult;
			var resultadoObjeto = JsonConvert.DeserializeObject<Models.Login>(resultadoLogin.Value.ToString());


			var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
			Claim claimNombre = new(ClaimTypes.Name, resultadoObjeto.Name);
			Claim claimRole = new(ClaimTypes.Role, "1"); //modificar api para que me devuelva el role
			Claim claimEmail = new(ClaimTypes.Email, resultadoObjeto.Email);

			identity.AddClaim(claimNombre);
			identity.AddClaim(claimRole);
			identity.AddClaim(claimEmail);

			var claimPrincipal = new ClaimsPrincipal(identity);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties
			{
				ExpiresUtc = DateTime.Now.AddHours(1)
			});
			 
            return View("~/Views/Home/Index.cshtml", resultadoObjeto);
		}

		public async Task<IActionResult> CerrarSesion()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Login");
		}
	}
}
