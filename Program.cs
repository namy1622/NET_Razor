using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using razor08.efcore.Data;
using razorwebapp_sql.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// cấu hinhf cài đặt Email
builder.Services.AddOptions();
var mailsetting = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailsetting);
builder.Services.AddSingleton<IEmailSender, SendMailService>();

builder.Services.AddDbContext<ArticleContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionMovieContext")));

// Dang ki Identity
// builder.Services.AddIdentity<AppUser, IdentityRole>()
//         .AddEntityFrameworkStores<ArticleContext>()
//         .AddDefaultTokenProviders();

// Microsoft.Extensions.DependencyInjection.IdentityServiceCollectionExtensions
//     .AddIdentity<AppUser, IdentityRole>(builder.Services)
//     .AddEntityFrameworkStores<ArticleContext>()
//     .AddDefaultTokenProviders();

//-----------------------------


builder.Services.AddDefaultIdentity<AppUser>()
        .AddEntityFrameworkStores<ArticleContext>();
       // .AddDefaultTokenProviders();

       

//****************************************************
//-------------------------------------------------------
// Truy cập IdentityOptions
builder.Services.Configure<IdentityOptions> (options => {
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại

});

//-------------------------------------------------------
//**********************************************************


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();



/*
        CREATE, READ, UPDATE, DELETE ==>  CRUD
        dotnet aspnet-codegenerator razorpage -m razorwebapp_sql.Models.Article -dc razor08.efcore.Data.ArticleContext -outDir Pages/Blog -udl --referenceScriptLibraries
*/

/*
        Identity:
                -Authentication: Xác định danh tính -> login, logout,..
                - Authorization: xác thực quyền truy cập 

                - Quản lý user: Sign up, user, Role,... 

        /Identity/Account/Login
        /Identity/Account/Manage

        // ịnect dịch vụ quản lý tài khoản
         SignInManager<AppUser> SignInManager
         UserManager<AppUser> UserManager

         dotnet aspnet-codegenerator identity -dc razor08.efcore.Data.ArticleContext

         Microsoft.AspNetCore.Identity.IdentityBuilderExtensions.AddDefaultTokenProviders
         Microsoft.AspNetCore.Identity.IdentityBuilderExtensions.AddDefaultTokenProviders

         Microsoft.AspNetCore.Identity.IdentityBuilder
         Microsoft.AspNetCore.Identity.IdentityBuilder
*/



















// public class Program
// {
//         public static void Main(string[] args)
//         {
//                 WebHost.CreateDefaultBuilder(args)
//                 .UseKestrel()
//                 .UseStartup<Startup>()
//                 .Build();
//                 .Run();
//         }
// }