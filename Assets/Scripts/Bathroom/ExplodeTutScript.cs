using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeTutScript : MonoBehaviour
{
    // some default values are included here, feel free to change them!!
    [Range(0.1f, 60.0f)]
    public float destroyDelay = 5.0f;
    [Range(0.0f, 500.0f)]
    public float minForce = 50.0f;
    [Range(50.0f, 1000.0f)]
    public float maxForce = 400.0f;
    [Range(0.1f, 10.0f)]
    public float radius = 0.4f;


    void Start()
    {
        Explode();
    }

    //private void OnMouseDown()
    //{
    //    //Explode();
    //}

    public void Explode()
    {

        foreach (Transform t in transform)
        {

            var rb = t.GetComponent<Rigidbody>();

            if (rb != null)
            {

                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);


            }

            Destroy(t.gameObject, destroyDelay);
        }
    }

}
