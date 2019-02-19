using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickupObject : MonoBehaviour
{
	GameObject mainCamera;
	GameObject currentlySelectedObj;
    public string outline_shader = "Outlined/UltimateOutline";
    public string default_shader = "Standard";

    private GameObject lastOutlinedObject;

    private Inventory inventory;

    // Use this for initialization
    void Start()
	{
        mainCamera = GameObject.FindWithTag("MainCamera");
        this.inventory = new Inventory();
    }

    private void Awake()
    {
        FindObjectOfType<AudioManager>().Play("Theme Music");
    }

    // Update is called once per frame
    void Update()
	{
        setObjOutline();
        if (!this.inventory.IsEmpty())
		{
            foreach (InventoryItem obj in this.inventory.GetInventory())
            {
                if (obj != null)
                {
                    carry(obj);
                }
            }
            checkDrop();
        }

		pickup();
		
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    filterThroughInventory();
        //}
    }

    void carry(InventoryItem o)
    {
        var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
        if (o.gameObjectLocationInInventory.Equals(InventoryItem.Location.LEFT))
        {
            pos.x -= Screen.width/4;
            pos.y -= Screen.height/3;
        } else if (o.gameObjectLocationInInventory.Equals(InventoryItem.Location.RIGHT))
        {
            pos.x += Screen.width/4;
            pos.y -= Screen.height/3;

        }
        if (currentlySelectedObj)
        {
            findAndSetShaderForObj(default_shader, currentlySelectedObj);
        }
        findAndSetShaderForObj(outline_shader, o.item);
        o.item.gameObject.layer = 9;
        o.item.transform.SetParent(this.transform);
        o.item.transform.SetParent(null);
        o.item.transform.position = Camera.main.ScreenToWorldPoint(pos);
        o.item.transform.rotation = Quaternion.identity;
    }

    void pickup()
	{
        if (this.inventory.IsInventoryFull())
        {
            return;
        }

        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        if (Input.GetKeyDown(KeyCode.E))
		{
            RaycastHit[] hitObjects = Physics.RaycastAll(ray);
			if (hitObjects.Length > 0)
			{
                /*
                 * We're only adding the first item so that the player
                 * Has to click e each time to pick up an item
                 * (thats why it returns early when the first one is found)               
                 */
                foreach (RaycastHit ob in hitObjects)
                {
                    Pickupable p = ob.collider.GetComponent<Pickupable>();
                    if (p != null)
                    {
                        p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        this.inventory.AddGameObjectToInventory(p.gameObject);
                        currentlySelectedObj = p.gameObject;
                        return;
                    }
                }

			}
		}
	}

	public void dropObject()
	{
        if (currentlySelectedObj)
        {
            currentlySelectedObj.gameObject.GetComponent<Rigidbody>().useGravity = true;

            Vector3 playerPos = this.transform.position;
            Vector3 playerDirection = this.transform.forward;
            Quaternion playerRotation = this.transform.rotation;
            float spawnDistance = 0.1f;

            Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
            currentlySelectedObj.transform.position = spawnPos;
            this.inventory.RemoveGameObjectFromInventory(currentlySelectedObj);

            findAndSetShaderForObj(default_shader, currentlySelectedObj);
            //This piece of code adds the current item to the room its in instead of keeping it in the hands of the player
            //currentlySelectedObj.gameObject.transform.SetParent(SceneManager.GetSceneAt(1).GetRootGameObjects()[0].transform);
            GameObject obj = this.inventory.FindFirstObject();
            currentlySelectedObj.layer = 0;
            if (obj)
            {
                findAndSetShaderForObj(outline_shader, obj);
                currentlySelectedObj = obj;
            }
            else
            {
                currentlySelectedObj = null;
            }
        }
    }

    void checkDrop()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            dropObject();
        }
    }

    /*
     *
     * Object outlining code 
     * 
     */
    void setObjOutline()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (lastOutlinedObject != null)
        {
            findAndSetShaderForObj(default_shader, lastOutlinedObject);
            lastOutlinedObject = null;
        }

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.transform.gameObject;
            if (hitObj.GetComponent<Pickupable>() != null)
            {
                findAndSetShaderForObj(outline_shader, hitObj);
                lastOutlinedObject = hitObj;
            }
        }

    }

    void findAndSetShaderForObj(string shaderName, GameObject obj)
    {
        Shader shader = Shader.Find(shaderName);
        Renderer rendererObj = obj.transform.gameObject.GetComponent<Renderer>();
        rendererObj.material.shader = shader;
    }

    /*
    * When some key is pushed it filters to have the next object be the main
    * one
    */
    //void filterThroughInventory()
    //{
    //    if (currentlySelectedObj)
    //    {
    //        InventoryItem nextItem = this.inventory.FindNextObject(currentlySelectedObj);
    //        if (nextItem != null)
    //        {
    //            findAndSetShaderForObj(default_shader, currentlySelectedObj);
    //            findAndSetShaderForObj(outline_shader, nextItem.item);
    //            this.currentlySelectedObj = nextItem.item;
    //        }
    //    }
    //}

}
