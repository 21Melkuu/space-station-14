using Content.Server.Administration.Systems;
using Content.Shared.Administration;
using Content.Shared.SS220.Rename;

namespace Content.Server.Rename;

/// <summary>
/// This handles...
/// </summary>
public sealed class RenameSystem : EntitySystem //переименовать
{
    [Dependency] private readonly MetaDataSystem _meta = default!;
    [Dependency] private readonly AdminFrozenSystem _frozen = default!;

    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<RenameOnStartComponent, ComponentStartup>(OnStartUp);
        SubscribeLocalEvent<RenameOnStartComponent, ConfirmRenameMessage>(OnRenameEnter);
    }

    private void OnStartUp(Entity<RenameOnStartComponent> ent, ref ComponentStartup args)
    {
        /*_frozen.FreezeAndMute(ent.Owner);*/ //to prevent players from changing their name after showing up with their initial name
    }

    private void OnRenameEnter(Entity<RenameOnStartComponent> ent, ref ConfirmRenameMessage args)
    {
        _meta.SetEntityName(args.Actor, args.RenameMessage);

        if(TryComp<AdminFrozenComponent>(args.Actor, out var _))
            RemComp<AdminFrozenComponent>(args.Actor);
    }
}
