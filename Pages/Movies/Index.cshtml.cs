using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razor08.efcore.Data;
using razorwebapp_sql.Models;

namespace razorwebapp_sql.Pages_Movies
{
    public class IndexModel : PageModel
    {
        private readonly razor08.efcore.Data.ArticleContext _context;

        public IndexModel(razor08.efcore.Data.ArticleContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        public const int ITEMS_PER_PAGE = 10;
        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage {get; set;}
        public int countPages {get; set;}


        public async Task OnGetAsync(string SearchString)
        {
           // Article = await _context.Article.ToListAsync();

           int totalArticle = await _context.Article.CountAsync();
           countPages = (int)Math.Ceiling((double) totalArticle / ITEMS_PER_PAGE);

           if(currentPage < 1)
                currentPage = 1;
            if(currentPage > countPages)
                currentPage = countPages;

            var qr = (from a in _context.Article
                    orderby a.PublishDate descending  // giam dan
                    select a)
                    .Skip((currentPage - 1) * 10)
                    .Take(ITEMS_PER_PAGE);
            
            if(!string.IsNullOrEmpty(SearchString)){
                Article = qr.Where(a => a.Title.Contains(SearchString)).ToList();
            }
            else{
                Article = await qr.ToListAsync();
            }
            
        }
    }
}
