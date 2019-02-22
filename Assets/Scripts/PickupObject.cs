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

    // Update is called once per frame
    void Update()
	{
        SetObjOutline();
        if (!this.inventory.IsEmpty())
		{
            foreach (InventoryItem obj in this.inventory.GetInventory())
            {
                if (obj != null)
                {
                    Carry(obj);
                }
            }
            CheckDrop();
            CheckUse();
        }

        Interact();
        Pickup();
    }

    void Carry(InventoryItem o)
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
            FindAndSetShaderForObj(default_shader, currentlySelectedObj);
        }
        FindAndSetShaderForObj(outline_shader, o.item);
        //o.item.layer = 9;
        o.item.gameObject.layer = 9;
        o.item.transform.SetParent(this.transform);
        o.item.transform.SetParent(null);
        o.item.transform.position = Camera.main.ScreenToWorldPoint(pos);
        o.item.transform.rotation = Quaternion.identity;
    }

    void Pickup()
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

    void Interact()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RaycastHit[] hitObjects = Physics.RaycastAll(ray);
            if (hitObjects.Length > 0)
            {
                foreach (RaycastHit ob in hitObjects)
                {
                    Interactable p = ob.collider.GetComponent<Interactable>();
                    if (p != null)
                    {
                        //p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        //this.inventory.AddGameObjectToInventory(p.gameObject);
                        //currentlySelectedObj = p.gameObject;
                        foreach(InventoryItem obj in this.inventory.GetInventory())
                        {
                            if (obj != null)
                            {
                                p.Interact(this.inventory, obj.item);
                            }
                        }
                    }
                }

            }
        }
    }

    void CheckUse()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UseObject();
        }
    }

    private void ChangeCurrentlySelectedObjectLocation()
    {
        currentlySelectedObj.gameObject.GetComponent<Rigidbody>().useGravity = true;

        Vector3 playerPos = this.transform.position;
        Vector3 playerDirection = this.transform.forward;
        Quaternion playerRotation = this.transform.rotation;
        float spawnDistance = 0.1f;

        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
        currentlySelectedObj.transform.position = spawnPos;
    }

    public void UseObject()
    {
        if (currentlySelectedObj)
        {
            ChangeCurrentlySelectedObjectLocation();
            currentlySelectedObj.GetComponent<MeshRenderer>().enabled = false;
            currentlySelectedObj.GetComponent<Pickupable>().SetUsed(true);
            ChangeSelectedObject();
        }
    }

    private void ChangeSelectedObject()
    {
        this.inventory.RemoveGameObjectFromInventory(currentlySelectedObj);

        FindAndSetShaderForObj(default_shader, currentlySelectedObj);

        GameObject obj = this.inventory.FindFirstObject();
        currentlySelectedObj.layer = 0;
        if (obj)
        {
            FindAndSetShaderForObj(outline_shader, obj);
            currentlySelectedObj = obj;
        }
        else
        {
            currentlySelectedObj = null;
        }
    }


    public void DropObject()
	{
        if (currentlySelectedObj)
        {
            ChangeCurrentlySelectedObjectLocation();
            //This piece of code adds the current item to the room its in instead of keeping it in the hands of the player
            //currentlySelectedObj.gameObject.transform.SetParent(SceneManager.GetSceneAt(1).GetRootGameObjects()[0].transform);
            ChangeSelectedObject();
        }
    }

    void CheckDrop()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DropObject();
        }
    }

    /*
     *
     * Object outlining code 
     * 
     */
    void SetObjOutline()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (lastOutlinedObject != null)
        {
            FindAndSetShaderForObj(default_shader, lastOutlinedObject);
            lastOutlinedObject = null;
        }

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.transform.gameObject;
            if (hitObj.GetComponent<Pickupable>() != null)
            {
                FindAndSetShaderForObj(outline_shader, hitObj);
                lastOutlinedObject = hitObj;
            }
        }

    }

    void FindAndSetShaderForObj(string shaderName, GameObject obj)
    {
        Shader shader = Shader.Find(shaderName);
        Renderer rendererObj = obj.transform.gameObject.GetComponent<Renderer>();
        rendererObj.material.shader = shader;
    }

}
