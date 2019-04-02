﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private int platesBroke;
    public int numPlates;
    public GameObject portals;
    public GameObject inf;
    public GameObject camMan;
    private CameraHandler script;

    // Start is called before the first frame update
    void Start()
    {
        platesBroke = 0;
        script = camMan.GetComponent<CameraHandler>();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

    public void PlateBroke()
    {
        platesBroke++;
        if (platesBroke == numPlates)
        {
            Done();
        }
    }

    private void Done()
    {
        portals.SetActive(true);
        script.Deactivate();
        inf.SetActive(false);

    }

}
