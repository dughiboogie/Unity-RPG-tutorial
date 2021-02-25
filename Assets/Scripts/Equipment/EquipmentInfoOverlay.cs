using UnityEngine;
using UnityEngine.UI;

public class EquipmentInfoOverlay : ItemInfoOverlay
{
    public Text armorModifier;
    public Text damageModifier;

    public override void SetItem(Item newItem)
    {
        if(newItem.GetType() == typeof(Equipment)) {
            Equipment newEquipment = newItem as Equipment;
            base.SetItem(newEquipment);
            armorModifier.text = newEquipment.armorModifier.ToString();
            damageModifier.text = newEquipment.damageModifier.ToString();
        }
    }

    public override void ClearItem()
    {
        base.ClearItem();
        armorModifier.text = null;
        damageModifier.text = null;
    }
}
