using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

public class LibraryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Search()
    {
        return View();
    }

    public IActionResult AddMedia()
    {
        return View();
    }
}