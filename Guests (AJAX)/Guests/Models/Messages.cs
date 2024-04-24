using StoringPassword.Models;
using System.ComponentModel.DataAnnotations;

namespace Guests.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        [Required]
        public string MessageText { get; set; }
        public DateOnly Date { get; set; }
    }
}
