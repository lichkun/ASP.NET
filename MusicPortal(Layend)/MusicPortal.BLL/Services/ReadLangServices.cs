using Microsoft.Extensions.Configuration;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities;

namespace MultilingualSite.Services
{
    public class ReadLanguagesService : ILanguage
    {
        IConfiguration _con;
        List<Language> languageLists;
        public ReadLanguagesService(IConfiguration con)
        {
            string section = "Lang";
            _con = con;
            IConfigurationSection pointSection = _con.GetSection(section);
            List<Language> lists = new List<Language>();
            foreach (var language in pointSection.AsEnumerable())
            {
                if (language.Value != null)
                    lists.Add(new Language
                    {
                        ShortName = language.Key.Replace(section + ":", ""),
                        Name = language.Value
                    });
            }

            languageLists = lists;
        }

        public List<Language> Languages() => languageLists;

    }
}
