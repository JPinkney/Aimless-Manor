using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public string room;

    private bool isLocked = false;
    private Collider bounds;
    private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        this.door = GameObject.Find(gameObject.name + "/Door (Rotatable)(Clone)");
        this.bounds = GameObject.Find(gameObject.name + "/Bounds").GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!RoomController.CompletedRooms[room])
        {
            Vector3 position = GameObject.Find("Player").transform.position;
            if (!this.isLocked && this.bounds.bounds.Contains(position))
            {
                this.door.GetComponent<OpenableRotatorAllAxes>().closeDoor();
                this.door.GetComponent<BoxCollider>().enabled = false;
                this.isLocked = true;
            }
        } else if (this.isLocked)
        {
            this.door.GetComponent<BoxCollider>().enabled = true;
            this.door.GetComponent<OpenableRotatorAllAxes>().openDoor();
            this.isLocked = false;
        }
    }
}