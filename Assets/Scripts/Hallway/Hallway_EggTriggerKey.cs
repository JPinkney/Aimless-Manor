using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway_EggTriggerKey : Interactable
{
    public GameObject key;
    public Transform brokenGlass;
    public GameObject keyController;

    public GameObject leftDoor;
    public GameObject rightDoor;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Non-standard break started");
    }

    public override void Interact(Inventory inv, GameObject obj)
    {
        Debug.Log(this.gameObject.GetComponent<Hallway_EggTriggerKey>().enabled);

        if (keyController)
        {
            keyController.SetActive(true);
        }

        if (!this.gameObject.GetComponent<Hallway_EggTriggerKey>().enabled)
        {
            return;
        }

        this.gameObject.SetActive(false);

        if (brokenGlass)
        {
            Instantiate(brokenGlass, transform.position, transform.rotation);
            brokenGlass.localScale = transform.localScale;
        }
        Physics.IgnoreLayerCollision(9, 9);

        this.key.SetActive(true);
 
        var leftDoorRotationScript = leftDoor.GetComponent<OpenTutorialDoors>();
        if (leftDoorRotationScript)
        {
            leftDoorRotationScript.open();
        }

        var rightDoorRotationScript = rightDoor.GetComponent<OpenTutorialDoors>();
        if (rightDoorRotationScript)
        {
            rightDoorRotationScript.open();
        }

    }
}
