using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorialDoors : MonoBehaviour
{
    //guide from: https://answers.unity.com/questions/896552/opening-door-script-in-c.html
    //substantially modified !EXPERIMENTAL! doing multiple axes at once can cause it to look funky...

    //the actual object that will be rotated, set it to be the correct door GameObject in the hierarchy
    public GameObject objectToRotate;

    //inspector-settable Vector3s for desired opening and closing rotations
    public Vector3 doorOpenRot = new Vector3(0.0f, 150.0f, 0.0f);
    public Vector3 doorCloseRot = new Vector3(0.0f, 0.0f, 0.0f);

    //inspector-settable opening/closing animation speed
    [Range(1.0f, 10.0f)]
    public float doorAnimSpeed = 2.0f;

    private Quaternion doorOpen = Quaternion.identity;
    public Quaternion doorClose = Quaternion.identity;

    public bool doorStatus = false; //false is close, true is open
    private bool doorGo = false; //for Coroutine, when start only one

    void Start()
    {
        //doorStatus = false; //door is open, maybe change
        //Initialization of quaternions, 'destination rotations'
        doorOpen = Quaternion.Euler(doorOpenRot.x, doorOpenRot.y, doorOpenRot.z);
        doorClose = Quaternion.Euler(doorCloseRot.x, doorCloseRot.y, doorCloseRot.z);
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

    public void open()
    {
        StartCoroutine(this.MoveDoor(doorOpen));
    }

}
