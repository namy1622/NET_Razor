using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor08.efcore.Data;

namespace App.Admin.Role{
    public class RolePageModel : PageModel{
         protected readonly RoleManager<IdentityRole> _roleManager;
         protected readonly ArticleContext _context;

        //  protected readonly InputModel _ipModel;

        [TempData]
         public string StatusMessage{set; get;}  // nội dung thiết lập trang này có thể gọi trang khác

        public RolePageModel(RoleManager<IdentityRole> roleManager, ArticleContext myBlogContext){
            _roleManager = roleManager;
            _context = myBlogContext;

            // _ipModel = ipModel;
        }
    }
}