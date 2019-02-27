using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //need to import UnityEngine.UI to control the text displayed on the GUI

public class DebugText : MonoBehaviour {

    private float nextActionTime = 0.0f;
    public float updateTime = 1f;

    float deltaTime = 0.0f;

    Text screenText;

    void Start () {
        screenText = GameObject.Find("Canvas/DebugText").GetComponent<Text>();
        screenText.enabled = false;
    }
    
    // Update is called once per frame
    void Update () {

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            screenText.enabled = !screenText.enabled;
        }

        if (Time.time > nextActionTime)
        {
            nextActionTime += updateTime;
            float fps = 1.0f / deltaTime;
            screenText.text =
                // \n puts 'enter' in the string
                "PRESS F TO SHOW/HIDE THIS TEXT \n" +
                "[NOT SO AIMLESS MANOR]  \n" +
                "VERSION: 1.1 \n" +
                "APPLICATION RUNNING ON: [" + Application.platform + "] \n" +
                "SCREEN RESOLUTION: [" + Screen.currentResolution + "] \n" +
                "FPS: [" + fps + "] \n"
            ;

        }


    }
}