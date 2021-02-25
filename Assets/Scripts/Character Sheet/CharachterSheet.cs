using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterSheet : MonoBehaviour
{

    #region Singleton

    public static CharachterSheet instance; // Singleton pattern

    private void Awake()
    {
        if(instance != null) {
            Debug.LogWarning("An instance of the CharachterSheet object already exists!");
            return;
        }
        instance = this;
    }

    #endregion

    public Transform player;

    private Equipment[] currentEquipment;
    private PlayerStats playerStats;

    // TODO make character name, level and class dynamic
    private string characterName = "Brian";
    private int characterLevel = 1;
    private string characterClass = "fighter";

    void Start()
    {
        EquipmentManager equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChanged += UpdateCurrentEquipment;
        currentEquipment = new Equipment[equipmentManager.GetCurrentEquipment().Length];

        foreach(Equipment item in equipmentManager.GetCurrentEquipment()) {
            UpdateCurrentEquipment(item, null);
        }

        playerStats = player.GetComponent<PlayerStats>();


    }

    private void UpdateCurrentEquipment(Equipment newItem, Equipment oldItem)
    {
        if(newItem != null) {
            int equipmentSlot = (int)newItem.equipmentSlot;

            if(newItem.isDefaultItem == true)
                currentEquipment[equipmentSlot] = null;
            else
                currentEquipment[equipmentSlot] = newItem;
        }
    }
}    