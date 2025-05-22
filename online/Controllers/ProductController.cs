using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using online.Extention;
using online.Models;
using online.ModelView;
using PagedList.Core;

namespace online.Controllers
{
    public class ProductController : Controller
    {
        private readonly newDataContext _context;

        public ProductController(newDataContext context)
        {
            _context = context;
        }

        [Route("Shop.html", Name = "ShopProduct")]
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;
                var IsTintucs = _context.Products
                    .AsNoTracking()
                    .OrderByDescending(x => x.DateCreate);// xap xep theo ngay tao
                PagedList<Product> models = new PagedList<Product>(IsTintucs, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch { return RedirectToAction("Index", "Home"); }


        }

        //[Route("/{Alias}", Name = "ListProduct")]
        //public IActionResult List(string Alias, int page = 1)
        //{
        //    try
        //    {
        //        var pageSize = 10;
        //        var danhmuc = _context.Categories.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
        //        var IsTintucs = _context.Products
        //            .AsNoTracking()
        //            .Where(x => x.CatId == danhmuc.CatId)
        //            .OrderByDescending(x => x.DateCreate);
        //        PagedList<Product> models = new PagedList<Product>(IsTintucs, page, pageSize);
        //        ViewBag.CurrentPage = page;
        //        ViewBag.CurrentCat = danhmuc;
        //        return View(models);
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }


        //}

        [Route("/{Alias}-{id}.html", Name = "ProductDetails")]
        public IActionResult Detail(int id)
        {
            try
            {
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }

                var lsProducts = _context.Products.AsNoTracking()
                    .Where(x => x.CatId == product.CatId && x.ProductId != id && x.Active == true)
                      .OrderByDescending(x => x.DateCreate)
                    .Take(4)
                    .ToList();

                ViewBag.SanPham = lsProducts;
                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }



        public async Task<IActionResult> ProductByCategory(int cateID)
        {
            var newDataContext = _context.Products.Include(p => p.Cat).Where(p => p.CatId == cateID);
            return View(await newDataContext.ToListAsync());
        }
	}
}
