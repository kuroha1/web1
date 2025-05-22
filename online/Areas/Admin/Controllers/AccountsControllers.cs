using Microsoft.AspNetCore.Mvc;
using online.Models;

namespace online.Areas.Admin.Controllers
{
    public class AccountsControllers : Controller
    {

        private readonly newDataContext _context;

        public AccountsControllers(newDataContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
