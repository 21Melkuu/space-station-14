using Robust.Shared.Serialization;


namespace Content.Shared.SS220.ApproveMaths
{
    [Serializable, NetSerializable]
    public sealed class UiButtonPressedMessage : BoundUserInterfaceMessage
    {
        public readonly UiButton Button;

        public UiButtonPressedMessage(UiButton button)
        {
            Button = button;
        }
    }

    public enum UiButton
    {
        Check
    }

    [Serializable, NetSerializable]
    public enum ApproveMathsUiKey
    {
        Key
    }
}
