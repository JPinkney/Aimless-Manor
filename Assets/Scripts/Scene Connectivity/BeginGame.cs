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
        }  else if (Input.GetKeyDown(KeyCode.Escape) || XCI.GetButton(XboxButton.Start))
        {
            Quit();
        }
    }

    // Start is called before the first frame update
    public void Restart ()
    {
        SceneManager.LoadScene(1);
        RoomController.SetAllRoomsIncomplete();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
