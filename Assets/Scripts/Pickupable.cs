using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour {

    private bool used = false;

    public void SetUsed(bool use)
    {
        this.used = use;
    }

    public bool GetUsed()
    {
        return this.used;
    }
}
