using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PortalTeleporter : MonoBehaviour
{
    public GameObject player;
    public Transform receiver;
    public bool gravity = true;

    public int side = 9;
    private DirectionMarking dm;
    private FootstepMarking fs;

    private Transform playerTransform;
    private PlayerController playerController;
    private bool playerIsOverlapping = false;

    private void Start()
    {
        // Get player here
        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
        playerController = player.GetComponent<PlayerController>();

        try {
            dm = GameObject.Find("DirectionMarker").GetComponent<DirectionMarking>();
        } catch (NullReferenceException e) { }
        try
        {
            fs = GameObject.Find("DirectionMarker").GetComponent<FootstepMarking>();
        }
        catch (NullReferenceException e) { }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = playerTransform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                //player.gameObject.GetComponent<Rigidbody>().useGravity = gravity;

                playerTransform.position = receiver.position;
                Debug.Log(playerTransform.rotation);

                playerController.euler = receiver.rotation.eulerAngles;

                playerTransform.rotation = receiver.rotation;
                Debug.Log(playerTransform.rotation);

                try
                {
                    dm.UpdateMarker(side);
                } catch (NullReferenceException e){
                }

                try
                {
                    fs.UpdateMarker(side);
                } catch (NullReferenceException e){
                }

                playerIsOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            playerIsOverlapping = false;
        }
    }
}