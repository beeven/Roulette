using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Scoreboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            InitializeDatabase();

            MainWindow window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Show();
            
        }



        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<ConsoleWindow>();

            services.AddDbContext<CandidateDbContext>(options =>
            {
                options.UseSqlite("Data Source=scoreboard.db");
            });
        }

        private void InitializeDatabase()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CandidateDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
