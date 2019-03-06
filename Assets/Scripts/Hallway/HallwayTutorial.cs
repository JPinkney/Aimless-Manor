using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayTutorial : Interactable
{

    public GameObject hiddenSkull;
    public GameObject dome;
    public GameObject key;
    public GameObject tutorialRoomPortal;

    public override void Interact(Inventory inv, GameObject obj)
    {
        if (obj && obj.tag == "Skull")
        {
            obj.SetActive(false);
            inv.RemoveGameObjectFromInventory(obj);
            hiddenSkull.SetActive(true);
            dome.SetActive(false);
            key.SetActive(true);
            tutorialRoomPortal.SetActive(true);
        }
    }
}
