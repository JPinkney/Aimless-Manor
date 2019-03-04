using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    /*
     * Interact is the event that is called when you're trying to interact
     * with an item. Implementation is in the object. For example, the cauldron
     */
    public abstract void Interact(Inventory inv, GameObject obj);

    /*
     * Given a game object check if that object is a valid possible item
     */
    public virtual bool PossibleItem(GameObject obj)
    {
        return true;
    }
}
