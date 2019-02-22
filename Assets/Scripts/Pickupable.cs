using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour {

    private bool used = false;
    private bool key_got = false;

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

    public void obtainKey()
    {
        this.key_got = true;
    }

    public bool KeyObtained()
    {
        return this.key_got;
    }
}
