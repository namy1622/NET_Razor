
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorwebapp_sql.Models;

namespace App.Admin.User
{
    public class SetPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SetPasswordModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

       
        [BindProperty]
        public InputModel Input { get; set; }

       
        [TempData]
        public string StatusMessage { get; set; }

               public class InputModel
        {
          
            [Required]
            [StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1} kí tự.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu mới")]
            public string NewPassword { get; set; }

           
            [DataType(DataType.Password)]
            [Display(Name = "Nhập lại mật khẩu")]
            [Compare("NewPassword", ErrorMessage = "{0} không chính xác.")]
            public string ConfirmPassword { get; set; }
        }

        public AppUser user{set; get;}
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(string.IsNullOrEmpty(id)){
                return NotFound("Không có User");
            }

             user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Không thấy User với ID: '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Page();
            }

            user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            // để tạo lại mk = AddPasswordAsync thì passHash phải null nên trước đó phải cho mk về null
            // vd sd Remove
            await _userManager.RemovePasswordAsync(user);
            var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = $"Đã cập nhật mật khẩu cho user: {user.UserName}";

            return RedirectToPage("./Index");
        }
    }
}
