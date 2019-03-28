using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionMarking : MonoBehaviour
{
    public Sprite top, bot, lf, lb, rf, rb;
    public GameObject room;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = bot;
    }

    public void Update()
    {
        if (this.PlayerInBound())
        {
            gameObject.GetComponent<Image>().enabled = true;
        } else {
            gameObject.GetComponent<Image>().enabled = false;
        }
    }

    private bool PlayerInBound()
    {
        Vector3 position = GameObject.Find("Player").transform.position;
        return room.GetComponent<Collider>().bounds.Contains(position);
    }

    public void UpdateMarker(int direction)
    {
        switch (direction)
        {
            case 1:
                gameObject.GetComponent<Image>().sprite = bot;
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = lf;
                break;
            case 3:
                gameObject.GetComponent<Image>().sprite = lb;
                break;
            case 4:
                gameObject.GetComponent<Image>().sprite = rb;
                break;
            case 5:
                gameObject.GetComponent<Image>().sprite = rf;
                break;
            case 6:
                gameObject.GetComponent<Image>().sprite = top;
                break;
            default:
                gameObject.GetComponent<Image>().sprite = bot;
                break;
        }
    }
}
