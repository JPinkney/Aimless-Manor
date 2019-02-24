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
    bool m_loadingLevel;
    PortalScript m_sourcePortal;
    PortalScript m_destPortal;

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

        KeyTracker = GameObject.Find("KeyUI");

        LoadRoom(null);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        //    SceneManager.LoadScene(4);
        //}
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
    public void LoadRoom(PortalScript portal)
    {
        int levelIndex;
        if (portal == null)
        {
            levelIndex = 1;
        }
        else
        {
            levelIndex = portal.m_destRoomID;
        }

        SceneManager.LoadScene(levelIndex, LoadSceneMode.Additive);
        m_loadingLevel = true;
        m_sourcePortal = portal;
    }

    public void SetupRoom(RoomScript room)
    {

        if (m_sourcePortal)
        {
            //PortalScript destPortal = room.m_Portals[0];
            /*
             * This is going to find the portal in the new room that matches
             * the portals tag of the old room           
             */
            PortalScript destPortal = FindObjectByTag(room.m_Portals, m_sourcePortal.tag);
            Debug.Log(destPortal);

            if (destPortal)
            {
                destPortal.m_LinkedPortal = m_sourcePortal;
                m_sourcePortal.m_LinkedPortal = destPortal;

                room.transform.rotation = Quaternion.LookRotation(
                  destPortal.transform.InverseTransformDirection(-m_sourcePortal.transform.forward),
                  destPortal.transform.InverseTransformDirection(m_sourcePortal.transform.up));
                room.transform.position = m_sourcePortal.transform.position + (room.transform.position - destPortal.transform.position);

                m_sourcePortal = null;
            }

        }

        m_loadingLevel = false;
    }

    private PortalScript FindObjectByTag(PortalScript[] portals, string findTag)
    {
        foreach(PortalScript s in portals)
        {
            if (s.tag == findTag)
            {
                return s;
            }
        }
        return null;
    }

}
