using Microsoft.EntityFrameworkCore;
using StudentsMVC;
using StudentsMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Для использования функциональности библиотеки SignalR,
// в приложении необходимо зарегистрировать соответствующие сервисы
builder.Services.AddSignalR();     

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<StudentContext>(options => options.UseSqlServer(connection));

// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles(); // обрабатывает запросы к файлам в папке wwwroot

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}");

app.MapHub<NotificationHub>("/notification");   // NotificationHub будет обрабатывать запросы по пути /notification

app.Run();
