using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    public GameObject man;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("NoTele"))
        {
            man.GetComponent<TheEnabler>().ToggleCol();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("NoTele"))
        {
            man.GetComponent<TheEnabler>().ToggleCol();
        }
    }
}
