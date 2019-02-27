using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PP_Script : MonoBehaviour
{
    PostProcessVolume m_Volume;
    Vignette m_Vignette;

    public bool On = false;

    public bool vignette;


    void Start()
    {
        vignette = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_Vignette = ScriptableObject.CreateInstance<Vignette>();
            m_Vignette.enabled.Override(true);
            m_Vignette.intensity.Override(1f);

            m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);

            vignette = true;

        }

        if (vignette == true)
        {
            m_Vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (vignette == true)
            {
                OnDestroy();
                vignette = false;
                print("thanks");
            }
        }


    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
        On = true;

    }
}
