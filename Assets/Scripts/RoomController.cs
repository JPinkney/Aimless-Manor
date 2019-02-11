using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour {
	RoomScript[] m_RoomList;
	public GameObject m_DoorPrefab;
	public static RoomController m_staticRef;
    bool m_loadingLevel;
    PortalScript m_sourcePortal;

	void Start () {
		m_staticRef = this;
        m_loadingLevel = false;

        LoadRoom(null);
	}

      public void LoadRoom(PortalScript portal) {
        int levelIndex;
        if(portal == null)
        {
            levelIndex = 1;
        }
        else
        {
            levelIndex = Random.Range(1, 4);
            //levelIndex = 2;
        }

        SceneManager.LoadScene(levelIndex, LoadSceneMode.Additive);
        m_loadingLevel = true;
        m_sourcePortal = portal;
      }

      public void SetupRoom(RoomScript room) {
        room.tag = "Processed";

        if (m_sourcePortal)
        {
          PortalScript destPortal = room.m_Portals[0];
          destPortal.m_LinkedPortal = m_sourcePortal;
          m_sourcePortal.m_LinkedPortal = destPortal;

          room.transform.rotation = Quaternion.LookRotation(
            destPortal.transform.InverseTransformDirection(-m_sourcePortal.transform.forward), 
            destPortal.transform.InverseTransformDirection(m_sourcePortal.transform.up));
          room.transform.position = m_sourcePortal.transform.position + (room.transform.position - destPortal.transform.position);

          m_sourcePortal = null;
        }

        m_loadingLevel = false;
      }
	
}
