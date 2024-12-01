using Content.Shared.SS220.Rename;

namespace Content.Client.SS220.Rename;

public sealed class RenameBoundUserInterface : BoundUserInterface
{
    [ViewVariables]
    private RenameWindow? _window;

    public RenameBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();
        _window = new RenameWindow(this);
        _window.OpenCentered();
    }

    public void SubmitName(string newFullName)
    {
        SendMessage(new ConfirmRenameMessage(newFullName));
    }
}
