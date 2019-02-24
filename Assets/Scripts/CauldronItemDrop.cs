using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronItemDrop : MonoBehaviour
{

    public GameObject waterEffect;
    private bool showWaterEffect;

    private void OnTriggerEnter(Collider other)
    {
        this.showWaterEffect = true;
    }

    IEnumerator ExecuteItemDropEffect(float time)
    {
        Debug.Log(this.waterEffect);
        this.waterEffect.SetActive(true);
        yield return new WaitForSeconds(time);
        this.showWaterEffect = false;
        this.waterEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.showWaterEffect)
        {
            StartCoroutine(this.ExecuteItemDropEffect(1));
        }
    }
}
