using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class RoomScript : MonoBehaviour
{
    public PortalScript[] m_Portals;

    public GameObject[] keys;

    public void Start()
    {
        RoomController.m_staticRef.SetupRoom(this);

        keys = GameObject.FindGameObjectsWithTag(this.gameObject.tag + "_key");
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            /*
             * This will be needed when we have more rooms but for now its
             * fine       
             */
            //SceneManager.UnloadSceneAsync(this.gameObject.scene);
            Destroy(gameObject);
            Destroy(this);
        }
    }

    private void Update()
    {
        if(!RoomController.CompletedRooms["room_" + this.gameObject.tag])
        {
            Complete();
        }
    }

    public bool Complete()
    {

        for(int i = 0; i < keys.Length; i++)
        {
            if (!keys[i].GetComponent<Pickupable>().KeyObtained())
            {
                return false;
            }
        }

        RoomController.CompletedRooms["room_" + this.gameObject.tag] = true;
        return true;
    }
}
