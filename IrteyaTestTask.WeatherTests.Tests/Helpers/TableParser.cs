using OpenQA.Selenium;
using System.Reflection;

namespace IrteyaTestTask.WeatherTests.Tests.Helpers;

public static class TableParser
{
    public static List<T> Parse<T>(IWebElement table) where T : new()
    {
        var rows = table.FindElements(By.CssSelector("tbody tr"));
        var headers = table.FindElements(By.CssSelector("thead th"))
            .Select(h => NormalizeHeader(h.Text))
            .ToList();

        var props = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite)
            .ToList();

        var items = new List<T>();

        foreach (var row in rows)
        {
            var model = new T();
            var cells = row.FindElements(By.TagName("td")).ToList();

            for (int i = 0; i < headers.Count; i++)
            {
                var header = headers[i];
                var cellText = cells[i].Text.Trim();

                var prop = FindMatchingProperty(props, header);
                if (prop == null)
                    continue;

                object? value = ConvertValue(cellText, prop.PropertyType);
                prop.SetValue(model, value);
            }

            items.Add(model);
        }

        return items;
    }

    private static string NormalizeHeader(string header)
    {
        var cleaned = new string(header.Where(char.IsLetterOrDigit).ToArray());
        return cleaned;
    }

    private static PropertyInfo? FindMatchingProperty(List<PropertyInfo> props, string header)
    {
        var exact = props.FirstOrDefault(p =>
            p.Name.Equals(header, StringComparison.OrdinalIgnoreCase));

        if (exact != null)
            return exact;

        var partial = props.FirstOrDefault(p =>
            p.Name.StartsWith(header, StringComparison.OrdinalIgnoreCase));

        return partial;
    }

    private static object? ConvertValue(string raw, Type targetType)
    {
        if (targetType == typeof(string))
            return raw;

        if (targetType == typeof(int))
            return int.Parse(raw);

        if (targetType == typeof(DateOnly))
            return DateOnly.Parse(raw);

        if (targetType == typeof(DateTime))
            return DateTime.Parse(raw);

        return Convert.ChangeType(raw, targetType);
    }
}
