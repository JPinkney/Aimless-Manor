using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLook : MonoBehaviour
{
    Light testLight;
    public float minWaitTime;
    public float maxWaitTime;
    public GameObject pe;

    // Use this for initialization
    void Start()
    {
        testLight = GetComponent<Light>();
        StartCoroutine(Flashing());
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Look")
            {
                pe = GameObject.Find("Look");
                pe.GetComponent<Light>().enabled = pe.GetComponent<Light>().enabled;
                //testLight.enabled = testLight.enabled; ;

            }

        }
        else
        {
            pe = GameObject.Find("Look");
            pe.GetComponent<Light>().enabled = !pe.GetComponent<Light>().enabled;
            //testLight.enabled = !testLight.enabled; ;
        }

    }

    IEnumerator Flashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            testLight.enabled = !testLight.enabled;

        }
    }
}


