using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoomScript : MonoBehaviour
{
    public PortalScript[] m_Portals;

    public GameObject[] keys;

    public void Start()
    {
        RoomController.m_staticRef.SetupRoom(this);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
        
            /*
             * Don't unload the hallway
             */
            if (this.gameObject.tag != "hallway")
            {
                /*
                 * Set it to false in loaded rooms so that the RoomController knows
                 * that it can reload it on the new trigger           
                 */
                //RoomController.LoadedRooms[this.gameObject.scene.buildIndex] = false;
                //SceneManager.UnloadSceneAsync(this.gameObject.scene);

                /*
                 * Close the door behind you
                 */
                var first_portal = m_Portals[0];
                Debug.Log(first_portal);
                var linked_portal = first_portal.m_LinkedPortal;
                Debug.Log("Linked portal: " + linked_portal);
                if (linked_portal)
                {
                    Debug.Log("Bleh");
                    var door_rotator = linked_portal.m_currDoor.GetComponent<OpenableRotatorAllAxes>();
                    Debug.Log(door_rotator);
                    door_rotator.closeDoor();
                }

            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            GameObject player = GameObject.Find("colleeder");
            GameObject mana = GameObject.Find("GameManager");
            player.GetComponent<Collisions>().man = mana;
        }
    }

    private void Update()
    {
        if (keys.Length > 0 && !RoomController.CompletedRooms[this.gameObject.tag])
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

        RoomController.CompletedRooms[this.gameObject.tag] = true;
        RoomController.KeyTracker.GetComponent<KeyHUD>().CollectKey();
        return true;
    }
}
