using Microsoft.AspNetCore.SignalR;

namespace SignalR_Draw
{
    /*
    Ключевой сущностью в SignalR, через которую клиенты обмениваются сообщеними 
    с сервером и между собой, является хаб (hub). 
    Хаб представляет некоторый класс, который унаследован от абстрактного класса 
    Microsoft.AspNetCore.SignalR.Hub.
    */
    public class DrawHub : Hub
    {
        public async Task Send(Data data)
        {
            // Вызов метода addLine на всех клиентах, кроме клиента с определенным id
            await Clients.AllExcept(Context.ConnectionId).SendAsync("addLine", data);
        }
    }
}
