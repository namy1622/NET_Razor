// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorwebapp_sql.Models;

namespace razorwebapp_sql.Areas.Identity.Pages.Account.Manage
{
    // muoons truy cập được phải đăng nhập
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        public string Username { get; set; }

       
        [TempData]
        public string StatusMessage { get; set; }
       
        [BindProperty]
        public InputModel Input { get; set; }

        
        public class InputModel
        {
           
            [Phone(ErrorMessage = "{0} bị sai định dạng")]
            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }

            [DisplayName("Ngày sinh")]
             public DateTime? BirthDate {get; set;}

             [DisplayName("Địa chỉ")]
              [StringLength(400)]
             public string? HomeAddress {set; get;}
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,

                HomeAddress = user.HomeAddress,
                BirthDate = user.BirthDate
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Không tìm thấy user với ID: '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            // var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            // if (Input.PhoneNumber != phoneNumber)
            // {
            //     var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //     if (!setPhoneResult.Succeeded)
            //     {
            //         StatusMessage = "Unexpected error when trying to set phone number.";
            //         return RedirectToPage();
            //     }
            // }

            user.HomeAddress = Input.HomeAddress;
            user.PhoneNumber = Input.PhoneNumber;
            user.BirthDate = Input.BirthDate;

            await _userManager.UpdateAsync(user);

            // RefreshSignInAsync(user) -> refresh(tair laị để update thông tin mới )
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Hồ sơ đã được cập nhật";
            return RedirectToPage();
        }
    }
}
