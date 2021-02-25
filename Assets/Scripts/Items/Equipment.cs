using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlots equipmentSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.EquipItem(this);
        RemoveFromInventory();
    }

}

public enum EquipmentSlots { Head, Chest, Legs, Arms, Weapon, Shield, Feet }
public enum EquipmentMeshRegion { Legs, Arms, Torso }   // Corresponds directly to Player > PlayerGFX > Body > BlendShapes