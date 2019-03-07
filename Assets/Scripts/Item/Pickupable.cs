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

    public void obtainKey()
    {
        Debug.Log("OBTAINING KEY");
        this.key_got = true;
    }

    public bool KeyObtained()
    {
        return this.key_got;
    }
}
