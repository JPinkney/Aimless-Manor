using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway_EggTriggerKey : Interactable
{
    public GameObject key;
    public Transform brokenGlass;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Non-standard break started");
    }

    public override void Interact(Inventory inv, GameObject obj)
    {
        Debug.Log(this.gameObject.GetComponent<Hallway_EggTriggerKey>().enabled);

        if (!this.gameObject.GetComponent<Hallway_EggTriggerKey>().enabled)
        {
            return;
        }

        Debug.Log("Break for key is triggered");

        this.gameObject.SetActive(false);

        if (brokenGlass)
        {
            Instantiate(brokenGlass, transform.position, transform.rotation);
            brokenGlass.localScale = transform.localScale;
        }
        Physics.IgnoreLayerCollision(9, 9);

        this.key.SetActive(true);
    }
}
