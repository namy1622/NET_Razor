using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor08.efcore.Data;


namespace App.Admin.Role
{
    public class EditModel(RoleManager<IdentityRole> roleManager, ArticleContext myBlogContext) : RolePageModel(roleManager, myBlogContext)
    {
        public class InputModel
        {

            [Required(ErrorMessage = "Phải nhập {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài {2} đến {1} ký tự")]
            [DisplayName("Tên của role")]
            public string Name { get; set; }

        }

        [BindProperty]
        public required InputModel Input { set; get; }

        public required IdentityRole role {set; get;}
        public async Task<IActionResult> OnGet(string roleid)
        {
            if(roleid == null) return NotFound("Không tìm thấy role");

            role =  await _roleManager.FindByIdAsync(roleid); // tìm role id rồi gán cho role

            if(role != null){
                Input = new InputModel(){
                    Name = role.Name
                };
                return Page();
            }
            return NotFound("Không tìm thấy role");
        }

        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            if(roleid == null) return  NotFound("Không tìm thấy id của role");
            role = await _roleManager.FindByIdAsync(roleid);
            if(role == null) return NotFound("Không tìm thấy role");

            if(!ModelState.IsValid){  // nếu dữ liệu ko phù hợp
                return Page();  
            }
            
             role.Name = Input.Name;
             var result = await _roleManager.UpdateAsync(role);

             if(result.Succeeded){
                StatusMessage = $"Cập nhật role thành công: {Input.Name}";
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
