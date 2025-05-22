using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using online.Extention;
using online.ModelView;

namespace online.Controllers.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<Cartitem>>("Giohang");
            //int soluongsanpham = 0;
            //if(cart != null)
            //{
            //    soluongsanpham = cart.Count();
            //}
            return View(cart);
        }
    }
}
