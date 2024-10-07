using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor08.efcore.Data;

namespace razorwebapp_sql.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ArticleContext articleContext;

    public IndexModel(ILogger<IndexModel> logger, ArticleContext _articleContext)
    {
        _logger = logger;
        articleContext = _articleContext;
    }

    public void OnGet()
    {
        var posts = (from a in articleContext.Article
                orderby a.PublishDate descending
                select a).ToList();

                ViewData["posts"] = posts;
    }
}
