using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Infastructure;
using MusicPortal.BLL.Services;
using MusicPortal.Models;
using MusicPortal.Services;
using MultilingualSite.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddMusicContext(connection);

builder.Services.AddUnitOfWorkService();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEncryption, Cryptografic>();
builder.Services.AddTransient<IAccount, AccountService>();
builder.Services.AddTransient<IService<UserDTO>, UserService>();
builder.Services.AddTransient<IService<ArtistDTO>, ArtistService>();
builder.Services.AddTransient<IService<GenreDTO>, GenreService>();
builder.Services.AddTransient<IService<SongDTO>, SongService>();

builder.Services.AddScoped<ILanguage, ReadLanguagesService>();  


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
