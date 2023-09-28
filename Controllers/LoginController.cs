using Microsoft.AspNetCore.Mvc;

namespace TechOilFrontend.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}
		public IActionResult Ingresar()
		{
			return View();
		}
	}
}
