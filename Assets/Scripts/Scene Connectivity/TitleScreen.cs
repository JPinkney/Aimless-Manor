using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public void Update()
    {
        if (RoomController.IsAllRoomCompleted())
        {
            GameObject.Find("BigTitle").GetComponent<Text>().text = "The End";
            GameObject.Find("PlayButton/toPlay").GetComponent<Text>().text = "to Replay";
        }
        else
        {
            GameObject.Find("BigTitle").GetComponent<Text>().text = "Homewrecker";
            GameObject.Find("PlayButton/toPlay").GetComponent<Text>().text = "to Play";
        }
    }
}
