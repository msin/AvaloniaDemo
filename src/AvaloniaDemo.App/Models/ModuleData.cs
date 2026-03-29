using System.Text;
using System.Text.Json;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using AvaloniaDemo.App.ViewModels.Entities;
using Avalonia.Media;

namespace AvaloniaDemo.App.Models;

public static class ModuleData
{
    public static TModule[] Load()
    {
        TModule[] result = [];
        try
        {
            var uri = new Uri("avares://AvaloniaDemo.App/Assets/modules.json");

            using var stream = AssetLoader.Open(uri);
            using var reader = new StreamReader(stream, Encoding.GetEncoding(1251));

            var json = reader.ReadToEnd();
            
            result = JsonSerializer.Deserialize<TModule[]>(json) ?? [];

            // foreach (var module in result)
            // {
            //     if (string.IsNullOrWhiteSpace(module.Icon)) continue;
            //
            //     try
            //     {
            //         module.Image = LoadIcon(module.Icon);
            //
            //         System.Diagnostics.Debug.WriteLine($"✓ Successfully loaded '{module.Icon}'");
            //     }
            //     catch (Exception imgEx)
            //     {
            //         System.Diagnostics.Debug.WriteLine($"✗ Ошибка загрузки иконки '{module.Icon}': {imgEx.Message}");
            //     }
            // }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"✗ Ошибка загрузки JSON: {ex.Message}\n{ex.StackTrace}");
        }

        return result.Where(t => t.Idx >= 0).OrderBy(x => x.Idx).ToArray();
    }

    //  /Images/Programming/IDE_32x32.png
    private static IImage? LoadIcon(string imageKey)
    {
        var uri = new Uri($"avares://AvaloniaDemo.App/Assets{imageKey}");
        
        var stream = AssetLoader.Open(uri);
        
        return new Bitmap(stream);
    }
}