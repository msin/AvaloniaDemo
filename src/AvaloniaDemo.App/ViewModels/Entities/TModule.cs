using System.Text.Json.Serialization;
using Avalonia.Media;

namespace AvaloniaDemo.App.ViewModels.Entities
{
    public class TModule
    {
        [JsonPropertyName("ID")]
        public int Id { get; set; }

        [JsonPropertyName("PARENT_ID")]
        public int ParentId { get; set; }

        [JsonPropertyName("KEY")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("PAGE")]
        public string Page { get; set; } = string.Empty;

        [JsonPropertyName("IDX")]
        public int Idx { get; set; }
        
        [JsonPropertyName("ICON")]
        public string Icon { get; set; } = string.Empty;
        
        // [JsonIgnore]
        // public IImage? Image { get; set; }

        [JsonPropertyName("NAME")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("DESCR")]
        public string Descr { get; set; } = string.Empty;
    }
}
