using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugButtons : MonoBehaviour {

    // Update is called once per frame
    void Update () {
        
        // LOAD SCENES BY PRESSING 1, 2, and 3 buttons on keyboard
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            LoadTutorial();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            LoadHallway();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)){
            LoadKitchen();
        }

        // QUIT GAME BY PRESSING ESCAPE
        if (Input.GetKeyDown(KeyCode.Escape)){
            ExitApp();
        }
    }

    // THESE FUNCTIONS CAN ALSO BE ACCESSED BY OTHER SCRIPTS AND EVENTS
    void LoadTutorial(){
        SceneManager.LoadScene(1);
    }

    void LoadHallway(){
        SceneManager.LoadScene(2);
    }

    void LoadKitchen(){
        SceneManager.LoadScene(3);
    }

    void ExitApp(){
        Application.Quit();
    }

}
