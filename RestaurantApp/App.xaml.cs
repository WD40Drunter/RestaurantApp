using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApp.Model;
using RestaurantApp.Services;
using RestaurantApp.ViewModel;
using System.IO;
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
                    .AddScoped<IDishService, DishService>()
                    .AddScoped<IUserService, UserService>()
                    .AddScoped<IAdressServices, AdressServices>()
                    .AddScoped<IStatusServices, StatusServices>()
                    .AddTransient<MainWindowViewModel>()
                    .AddTransient<RestaurantMenuViewModel>()
                    .AddTransient<RegisterWindowViewModel>()
                    .AddTransient<RestaurantAdditionWindowViewModel>()
                    .AddTransient<UserListWindowViewModel>()
                    .BuildServiceProvider()
                );
        }
    }


}
