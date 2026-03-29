using Avalonia;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using AvaloniaDemo.App.ViewModels.Entities;
using Eremex.AvaloniaUI.Controls.Common;
using Eremex.AvaloniaUI.Controls.TreeList;

namespace AvaloniaDemo.App.Views;

public partial class MainWindow : MxWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnFocusedNodeChanged(object sender, TreeListFocusedNodeChangedEventArgs e)
    {
        if (e.Node is not { ParentNode: null }) return;
    
        if (e.Node.IsExpanded) return;
        
        PageSelector.CollapseAllNodes();
        
        e.Node.IsExpanded = true;
    }
}

public class ResizedImageSelector : ITreeListNodeImageSelector
{
    public IImage? SelectImage(TreeListNode? node)
    {
        if (node?.Content is not TModule module)  return null;
        
        var image = new Bitmap(AssetLoader.Open(new Uri($"avares://AvaloniaDemo.App/Assets{module.Icon}")));
        
        return image;
    }
}
