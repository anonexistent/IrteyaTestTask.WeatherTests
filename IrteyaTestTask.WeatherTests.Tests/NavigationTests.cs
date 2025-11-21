using IrteyaTestTask.WeatherTests.Config;
using IrteyaTestTask.WheatherTests.WebDriver;
using OpenQA.Selenium;

namespace IrteyaTestTask.WeatherTests.Tests;

[TestFixture]
public class NavigationTests
{
    private TestConfig _cfg;
    private IWebDriver _driver;

    [SetUp]
    public void SetUp()
    {
        _cfg = new TestConfig();
        _driver = WebDriverFactory.Create(_cfg);
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Quit();
        _driver?.Dispose();
    }

    [Test, Category("routing")]
    public void Navigate_FromHome_ToWeather()
    {
        _driver.Navigate().GoToUrl(_cfg.BaseUrl);

        var weatherLink = _driver.FindElement(By.LinkText("Weather"));
        weatherLink.Click();

        Assert.That(_driver.Url, Does.Contain("/weather"));
        Assert.DoesNotThrow(() => _driver.FindElement(By.CssSelector("table.table")));
    }
}
