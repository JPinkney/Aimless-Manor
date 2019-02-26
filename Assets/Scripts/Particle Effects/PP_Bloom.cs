using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PP_Bloom : MonoBehaviour
{
    PostProcessVolume m_Volume;
    Bloom m_Bloom;
    ColorGrading m_ColorGrading;

    public bool bloom;

    public float smooth = 2;

    private float newBloom;


    void Start()
    {
        bloom = false;

        m_Bloom = ScriptableObject.CreateInstance<Bloom>();
        m_Bloom.enabled.Override(true);
        m_Bloom.intensity.Override(0f);

        m_ColorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        m_ColorGrading.enabled.Override(true);
        m_ColorGrading.temperature.Override(0f);
        m_ColorGrading.tint.Override(0f);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Bloom, m_ColorGrading);

        //newBloom = m_Bloom.intensity.value;
    }

    void Update()
    {

        //m_Bloom.intensity.value = Mathf.Lerp(m_Bloom.intensity.value, endB, Time.deltaTime);
        ChangeBloom();
        ChangeColour();
        OnDestroy();

    }

    void ChangeBloom()
    {
        float startB = 1f;
        float endB = 50f;
        //bool done = false;



        if (Input.GetMouseButtonDown(0))
        {
            //m_Bloom = ScriptableObject.CreateInstance<Bloom>();
            //m_Bloom.enabled.Override(true);
            //m_Bloom.intensity.Override(0f);

            //m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Bloom);

            m_Bloom.intensity.value = startB;
            //done = true;
        }

            m_Bloom.intensity.value = Mathf.Lerp(m_Bloom.intensity.value, endB, Time.deltaTime);

            Debug.Log(m_Bloom.intensity.value);

            bloom = true;
        //}

        if (Input.GetMouseButtonDown(1))
        {
        //m_Bloom = ScriptableObject.CreateInstance<Bloom>();
        //m_Bloom.enabled.Override(true);
        //m_Bloom.intensity.Override(0f);

        //m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Bloom);

        //m_Bloom.intensity.value = startB;

        m_Bloom.intensity.value = Mathf.Lerp(m_Bloom.intensity.value, startB, Time.deltaTime * smooth);

        Debug.Log(m_Bloom.intensity.value);

         bloom = true;
        }
    }

    void ChangeColour()
    {
        float startC = 1f;
        float endC = 50f;

        Vector4 lift1 = new Vector4(0, 0, 0, 0);
        Vector4 lift2 = new Vector4(1, 1, 1, 1);

        if (Input.GetMouseButtonDown(0))
        {
            //m_Bloom = ScriptableObject.CreateInstance<Bloom>();
            //m_Bloom.enabled.Override(true);
            //m_Bloom.intensity.Override(0f);

            //m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Bloom);

            m_ColorGrading.tint.value = startC;
            m_ColorGrading.temperature.value = startC;

            m_ColorGrading.lift.value = lift1;
            m_ColorGrading.saturation.value = startC;
            //done = true;
        }


        m_ColorGrading.tint.value = Mathf.Lerp(m_ColorGrading.tint.value, endC, Time.deltaTime);
        m_ColorGrading.temperature.value = Mathf.Lerp(m_ColorGrading.temperature.value, endC, Time.deltaTime);

        m_ColorGrading.saturation.value = Mathf.Lerp(m_ColorGrading.saturation.value, endC, Time.deltaTime);

        m_ColorGrading.lift.value = Vector4.Lerp(m_ColorGrading.lift.value, lift2, Time.deltaTime);

        //m_ColorGrading.satVsSatCurve.value = Spline.Lerp(m_ColorGrading.saturation.value, endC, Time.deltaTime);

    }


    void OnDestroy()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    RuntimeUtilities.DestroyVolume(m_Volume, true, true);
        //    bloom = false;
        //}
        
    }

}
