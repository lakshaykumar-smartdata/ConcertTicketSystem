using Microsoft.AspNetCore.Mvc;

namespace ConcertTicketSystem.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
