using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform equipmentParent;
    public GameObject equipmentUI;

    private EquipmentManager equipmentManager;
    private EquipmentSlot[] slots;

    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChanged += UpdateUI;
        slots = equipmentParent.GetComponentsInChildren<EquipmentSlot>();

        for(int i = 0; i < slots.Length; i++) {
            string equipmentSlotName = System.Enum.GetNames(typeof(EquipmentSlots))[i];
            slots[i].slotName = equipmentSlotName;
        }
        
        /*foreach(Equipment item in equipmentManager.GetCurrentEquipment()) {
            UpdateUI(item, null);
        }*/
    }

    void Update()
    {
        if(Input.GetButtonDown("Inventory")) {
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        }
    }

    private void UpdateUI(Equipment newItem, Equipment oldItem)
    {
        int equipmentSlot;
        if(newItem != null) {
            equipmentSlot = (int)newItem.equipmentSlot;

            // Debug.Log("Updating equipment slot of index " + equipmentSlot);

            if(newItem.isDefaultItem == true)
                slots[equipmentSlot].ClearSlot();
            else
                slots[equipmentSlot].AddEquipment(newItem);
        }
        else {
            equipmentSlot = (int)oldItem.equipmentSlot;
            slots[equipmentSlot].ClearSlot();
        }

    }

}
