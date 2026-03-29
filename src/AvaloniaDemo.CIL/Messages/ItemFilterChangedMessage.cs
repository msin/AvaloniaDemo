using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AvaloniaDemo.CIL.Messages;

public class ItemFilterChangedMessage(string value) : ValueChangedMessage<string>(value);
