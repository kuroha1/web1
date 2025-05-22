using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using online.Extention;
using online.Helper;
using online.Models;
using online.ModelView;
using System.Security.Claims;

namespace online.Controllers
{
	[Authorize]
	public class AccountsController : Controller
	{
		private readonly newDataContext _context;

		public AccountsController(newDataContext context)
		{
			_context = context;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult ValidatePhone(string phone)
		{
			try
			{
				var khachang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == phone.ToLower());
				if (khachang != null)
				{
					return Json(data: "Số điện thoại : " + phone + "đã tồn tại");
				}
				return Json(data: true);
			}
			catch
			{
				return Json(data: true);
			}
		}


		[HttpGet]
		[AllowAnonymous]
		public IActionResult ValidateEmail(string Email)
		{
			try
			{
				var khachang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
				if (khachang != null)
				{
					return Json(data: "Số điện thoại : " + Email + "đã tồn tại");
                }
				return Json(data: true);
			}
			catch
			{
				return Json(data: true);
			}
		}

		[Route("tai-khoan-cua-toi.html", Name = "Index")]
		public IActionResult Index()
		{
			var taikhoanID = HttpContext.Session.GetString("CustomersId");
			if (taikhoanID != null)
			{
				var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
				if (khachhang != null)
					return View(khachhang);
			}

			return RedirectToAction("DangNhap");
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("dang-ky.html", Name = "DangKy")]
		public IActionResult DangKyTaiKhoan()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("dang-ky.html", Name = "DangKy")]
		public async Task<IActionResult> DangKyTaiKhoan(RegisterViewModel taikhoan)
		{
			try
			{
				if (ModelState.IsValid)
				{
					string salt = Utilities.GetRamdonkey();
					Customer khachhang = new Customer
					{
						FullName = taikhoan.FullName,
						Phone = taikhoan.Phone.Trim().ToLower(),
						Email = taikhoan.Email.Trim().ToLower(),
						Password = (taikhoan.Password + salt.Trim()).ToMD5(),
						Active = true,
						Salt = salt,
						CreateDate = DateTime.Now
					};
					try
					{
						_context.Add(khachhang);
						await _context.SaveChangesAsync();
						//luu Session MaKh
						HttpContext.Session.SetString("CustomersId", khachhang.CustomerId.ToString());
						var TaikhoanId = HttpContext.Session.GetString("CustomersId");
						//Identity
						var claims = new List<Claim>
						{
							   new Claim(ClaimTypes.Name,khachhang.FullName),
							   new Claim("CustomersId",khachhang.CustomerId.ToString())
						};
						ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Login");
						ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
						await HttpContext.SignInAsync(claimsPrincipal);

						return RedirectToAction("Index", "Accounts");
					}
					catch
					{
						return RedirectToAction("DangKyTaiKhoan", "Accounts");
					}
				}
				else
				{
					return View(taikhoan);
				}
			}
			catch
			{
				return View(taikhoan);
			}
		}

		[AllowAnonymous]
		[Route("dang-nhap.html", Name = "DangNhap")]
		public IActionResult DangNhap(string returnUrl = null)
		{
			var taikhoanID = HttpContext.Session.GetString("CustomersId");
			if (taikhoanID != null)
			{
				return RedirectToAction("Index", "Accounts");
			}
			return View();
		}


        //[HttpPost]
        //[AllowAnonymous]
        //[Route("dang-nhap.html", Name = "DangNhap")]
        //public async Task<IActionResult> DangNhap(LoginViewModel Custom)
        //{
        //	try
        //	{
        //		if (ModelState.IsValid)
        //		{
        //			//bool isEmail = Utilities.IsValidEmail(Custom.UserName);
        //			//if (!isEmail) return View(Custom);

        //                  var account = _context.Accounts.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == Custom.UserName);
        //                  if (account != null)
        //                  {
        //                      return Redirect("Admin/Home/Index");
        //                  }

        //                  var kh = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == Custom.UserName);
        //			if (kh == null) return RedirectToAction("DangKyTaiKhoan");

        //			string password = (Custom.Password + kh.Salt.Trim()).ToMD5();
        //			if (Custom.Password != password)
        //			{
        //				ViewBag.Error = "+ Sai thong tin dang nhap!";
        //				return View(Custom);
        //			}
        //			//ktra active
        //			if (kh.Active == false) return RedirectToAction("thongbao", "Accounts");
        //                  Console.WriteLine(kh.CustomerId);
        //                  //luw session
        //                  HttpContext.Session.SetString("CustomersId", kh.CustomerId.ToString());
        //			var taikhoanID = HttpContext.Session.GetString("CustomersId");

        //			//identity
        //			var claims = new List<Claim>
        //			{
        //				new Claim(ClaimTypes.Name,kh.FullName),
        //				new Claim("CustomersId",kh.CustomerId.ToString())
        //			};
        //			ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "DangNhap");
        //			ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        //			await HttpContext.SignInAsync(claimsPrincipal);

        //			return RedirectToAction("Index", "Accounts");
        //		}
        //	}
        //	catch
        //	{
        //		return RedirectToAction("DangKyTaiKhoan", "Accounts");
        //	}
        //	return View(Custom);
        //}

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> DangNhap(LoginViewModel customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var account = _context.Accounts.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == customer.UserName);
                    if (account != null)
                    {
                        return Redirect("Admin/Home/Index");
                    }
                    var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == customer.UserName);
                    if (khachhang == null)
                    {
                        return View("DangKyTaiKhoan");
                    }
                    string pass = (customer.Password + khachhang.Salt.Trim()).ToMD5();
                    if (khachhang.Password != pass)
                    {
                        return View(customer);
                    }
                    // Kiểm tra xem Account có bị disable?
                    if (khachhang.Active == false) return RedirectToAction("ThongBao", "Accounts");
                    Console.WriteLine(khachhang.CustomerId);
                    // Lưu Session MaKh
                    HttpContext.Session.SetString("CustomerId", khachhang.CustomerId.ToString());
                    var taikhoanID = HttpContext.Session.GetString("CustomerId");
                    //Identity
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, khachhang.FullName),
                        new Claim("CustomerId", khachhang.CustomerId.ToString())
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);

                    return RedirectToAction("Index", "Accounts");
                }
            }
            catch
            {
                return RedirectToAction("DangKyTaiKhoan", "Accounts");
            }
            return View();
        }




        [HttpGet]
		[Route("dang-xuat.html", Name = "Logout")]
		public IActionResult Logout() 
		{
			HttpContext.SignOutAsync();
			HttpContext.Session.Remove("CustomersId");
			return RedirectToAction("Index", "Home");
		}

	}
}

