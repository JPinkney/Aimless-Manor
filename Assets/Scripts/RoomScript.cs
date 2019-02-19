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
        
    }

    public bool Complete()
    {

        for(int i = 0; i < keys.Length; i++)
        {
            if (!keys[i].GetComponent<Pickupable>().GetUsed())
            {
                return false;
            }
        }

        // RoomController - call complete in there somewhere
        // the key is currently a path
        // but the room name/tag could be using "contains"?
        // loop and contains?
        return true;
    }
}
