using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public enum Location
    {
        LEFT=0,
        RIGHT=1
    };

    public GameObject item;
    public Location gameObjectLocationInInventory;

    public InventoryItem(GameObject inventory_item, Location itemLocation)
    {
        this.item = inventory_item;
        this.gameObjectLocationInInventory = itemLocation;
    }
}

public class Inventory
{

    private static int inventory_size = 2;
    private InventoryItem[] inventory = new InventoryItem[inventory_size];

    public Inventory()
    {
        for (int x = 0; x < inventory_size; x++)
        {
            this.inventory[x] = null;
        }
    }

    private bool IsObjectAlreadyInInventory(GameObject obj)
    {
        if (this.inventory[0] != null && this.inventory[0].item != null && this.inventory[0].item == obj)
        {
            return true;
        }
        if (this.inventory[1] != null && this.inventory[1].item != null && this.inventory[1].item == obj)
        {
            return true;
        }
        return false;
    }

    public void AddGameObjectToInventory(GameObject obj)
    {

        /*
         * We can just return early in this case because we don't actually need
         * to have the key in the inventory       
         */
        if (obj.tag.Contains("key"))
        {
            return;
        }

        if (obj.name.Contains("scaled"))
        {
            string smaller = obj.name.Substring(0, obj.name.LastIndexOf("_"));
            Debug.Log(smaller);

            GameObject smol = GameObject.Find(smaller);
            // smol.SetActive(true);

            string origName = obj.name;
            GameObject big = GameObject.Find(origName);

            obj = smol;
            while (big != null)
            {
                    big.SetActive(false);
                    big = GameObject.Find(origName);
            }
        }

        if (this.IsObjectAlreadyInInventory(obj))
        {
            return;
        }

        int index = this.GetNextAvailableOpenIndex();
        if (index < 0)
        {
            return;
        }

        Debug.Log("Adding: " + obj);
        if(index == 0)
        {
            this.inventory[index] = new InventoryItem(obj, InventoryItem.Location.LEFT);
        }
        else if (index == 1)
        {
            this.inventory[index] = new InventoryItem(obj, InventoryItem.Location.RIGHT);
        }
    }

    public void RemoveGameObjectFromInventory(GameObject obj)
    {
        for (int ind = 0; ind < inventory.Length; ind++)
        {
            Debug.Log("Trying to remove");
            if (inventory[ind] != null && inventory[ind].item == obj)
            {
                Debug.Log("Finally removed");
                this.inventory[ind] = null;
            }
        }
    }

    public bool IsEmpty()
    {
        return this.inventory[0] == null && this.inventory[1] == null;
    }

    public InventoryItem[] GetInventory()
    {
        return this.inventory;
    }

    public bool IsInventoryFull()
    {
        return this.inventory[0] != null && this.inventory[1] != null;
    }

    public GameObject FindFirstObject()
    {
        if (this.inventory[0] != null)
        {
            return this.inventory[0].item;
        } else if (this.inventory[1] != null)
        {
            return this.inventory[1].item;
        }
        return null;
    }

    public InventoryItem FindFirstInventoryItem()
    {
        if (this.inventory[0] != null)
        {
            return this.inventory[0];
        }
        else if (this.inventory[0] != null)
        {
            return this.inventory[1];
        }
        return null;
    }

    /*
     * Given an index find the next available object
     * 
     * Used for filtering inventory
     */
    public InventoryItem FindNextObject(GameObject obj)
    {

        int index = GetIndexOfCurrentGameObject(obj);
        InventoryItem firstObj = this.FindFirstInventoryItem();
        if (index < 0)
        {
            return null;
        } else if (!this.IsInventoryFull() && firstObj != null)
        {
            //If its not full that means that 
            return firstObj;
        }

        if (index == 0 && this.inventory[1] != null)
        {
            return this.inventory[1];
        } else if (index == 1 && this.inventory[0] != null)
        {
            return this.inventory[0];
        } else
        {
            return null;
        }
    }

    private int GetIndexOfCurrentGameObject(GameObject obj)
    {
        int index = 0;
        foreach (InventoryItem i in this.inventory)
        {
            if (obj == i.item)
            {
                return index;
            }
            index++;
        }
        return -1;
    }

    /*
     * Find the next available open space in the inventory
     * 
     * Returns:   
     * If there is space in the inventory then return the index
     * Else return -1 if there is no space
     */
    private int GetNextAvailableOpenIndex()
    {
        Debug.Log("Next available index");
        Debug.Log("First one is: " + this.inventory[0]);
        if (this.inventory[0] == null)
        {
            return 0;
        }
        else if (this.inventory[1] == null)
        {
            return 1;
        }
        return -1;
    }
}
