using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickupObject : MonoBehaviour
{
	GameObject mainCamera;
	GameObject currentlySelectedObj;
    public Material mat;

    private GameObject lastOutlinedObject;

    private Inventory inventory;


    // Use this for initialization
    void Start()
	{
        mainCamera = GameObject.FindWithTag("MainCamera");
        this.inventory = new Inventory();

        Debug.Log(GameObject.Find("button_e").name);
    }

    // Update is called once per frame
    void Update()
	{
        SetObjOutline(true);
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
        checkComplete();
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
            FindAndSetOutlineMaterialForObj(true, currentlySelectedObj, false);
        }
        FindAndSetOutlineMaterialForObj(false, o.item, false);
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
            // KEY E appears
            GameObject.Find("button_e").GetComponent<Image>().enabled = false;

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

                        Debug.Log(p.tag.Contains("key"));
                        if (p.tag.Contains("key"))
                        {
                            pickupKey(p);
                        }

                        if (this.inventory.IsInventoryFull())
                        {
                            return;
                        }

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
            currentlySelectedObj.GetComponent<Pickupable>().obtainKey();
            ChangeSelectedObject();
        }
    }

    private void ChangeSelectedObject()
    {
        this.inventory.RemoveGameObjectFromInventory(currentlySelectedObj);

        FindAndSetOutlineMaterialForObj(true, currentlySelectedObj, false);

        GameObject obj = this.inventory.FindFirstObject();
        currentlySelectedObj.layer = 0;
        if (obj)
        {
            FindAndSetOutlineMaterialForObj(false, obj, false);
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

    void pickupKey(Pickupable p)
    {
        Debug.Log("Picking up: " + p.gameObject.name);
        p.gameObject.GetComponent<Renderer>().enabled = false;
        p.obtainKey();
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

            FindAndSetOutlineMaterialForObj(true, currentlySelectedObj, false);
            //This piece of code adds the current item to the room its in instead of keeping it in the hands of the player
            //currentlySelectedObj.gameObject.transform.SetParent(SceneManager.GetSceneAt(1).GetRootGameObjects()[0].transform);
            GameObject obj = this.inventory.FindFirstObject();
            currentlySelectedObj.layer = 0;
            if (obj)
            {
                FindAndSetOutlineMaterialForObj(false, obj, false);
                currentlySelectedObj = obj;
            }
            else
            {
                currentlySelectedObj = null;
            }
        }
    }

    void checkUse()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            useObject();
        }
    }

    public void useObject()
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

            currentlySelectedObj.GetComponent<MeshRenderer>().enabled = false;
            // Destroy(currentlySelectedObj);
            currentlySelectedObj.GetComponent<Pickupable>().obtainKey();

            this.inventory.RemoveGameObjectFromInventory(currentlySelectedObj);

            FindAndSetOutlineMaterialForObj(true, currentlySelectedObj, false);


            //This piece of code adds the current item to the room its in instead of keeping it in the hands of the player
            //currentlySelectedObj.gameObject.transform.SetParent(SceneManager.GetSceneAt(1).GetRootGameObjects()[0].transform);
            GameObject obj = this.inventory.FindFirstObject();
            currentlySelectedObj.layer = 0;
            if (obj)
            {
                FindAndSetOutlineMaterialForObj(false, obj, false);
                currentlySelectedObj = obj;
            }
            else
            {
                currentlySelectedObj = null;
            }
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
    void SetObjOutline(bool keyChange)
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (lastOutlinedObject != null)
        {
            FindAndSetOutlineMaterialForObj(true, lastOutlinedObject, keyChange);
            lastOutlinedObject = null;
        }

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.transform.gameObject;
            if (hitObj.GetComponent<Pickupable>() != null)
            {
                FindAndSetOutlineMaterialForObj(false, hitObj, keyChange);
                lastOutlinedObject = hitObj;
            }
        }

    }

    void FindAndSetOutlineMaterialForObj(bool remove, GameObject obj, bool keyChange)
    {
        Renderer rendererObj = obj.transform.gameObject.GetComponent<Renderer>();
        if (remove)
        {
            //var numMaterials = rendererObj.materials.Length - 2;
            //rendererObj.materials[numMaterials] = this.mat;

            if (keyChange)
            {
                // KEY E disappears
                GameObject.Find("button_e").GetComponent<Image>().enabled = false;
            }
        }
        else
        {
            //var numMaterials = rendererObj.materials.Length;
            //rendererObj.materials[numMaterials] = this.mat;

            if (keyChange)
            {
                // KEY E appears
                GameObject.Find("button_e").GetComponent<Image>().enabled = true;
            }

        }


    }

    /*
    * When some key is pushed it filters to have the next object be the main
    * one   
    *
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

    void checkComplete()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (string key in RoomController.CompletedRooms.Keys)
            {
                Debug.Log("Room: " + key + " Status: " + RoomController.CompletedRooms[key]);
            }
        }
    }
}
