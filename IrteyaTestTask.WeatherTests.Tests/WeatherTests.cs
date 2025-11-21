using IrteyaTestTask.UI.Domain.Models;
using IrteyaTestTask.WeatherTests.Config;
using IrteyaTestTask.WeatherTests.Tests.Pages;
using IrteyaTestTask.WeatherTests.Tests.Services;
using IrteyaTestTask.WheatherTests.WebDriver;
using OpenQA.Selenium;

namespace IrteyaTestTask.WeatherTests.Tests;

[TestFixture]
public class WeatherTests
{
    private TestConfig _cfg;
    private IWebDriver _driver;
    private WeatherSourceValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _cfg = new TestConfig();
        _driver = WebDriverFactory.Create(_cfg);
        _validator = new WeatherSourceValidator();
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Quit();
        _driver?.Dispose();
    }

    [Test, Category("validation")]
    public async Task WeatherTable_MatchesWeatherService()
    {
        var page = new WeatherPage(_driver, _cfg);
        page.Navigate();

        await Task.Delay(500);

        var uiData = page.ParseTable<WeatherForecast>();
        var sourceData = await _validator.GetSourceAsync();

        Assert.That(uiData.Count, Is.EqualTo(sourceData.Count));

        for (int i = 0; i < sourceData.Count; i++)
        {
            Assert.That(uiData[i].Date, Is.EqualTo(sourceData[i].Date));
            Assert.That(uiData[i].TemperatureC, Is.EqualTo(sourceData[i].TemperatureC));
            Assert.That(uiData[i].Summary, Is.EqualTo(sourceData[i].Summary));
        }
    }
}
