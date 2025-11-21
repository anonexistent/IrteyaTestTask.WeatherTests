using IrteyaTestTask.WeatherTests.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace IrteyaTestTask.WheatherTests.WebDriver;

public static class WebDriverFactory
{
    public static IWebDriver Create(TestConfig cfg)
    {
        return cfg.Browser.ToLower() switch
        {
            "chrome" => CreateChrome(cfg),
            _ => throw new NotSupportedException("Unknown browser")
        };
    }

    private static IWebDriver CreateChrome(TestConfig cfg)
    {
        var options = new ChromeOptions();
        if (cfg.Headless) options.AddArgument("--headless=new");

        return new ChromeDriver(options);
    }
}
