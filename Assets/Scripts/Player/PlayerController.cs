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
            Vector3 rot = new Vector3(0f, axisX, 0f);
            transform.Rotate(rot);
        }

        //Looking up and down with controller
        if (axisY < 0 || axisY > 0)
        {
            //Look Right
            Vector3 rot = new Vector3(-axisY, 0f, 0f);
            cam.transform.Rotate(rot);
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
            Vector3 rot = new Vector3(0f, yRot, 0f);
            transform.Rotate(rot);
        }

        float xRot = Input.GetAxisRaw("Mouse Y");
        if (xRot > 0 || xRot < 0)
        {
            Vector3 camRot = new Vector3(xRot, 0f, 0f);
            cam.transform.Rotate(-camRot);
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

    }
}
