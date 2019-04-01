using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMove : MonoBehaviour
{

    public Transform playerCamera;
    public Transform front;
    public Transform back;
    public int direc;
    public float dist;
    //public Transform altBack;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("Main Camera").GetComponent<Transform>();


        /*

        Vector3 positionFromPortal = playerCamera.position - portal.position;
        //float xPos = positionFromPortal[0];
        Vector3 position = transform.position;
        float x = (portal.position.x + positionFromPortal.x) * move;
        float y = position[1];
        float z = position[2];
        transform.position = new Vector3(x, y, z);
        */
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 hallSize = front.position - back.position;
        Vector3 position = transform.position;
        float x = position[0];
        float y = position[1];
        float z = playerCamera.position.z + (dist * direc);
        transform.position = new Vector3(x, y, z);
    }
}
