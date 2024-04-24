using Microsoft.EntityFrameworkCore;
using Soccer.BLL.Interfaces;
using Soccer.BLL.Services;
using Soccer.BLL.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSoccerContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();

// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles(); // обрабатывает запросы к файлам в папке wwwroot

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Teams}/{action=Index}/{id?}");

app.Run();
