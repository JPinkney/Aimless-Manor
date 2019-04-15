using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeTutScript : MonoBehaviour
{
    public float destroyDelay = 3.0f;
    public float minForce = 50.0f;
    public float maxForce = 500.0f;
    public float radius = 0.5f;


    void Start()
    {
        Explode();
    }

    private void OnMouseDown()
    {
        //Explode();
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
