using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class BeginGame : MonoBehaviour
{

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) || XCI.GetButton(XboxButton.X))
        {
            Restart();
        }  
    }

    // Start is called before the first frame update
    public void Restart ()
    {
        SceneManager.LoadScene(0);
        RoomController.SetAllRoomsIncomplete();
    }
}
