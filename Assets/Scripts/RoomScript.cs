using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class RoomScript : MonoBehaviour {

    public PortalScript[] m_Portals;

  public void Start() {
    RoomController.m_staticRef.SetupRoom(this);
  }

  public void OnTriggerExit(Collider other) {
        if(other.name == "Player")
        {
            /*
             * This will be needed when we have more rooms but for now its
             * fine       
             */
            //SceneManager.UnloadSceneAsync(this.gameObject.scene);

            AudioManager audioManager = FindObjectOfType<AudioManager>();
            audioManager.WaitForCurrentlyPlayingSoundToFinish();

            Inventory inven = other.gameObject.GetComponent<Inventory>();
            if (inven != null)
            {
                InventoryItem[] inventory = inven.GetInventory();

                /*
                 * Inventory doesn't derive from monobehaviour so we cannot have
                 * the destroy object in inventory so i'm yoloing it here               
                 */
                if (inventory[0] != null)
                {
                    Destroy(inventory[0].item);
                }

                if (inventory[1] != null)
                {
                    Destroy(inventory[1].item);
                }
            }

            Destroy(gameObject);
            Destroy(this);
        }
    }

}
