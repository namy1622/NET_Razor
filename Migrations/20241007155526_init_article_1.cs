using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using razorwebapp_sql.Models;

#nullable disable

namespace razorwebapp_sql.Migrations
{
    /// <inheritdoc />
    public partial class init_article_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ID);
                });


                Randomizer.Seed = new Random(867509);
                var fakeArticle = new Faker<Article>();
                fakeArticle.RuleFor(a => a.Title, f => f.Lorem.Sentence(5, 5));
                fakeArticle.RuleFor(a => a.PublishDate, f => f.Date.Between(new DateTime(202, 1,1), new DateTime(2024, 10, 7)));
                fakeArticle.RuleFor(a => a.Content, f => f.Lorem.Paragraphs(1, 4));

                for( int i = 0; i < 150; i++){
                     Article article = fakeArticle.Generate();
                     migrationBuilder.InsertData(
                        table: "Article",
                        columns: new[] {"Title", "PublishDate", "Content"},
                        values: new object[] {
                            article.Title,
                            article.PublishDate,
                            article.Content
                        }
                     );
                }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");
        }
    }
}
