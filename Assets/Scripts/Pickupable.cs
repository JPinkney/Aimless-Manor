using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour {

    // public bool key = false;
    private bool used = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetUsed(bool use)
    {
        this.used = use;
    }

    public bool GetUsed()
    {
        return this.used;
    }
}
