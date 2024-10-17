using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor08.efcore.Data;


namespace App.Admin.Role
{
    public class DeleteModel(RoleManager<IdentityRole> roleManager, ArticleContext myBlogContext) : RolePageModel(roleManager, myBlogContext)
    {
       
        public IdentityRole role {set; get;}
        public async Task<IActionResult> OnGet(string roleid)
        {
            if(roleid == null ) return NotFound("Không tìm thấy role");

            role = await _roleManager.FindByIdAsync(roleid);
            if(role == null){
                return NotFound("Không tìm thấy role");
            }
            return Page();

        }

        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            if(roleid == null) return NotFound("Không tìm thấy id của role");
            role = await _roleManager.FindByIdAsync(roleid);
            if(role == null) return NotFound("Không tìm thấy role");

            
            
             
             var result = await _roleManager.DeleteAsync(role);

             if(result.Succeeded){
                StatusMessage = $"Bạn vừa xóa role: {role.Name}";
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
