using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour
{
    public bool isGrounded;

    private Rigidbody rb;

    public float moveSpeed;
    public float turnSpeed;

    public float speed;

    //public GameObject man;
    public Camera cam;

    public float minY = -70.0f;
    public float maxY = 70.0f;
    public Vector3 euler;

    private bool updateCrosshair = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        /*
         * RIGHT STICK MOVEMENT
         */
        float axisX = XCI.GetAxis(XboxAxis.RightStickX);
        float axisY = XCI.GetAxis(XboxAxis.RightStickY);

        //Looking left and right with controller
        if (axisX < 0 || axisX > 0)
        {
            //Look Left
            //Vector3 rot = new Vector3(0f, axisX, 0f);
            //transform.Rotate(rot);
            euler.y += axisX;
        }

        //Looking up and down with controller
        if (axisY < 0 || axisY > 0)
        {
            //Look Right
            //Vector3 rot = new Vector3(-axisY, 0f, 0f);
            //cam.transform.Rotate(rot);
            euler.x += (-axisY);
        }

        /*
         * LEFT STICK MOVEMENT
         */
        float leftAxisY = XCI.GetAxis(XboxAxis.LeftStickX); //Left is - Right is +
        float leftAxisX = XCI.GetAxis(XboxAxis.LeftStickY);

        //Left stick moving left
        if (leftAxisX < 0)
        {
            rb.transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        }

        //Left stick moving right
        if (leftAxisX > 0)
        {
            rb.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        //Left stick moving left
        if (leftAxisY < 0)
        {
            rb.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        //Left stick moving right
        if (leftAxisY > 0)
        {
            rb.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        /*
         * MOUSE AND KEYBOARD CONTROLS
         */
        float yRot = Input.GetAxisRaw("Mouse X");
        if (yRot > 0 || yRot < 0)
        {
            //This is old rotation code I don't want to remove just in case
            Vector3 rot = new Vector3(0f, yRot, 0f);
            transform.Rotate(rot);
            euler.y += yRot;
            ////transform.rotation = Quaternion.Euler(euler);
            //transform.Rotate(euler);
            //updateCrosshair = true;
        }

        float xRot = Input.GetAxisRaw("Mouse Y");
        if (xRot > 0 || xRot < 0)
        {
            //This is old rotation code I don't want to remove just in case
            Vector3 camRot = new Vector3(xRot, 0, 0f);
            //if (cam.transform.rotation.x < maxY && cam.transform.rotation.x > minY)
            //{
            //    Debug.Log(cam.transform.rotation.x);
            cam.transform.Rotate(-camRot);
            //}
            //euler.x += (-xRot);
            //if (euler.x >= maxY)
            //{
            //    euler.x = maxY;
            //}
            //if (euler.x <= minY)
            //{
            //    euler.x = minY;
            //}
            //updateCrosshair = true;
        }

       

        if (Input.GetKey(KeyCode.W))
        {
            rb.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
            
        if (Input.GetKey(KeyCode.S))
        {
            rb.transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
        }
            
        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
          
        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        //if (updateCrosshair)
        //{
        //    transform.rotation = Quaternion.Euler(euler);
        //    updateCrosshair = false;
        //}


    }
}
