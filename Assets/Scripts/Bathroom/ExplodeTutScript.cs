using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeTutScript : MonoBehaviour
{
    [Range(0.1f, 60.0f)]
    public float destroyDelay = 5.0f;
    [Range(1.0f, 100.0f)]
    public float minForce = 20.0f;
    [Range(100.0f, 1000.0f)]
    public float maxForce = 200.0f;
    [Range(0.1f, 1.0f)]
    public float radius = 0.5f;

    //[Range(-0.5f, 0.5f)]
    //public float explodeOffsetY = 0.0f;
    //private Vector3 explodePos = new Vector3();

    void Start()
    {
        //explodePos.x = transform.position.x;
        //explodePos.y = transform.position.y + explodeOffsetY;
        //explodePos.z = transform.position.z;

        Explode();
    }

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
