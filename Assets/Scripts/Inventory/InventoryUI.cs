using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;   // Reference to the parent of the inventory slots
    public GameObject inventoryUI;

    Inventory inventory;    // Contains the actual objects
    InventorySlot[] slots;  // Contains the UI inventory slots

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;    // Add the function "UpdateUI" to be called onItemChangedCallback

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();   // Get all the child inventory slots of itemsParent
    }

    void Update()
    {
        if(Input.GetButtonDown("Inventory")) {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    /**
     * TODO Update method to avoid looping through the slots every time an object is picked up or removed from the inventory
     */ 
    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++) { // Loop through all the UI inventory 
            if(i < inventory.items.Count)
                slots[i].AddItem(inventory.items[i]);   // If i < inventory length add current inventory item to slot
            else
                slots[i].ClearSlot();   // Clear all slots for i > inventory length
        }
    }
}
