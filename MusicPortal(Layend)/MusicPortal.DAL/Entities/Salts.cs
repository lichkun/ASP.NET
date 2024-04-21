using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Salts
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public string Salt { get; set; }
    }
}
