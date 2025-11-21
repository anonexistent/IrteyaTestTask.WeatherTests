using IrteyaTestTask.WeatherTests.Config;
using IrteyaTestTask.WeatherTests.Tests.Helpers;
using OpenQA.Selenium;

namespace IrteyaTestTask.WeatherTests.Tests.Pages;

public class WeatherPage : BasePage
{
    public override string RelativeUrl => "weather";

    public WeatherPage(IWebDriver driver, TestConfig config)
        : base(driver, config)
    {
    }

    public IWebElement Table => Driver.FindElement(By.CssSelector("table.table"));

    public List<T> ParseTable<T>() where T : new()
        => TableParser.Parse<T>(Table);
}
