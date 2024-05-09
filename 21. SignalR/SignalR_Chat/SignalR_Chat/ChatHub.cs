using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR_Chat.Models;

namespace SignalR_Chat
{
    /*
    Ключевой сущностью в SignalR, через которую клиенты обмениваются сообщеними 
    с сервером и между собой, является хаб (hub). 
    Хаб представляет некоторый класс, который унаследован от абстрактного класса 
    Microsoft.AspNetCore.SignalR.Hub.
    */
    public class ChatHub : Hub
    {
        private readonly ChatContext _context;
        public ChatHub(ChatContext context)
        {
            _context = context;
        }
        // Отправка сообщений
        public async Task Send(string username, string message)
        {
            var chatMessage = new Message
            {
                UserName = username,
                MessageText = message,
                Date = DateOnly.FromDateTime(DateTime.Now)
            };
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
            // Вызов метода AddMessage на всех клиентах
            await Clients.All.SendAsync("AddMessage", username, message);
        }

        // Подключение нового пользователя
        public async Task Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!_context.Users.Any(x => x.ConnectionId == id))
            {
                _context.Users.Add(new User { ConnectionId = id, Name = userName });
                await _context.SaveChangesAsync();

                var Users = await _context.Users.ToListAsync(); 
                var Messages = await _context.ChatMessages.ToListAsync(); 

                // Вызов метода Connected только на текущем клиенте, который обратился к серверу
                await Clients.Caller.SendAsync("Connected", id, userName, Users, Messages);

                // Вызов метода NewUserConnected на всех клиентах, кроме клиента с определенным id
                await Clients.AllExcept(id).SendAsync("NewUserConnected", id, userName);
            }
        }

        // OnDisconnectedAsync срабатывает при отключении клиента.
        // В качестве параметра передается сообщение об ошибке, которая описывает,
        // почему произошло отключение.
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var item = _context.Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                _context.Users.Remove(item);
                await _context.SaveChangesAsync();
                var id = Context.ConnectionId;
                // Вызов метода UserDisconnected на всех клиентах
                await Clients.All.SendAsync("UserDisconnected", id, item.Name);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
