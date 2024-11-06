using System.Linq;
using  Robust.Shared.Containers;
using Content.Shared.Whitelist;
using Content.Shared.Mech.Equipment.Components;
using Content.Shared.Mech;
using Robust.Shared.Network;
using Content.Shared.Popups;
using Content.Shared.Interaction;
using Robust.Shared.Timing;

namespace Content.Shared.SS220.MechClothing;

/// <summary>
/// This handles...
/// </summary>
public abstract class SharedMechClothingSystem : EntitySystem
{
    /// <inheritdoc/>

    [Dependency] private readonly SharedContainerSystem _container = default!;
    [Dependency] private readonly EntityWhitelistSystem _whitelistSystem = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly INetManager _net = default!;
    [Dependency] private readonly IGameTiming _timing = default!;
    public override void Initialize()
    {
        SubscribeLocalEvent<MechClothingComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<MechClothingComponent, MechToggleEquipmentEvent>(OnToggleEquipmentAction);
        SubscribeLocalEvent<MechClothingComponent, UserActivateInWorldEvent>(RelayInteractionEvent);

    }

    private void OnStartup(Entity<MechClothingComponent> ent, ref ComponentStartup args)
    {
        ent.Comp.EquipmentContainer = _container.EnsureContainer<Container>(ent.Owner, ent.Comp.EquipmentContainerId);
        ent.Comp.BatterySlot = _container.EnsureContainer<ContainerSlot>(ent.Owner, ent.Comp.BatterySlotId);
    }

    private void OnToggleEquipmentAction(Entity<MechClothingComponent> ent, ref MechToggleEquipmentEvent args)
    {
        if (args.Handled)
            return;
        args.Handled = true;
        CycleEquipment(ent.Owner);
    }

    public virtual void UpdateUserInterface(EntityUid uid, MechClothingComponent? component = null)
    {

    }

    public void InsertEquipment(EntityUid uid, EntityUid toInsert, MechClothingComponent? component = null,
        MechEquipmentComponent? equipmentComponent = null)
    {
        if (!Resolve(uid, ref component))
            return;

        if (!Resolve(toInsert, ref equipmentComponent))
            return;

        if (component.EquipmentContainer.ContainedEntities.Count >= component.MaxEquipmentAmount)
            return;

        if (_whitelistSystem.IsWhitelistFail(component.EquipmentWhitelist, toInsert))
            return;

        equipmentComponent.EquipmentOwner = uid;
        _container.Insert(toInsert, component.EquipmentContainer);
        var ev = new MechEquipmentInsertedEvent(uid);
        RaiseLocalEvent(toInsert, ref ev);
        UpdateUserInterface(uid, component);
    }

    public void CycleEquipment(EntityUid uid, MechClothingComponent? component = null)
    {
        if (!Resolve(uid, ref component))
            return;

        var allEquipment = component.EquipmentContainer.ContainedEntities.ToList();

        var equipmentIndex = -1;
        if (component.CurrentSelectedEquipment != null)
        {
            bool StartIndex(EntityUid u) => u == component.CurrentSelectedEquipment;
            equipmentIndex = allEquipment.FindIndex(StartIndex);
        }

        equipmentIndex++;
        component.CurrentSelectedEquipment = equipmentIndex >= allEquipment.Count
            ? null
            : allEquipment[equipmentIndex];

        var popupString = component.CurrentSelectedEquipment != null
            ? Loc.GetString("mech-equipment-select-popup", ("item", component.CurrentSelectedEquipment))
            : Loc.GetString("mech-equipment-select-none-popup");

        if (_net.IsServer)
            _popup.PopupEntity(popupString, uid);

        Dirty(uid, component);
    }

    private void RelayInteractionEvent(Entity<MechClothingComponent> ent, ref UserActivateInWorldEvent args)
    {

        if (!_timing.IsFirstTimePredicted)
            return;

        if (ent.Comp.CurrentSelectedEquipment != null)
        {
            RaiseLocalEvent(ent.Comp.CurrentSelectedEquipment.Value, args);
        }
    }

}
