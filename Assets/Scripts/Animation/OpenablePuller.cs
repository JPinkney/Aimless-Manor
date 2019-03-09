using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenablePuller : MonoBehaviour
{
    //this script should work even if you set the objectToPull to the object this script is attached to (its own parent in a way)
    //if parent is moved the child drawer should still be moving properly unless the set objectToPull object is 'its own parent'

    //the actual object that will be pulled/pushed, set it to be the correct 'drawer' GameObject in the hierarchy
    public GameObject objectToPull;

    //THESE ARE MEANT TO BE ADDED TO CURRENT LOCAL
    //original local pos
    private Vector3 origObjectPos = new Vector3(0.0f, 0.0f, 0.0f);

    //inspector-settable relative offset to be added for drawer movement
    public Vector3 drawerOpenPos = new Vector3(0.2f, 0.0f, 0.0f);
    public Vector3 drawerClosePos = new Vector3(0.0f, 0.0f, 0.0f);

    //to be set up in Start(), actual local coords to move to. at the moment this script will not update this value if the objectToPull object is moved manually so watch out, animation will break...
    private Vector3 drawerOpen = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 drawerClose = new Vector3(0.0f, 0.0f, 0.0f);

    [Range(1.0f, 10.0f)]
    public float drawerAnimSpeed = 4.0f;

    public bool drawerStatus = false; //false is close, true is open
    private bool drawerGo = false; //for Coroutine, when start only one

    // starting value for the Lerp
    //static float t = 0.0f;

    void Start()
    {
        //set where the drawerOpen and drawerClose destination positions are in local space
        //get the object's current pos in local space (note that the parent object can be moved in world space, and this script should still work for the child object!)
        origObjectPos = new Vector3(
            objectToPull.transform.localPosition.x,
            objectToPull.transform.localPosition.y,
            objectToPull.transform.localPosition.z
        );

        drawerOpen = origObjectPos + drawerOpenPos;
        drawerClose = origObjectPos + drawerClosePos;

        ////by default start closed
        //drawerStatus = false;

    }

    void Update()
    {
        //If press P key on keyboard (***********************CHANGE TO 'ON INTERACT')
        if (Input.GetKeyDown(KeyCode.P) && !drawerGo)
        {
            if (drawerStatus)
            { //close drawer
                StartCoroutine(this.MoveDrawer(drawerClose));
                Debug.Log("Drawer closing started on: " + objectToPull.gameObject.name + " inside " + this.gameObject.name + " in scene " + objectToPull.scene.name);
            }
            else
            { //open drawer
                StartCoroutine(this.MoveDrawer(drawerOpen));
                Debug.Log("Drawer opening started on: " + objectToPull.gameObject.name + " inside " + this.gameObject.name + " in scene " + objectToPull.scene.name);
            }
        }

    }
    public IEnumerator MoveDrawer(Vector3 dest)
    {
        drawerGo = true;

        while ((Vector3.Distance(objectToPull.transform.localPosition, dest)) > 0.03f)
        {
            objectToPull.transform.localPosition = Vector3.Lerp(objectToPull.transform.localPosition, dest, Time.deltaTime * drawerAnimSpeed);
            yield return null;
        }
        //Change door status
        drawerStatus = !drawerStatus;
        drawerGo = false;
        yield return null;
    }

}
