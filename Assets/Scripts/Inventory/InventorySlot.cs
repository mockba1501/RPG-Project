using UnityEngine;
using UnityEngine.UI;

//Keeps track of everything happinning on an inventory slot
// - Update the UI on the slot
// - Defines what happens when we press it or press remove

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    Item item;
    
    public void AddItem(Item newItem)
    {
        item = newItem;
        
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled=false;
        removeButton.interactable = false;  
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
