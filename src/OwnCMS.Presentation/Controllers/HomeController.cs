using Microsoft.AspNetCore.Mvc;
using OwnCMS.Application.Features.Articles;

namespace OwnCMS.Presentation.Controllers;

public class HomeController(IArticleService articleService) : Controller
{
    // GET
    public IActionResult Index()
    {
        var articles = articleService.GetAll();
        ViewBag.CategorizedArticles = articleService.GetArticlesGroupedByCategory().ToList();
        ViewBag.UncategorizedArticles = articleService.GetUncategorizedArticles().ToList();
        return View(articles);
    }

    public IActionResult About()
    {
        ViewBag.CategorizedArticles = articleService.GetArticlesGroupedByCategory().ToList();
        ViewBag.UncategorizedArticles = articleService.GetUncategorizedArticles().ToList();
        return View();
    }

    [Route("article/{slug}")]
    public IActionResult Article(string slug)
    {
        var article = articleService.GetBySlug(slug);
        if (article == null)
        {
            return NotFound();
        }

        ViewBag.CategorizedArticles = articleService.GetArticlesGroupedByCategory().ToList();
        ViewBag.UncategorizedArticles = articleService.GetUncategorizedArticles().ToList();
        return View(article);
    }
}