using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Scaffold-DbContext "Server=LAPTOP-1Q4C7KIH;Database=newData;Trusted_Connection=SSPi;Encrypt=false;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - Force
namespace online.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
