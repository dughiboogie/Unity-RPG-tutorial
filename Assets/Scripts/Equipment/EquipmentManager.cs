using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        if(instance != null) {
            Debug.LogWarning("Multiple instances of EquipmentManager found!");
            return;
        }

        instance = this;
    }

    #endregion

    private Equipment[] currentEquipment;
    private SkinnedMeshRenderer[] currentMeshes;
    private Inventory inventory;

    // Callback to call when an item is equipped/unequipped
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    public SkinnedMeshRenderer targetMesh;
    public Equipment[] defaultItems;

    /**
     * EquipmentManager.Start() is ordered in the project settings to be called before the default time
     */
    private void Start()
    {
        inventory = Inventory.instance;

        int equipmentSlotsNumber = System.Enum.GetNames(typeof(EquipmentSlots)).Length;
        currentEquipment = new Equipment[equipmentSlotsNumber];
        currentMeshes = new SkinnedMeshRenderer[equipmentSlotsNumber];

        EquipDefaultItems();
    }

    public void EquipItem(Equipment newItem)
    {
        int equipmentSlot = (int)newItem.equipmentSlot;

        Equipment oldItem = UnequipItem(equipmentSlot);

        SetEquipmentBlendShapes(newItem, 100);

        currentEquipment[equipmentSlot] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);

        newMesh.transform.parent = targetMesh.transform;    // targetMesh = player mesh
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[equipmentSlot] = newMesh;

        if(onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldItem);
    }

    public Equipment UnequipItem(int equipmentSlot)
    {
        if(currentEquipment[equipmentSlot] != null) {

            if(currentMeshes[equipmentSlot] != null) {
                Destroy(currentMeshes[equipmentSlot].gameObject);
            }

            Equipment oldItem = currentEquipment[equipmentSlot];
            inventory.Add(oldItem);

            SetEquipmentBlendShapes(oldItem, 0);
            currentEquipment[equipmentSlot] = null;
            
            if(onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);

            return oldItem;
        }

        return null;
    }

    public void UnequipAll()
    {
        for(int i = 0; i < currentEquipment.Length; i++) {
            UnequipItem(i);
        }

        EquipDefaultItems();
    }

    private void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach(EquipmentMeshRegion blendShape in item.coveredMeshRegions) {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    private void EquipDefaultItems()
    {
        foreach(Equipment item in defaultItems) {
            EquipItem(item);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)) {
            UnequipAll();
        }
    }

    public Equipment[] GetCurrentEquipment()
    {
        return currentEquipment;
    }
}
