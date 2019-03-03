using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;

public class RoomController : MonoBehaviour
{
    RoomScript[] m_RoomList;
    public static Dictionary<string, bool> CompletedRooms = new Dictionary<string, bool>();
    public static GameObject KeyTracker;

    public GameObject m_DoorPrefab;
    public static RoomController m_staticRef;

    void Start()
    {
        m_staticRef = this;

        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++){
            string scene_name = EditorBuildSettings.scenes[i].path;
            
            if(scene_name != "")
            {
                scene_name = scene_name.Substring(scene_name.LastIndexOf('/') + 1, scene_name.LastIndexOf('.') - (scene_name.LastIndexOf('/') + 1));

                if (scene_name != "root")
                {
                    CompletedRooms[scene_name] = false;
                }
            }
        }

        KeyTracker = GameObject.Find("KeyUI");

        LoadRoom(-1);
    }

    /*
     * So this is where the next room gets loaded and the source portal
     * gets set. After this setup room gets called once the room spawns
     * so that the source portal then looks for a destination portal.
     * 
     * However, the idea is that we can actually set the destination portal
     * from the portal script and then that will take care of which one we go
     * to   
     */
    public void LoadRoom(int destination_room_id)
    {
        int levelIndex;
        if (destination_room_id == -1)
        {
            levelIndex = 1;
        }
        else
        {
            levelIndex = destination_room_id;
        }

        SceneManager.LoadScene(levelIndex, LoadSceneMode.Additive);
    }

}
