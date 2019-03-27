using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear_AfterAllKeys : MonoBehaviour
{
    public GameObject doorL, doorR;

    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.doorL.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (RoomController.IsAllRoomCompleted() && !done)
        {
            this.gameObject.SetActive(false);

            this.doorL.transform.Rotate(0, -90, 0); // left: -90
            this.doorR.transform.Rotate(0, 90, 0);

            this.done = true;
        }
    }
}
