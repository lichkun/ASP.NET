using Microsoft.EntityFrameworkCore;
using SignalR_Chat;
using SignalR_Chat.Models;   // ������������ ���� ������ ChatHub

var builder = WebApplication.CreateBuilder(args);
// ��� ������������� ���������������� ���������� SignalR,
// � ���������� ���������� ���������������� ��������������� �������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ChatContext>(options => options.UseSqlServer(connection));
builder.Services.AddSignalR();  

var app = builder.Build();
// � ������� ������������ ������ ���������� UseDefaultFiles() ����� ���������
// �������� ����������� ���-������� �� ��������� ��� ��������� � ��� �� ������� ����.
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHub<ChatHub>("/chat");   // ChatHub ����� ������������ ������� �� ���� /chat

app.Run();
