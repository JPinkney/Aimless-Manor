using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHUD : MonoBehaviour
{
    public int collectedKeys;
    public int totalKeys;

    public Image[] keys;
    public Sprite collected;
    public Sprite empty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(collectedKeys > totalKeys)
        //{
        //    collectedKeys = totalKeys;
        //}

        //for (int i = 0; i < keys.Length; i++)
        //{
        //    if (i < collectedKeys)
        //    {
        //        keys[i].sprite = collected;
        //    } else
        //    {
        //        keys[i].sprite = empty;
        //    }

        //    if (i < totalKeys)
        //    {
        //        keys[i].enabled = true;
        //    } else
        //    {
        //        keys[i].enabled = false;
        //    }
        //}
    }

    public void CollectKey()
    {
        this.collectedKeys++;
    }
    
}
