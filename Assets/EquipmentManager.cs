using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;
    }

    #endregion

    Equipment[] currentEquipment;

    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChangedCallBack;

    private void Start()
    {
        inventory = Inventory.instance;

        //Get the total element within the enum and use it to initialize the array
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        currentEquipment = new Equipment[numSlots];
    }

    public void Equip (Equipment newItem)
    {
        //Get the index of the enum element
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        //To swap an element from the inventory with the used one
        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        //If there are lisners to the trigger
        if(onEquipmentChangedCallBack != null)
        {
            onEquipmentChangedCallBack(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            //If there are lisners to the trigger
            if (onEquipmentChangedCallBack != null)
            {
                onEquipmentChangedCallBack(null, oldItem);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i); 
        }
    }

    //Create an update method to check if a certain key is hit to unequip all items
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }


}
