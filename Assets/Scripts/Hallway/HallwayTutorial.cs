using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayTutorial : Interactable
{

    public GameObject hiddenEgg;
    public GameObject dome;
    public GameObject tutorialRoomPortal;

    public override void Interact(Inventory inv, GameObject obj)
    {
        if (obj && obj.tag == "Egg")
        {
            obj.SetActive(false);
            inv.RemoveGameObjectFromInventory(obj);
            hiddenEgg.SetActive(true);
            dome.SetActive(false);
            tutorialRoomPortal.SetActive(true);
        }
    }
}
