using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionsWithOther : MonoBehaviour
{
    //attach this script to an object that you don't want to collide with a list of OTHER objects
    //collider(s) on itself AND on children objects will be ignorned!!!

    public List<GameObject> objectsToIgnore = new List<GameObject>();

    //[HideInInspector]
    public List<Collider> otherCols = new List<Collider>();

    // Start is called before the first frame update
    void Awake()
    {
        SetupCollisionsIgnore();
    }

    public void SetupCollisionsIgnore()
    {
        // please don't mind the mess... not an official programmer here..... it just works

        //the colliders in THIS OBJECT
        Component[] thisCols = this.gameObject.GetComponentsInChildren(typeof(Collider));

        //add the colliders of OTHER OBJECTS to the otherCols big list
        foreach (GameObject g in objectsToIgnore){
            Component[] others = g.gameObject.GetComponentsInChildren(typeof(Collider));
            foreach(Collider c in others) {
                otherCols.Add(c);
            }
        }

        //Physics.IgnoreCollision(otherCols, thisObjCols);
        foreach (Collider colT in thisCols)
        {
            foreach (Collider colO in otherCols)
            {
                Physics.IgnoreCollision(colT, colO);
                Debug.Log("IGNORED collisions between: " + colT + " and " + colO);
            }
        }

    }


}
