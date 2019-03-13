using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //required for unity events

public class ItemEvents : MonoBehaviour
{

    public UnityEvent hovered;
    public UnityEvent unHovered;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnHover()
    {
        hovered.Invoke();
    }

    void OnUnHover()
    {

    }

}
