using MyAppServer.Services;

namespace PosAppServer.Services.Injector
{
    public class ServicesRegistrator
    {
        public static void AddCustomServices(IServiceCollection services)
        {
            services.AddScoped<MailJetService>();
            services.AddScoped<AccountService>();
            services.AddScoped<TodoService>();

        }
    }
}
