using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using online.Extention;
using online.ModelView;

namespace online.Controllers.Components
{
    public class HeaderCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<Cartitem>>("Giohang");
            return View(cart);
        }
    }
}
