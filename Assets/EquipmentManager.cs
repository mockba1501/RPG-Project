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

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;  //Refers to the player mesh
    Equipment[] currentEquipment;           //Items we currently have equipped
    SkinnedMeshRenderer[] currentMeshes;

    Inventory inventory;

    //call back for when an item is equipped/unequipped
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChangedCallBack;

    private void Start()
    {
        inventory = Inventory.instance;         //Reference to the inventory

        //Get the total element within the enum and use it to initialize the array
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    public void Equip (Equipment newItem)
    {
        //Get the index of the enum element
        int slotIndex = (int)newItem.equipSlot;
    
        Equipment oldItem = Unequip(slotIndex); 

        /*
        //No need anymore as this is already being handled inside the unequip method
        //To swap an element from the inventory with the used one
        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }
        */
        //If there are lisners to the trigger
        if(onEquipmentChangedCallBack != null)
        {
            onEquipmentChangedCallBack(newItem, oldItem);
        }

        SetEquipmentBlendShapes(newItem, 100);

        currentEquipment[slotIndex] = newItem;

        //Instantiate the new equipment mesh
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        //Use it to deform the newMesh
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh; 
    }

    //Changed the return type from void to Equipment
    public Equipment Unequip(int slotIndex)
    {
        //Check if it is not empty
        if(currentEquipment[slotIndex] != null)
        {
            if(currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            Equipment oldItem = currentEquipment[slotIndex];
            //To return it back to its original state
            SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            //If there are lisners to the trigger
            if (onEquipmentChangedCallBack != null)
            {
                onEquipmentChangedCallBack(null, oldItem);
            }
            //Return the old item
            return oldItem;
        }
        //If it was empty
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i); 
        }
        //To ensure that the default items are always there
        EquipDefaultItems();
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            //will set the weight for all the regions covered by the mesh region
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }

    //Create an update method to check if a certain key is hit to unequip all items
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }


}
