using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    // public ItemInfoOverlay itemInfoOverlay; // TODO Instantiate at runtime the correct type of prefab - equipmentOverlay if type of item is Equipment, itemOverlay otherwise

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;

        /*
        if(item.GetType() == typeof(Equipment)){
            Equipment equipment = item as Equipment;
            item = equipment;
            EquipmentInfoOverlay equipmentInfoOverlay = itemInfoOverlay as EquipmentInfoOverlay;
            itemInfoOverlay = equipmentInfoOverlay;
        }

        itemInfoOverlay.SetItem(newItem);
        */
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;

        // itemInfoOverlay.ClearItem();
    }

    /**
     * TODO Update method to drop the object instead of destroying it
     */
    public void OnRemoveClicked()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if(item != null) {
            item.Use();
        }
    }

    /*
    public void OnMouseOver()
    {
        if(item != null)
            itemInfoOverlay.gameObject.SetActive(true);

    }

    public void OnMouseExit()
    {
        if(item != null)
            itemInfoOverlay.gameObject.SetActive(false);
    }
    */
}
