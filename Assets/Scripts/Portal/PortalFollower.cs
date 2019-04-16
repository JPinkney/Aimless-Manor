using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFollower : MonoBehaviour
{
    public Transform player;
    public Transform portal;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.position;
        Vector3 camPos = transform.position;
        Vector3 newPos = new Vector3(playerPos.x, camPos.y, playerPos.z);
        portal.position = newPos;


    }
}
