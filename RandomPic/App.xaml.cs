using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using RandomPic.Data;

namespace RandomPic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            InitializeDatabase();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(MainWindow));
            services.AddDbContext<QuizContext>(options =>
            {
                options.UseSqlite("Data Source=quiz.db");
            });
            services.AddTransient<RandomPicturePage>();
            services.AddTransient<QuizPage>();
            services.AddTransient<WelcomePage>();
            services.AddTransient<QuizManagerPage>();
            services.AddTransient<EndingPage>();
        }

        private void InitializeDatabase()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;

                //try
                //{
                    var context = services.GetRequiredService<QuizContext>();
                    context.Database.Migrate(); // apply all migrations
                    //SeedData.Initialize(services); // Insert default data
                //}
                //catch (Exception ex)
                //{
                //    var logger = services.GetRequiredService<ILogger<Program>>();
                //    logger.LogError(ex, "An error occurred seeding the DB.");
                //}
            }


        }

    }
}
