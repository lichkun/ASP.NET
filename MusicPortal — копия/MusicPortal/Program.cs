using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repository;
using MusicPortal.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MusicPortalContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IEncryption, Cryptografic>();
builder.Services.AddScoped<IRepository, AccountRepository>();
builder.Services.AddScoped<IRepositoryEntity<User>, UserRepository>();
builder.Services.AddScoped<IRepositoryEntity<Genre>, GenreRepository>();
builder.Services.AddScoped<IRepositoryEntity<Artist>, ArtistRepository>();
builder.Services.AddScoped<IRepositoryEntity<Song>, SongRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
