using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class RoomScript : MonoBehaviour
{
    public PortalScript[] m_Portals;

    public GameObject[] keys;

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            /*
             * This will be needed when we have more rooms but for now its
             * fine       
             */

            //Before we delete move the current connecting door to the new room
            //This solution hurts so bad. Cringe Cringe *dies inside*

            /*
             * Set it to false in loaded rooms so that the RoomController knows
             * that it can reload it on the new trigger           
             */
            RoomController.LoadedRooms[this.gameObject.scene.buildIndex] = false;

            SceneManager.UnloadSceneAsync(this.gameObject.scene);
            //Destroy(gameObject);
            //Destroy(this);
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
    