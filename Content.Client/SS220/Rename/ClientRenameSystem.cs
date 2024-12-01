using Content.Shared.SS220.Rename;
using Robust.Client.UserInterface;


namespace Content.Client.SS220.Rename;

/// <summary>
/// This handles...
/// </summary>
public sealed class ClientRenameSystem : EntitySystem
{
    private readonly RenameBoundUserInterface? _owner;
    [Dependency] private readonly IUserInterfaceManager? _ui = default!;
    [Dependency] private readonly IEntityManager _entityManager = default!;

    [ViewVariables]
    private RenameWindow? _window;

    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<RenameRenameComponent, ComponentAdd>(OnStartUp);
    }

    private void OnStartUp(Entity<RenameRenameComponent> ent, ref ComponentAdd args)
    {
          _window = new RenameWindow(new RenameBoundUserInterface(ent.Owner, RenameUiKey.Key));
          _window.OpenCentered();

          if(TryComp<UserInterfaceComponent>(ent.Owner, out var userInterfaceComponent))
              return;

    }
}
