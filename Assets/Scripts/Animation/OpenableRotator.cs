using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableRotator : MonoBehaviour
{
    //guide from: https://answers.unity.com/questions/896552/opening-door-script-in-c.html

    //the actual object that will be rotated, set it to be the correct door GameObject in the hierarchy
    public GameObject objectToRotate;

    //y-value for rotation
    public float doorOpenAngle = -150.0f;
    public float doorCloseAngle = 0.0f;
    public float doorAnimSpeed = 2.0f;
    private Quaternion doorOpen = Quaternion.identity;
    private Quaternion doorClose = Quaternion.identity;
    public bool doorStatus = false; //false is close, true is open
    private bool doorGo = false; //for Coroutine, when start only one
    void Start()
    {
        doorStatus = false; //door is open, maybe change
                            //Initialization of quaternions
        doorOpen = Quaternion.Euler(0, doorOpenAngle, 0);
        doorClose = Quaternion.Euler(0, doorCloseAngle, 0);
    }
    void Update()
    {
        //If press Space key on keyboard (***********************CHANGE TO 'ON INTERACT')
        if (Input.GetKeyDown(KeyCode.Space) && !doorGo)
        {
            if (doorStatus)
            { //close door
                StartCoroutine(this.MoveDoor(doorClose));
            }
            else
            { //open door
                StartCoroutine(this.MoveDoor(doorOpen));
            }
        }
    }
    public IEnumerator MoveDoor(Quaternion dest)
    {
        doorGo = true;
        //Check if close/open, if angle less 4 degree, or use another value more 0
        while (Quaternion.Angle(objectToRotate.transform.localRotation, dest) > 4.0f)
        {
            objectToRotate.transform.localRotation = Quaternion.Slerp(objectToRotate.transform.localRotation, dest, Time.deltaTime * doorAnimSpeed);
            yield return null;
        }
        //Change door status
        doorStatus = !doorStatus;
        doorGo = false;
        yield return null;
    }
}
