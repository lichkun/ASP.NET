using Microsoft.EntityFrameworkCore;
using MyTopMovies.Models;
using RazorPagesMovies.Repositories;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MyTopMovieContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped <IRepository<Movie>,MovieRepository>();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapFallbackToPage("/Index"); 
});

app.Run();
