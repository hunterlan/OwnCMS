using Microsoft.AspNetCore.Mvc;
using OwnCMS.Application.Features.Articles;

namespace OwnCMS.Presentation.Controllers;

public class HomeController(IArticleService articleService) : Controller
{
    // GET
    public IActionResult Index()
    {
        var articles = articleService.GetAll();
        return View(articles);
    }

    public IActionResult About()
    {
        return View();
    }
}