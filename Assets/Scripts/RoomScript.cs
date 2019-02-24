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

        /*
         * I'm commenting this out because I think it might be easier if we
         * just manually set the keys because FindGameObjectsWithTag doesn't
         * find hidden objects iirc
         */
        //keys = GameObject.FindGameObjectsWithTag(this.gameObject.tag + "_key");
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
        if (keys.Length > 0 && !RoomController.CompletedRooms["room_" + this.gameObject.tag])
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
        RoomController.KeyTracker.GetComponent<KeyHUD>().CollectKey();
        return true;
    }
}
    