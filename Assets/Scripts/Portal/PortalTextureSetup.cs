using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera cameraTing;
    //public Camera camera2;

    public Material cameraMat;
    //public Material cameraMat2;


    // Start is called before the first frame update
    void Start()
    {
        if (cameraTing.targetTexture != null)
        {
            cameraTing.targetTexture.Release();
        }
        cameraTing.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMat.mainTexture = cameraTing.targetTexture;
        /*
        if (camera2.targetTexture != null)
        {
            camera2.targetTexture.Release();
        }
        camera2.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMat2.mainTexture = camera2.targetTexture;
        */
    }

    

}
