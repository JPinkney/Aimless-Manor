using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isGrounded;

    private Rigidbody rb;

    public float raycastDistance = 1;

    public float jumpSpeed = 10f;

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

        var ray = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(ray, raycastDistance);

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            var distanceToGround = hit.distance;
        }

        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rot = new Vector3(0f, yRot, 0f);

        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 camRot = new Vector3(xRot, 0f, 0f);

        transform.Rotate(rot);

        cam.transform.Rotate(-camRot);

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
    
    private void Jump()
    {
        Vector3 vel = rb.velocity;
        vel.y = jumpSpeed;
        rb.velocity = vel;

    }
}
