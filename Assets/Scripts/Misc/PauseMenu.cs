using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject PauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || XCI.GetButton(XboxButton.Start))
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X) || XCI.GetButton(XboxButton.Start))
        {
            if (isPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(1);
        RoomController.SetAllRoomsIncomplete();
    }

    public void Quit()
    {
        Resume();
        SceneManager.LoadScene(0);
        RoomController.SetAllRoomsIncomplete();
    }
}
