using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace AvaloniaDemo.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private IList<EmployeeInfo> employees;

    [ObservableProperty]
    private IList<ColumnDefinition> columnDefinitions;

    public MainWindowViewModel()
    {
        ColumnDefinitions = new List<ColumnDefinition>
        {
            new ColumnDefinition("Id", "ID", ColumnType.Integer),
            new ColumnDefinition("FirstName", "First Name", ColumnType.String),
            new ColumnDefinition("LastName", "Last Name", ColumnType.String),
            new ColumnDefinition("BirthDate", "Birth Date", ColumnType.Date),
            new ColumnDefinition("Department", "Department", ColumnType.ComboBox)
        };

        Employees = new List<EmployeeInfo>
        {
            new EmployeeInfo(1, "Alex", "Smith", new DateTime(1990, 1, 1), "Engineering"),
            new EmployeeInfo(2, "Samantha", "Brown", new DateTime(1988, 2, 5), "Marketing"),
            new EmployeeInfo(3, "Nick", "Morris", new DateTime(2000, 8, 25), "Engineering"),
            new EmployeeInfo(4, "Julia", "Lee", new DateTime(2005, 12, 3), "HR")
        };
    }

    public static IList<string> Departments { get; } = new List<string>
    {
        "Engineering",
        "Marketing",
        "HR",
        "Finance",
        "Sales"
    };
}

public partial class EmployeeInfo : ObservableObject
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string firstName;

    [ObservableProperty]
    private string lastName;

    [ObservableProperty]
    private DateTime birthDate;

    [ObservableProperty]
    private string department;

    public EmployeeInfo(int id, string firstName, string lastName, DateTime birthDate, string department)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Department = department;
    }
}

public enum ColumnType
{
    Integer,
    String,
    Date,
    ComboBox
}

public class ColumnDefinition
{
    public string FieldName { get; }
    public string Header { get; }
    public ColumnType ColumnType { get; }

    public ColumnDefinition(string fieldName, string header, ColumnType columnType)
    {
        FieldName = fieldName;
        Header = header;
        ColumnType = columnType;
    }
}
