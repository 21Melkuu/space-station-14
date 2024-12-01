using Robust.Shared.Serialization;

namespace Content.Shared.SS220.Rename;

[Serializable, NetSerializable]
public enum RenameUiKey
{
    Key
}

[Serializable, NetSerializable]
public sealed class RenameBoundUserInterfaceState : BoundUserInterfaceState
{

}

[Serializable, NetSerializable]
public sealed class ConfirmRenameMessage : BoundUserInterfaceMessage
{
    public readonly string RenameMessage;

    public ConfirmRenameMessage(string renameMessage)
    {
        RenameMessage = renameMessage;
    }
}
