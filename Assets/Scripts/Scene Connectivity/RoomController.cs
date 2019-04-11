using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;
using System;
//using UnityEditor.SceneManagement;

//#if UNITY_EDITOR
public class RoomController : MonoBehaviour
{
    RoomScript[] m_RoomList;

    public string [] room_roots;
    public static Dictionary<string, bool> CompletedRooms = new Dictionary<string, bool>();

    // This is going to stop a mapping of build setting number to if its loaded or not
    public static Dictionary<int, bool> LoadedRooms = new Dictionary<int, bool>();
    public static GameObject KeyTracker;

    public GameObject m_DoorPrefab;
    public static RoomController m_staticRef;

    public bool lowSpecModeEnabled = false;

    public RoomPortalDisabler portalDisable;

    PortalScript m_sourcePortal;
    PortalScript m_destPortal;

   
    void Start()
    {
        m_staticRef = this;

        // no - scene index should be the same number as the build number
        // meaning 0 is true and 1 is true
        // and 0 is hallway, so it should be i + 2
        // and the lengths of the LoadedRooms should be 4 in total


        for (int i = 0; i < room_roots.Length; i ++)
        {
            CompletedRooms[room_roots[i]] = false;
            LoadedRooms[i+2] = false;
        }
        DontDestroyOnLoad(this.gameObject);

        LoadedRooms[1] = true;

        DontDestroyOnLoad(this.gameObject);

        portalDisable = FindObjectOfType<RoomPortalDisabler>();

        KeyTracker = GameObject.Find("KeyUI");

        LoadRoom(null);
    }



    private void Update()
    {
        //List<String> checkDuplicateRooms = new List<String>();
        //for (int i = 1; i < SceneManager.sceneCount; i++)
        //{
        //    String sceneName = SceneManager.GetSceneAt(i).name;
        //    if (checkDuplicateRooms.Contains(sceneName))
        //    {
        //        SceneManager.UnloadSceneAsync(i);
        //        Debug.Log("Unloading Scene: " + sceneName);
        //    } else
        //    {
        //        checkDuplicateRooms.Add(sceneName);
        //    }
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
            //levelIndex = 1;
            levelIndex = 2;
        }
        else
        {
            levelIndex = portal.m_destRoomID;
        }
        Debug.Log("----------------Loading Index: " + levelIndex);
        if (!LoadedRooms[levelIndex])
        {


            if (lowSpecModeEnabled)
            {
                StartCoroutine(LoadSceneFromIndexAsync(portal, levelIndex));
            }
            else
            {
                LoadSceneFromIndexSync(portal, levelIndex);
                LoadedRooms[levelIndex] = true;
            }

        }

    }

    private void LoadSceneFromIndexSync(PortalScript portal, int levelIndex)
    {
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Additive);
        portalDisable.SetCameraVisibility(levelIndex, true);
        m_sourcePortal = portal;
    }

    private IEnumerator LoadSceneFromIndexAsync(PortalScript portal, int levelIndex)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Additive);
        while (!ao.isDone)
        {
            yield return null;
        }
        LoadedRooms[levelIndex] = true;
        portalDisable.SetCameraVisibility(levelIndex, false);
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

            if (destPortal)
            {
                destPortal.m_LinkedPortal = m_sourcePortal;
                m_sourcePortal.m_LinkedPortal = destPortal;
                m_sourcePortal = null;
            }

        }

    }

    private PortalScript FindObjectByTag(PortalScript[] portals, string findTag)
    {
        foreach (PortalScript s in portals)
        {
            if (s.tag == findTag)
            {
                return s;
            }
        }
        return null;
    }


    public static bool IsAllRoomCompleted()
    {
        if (RoomController.CompletedRooms.Count == 0) { return false; }

        foreach (KeyValuePair<string, bool> entry in RoomController.CompletedRooms)
        {
            if (!entry.Value)
            {
                return false;
            }
        }

        return true;
    }

    public static void SetAllRoomsIncomplete()
    {
        try
        {
            foreach (KeyValuePair<string, bool> entry in RoomController.CompletedRooms)
            {
                RoomController.CompletedRooms[entry.Key] = false;
            }

            KeyTracker.GetComponent<KeyHUD>().collectedKeys = 0;
        }
        catch (NullReferenceException nre)
        {
        }
        catch (InvalidOperationException ioe)
        {
        }
    }

}
