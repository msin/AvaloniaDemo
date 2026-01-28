using AvaloniaDemo.App.ViewModels;
using Xunit.Abstractions;

namespace AvaloniaDemo.Test;

public class ProcessValueTests(ITestOutputHelper output)
{
    private readonly MainWindowViewModel _main = new();
    private readonly Action<string> _write = output.WriteLine;

    [Fact]
    public void ProcessValueWithNullValueReturnsEmptyString()
    {
        // Arrange
        string? value = null;
        string propName = "TestProp";

        // Act
        string result = _main.ProcessValue(value, propName);
        _write($"{propName} : = '{value ?? "null"}' => \"{result}\"");

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ProcessValueWithEmptyValueReturnsEmptyString()
    {
        // Arrange
        string value = string.Empty;
        string propName = "TestProp";

        // Act
        string result = _main.ProcessValue(value, propName);
        _write($"{propName} : = '{value}' => \"{result}\"");

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ProcessValueWithoutPercentSignReturnsFormattedValue()
    {
        // Arrange
        string value = "test";
        string propName = "Name";

        // Act
        string result = _main.ProcessValue(value, propName);
        _write($"{propName} : = '{value}' => \"{result}\"");

        // Assert
        Assert.Equal("[Name] = 'test'", result);
    }

    [Fact]
    public void ProcessValueStartsWithPercentReturnsEndsWith()
    {
        // Arrange
        string value = "%suffix";
        string propName = "Code";

        // Act
        string result = _main.ProcessValue(value, propName);
        _write($"{propName} : = '{value}' => \"{result}\"");

        // Assert
        Assert.Equal("EndsWith([Code],'suffix')", result);
    }

    [Fact]
    public void ProcessValueEndsWithPercentReturnsStartsWith()
    {
        // Arrange
        string value = "prefix%";
        string propName = "Code";

        // Act
        string result = _main.ProcessValue(value, propName);
        _write($"{propName} : = '{value}' => \"{result}\"");

        // Assert
        Assert.Equal("StartsWith([Code],'prefix')", result);
    }

    [Fact]
    public void ProcessValueWithPercentInMiddleReturnsStartsAndEndsWith()
    {
        // Arrange
        string value = "start%end";
        string propName = "Description";

        // Act
        string result = _main.ProcessValue(value, propName);
        _write($"{propName} : = '{value}' => \"{result}\"");

        // Assert
        Assert.Equal("StartsWith([Description],'start') and EndsWith([Description],'end')", result);
    }

    [Fact]
    public void ProcessValueWithMultiplePercentsUsesFirstAndLast()
    {
        // Arrange
        string value = "first%middle%last";
        string propName = "Field";
    
        // Act
        string result = _main.ProcessValue(value, propName);
        _write($"{propName} : = '{value}' => \"{result}\"");
    
        // Assert
        Assert.Equal("StartsWith([Field],'first') and Contains([Field],'middle') and EndsWith([Field],'last')", result);
    }
}