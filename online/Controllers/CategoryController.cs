using Microsoft.AspNetCore.Mvc;

namespace online.Controllers
{
	public class CategoryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
