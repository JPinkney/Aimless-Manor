using UnityEngine;
using System.Collections;

/*
 * On the actual portal we have the portal script that actually spawns the doors
 * onto the portal object
 */
public class PortalScript : MonoBehaviour
{
    public PortalScript m_LinkedPortal;
    public GameObject m_currDoor;

    public PortalScript m_DestPortal;
    public int m_destRoomID;

    public void Start()
    {
        if (m_LinkedPortal == null)
            CloseDoor();
        else
            OpenDoor();
    }

    public void CloseDoor()
    {
        //If its in the hallway then instantiate the doors
        //Otherwise we shouldn't need them
        if (this.gameObject.scene.buildIndex == 1 && m_LinkedPortal == null)
        {
            m_currDoor = Instantiate(RoomController.m_staticRef.m_DoorPrefab, transform.position, transform.rotation) as GameObject;
            m_currDoor.GetComponent<OpenableRotatorAllAxes>().objectToRotate = m_currDoor;
            m_currDoor.transform.parent = transform;
        }
    }

    public void OpenDoor()
    {
        //Destroy(m_currDoor); // Could play open anim
        //Destroy(m_LinkedPortal.m_currDoor);
    }

    public void LoadRoom()
    {
        if (m_LinkedPortal)
            return;

        RoomController.m_staticRef.LoadRoom(this);
    }

    public void OnDestroy()
    {
        if (m_LinkedPortal)
            m_LinkedPortal.CloseDoor();
    }
}