using IrteyaTestTask.WeatherTests.Config;
using OpenQA.Selenium;

namespace IrteyaTestTask.WeatherTests.Tests.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly string BaseUrl;

    protected BasePage(IWebDriver driver, TestConfig config)
    {
        Driver = driver;
        BaseUrl = config.BaseUrl;
    }

    public abstract string RelativeUrl { get; }

    public void Navigate() => Driver.Navigate().GoToUrl($"{BaseUrl}/{RelativeUrl}");
}
