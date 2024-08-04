using Common;
using TST.DataAccess.Da;
using TST.DataAccess.Interfaces;
using TST.Web.Core;

namespace TST.Web
{
    public class DI
    {
        public void Register(IServiceCollection services)
        {
            // Web
            services.AddScoped<FileHelper>();

            // Da
            services.AddScoped<IClientDa, ClientDa>();
            services.AddScoped<IContactDa, ContactDa>();
            services.AddScoped<IPerformanceDa, PerformanceDa>();
            services.AddScoped<IRoleDa, RoleDa>();
            services.AddScoped<ISlideDa,SlideDa>();
            services.AddScoped<IUserDa, UserDa>();
            services.AddScoped<IWelcomeDa, WelcomeDa>();
            services.AddScoped<IPerformanceType,  PerformanceTypeDa>();

            // Other
            services.AddSingleton<Resources>();
        }
    }
}
