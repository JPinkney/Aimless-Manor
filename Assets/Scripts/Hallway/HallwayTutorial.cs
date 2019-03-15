using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayTutorial : Interactable
{
    public GameObject hiddenEgg;
    public GameObject tutorialRoomPortal;
    public ParticleSystem glimmer;

    public override void Interact(Inventory inv, GameObject obj)
    {
        if (obj && obj.tag == "Egg")
        {
            obj.SetActive(false);
            inv.RemoveGameObjectFromInventory(obj);
            hiddenEgg.SetActive(true);
            tutorialRoomPortal.SetActive(true);
            glimmer.Stop();
        }
    }
}
