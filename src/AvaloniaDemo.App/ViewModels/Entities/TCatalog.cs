using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Platform;
using AvaloniaDemo.CIL.Common;

namespace AvaloniaDemo.App.ViewModels.Entities;

public class TCatalog
{
    public string Key { get; set; }
    public int Val { get; set; }
    public string Name { get; set; }
}

public static class CatalogData
{
    public static FrozenDictionary<string, KeyVal[]> Data { get; } = Load();
    
    private static FrozenDictionary<string, KeyVal[]> Load()
    {
        TCatalog[] result = [];
        try
        {
            // Используем AssetLoader для доступа к ZIP архиву
            var uri = new Uri("avares://AvaloniaDemo.App/Assets/catalog.zip");

            using (var zipStream = AssetLoader.Open(uri))
            using (var archive = new System.IO.Compression.ZipArchive(zipStream, System.IO.Compression.ZipArchiveMode.Read))
            {
                // Ищем файл items.csv в архиве
                var csvEntry = archive.Entries.FirstOrDefault(e => e.Name == "catalog.txt");

                if (csvEntry != null)
                {
                    using var entryStream = csvEntry.Open();
                    using var reader = new StreamReader(entryStream, System.Text.Encoding.GetEncoding(1251));
                    var items = ParseTxtData(reader);
                    result = items;

                    System.Diagnostics.Debug.WriteLine($"✓ Загружено {items.Length} товаров из TXT");
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

        return result
            .ToLookup(t => t.Key)
            .ToFrozenDictionary(
                t => t.Key, 
                t => t.Select(x => new KeyVal(x.Val, x.Name)).ToArray());
    }

    private static TCatalog[] ParseTxtData(StreamReader reader)
    {
        var items = new List<TCatalog>();

        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var values = line.Split('\t');

            if (values.Length == 0) continue;

            var item = new TCatalog()
            {
                Key = values[0],
                Val = int.Parse(values[1]),
                Name = values[2],
            };

            items.Add(item);
        }

        return items.ToArray();
    }
}