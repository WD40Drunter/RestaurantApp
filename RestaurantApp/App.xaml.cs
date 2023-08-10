using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApp.Model;
using RestaurantApp.Services;
using RestaurantApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RestaurantApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }
        
        public IConfigurationRoot Configuration { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var cs = Configuration.GetConnectionString("RestaurantDB");

            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddDbContext<Context>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("RestaurantDB"));
                })
                    .AddScoped<IRestaurantsService, RestaurantsServices>()
                    .AddTransient<MainWindowViewModel>()
                    .BuildServiceProvider()
                );
        }
    }

    
}
