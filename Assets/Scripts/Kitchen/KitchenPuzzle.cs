using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenPuzzle : MonoBehaviour
{
    public GameObject doorCollider;
    public GameObject pedestalCollider;
    public GameObject keyTing;

    public GameObject portal;
    public GameObject plate1;
    public GameObject plate2;
    public GameObject plate3;
    private int cur = 1;


    private AudioManager audioManager;

    public AudioClip audio1;
    private bool play1 = true;
    private AudioSource source1;
    public AudioClip audio2;
    private bool play2 = true;
    private AudioSource source2;

    // Start is called before the first frame update
    void Start()
    {
        keyTing.SetActive(false);
        portal.SetActive(false);
        audioManager = AudioManager.instance;
        source1 = gameObject.AddComponent<AudioSource>();
        source2 = gameObject.AddComponent<AudioSource>();
        source1.clip = audio1;
        source2.clip = audio2;
    }


    public void PlateBroke(int num)
    {
        if (num == cur)
        {
            cur++;
            PlayAudio(num);
        }
        else
        {
            cur = 1;
            //portal.SetActive(true);
            //doorCollider.SetActive(true);
            plate1.SetActive(true);
            plate2.SetActive(true);
            plate3.SetActive(true);
            StartCoroutine(Door());
            //yield return new WaitForSeconds(5);
            //doorCollider.SetActive(false);
            //portal.SetActive(false);
        }
        if (cur == 4)
        {
            //portal.SetActive(true);
            //pedestalCollider.SetActive(true);
            StartCoroutine(Pedestal());
            //yield return new WaitForSeconds(5);
            //portal.SetActive(false);
            //pedestalCollider.SetActive(false);
        }

    }

    private void PlayAudio(int num)
    {
        if (num == 1)
        {
           audioManager.Play(source1);
        }
        if (num == 2)
        {
            audioManager.Play(source2);
        }
    }


    IEnumerator Door()
    {
        //print(Time.time);
        //Debug.Log("fuck this shit");
        portal.SetActive(true);
        doorCollider.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        plate1.SetActive(true);
        plate2.SetActive(true);
        plate3.SetActive(true);
        //Debug.Log("fuck that shit");
        doorCollider.SetActive(false);
        portal.SetActive(false);
        //print(Time.time);
    }
    IEnumerator Pedestal()
    {
        //print(Time.time);
        //Debug.Log("fuck this shit");
        portal.SetActive(true);
        pedestalCollider.SetActive(true);
        keyTing.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("fuck that shit");
        pedestalCollider.SetActive(false);
        portal.SetActive(false);
        //print(Time.time);
    }

}
