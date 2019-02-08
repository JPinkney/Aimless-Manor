using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {
  public PortalScript m_LinkedPortal;
	GameObject m_currDoor;

	public void Start() {
    if (m_LinkedPortal == null)
      CloseDoor();
    else
      OpenDoor();
	}

  public void CloseDoor() {
    m_currDoor = Instantiate(RoomController.m_staticRef.m_DoorPrefab, transform.position, transform.rotation) as GameObject;
    m_currDoor.transform.parent = transform;
  }

  public void OpenDoor() {
    Destroy(m_currDoor); // Could play open anim
    Destroy(m_LinkedPortal.m_currDoor);
  }

  public void LoadRoom() {
    if (m_LinkedPortal)
      return;

    RoomController.m_staticRef.LoadRoom(this);
  }

  public void OnDestroy() {
    if (m_LinkedPortal)
      m_LinkedPortal.CloseDoor();
  }
}
