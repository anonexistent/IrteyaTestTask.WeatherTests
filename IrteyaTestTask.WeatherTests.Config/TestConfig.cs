using Microsoft.Extensions.Configuration;

namespace IrteyaTestTask.WeatherTests.Config;

public class TestConfig
{
    public string BaseUrl { get; }
    public string Browser { get; }
    public bool Headless { get; }

    public TestConfig()
    {
        var cfg = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        BaseUrl = cfg["Web:BaseUrl"]!;
        Browser = cfg["Driver:Browser"]!;
        Headless = bool.Parse(cfg["Driver:Headless"]!);
    }
}
