using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public List<GameObject> cameras;
    private int camIndex;
    private int index;
    private int count;
    private bool cont;
    private int doTheThing;
    public bool camToggle;
    private bool deactivate;

    // Start is called before the first frame update
    void Start()
    {
        cameras = gameObject.GetComponent<CameraManagerDebug>().scenaCameras;
        camIndex = cameras.Count;
        index = 0;
        cont = true;
        count = 0;
        doTheThing = 0;
        if (camToggle)
        {
            deactivate = false;
        }
        else
        {
            deactivate = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (deactivate)
        {
            if (cont && doTheThing == 5)
            {
                //Debug.Log("where we dropping bois");
                cameras[index].SetActive(false);
                index = (index + 1) % camIndex;
                cameras[index].SetActive(true);
                //Debug.Log(index);
                //Debug.Log("set on");
                count++;
                doTheThing = 0;
                if (count > camIndex)
                {
                    cont = false;
                    doTheThing = 5;
                }

            }
            if (cont)
            {
                doTheThing += 1;
            }

            if (cont == false)
            {
                if (index > -1 && doTheThing == 5)
                {
                    cameras[index].SetActive(false);
                    index = -1;
                }
                doTheThing = 0; //don't need?
            }
        }
        
    }

    public void Deactivate()
    {
        deactivate = true;
    }
}
