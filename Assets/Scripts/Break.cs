using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public Transform brokenGlass;
    public ParticleSystem glassShatter;

    void OnMouseDown()
    {
        Destroy(gameObject);
        Instantiate(brokenGlass, transform.position, transform.rotation);
        brokenGlass.localScale = transform.localScale;

        glassShatter.Play();

    }

}
