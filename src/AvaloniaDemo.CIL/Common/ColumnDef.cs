namespace AvaloniaDemo.CIL.Common;

public enum ColumnType
{
    Integer,
    String,
    Date,
    ComboBox
}

public class ColumnDef(string propName, string fieldName, string header, ColumnType columnType)
{
    public string PropName { get; } = propName;
    public string FieldName { get; } = fieldName;
    public string Header { get; } = header;
    public ColumnType ColumnType { get; } = columnType;
}