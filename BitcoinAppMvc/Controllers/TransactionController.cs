using Microsoft.AspNetCore.Mvc;

namespace BitcoinAppMvc.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
