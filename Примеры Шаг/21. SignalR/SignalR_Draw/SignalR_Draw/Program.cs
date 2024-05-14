using SignalR_Draw;   // пространство имен класса ChatHub

var builder = WebApplication.CreateBuilder(args);
// ƒл€ использовани€ функциональности библиотеки SignalR,
// в приложении необходимо зарегистрировать соответствующие сервисы
builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHub<DrawHub>("/draw");   // DrawHub будет обрабатывать запросы по пути /draw

app.Run();