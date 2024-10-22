using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razor08.efcore.Data;

namespace App.Admin.Role
{
    [Authorize(Roles="admin_1")]
    public class IndexModel : RolePageModel
    {
        public IndexModel(RoleManager<IdentityRole> roleManager, ArticleContext myBlogContext) : base(roleManager, myBlogContext)
        {
        }


        // public IndexModel(RoleManager<IdentityRole> roleManager, ArticleContext myBlogContext) : base(roleManager, myBlogContext)
        // {
        // }

        // danh sachs các role
        public List<IdentityRole> roles {set; get;}

        public async Task OnGet()
        {
            // ds all role trong hethong
            roles = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
        }

        public void OnPost() => RedirectToPage();  // nếu gọi theo pt http onpost thì chuyên hướng về onGet()
    }
}
