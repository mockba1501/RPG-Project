using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    //Override the old definition of the name and use this instead
    new public string name = "New Item";

    public Sprite icon = null;

    public bool isDefaultItem = false;
}
