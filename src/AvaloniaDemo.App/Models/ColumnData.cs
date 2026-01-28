using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Avalonia.Platform;
using AvaloniaDemo.CIL.Common;

namespace AvaloniaDemo.App.Models;

public static class ColumnData
{
    public static ColumnDef[] Data { get; } = Load();

    private static ColumnDef[] Load()
    {
        try
        {
            var uri = new Uri("avares://AvaloniaDemo.App/Assets/columns.json");

            using var stream = AssetLoader.Open(uri);
            using var reader = new StreamReader(stream, Encoding.GetEncoding(1251));

            var json = reader.ReadToEnd();
            return JsonSerializer.Deserialize<ColumnDef[]>(json) ?? [];
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"✗ Ошибка загрузки JSON: {ex.Message}\n{ex.StackTrace}");
        }
        
        return [];
    }
}