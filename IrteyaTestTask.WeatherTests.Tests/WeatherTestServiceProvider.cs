using IrteyaTestTask.UI.Application.Services;
using IrteyaTestTask.UI.Infrastructure;
using IrteyaTestTask.UI.Infrastructure.WeatherRepositories;
using IrteyaTestTask.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IrteyaTestTask.WeatherTests.Tests;

public static class WeatherTestServiceProvider
{
    public static IServiceProvider Create()
    {
        var services = new ServiceCollection();

        services.AddDbContext<WeatherDbContext>(o =>
            o.UseSqlite("Data Source=test_weather.db"));

        services.AddScoped<IWeatherRepository, EfWeatherRepository>();
        services.AddScoped<IWeatherService, WeatherService>();

        return services.BuildServiceProvider();
    }
}
