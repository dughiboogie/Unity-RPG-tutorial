using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour {
    public Image icon;
    public Button removeButton;
    public string slotName;
    public EquipmentInfoOverlay equipmentInfoOverlay;

    private Equipment equipment;

    public void AddEquipment(Equipment newEquipment)
    {
        equipment = newEquipment;
        icon.sprite = newEquipment.icon;
        icon.enabled = true;
        removeButton.interactable = true;

        equipmentInfoOverlay.SetItem(newEquipment);
    }

    public void ClearSlot()
    {
        equipment = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;

        equipmentInfoOverlay.ClearItem();
    }

    /**
     * The only case in which an equipped item is removed is the case when the default item of the corresponding slot should be equipped.
     * Therefore when the remove button is clicked the function EquipItem is called with the correct default item. It's this function
     * that will then call UnequipItem.
     * 
     * If the slot of the current equipment has no default item, the UnequipItem function should be called directly from this function.
     */
    public void OnRemoveClicked()
    {
        EquipmentManager equipmentManager = EquipmentManager.instance;
        int equipmentSlot = (int)equipment.equipmentSlot;

        bool slotHasDefaultItem = false;

        for(int i = 0; i < equipmentManager.defaultItems.Length && !slotHasDefaultItem; i++) {
            if((int)equipmentManager.defaultItems[i].equipmentSlot == equipmentSlot) {
                equipmentManager.EquipItem(equipmentManager.defaultItems[i]);
                slotHasDefaultItem = true;
            }
        }

        if(!slotHasDefaultItem)
            equipmentManager.UnequipItem(equipmentSlot);
    }

    public void OnMouseOver()
    {
        if(equipment != null)
            equipmentInfoOverlay.gameObject.SetActive(true);

    }

    public void OnMouseExit()
    {
        if(equipment != null)
            equipmentInfoOverlay.gameObject.SetActive(false);
    }
}
