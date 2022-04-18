using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]

public class Equipment : Item
{
    public EquipmentSlot equipSlot; //Slot to store equipment in
    public SkinnedMeshRenderer mesh;

    public int armorModifier;       //Increase/decrease in armor
    public int damageModifier;      //Increase/decrease in damage

    public override void Use()
    {
        base.Use();

        //Equip the item using the Equipment Manager
        EquipmentManager.instance.Equip(this);
        //Remove from the Inventory
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapons, Shield, Feet}
