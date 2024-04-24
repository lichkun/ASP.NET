using Castle.Core.Resource;
using MusicPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.DTO
{
    public class SongDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Localization.Resource),
               ErrorMessageResourceName = "TitleRequired")]
        [Display(Name = "Title", ResourceType = typeof(Localization.Resource))]
        public string Title { get; set; }
        [Required(ErrorMessageResourceType = typeof(Localization.Resource),
               ErrorMessageResourceName = "FilePathRequired")]
        [Display(Name = "FilePath", ResourceType = typeof(Localization.Resource))]
        public string FilePath { get; set; }
        public string Genre { get; set; }
        public string User { get; set; }
        public string Artist { get; set; }
        public int GenreId { get; set; }
        public int UserId { get; set; }
        public int ArtistId { get; set; }
    }
}
