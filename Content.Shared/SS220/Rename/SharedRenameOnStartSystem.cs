using Content.Shared.Weapons.Melee.Events;
using Robust.Shared.Network;

namespace Content.Shared.SS220.Rename;

/// <summary>
/// This handles...
/// </summary>
public sealed partial class SharedRenameOnStartSystem : EntitySystem
{
    [Dependency] private readonly SharedUserInterfaceSystem _ui = default!;
    [Dependency] private readonly INetManager _net = default!;

    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<RenameRenameComponent, AttackedEvent>(OnComponentStartup);
    }

    private void OnComponentStartup(Entity<RenameRenameComponent> ent, ref AttackedEvent args)
    {

    }

}
