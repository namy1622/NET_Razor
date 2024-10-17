using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor08.efcore.Data;


namespace App.Admin.Role
{
    public class CreateModel(RoleManager<IdentityRole> roleManager, ArticleContext myBlogContext) : RolePageModel(roleManager, myBlogContext)
    {
        public class InputModel
        {

            [Required(ErrorMessage = "Phải nhập {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài {2} đến {1} ký tự")]
            [DisplayName("Tên của role")]
            public string Name { get; set; }

        }

        [BindProperty]
        public InputModel Input { set; get; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if(!ModelState.IsValid){  // nếu dữ liệu ko phù hợp
                return Page();  
            }
            
             var newRole = new IdentityRole(Input.Name);
             var result = await _roleManager.CreateAsync(newRole);

             if(result.Succeeded){
                StatusMessage = $"Bạn vừa tạo role mới: {Input.Name}";
                return RedirectToPage("./Index");
             }
             else{ // nếu bị lỗi
                result.Errors.ToList().ForEach(error =>{
                    ModelState.AddModelError(string.Empty,error.Description);
                    
                });
             }

            return Page();
        }


    }


}
