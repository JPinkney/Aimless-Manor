using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionsWithPlayer : MonoBehaviour
{
    //attach this script to an object that you don't want to collide with the player
    //collider(s) on itself AND on children objects will be ignorned!!!

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        SetupCollisionsIgnore();
    }

    public void SetupCollisionsIgnore()
    {
        Component[] playerCols = player.gameObject.GetComponentsInChildren(typeof(Collider));
        Component[] thisObjCols = this.gameObject.GetComponentsInChildren(typeof(Collider));

        //Physics.IgnoreCollision(playerCols, thisObjCols);
        foreach (Collider colP in playerCols)
        {
            foreach (Collider colO in thisObjCols)
            {
                Physics.IgnoreCollision(colP, colO);
            }
        }
    }

}
