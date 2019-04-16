using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear_AfterAllKeys : MonoBehaviour
{
    public GameObject doorL, doorR;
    public GameObject AllKeysBox;

    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (RoomController.IsAllRoomCompleted() && !done)
        {
            AllKeysBox.SetActive(true);

            this.gameObject.SetActive(false);

            this.doorL.transform.Rotate(0, -90, 0); // left: -90
            this.doorR.transform.Rotate(0, 90, 0);

            this.done = true;
        }


    }
}
