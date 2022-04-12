using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]

public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int damageModifier;

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
