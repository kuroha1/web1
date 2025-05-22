using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using online.Extention;
using online.Helpers;
using online.Models;
using online.ModelView;
using System.Net.Http;

namespace online.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly newDataContext _context;


        public ShoppingCartController(newDataContext context)
        {
            _context = context;
        }

        public List<Cartitem> Giohang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<Cartitem>>("Giohang");
                if (gh == default(List<Cartitem>))
                {
                    gh = new List<Cartitem>();
                }
                return gh;
            }
        }

		[HttpPost]
		[Route("api/cart/add")]
		public IActionResult AddtoCart(int productID, int? amount)
		{

			List<Cartitem> giohang = Giohang;
			try
			{
				//them san pham vao gio hnag
				Cartitem item = Giohang.SingleOrDefault(p => p.sanpham.ProductId == productID);
				if (item != null)// neu da co thi cap nhat so luong 
				{
					item.amount = item.amount + amount.Value;
					HttpContext.Session.Set<List<Cartitem>>("Giohang", giohang);
				}
				else
				{
					Product hh = _context.Products.SingleOrDefault(p => p.ProductId == productID);
					item = new Cartitem
					{
						amount = amount.HasValue ? amount.Value : 1,
						sanpham = hh
					};
					giohang.Add(item);//them vao gio hang
				}

				//luu session
				HttpContext.Session.Set<List<Cartitem>>("Giohang", giohang);
				return Json(new { succsess = true });
			}
			catch(Exception ex)
			{
				return Json(new { succsess = false });
			}
		}



		[HttpPost]
		[Route("api/cart/update")]
		public IActionResult UpdateCart(int productID, int? amount)
		{
			var cart = HttpContext.Session.Get<List<Cartitem>>("Giohang");

			try
			{
				if (cart != null)
				{
					Cartitem item = cart.SingleOrDefault(p => p.sanpham.ProductId == productID);
					if (item != null && amount.HasValue)
					{
						item.amount = amount.Value;
					}
					HttpContext.Session.Set<List<Cartitem>>("Giohang", cart);
				}
				return Json(new { succsess = true });
			}
			catch
			{
				return Json(new { succsess = false });
			}
		}


		[HttpPost]
		[Route("api/cart/remove")]
		public IActionResult Remove(int productID)
		{
			try
			{
				List<Cartitem> giohang = Giohang;
				Cartitem item = giohang.SingleOrDefault(p => p.sanpham.ProductId == productID);
				if (item != null)
				{
					giohang.Remove(item);
				}
				//luu session
				HttpContext.Session.Set<List<Cartitem>>("Giohang", giohang);
				return Json(new { succsess = true });
			}
			catch
			{
				return Json(new { succsess = false });
			}
		}

		[Route("cart.html", Name = "Cart")]
		public IActionResult Index()
		{
			//List<int> lsProductID = new List<int>();
			//var lsGiohang = Giohang;
			//foreach( var item in lsGiohang)
			//{
			//    lsProductID.Add(item.sanpham.ProductId);
			//}
			return View(Giohang);
		}
	}
}
