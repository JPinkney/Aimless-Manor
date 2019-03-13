using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class Blurred : MonoBehaviour
{

    private float timer;
    private float seconds;

    public float magnitude = 0.001f;

    private float redx = 0.01f;
    private float bluex = 0.01f;
    private float greenx = 0.01f;

    private float redy = 0.01f;
    private float bluey = 0.01f;
    private float greeny = 0.01f;

    public Material EffectMaterial;
    public Material Blurry;
    public Material ChromaticX;
    public Material Chromatic;

    private bool plus;
    private bool minus;
    private bool plus1;
    private bool minus1;
    private bool watery;
    private bool colour;

    private bool forwards;
    private bool backwards;

    void OnRenderImage(RenderTexture src, RenderTexture dst)

    {

        seconds += Time.deltaTime;

        //float minutes = Mathf.Floor(timer / 60);
        //float seconds = timer % 60;
        //Debug.Log(timer);

        //seconds = timer / 1000;
        if (seconds >=0 && seconds <= 29*2)
        {
            Graphics.Blit(src, dst, EffectMaterial);
        }
        if (seconds >= 29*2)
        {
            print("hello");
            BlurrCamera(src, dst);
        }
        
        if (seconds >= 38*2)
        {
            ChromaticAbX(src, dst, seconds);
        }
        
        if (seconds >= 50*3)
        {
            ChromaticAbY(src, dst, seconds);
        }

        if (seconds >= 65*3)
        {
            print("second go");
            //ChromXReset();
            //ChromYReset();
            //BlurReset();
           
            //ChromaticAbX(src, dst, seconds);
        }
        else
        {
           // Graphics.Blit(src, dst, EffectMaterial);
        }

        //Debug.Log(seconds);
        //if (seconds >= 23)
        //{
         //   ChromaticAbX(src, dst, seconds);
        //}
    }

    void BlurrCamera(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, Blurry,-1);

        print("Blurred");

        if (magnitude > 0.0f && magnitude < 0.09f && minus != true)
        {
            magnitude += 0.001f;
            Blurry.SetFloat("_Magnitude", magnitude);
            plus = true;
        }

        if (magnitude >= 0.09f)
        {
            plus = false;
            minus = true;
        }

        if (plus == false)
        {
            magnitude -= 0.001f;
            Blurry.SetFloat("_Magnitude", magnitude);
        }
        
        if (magnitude <= 0.0f)
        {

            plus = true;
            magnitude = 0.0f;
            Blurry.SetFloat("_Magnitude", magnitude);
        }
    }

    void ChromaticAbX(RenderTexture src, RenderTexture dst, float seconds)
    {
        Graphics.Blit(src, dst, ChromaticX,-1);
        print("chrom");

        if (redx > 0.0f && redx < 0.19f && minus1 != true)
        {
            redx += 0.003f;
            ChromaticX.SetFloat("_RedX", redx);
            print("XXXX");

            bluex += 0.002f;
            ChromaticX.SetFloat("_BlueX", bluex);

            greenx += 0.001f;
            ChromaticX.SetFloat("_GreenX", greenx);
        }

        if (redx >= 0.19f)
        {
            //plus1 = false;
            minus1 = true;

        }
        if ( minus1 == true && seconds > 44*3)
        {
            redx -= 0.003f;
            ChromaticX.SetFloat("_RedX", redx);

            bluex -= 0.002f;
            ChromaticX.SetFloat("_BlueX", bluex);

            greenx -= 0.001f;
            ChromaticX.SetFloat("_GreenX", greenx);
            
            if (redx < 0f)
            {
                minus1 = false;

                redx = 0.0f;
                ChromaticX.SetFloat("_RedX", redx);

                bluex = 0.0f;
                ChromaticX.SetFloat("_BlueX", bluex);

                greenx = 0.0f;
                ChromaticX.SetFloat("_GreenX", greenx);

                Graphics.Blit(src, dst, EffectMaterial,-1);
                
            }
        }
    }

    void ChromaticAbY(RenderTexture src, RenderTexture dst, float seconds)
    {
        Graphics.Blit(src, dst, Chromatic,-1);

        if (greeny > 0.0f && greeny < 0.11f && backwards != true)
        {
            redy += 0.001f;
            Chromatic.SetFloat("_RedY", redy);
            print("PLUS");

            bluey += 0.001f;
            Chromatic.SetFloat("_BlueY", bluey);

            greeny += 0.003f;
            Chromatic.SetFloat("_GreenY", greeny);
        }

        if (greeny >= 0.10f)
        {
            //forwards = false;
            backwards = true;
        }

        if (backwards == true && seconds > 54*3)
        {
            redy -= 0.001f;
            Chromatic.SetFloat("_RedY", redy);

            bluey -= 0.001f;
            Chromatic.SetFloat("_BlueY", bluey);

            greeny -= 0.002f;
            Chromatic.SetFloat("_GreenY", greeny);
            
            if (greeny <= 0f)
            {
                backwards = false;
                redy = 0.0f;
                Chromatic.SetFloat("_RedY", redy);

                bluey = 0.0f;
                Chromatic.SetFloat("_BlueY", bluey);

                greeny = 0.0f;
                Chromatic.SetFloat("_GreenY", greeny);
                Graphics.Blit(src, dst, EffectMaterial,-1);
            }
        }
    }


    void ChromXReset()
    {
        print("reset");
        redx = 0.01f;
        ChromaticX.SetFloat("_RedX", redx);

        bluex = 0.01f;
        ChromaticX.SetFloat("_BlueX", bluex);

        greenx = 0.01f;
        ChromaticX.SetFloat("_GreenX", greenx);
    }
    void ChromYReset()
    {
        redy = 0.01f;
        Chromatic.SetFloat("_RedY", redy);

        bluey = 0.01f;
        Chromatic.SetFloat("_BlueY", bluey);

        greeny = 0.01f;
        Chromatic.SetFloat("_GreenY", greeny);
    }

    void BlurReset()
    {
        magnitude = 0.01f;
        Blurry.SetFloat("_Magnitude", magnitude);
    }

}
