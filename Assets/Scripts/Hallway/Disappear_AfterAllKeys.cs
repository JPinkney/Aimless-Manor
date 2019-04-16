using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear_AfterAllKeys : MonoBehaviour
{
    public GameObject doorL, doorR;
    public GameObject AllKeysBox;
    public GameObject KeyLock;
    public GameObject KeyOpen;
    public GameObject chains;
    public GameObject chainsBroken;

    [Range(0.1f, 60.0f)]
    public float destroyDelay = 10.0f;

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

            KeyLock.SetActive(false);

            KeyOpen.SetActive(true);

            chains.SetActive(false);

            chainsBroken.SetActive(true);

            Destroy(chainsBroken.gameObject, destroyDelay);

            this.gameObject.SetActive(false);

            this.doorL.transform.Rotate(0, -90, 0); // left: -90
            this.doorR.transform.Rotate(0, 90, 0);

            this.done = true;
        }


    }
}
