//using System.Configuration;
using System.Configuration;
using App.Services;
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

// builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ArticleContext>();

// Dang ki Identity

//-----------------------------


// builder.Services.AddDefaultIdentity<AppUser>()
//         .AddEntityFrameworkStores<ArticleContext>()
//        .AddDefaultTokenProviders();

builder.Services.AddIdentity<AppUser, IdentityRole>()
     .AddEntityFrameworkStores<ArticleContext>()
     .AddDefaultTokenProviders();

//****************************************************
//-------------------------------------------------------
// Truy cập IdentityOptions
builder.Services.Configure<IdentityOptions>(options =>
{       
        
        // Thiết lập về Password
        options.Password.RequireDigit = false; // Không bắt phải có số
        options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
        options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
        options.Password.RequireUppercase = false; // Không bắt buộc chữ in
        options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
        options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

        // Cấu hình Lockout - khóa user
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
        options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
        options.Lockout.AllowedForNewUsers = true;

        // Cấu hình về User.
        options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;  // Email là duy nhất

        // Cấu hình đăng nhập.
        options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
        options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
        options.SignIn.RequireConfirmedAccount = true;
});

//-------------------------------------------------------
//**********************************************************

// gọi dv google
builder.Services.AddAuthentication()
        // theem provider từ google
        .AddGoogle(options =>
        {
                var gconfig = builder.Configuration.GetSection("Authentication:Google");
#pragma warning disable CS8601 // Possible null reference assignment.
                options.ClientId = gconfig["ClientId"];
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
                options.ClientSecret = gconfig["ClientSecret"];
#pragma warning restore CS8601 // Possible null reference assignment.
                // https://localhost:5001/signin-google
                options.CallbackPath = "/dang-nhap-tu-google";
        })
         // thêm provider từ FB
         .AddFacebook(fb_options =>
         {
                 var fbAuthNSection = builder.Configuration.GetSection("Authentication:Facebook");
                 fb_options.AppId = fbAuthNSection["AppId"];
                 fb_options.AppSecret = fbAuthNSection["AppSecret"];
                 fb_options.CallbackPath = "/dang-nhap-tu-facebook";
         })

        // .AddTwitter() // thêm provider từ TW
        // ...        
        ;

builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();

builder.Services.ConfigureApplicationCookie(options =>
{
        options.LoginPath = "/login/";
        options.LogoutPath = "/logout/";
        options.AccessDeniedPath = "/khongduoctruycap.html";
});

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

app.UseEndpoints(endpoint =>
{
    endpoint.MapRazorPages();
});

app.Run();



/*
        CREATE, READ, UPDATE, DELETE ==>  CRUD
        dotnet aspnet-codegenerator razorpage -m razorwebapp_sql.Models.Article -dc razor08.efcore.Data.ArticleContext -outDir Pages/Blog -udl --referenceScriptLibraries
*/

/*
        Identity:
                -Authentication: Xác định danh tính -> login, logout,..
                - Authorization: xác thực quyền truy cập 
                        + Role-based authorization - xác thực quyền theo vai trò
                        + Role(vai trò): (Admin, Editor, Manager, Member,...)

                        Index, Create, Edit, Delete  (/Areas/Admin/Pages/Role)

                - Quản lý user: Sign up, user, Role,... 

        /Identity/Account/Login
        /Identity/Account/Manage

        // ịnect dịch vụ quản lý tài khoản
         SignInManager<AppUser> SignInManager
         UserManager<AppUser> UserManager

         dotnet aspnet-codegenerator identity -dc razor08.efcore.Data.ArticleContext


        CallbackPath:
        https://localhost:5001/dang-nhap-tu-google    

        --*************************--


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