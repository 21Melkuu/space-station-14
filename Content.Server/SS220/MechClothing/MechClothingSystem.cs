using Content.Shared.Mech;
using Robust.Shared.Player;
using Robust.Server.GameObjects;
using Content.Shared.SS220.MechClothing;
using Robust.Server.Containers;

namespace Content.Server.SS220.MechClothing;

public sealed partial class MechClothingSystem : SharedMechClothingSystem
{
    [Dependency] private readonly UserInterfaceSystem _ui = default!;
    [Dependency] private readonly ContainerSystem _container = default!;
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<MechClothingComponent, MechOpenUiEvent>(OnOpenUi);
    }


    private void OnOpenUi(Entity<MechClothingComponent> ent, ref MechOpenUiEvent args)
    {
        args.Handled = true;

        if (!TryComp<ActorComponent>(args.Performer, out var actor))
            return;
        _ui.TryToggleUi(ent.Owner, MechUiKey.Key, actor.PlayerSession);
        UpdateUserInterface(ent.Owner, ent.Comp);
    }

    public override void UpdateUserInterface(EntityUid uid, MechClothingComponent? component = null)
    {
        if (!Resolve(uid, ref component))
            return;

        base.UpdateUserInterface(uid, component);

        var ev = new MechEquipmentUiStateReadyEvent();
        foreach (var ent in component.EquipmentContainer.ContainedEntities)
        {
            RaiseLocalEvent(ent, ev);
        }
        var state = new MechBoundUiState
        {
            EquipmentStates = ev.States
        };
        _ui.SetUiState(uid, MechUiKey.Key, state);
    }
}
