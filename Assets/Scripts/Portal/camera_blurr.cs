using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_blurr : MonoBehaviour
{
    public Material EffectMaterial;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnRenderImage(RenderTexture src, RenderTexture dst)

    {
        Graphics.Blit(src, dst, EffectMaterial);
    }

}
        
