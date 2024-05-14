using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace SignalR_Chat.Models
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> ChatMessages { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }
    public class Message
    {
        public int Id { get; set; }
        public virtual string UserName { get; set; }
        public string MessageText { get; set; }
        public DateOnly Date { get; set; }
    }

}
