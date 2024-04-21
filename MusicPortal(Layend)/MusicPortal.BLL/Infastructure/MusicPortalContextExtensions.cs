using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Infastructure
{
    public static class MusicPortalContextExtensions
    {
        public static void AddMusicContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MusicPortalContext>(options => options.UseSqlServer(connection));
        }
    }
}
