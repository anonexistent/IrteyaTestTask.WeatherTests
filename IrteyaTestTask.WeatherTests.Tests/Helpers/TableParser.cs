using OpenQA.Selenium;

namespace IrteyaTestTask.WeatherTests.Tests.Helpers;

public static class TableParser
{
    public static List<T> ParseTable<T>(IWebElement table) where T : new()
    {
        var rows = table.FindElements(By.TagName("tr")).Skip(1);
        var props = typeof(T).GetProperties();

        var result = new List<T>();

        foreach (var row in rows)
        {
            var obj = new T();
            var cells = row.FindElements(By.TagName("td")).ToList();

            for (int i = 0; i < Math.Min(cells.Count, props.Length); i++)
            {
                var prop = props[i];
                object? value = Convert.ChangeType(cells[i].Text, prop.PropertyType);
                prop.SetValue(obj, value);
            }

            result.Add(obj);
        }

        return result;
    }
}
