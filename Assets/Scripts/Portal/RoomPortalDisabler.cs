using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RoomPortalDisabler : MonoBehaviour
{

    /*
     * Sets the camera visibility to given boolean
     * 
     * Used for low spec mode
     */
    public void SetCameraVisibility(int index, bool cameraVisibility)
    {
        Scene s = SceneManager.GetSceneByBuildIndex(index);
        foreach (GameObject o in s.GetRootGameObjects())
        {
            CameraManagerDebug c = o.GetComponent<CameraManagerDebug>();
            if (c != null)
            {
                SetVisibility(c.scenaCameras, cameraVisibility);
            }
        }
    }

    private void SetVisibility(List<GameObject> cameras, bool cameraVisibility)
    {
        foreach (GameObject c in cameras)
        {

            c.SetActive(cameraVisibility);
        }
    }
}
