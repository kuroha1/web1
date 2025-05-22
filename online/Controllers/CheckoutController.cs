using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using online.Extention;
using online.Helper;
using online.Models;
using online.ModelView;

namespace online.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly newDataContext _context;

        public CheckoutController(newDataContext context)
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
        [HttpGet]
        [Route("checkout.html", Name = "Checkout")] 
        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<Cartitem>>("Giohang");
            var taiKhoanID = HttpContext.Session.GetString("CustomerId");
            MuaHangVM model = new MuaHangVM();
            if (taiKhoanID != null)
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taiKhoanID));
                model.CustomerId = khachhang.CustomerId;
                model.FullName = khachhang.FullName;
                model.Email = khachhang.Email;
                model.Phone = khachhang.Phone;
                model.Address = khachhang.Address;
            }
            ViewBag.Giohang = cart;
            return View(model);
        }
        [HttpPost]
        [Route("checkout.html", Name = "Checkout")]
        public IActionResult Index(MuaHangVM muaHang)
        {
            Console.WriteLine("aaaa");
            var cart = HttpContext.Session.Get<List<Cartitem>>("Giohang");
            var taiKhoanID = HttpContext.Session.GetString("CustomerId");
            MuaHangVM model = new MuaHangVM();
            Console.WriteLine(model.Address);
            if (taiKhoanID != null)
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taiKhoanID));
                model.CustomerId = khachhang.CustomerId;
                model.FullName = khachhang.FullName;
                model.Email = khachhang.Email;
                model.Phone = khachhang.Phone;
                model.Address = khachhang.Address;
                khachhang.Address = muaHang.Address;
                _context.Update(khachhang);
                _context.SaveChanges();
            }

            try
            {
                if (ModelState.IsValid)
                { 
                    Order donhang = new Order();
                    donhang.CustomerId = model.CustomerId; 
                    donhang.OrderDate = DateTime.Now;
                    donhang.TransactStatusId = 2;
                    donhang.Deleted = false;
                    donhang.Paid = false;
                    donhang.Note = Utilities.StripHTML(model.Note);
                    
                    _context.Add(donhang);
                    _context.SaveChanges();

                    foreach (var item in cart)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderId = donhang.OrderId;
                        orderDetail.ProductId = item.sanpham.ProductId;
                        orderDetail.Total = item.sanpham.Price;
                        _context.Add(orderDetail);
                    }
                    _context.SaveChanges();
                    HttpContext.Session.Remove("Giohang");
                    return RedirectToAction("Index", "Product");
                }
            }
            catch
            {
                Console.WriteLine("ddddd");
                ViewBag.Giohang = cart;
                return View(model);
            }
            ViewBag.Giohang = cart;
            return View(model);
        }
    }
}