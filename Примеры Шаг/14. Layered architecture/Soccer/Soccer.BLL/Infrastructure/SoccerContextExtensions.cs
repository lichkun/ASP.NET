using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Soccer.DAL.EF;


namespace Soccer.BLL.Infrastructure
{
    public static class SoccerContextExtensions
    {
        public static void AddSoccerContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<SoccerContext>(options => options.UseSqlServer(connection));
        }
    }
}
