using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razor08.efcore.Data;
using razorwebapp_sql.Models;

namespace App.Admin.User
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        [TempData]
        public string StatusMessage { set; get; } = "";

        public class UserAndRole :AppUser{
            public string roleName{set;get;}
        }
        // danh sachs các role
        public List<UserAndRole> users { set; get; }

        //=== Paging ===========================
        public const int ITEMS_PER_PAGE = 10;
        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }
        public int countPages { get; set; }

        public int totalUser {set; get;}
        //---- end paging -------------------------

        public async Task OnGet()
        {
            //users = await _userManager.Users.OrderBy(u =>u.UserName).ToListAsync();
            var qr = _userManager.Users.OrderBy(u => u.UserName);

             totalUser = await qr.CountAsync();
            countPages = (int)Math.Ceiling((double)totalUser / ITEMS_PER_PAGE);

            if (currentPage < 1)
                currentPage = 1;
            if (currentPage > countPages)
                currentPage = countPages;

            var qr1 = qr.Skip((currentPage - 1) * ITEMS_PER_PAGE)
                    .Take(ITEMS_PER_PAGE)
                    .Select(u => new UserAndRole(){
                        Id = u.Id,
                        UserName = u.UserName,
                    });

            users = await qr1.ToListAsync();

            foreach(var user in users){
                var roles = await _userManager.GetRolesAsync(user);
                user.roleName = string.Join(",", roles);
            }
        }

        public void OnPost() => RedirectToPage();  // nếu gọi theo pt http onpost thì chuyên hướng về onGet()
    }
}
