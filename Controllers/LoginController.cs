using Data.Base;
using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
			ViewBag.Nombre = resultadoObjeto.Name;
			return View("~/Views/Home/Index.cshtml", resultadoObjeto);
		}
	}
}
