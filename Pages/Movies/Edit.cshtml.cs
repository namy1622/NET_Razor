using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using razor08.efcore.Data;
using razorwebapp_sql.Models;

namespace razorwebapp_sql.Pages_Movies
{
    public class EditModel : PageModel
    {
        private readonly razor08.efcore.Data.ArticleContext _context;

        public EditModel(razor08.efcore.Data.ArticleContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return Content("Không thấy bài viết...");
            }

            var article =  await _context.Article.FirstOrDefaultAsync(m => m.ID == id);
            if (article == null)
            {
                return Content("Không thấy bài viết...");
            }
            Article = article;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.ID == id);
        }
    }
}
