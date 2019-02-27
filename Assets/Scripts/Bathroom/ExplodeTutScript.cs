using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeTutScript : MonoBehaviour
{
    public float destroyDelay;
    public float minForce;
    public float maxForce;
    public float radius;


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
