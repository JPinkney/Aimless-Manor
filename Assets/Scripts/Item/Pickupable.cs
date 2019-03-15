using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public bool ignoreCollisionsWithPlayer = true;

    private bool used = false;
    private bool key_got = false;

    private Component[] origRenderers;
    //public List<List<Material>> origMats = new List<List<Material>>();
    public List<Material[]> origMats = new List<Material[]>(); //a list of arrays of materials

    GameObject player;
    //GameObject key;

    // Use this for initialization
    void Start()
    {
        if(ignoreCollisionsWithPlayer)
        {
            SetupCollisionsIgnore();
        }

        origRenderers = this.GetComponentsInChildren(typeof(Renderer));
        foreach(Renderer rend in origRenderers)
        {
            origMats.Add(rend.materials);
        }

        //for(int r=0; r<origRenderers.Length; r++)
        //{

        //}
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
