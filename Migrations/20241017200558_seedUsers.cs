using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace razorwebapp_sql.Migrations
{
    /// <inheritdoc />
    public partial class seedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i< 150; i++){
                migrationBuilder.InsertData(
                    "Users",
                    columns: new []{
                        "Id",
                        "UserName",
                        "Email",
                        "SecurityStamp",
                        "EmailConfirmed",
                        "PhoneNumberConfirmed",
                        "TwoFactorEnabled",
                        "LockoutEnabled",
                        "AccessFailedCount",
                        "HomeAddress"

                    },
                    values: new object[]{
                        Guid.NewGuid().ToString(),
                        "User-"+i.ToString("D3"),  // D3: nếu i = 1 -> fomat thành 001
                        $"email{i.ToString("D3")}@example.com",
                        Guid.NewGuid().ToString(),
                        true,
                        false,
                        false,
                        false,
                        0,
                        "...@#%..."
                    }

                    
                //        [Id]
                //       ,[UserName]
                //       ,[NormalizedUserName]
                //       ,[Email]
                //       ,[NormalizedEmail]
                //       ,[EmailConfirmed]
                //       ,[PasswordHash]
                //       ,[SecurityStamp]
                //       ,[ConcurrencyStamp]
                //       ,[PhoneNumber]
                //       ,[PhoneNumberConfirmed]
                //       ,[TwoFactorEnabled]
                //       ,[LockoutEnd]
                //       ,[LockoutEnabled]
                //       ,[AccessFailedCount]
                //       ,[HomeAddress]
                //       ,[BirthDate]
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
