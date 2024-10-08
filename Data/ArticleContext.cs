using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using razorwebapp_sql.Models;

namespace razor08.efcore.Data
//  razor08.efcore.Data.ArticleContext
{
    public class ArticleContext : IdentityDbContext<AppUser>
    {
        public ArticleContext(DbContextOptions<ArticleContext> options) : base(options)
        {
            // Phương thức khởi tạo này chứa options để kết nối đến MS SQL Server
            // Thực hiện điều này khi Inject trong dịch vụ hệ thống
        }
        public DbSet<Article> Article {set; get;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // duyệt qua tên các bảng sau đó xóa chữ ASPnet(vì mặc định Identity tạo bảng có tiền tố ASPnet)
            foreach ( var entityType in builder.Model.GetEntityTypes()){
                var tabName = entityType.GetTableName();
                if(tabName.StartsWith("AspNet")){  // tiền tố AspNet (6 kí tự)
                    entityType.SetTableName(tabName.Substring(6)); // xóa 6 kí tự đầu 
                }
            }
        }


    }
}