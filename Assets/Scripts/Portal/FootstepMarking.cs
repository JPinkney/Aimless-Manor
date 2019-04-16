using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepMarking : MonoBehaviour
{

    public int curSide;
    public GameObject ceiling;
    public GameObject wall;
    public GameObject floor;
    public GameObject light;


    // Start is called before the first frame update
    void Start()
    {
        curSide = 1;
        floor.SetActive(true);
        wall.SetActive(false);
        ceiling.SetActive(false);
    }

    public void UpdateMarker(int direction)
    {
        curSide = direction;
        if (light.activeSelf)
        {
            switch (direction)
            {
                case 1:
                    floor.SetActive(true);
                    wall.SetActive(false);
                    ceiling.SetActive(false);
                    break;
                case 2:
                    floor.SetActive(false);
                    wall.SetActive(true);
                    ceiling.SetActive(false);
                    break;
                case 6:
                    floor.SetActive(false);
                    wall.SetActive(false);
                    ceiling.SetActive(true);
                    break;
            }
        }
        else
        {
            floor.SetActive(false);
            wall.SetActive(false);
            ceiling.SetActive(false);
        }
        
    }
}
