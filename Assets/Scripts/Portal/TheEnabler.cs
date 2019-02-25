using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnabler : MonoBehaviour
{

    public List<BoxCollider> col;
    public int numCol;


    public void ToggleCol()
    {
        for (int i = 0; i < numCol; i+=1)
        {
            if (col[i] != null)
            {
                col[i].GetComponent<BoxCollider>().enabled = !col[i].GetComponent<BoxCollider>().enabled;
            }
        }

    }
}
