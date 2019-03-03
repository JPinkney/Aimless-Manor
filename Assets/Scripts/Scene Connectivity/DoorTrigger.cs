using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

  float m_delayTimer;
  bool m_inTrigger;
    public int m_destRoomID;

public void Start() {
    m_delayTimer = 1.0f;
    m_inTrigger = false;
  }

  public void Open() {
        RoomController.m_staticRef.LoadRoom(m_destRoomID);
        GetComponent<Collider>().enabled = false; // Stops retrigger
    m_delayTimer = 1.0f;
  }

  public void Update() {
    if(m_delayTimer > 0)
      m_delayTimer -= Time.deltaTime;

    if ((m_delayTimer <= 0.0f) && m_inTrigger)
    {
        //Debug.Log("This is where it was re-opening the passageway");
        //Open();
    }

  }

  public void OnTriggerEnter(Collider other)
  {
    if (other.name != "Player")
      return;

    m_inTrigger = true;

    if(m_delayTimer > 0)
      return;

    Open();
  }

  public void OnTriggerExit(Collider other) 
  {
    if (other.tag != "Player")
      return;

    m_inTrigger = false;
  }
}
