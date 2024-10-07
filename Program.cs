using System.Configuration;
using Microsoft.EntityFrameworkCore;
using razor08.efcore.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ArticleContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionMovieContext")));

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();



/*
        CREATE, READ, UPDATE, DELETE ==>  CRUD
        dotnet aspnet-codegenerator razorpage -m razorwebapp_sql.Models.Article -dc razor08.efcore.Data.ArticleContext -outDir Pages/Blog -udl --referenceScriptLibraries
*/