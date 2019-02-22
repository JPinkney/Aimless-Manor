using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;

public class RoomController : MonoBehaviour
{
    RoomScript[] m_RoomList;
    public static Dictionary<string, bool> CompletedRooms = new Dictionary<string, bool>();

    public GameObject m_DoorPrefab;
    public static RoomController m_staticRef;
    bool m_loadingLevel;
    PortalScript m_sourcePortal;

    void Start()
    {
        m_staticRef = this;
        m_loadingLevel = false;

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


        LoadRoom(null);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadScene(4);
        }
    }

    public void LoadRoom(PortalScript portal)
    {
        int levelIndex;
        if (portal == null)
        {
            levelIndex = 1;
        }
        else
        {
            // levelIndex = Random.Range(1, 4);
            levelIndex = 3;
        }

        SceneManager.LoadScene(levelIndex, LoadSceneMode.Additive);
        m_loadingLevel = true;
        m_sourcePortal = portal;
    }

    public void SetupRoom(RoomScript room)
    {
        // room.tag = "Processed";

        if (m_sourcePortal)
        {
            PortalScript destPortal = room.m_Portals[0];
            destPortal.m_LinkedPortal = m_sourcePortal;
            m_sourcePortal.m_LinkedPortal = destPortal;

            room.transform.rotation = Quaternion.LookRotation(
              destPortal.transform.InverseTransformDirection(-m_sourcePortal.transform.forward),
              destPortal.transform.InverseTransformDirection(m_sourcePortal.transform.up));
            room.transform.position = m_sourcePortal.transform.position + (room.transform.position - destPortal.transform.position);

            m_sourcePortal = null;
        }

        m_loadingLevel = false;
    }

}
