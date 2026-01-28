using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Platform;
using AvaloniaDemo.CIL.Common;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaDemo.App.ViewModels.Entities;

/// <summary>
/// Модель данных для товара
/// </summary>
public partial class TItem : ObservableObject
{
    [ObservableProperty] private int id;
    [ObservableProperty] private string art;
    [ObservableProperty] private string name;
    [ObservableProperty] private string descr;
    [ObservableProperty] private int typeKey;
    [ObservableProperty] private int groupKey;
    [ObservableProperty] private int classKey;
    [ObservableProperty] private string uomKey;
    [ObservableProperty] private int statYn;
    [ObservableProperty] private int modeKey;
    [ObservableProperty] private int buffer;
    [ObservableProperty] private int slz;
    [ObservableProperty] private int yeld;
    [ObservableProperty] private int familyKey;
    [ObservableProperty] private int rpu;
    [ObservableProperty] private int cpu;
    [ObservableProperty] private int sbop;
    [ObservableProperty] private int dvers;
    [ObservableProperty] private int labor;
    [ObservableProperty] private int weeklimit;
    [ObservableProperty] private string attachment;
    [ObservableProperty] private int buffer2;
    [ObservableProperty] private string artExt;
}

public static class ItemData
{
    private static TItem[] _result = [];
    private static FrozenDictionary<string, (int index, ColumnType columnType)> _colDict;

    public static TItem[] Load(ColumnDef[] cols)
    {
        _colDict = cols
            .Select((col, index) => new {index, col})
            .ToFrozenDictionary(t => t.col.FieldName, t => (t.index, t.col.ColumnType));

        try
        {
            // Используем AssetLoader для доступа к ZIP архиву
            var uri = new Uri("avares://AvaloniaDemo.App/Assets/items.zip");

            using (var zipStream = AssetLoader.Open(uri))
            using (var archive = new System.IO.Compression.ZipArchive(zipStream, System.IO.Compression.ZipArchiveMode.Read))
            {
                // Ищем файл items.csv в архиве
                var csvEntry = archive.Entries.FirstOrDefault(e => e.Name == "items.csv");

                if (csvEntry != null)
                {
                    using var entryStream = csvEntry.Open();
                    using var reader = new StreamReader(entryStream, System.Text.Encoding.GetEncoding(1251));
                    var items = ParseCsvData(reader);
                    _result = items;

                    System.Diagnostics.Debug.WriteLine($"✓ Загружено {items.Length} товаров из CSV");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"✗ Файл items.csv не найден в архиве");
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"✗ Ошибка загрузки CSV: {ex.Message}\n{ex.StackTrace}");
        }

        return _result;
    }

    private static TItem[] ParseCsvData(StreamReader reader)
    {
        var items = new List<TItem>();

        // Читаем и парсим заголовок
        var headerLine = reader.ReadLine();

        if (headerLine == null) return items.ToArray();

        var headers = ParseCsvLine(headerLine);

        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var values = ParseCsvLine(line);

            if (values.Length == 0) continue;
            
            var item = new TItem
            {
                Id = GetCsvValueInt(values, "ID"),
                Art = GetCsvValue(values, "ART"),
                Name = GetCsvValue(values, "NAME"),
                Descr = GetCsvValue(values, "DESCR"),
                TypeKey = GetCsvValueInt(values, "TYPE_KEY"),
                GroupKey = GetCsvValueInt(values, "GROUP_KEY"),
                ClassKey = GetCsvValueInt(values, "CLASS_KEY"),
                UomKey = GetCsvValue(values, "UOM_KEY"),
                StatYn = GetCsvValueInt(values, "STAT_YN"),
                ModeKey = GetCsvValueInt(values, "MODE_KEY"),
                Buffer = GetCsvValueInt(values, "BUFFER"),
                Slz = GetCsvValueInt(values, "SLZ"),
                Yeld = GetCsvValueInt(values, "YELD"),
                FamilyKey = GetCsvValueInt(values, "FAMILY_KEY"),
                Rpu = GetCsvValueInt(values, "RPU"),
                Cpu = GetCsvValueInt(values, "CPU"),
                Sbop = GetCsvValueInt(values, "SBOP"),
                Dvers = GetCsvValueInt(values, "DVERS"),
                Labor = GetCsvValueInt(values, "LABOR"),
                Weeklimit = GetCsvValueInt(values, "WEEKLIMIT"),
                Attachment = GetCsvValue(values, "ATTACHMENT"),
                Buffer2 = GetCsvValueInt(values, "BUFFER2"),
                ArtExt = GetCsvValue(values, "ART_EXT")
            };

            items.Add(item);
        }
        
        return items.ToArray();
    }
    
    private static string[] ParseCsvLine(string line)
    {
        var values = line.Replace("\"", string.Empty).Split('\t');

        return values;
    }
    
    private static string GetCsvValue(string[] values, string columnName) => 
        _colDict.TryGetValue(columnName, out var col) ? values[col.index] : string.Empty;

    private static int GetCsvValueInt(string[] values, string columnName) =>
        _colDict.TryGetValue(columnName, out var col) 
            ? int.TryParse(values[col.index], out var result) ? result : 0 : 0;
}