using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementFull : MonoBehaviour
{
    public Transform playerCamera;
    public Transform front;
    public Transform back;
    //public Transform altBack;
    private void Start()
    {
        playerCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 playerOffsetFromBack = back.position - playerCamera.position;
        Vector3 temp = altBack.position + playerOffsetFromBack;
        temp.y = temp.y * -1;
        transform.position = temp;
        */


        /*
        Vector3 playerOffsetFromBack = back.position - playerCamera.position;
        Vector3 camPosition = front.position + playerOffsetFromBack;
        camPosition.y = camPosition.y * -1;
        camPosition.x = camPosition.x * -1;
        transform.position = camPosition;
        */

        
        Vector3 position = transform.position;
        position[0] = playerCamera.position.x;
        transform.position = position;
        //transform.rotation = playerCamera.rotation;
        //transform.rotation.x = 0.0f;

        Vector3 eulerAngles = playerCamera.rotation.eulerAngles;
        eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z);
        transform.rotation = Quaternion.Euler(eulerAngles);
        //transform.Rotate(Vector3.right, Time.deltaTime * CycleSpeed, Space.World);




        /*
        //follow player position
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffsetFromPortal;
        */




        //follow player rotation
        /*
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(front.rotation, back.rotation);

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.down);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        */
        //transform.localRotation = Quaternion.Euler(0, 180, 0);

        //transform.rotation = playerCamera.rotation;

    }
}
