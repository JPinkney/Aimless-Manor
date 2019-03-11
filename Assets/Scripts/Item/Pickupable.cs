using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public bool ignoreCollisionsWithPlayer = true;

    private bool used = false;
    private bool key_got = false;

    GameObject player;

    // Use this for initialization
    void Start()
    {
        if(ignoreCollisionsWithPlayer)
        {
            SetupCollisionsIgnore();
        }
    }

    public void obtainKey()
    {
        Debug.Log("OBTAINING KEY");
        this.key_got = true;
    }

    public bool KeyObtained()
    {
        return this.key_got;
    }

    public void SetupCollisionsIgnore()
    {
        player = GameObject.Find("Player");
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
