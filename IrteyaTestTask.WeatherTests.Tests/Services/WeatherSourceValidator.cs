using IrteyaTestTask.UI.Domain.Models;
using IrteyaTestTask.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IrteyaTestTask.WeatherTests.Tests.Services;

public class WeatherSourceValidator
{
    private readonly IWeatherService _service;

    public WeatherSourceValidator()
    {
        var provider = WeatherTestServiceProvider.Create();
        _service = provider.GetRequiredService<IWeatherService>();
    }

    public async Task<IReadOnlyList<WeatherForecast>> GetSourceAsync()
    {
        return await _service.GetForecastAsync();
    }
}