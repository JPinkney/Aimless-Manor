using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadRoom : MonoBehaviour
{

    public GameObject room;


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            room.SetActive(false); 
        }
    }
}
