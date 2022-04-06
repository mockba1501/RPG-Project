using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance;

    void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        Instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    //Set a limited space to the inventory
    public int space = 20;
    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            items.Add(item);

            //Trigger the event, so our UI will update
            if(onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        
        //So whenever something is changing in the inventory we are calling the call back
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
}
