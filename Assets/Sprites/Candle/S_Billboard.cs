using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Billboard : MonoBehaviour
{
    private Camera m_Camera;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_Camera.transform.position, Vector3.up);
    }
}
