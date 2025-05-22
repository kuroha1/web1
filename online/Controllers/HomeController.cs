using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using online.Models;
using online.ModelView;
using System.Diagnostics;

namespace online.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly newDataContext _context;

		public HomeController(ILogger<HomeController> logger, newDataContext context)
		{
			_logger = logger;
			_context= context;
		}

		public IActionResult Index()
		{
			HomeViewVM model = new HomeViewVM();

			var lsproducts = _context.Products.AsNoTracking()
				.Where(x => x.Active == true && x.HomeFlag == true)
				.OrderByDescending(x => x.DateCreate)
				.ToList();

			List<ProductHomeVM> lsProductView = new List<ProductHomeVM>();

			var lsCats = _context.Categories.AsNoTracking()
				.Where(x => x.Published== true)
				.OrderByDescending(x=>x.Ordering)
				.ToList();	

			foreach(var item in lsCats)
			{
				ProductHomeVM productHome = new ProductHomeVM();
				productHome.category = item;
				productHome.lsProducts = lsproducts.Where(x => x.CatId == item.CatId).ToList();
				lsProductView.Add(productHome);
			}

			model.Products = lsProductView;
			ViewBag.AllProducts = lsproducts;
			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}