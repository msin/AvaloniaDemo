using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Eremex.AvaloniaUI.Controls.DataGrid;
using Eremex.AvaloniaUI.Controls.Editors;
using AvaloniaDemo.App.ViewModels.Entities;
using AvaloniaDemo.CIL.Common;

namespace AvaloniaDemo.App.Views;

public class ColumnTemplate : ITemplate<object, GridColumn>
{
    public GridColumn Build(object param)
    {
        var columnDef = (ColumnDef)param;

        var catalogKey = columnDef.FieldName switch
        {
            "TYPE_KEY" => "ITEM_TYPE",
            "GROUP_KEY" => "ITEM_GROUP",
            "CLASS_KEY" => "ITEM_CLASS",
            "STAT_YN" => "ITEM_STAT",
            "MODE_KEY" => "ITEM_MODE",
            "FAMILY_KEY" => "FAMILY_KEY",
            "RPU" => "MU_NAME",
            "CPU" => "MU_NAME",
            _ => columnDef.FieldName
        };

        var gridColumn = new GridColumn
        {
            FieldName = columnDef.PropName,
            Header = columnDef.Header,
            HeaderHorizontalAlignment = HorizontalAlignment.Center,
            HeaderVerticalAlignment = VerticalAlignment.Center,
            Width = new GridLength(1, GridUnitType.Auto),
            MinWidth = 60, MaxWidth = 160,
            EditorProperties = columnDef.ColumnType switch
            {
                ColumnType.Integer => new SpinEditorProperties
                {
                    MaskType = MaskType.Numeric,
                    Mask = columnDef.FieldName.EndsWith("_ID") ? "F0" : "N0",
                },
                ColumnType.String => new TextEditorProperties(),
                ColumnType.Date => new DateEditorProperties
                {
                    Mask = "yyyy.MM.dd",
                    HorizontalContentAlignment = HorizontalAlignment.Center
                },
                ColumnType.ComboBox => new ComboBoxEditorProperties
                {
                    ItemsSource = catalogKey == "UOM_KEY"
                        ? UomData.Data
                        : CatalogData.Data.TryGetValue(catalogKey, out var source) ? source : [],
                    ValueMember = nameof(KeyVal.Id),
                    DisplayMember = nameof(KeyVal.Name),
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                },
                _ => new TextEditorProperties()
            }
        };

        return gridColumn;
    }
}