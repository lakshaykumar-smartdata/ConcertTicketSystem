using Microsoft.AspNetCore.Mvc;

namespace ConcertTicketSystem.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
