using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public bool ignoreCollisionsWithPlayer = true;

    private bool used = false;
    private bool key_got = false;

    private Component[] origRenderers;
    //public List<List<Material>> origMats = new List<List<Material>>();
    public List<Material[]> origMats = new List<Material[]>(); //a list of arrays of materials

    GameObject player;
    //GameObject key;

    public GameObject leftDoor;
    public GameObject rightDoor;

    public ParticleSystem confettiBurst;
    public ParticleSystem confettiRain;
    public ParticleSystem glimmer;
    public ParticleSystem glimmer2;

    public AudioClip party;

    private bool hasPlayed = false;

    public AudioClip someAudio;
    private AudioSource someSource;
    private AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        if(ignoreCollisionsWithPlayer)
        {
            SetupCollisionsIgnore();
        }

        origRenderers = this.GetComponentsInChildren(typeof(Renderer));
        foreach(Renderer rend in origRenderers)
        {
            origMats.Add(rend.materials);
        }

        audioManager = AudioManager.instance;
        someSource = gameObject.AddComponent<AudioSource>();
        someSource.clip = someAudio;

        //for(int r=0; r<origRenderers.Length; r++)
        //{

        //}
    }

    public void obtainKey()
    {
        if (confettiBurst)
        {
            confettiBurst.Play();
        }

        if (confettiRain)
        {
            confettiRain.Play();
        }

        if (party)
        {
            AudioSource.PlayClipAtPoint(party, this.transform.position);
        }

        if (glimmer)
        {
            glimmer.Stop();
        }

        if (glimmer2)
        {
            glimmer2.Stop();
        }

        if (someAudio && hasPlayed == false)
        {
            audioManager.Play(someSource);
            hasPlayed = true;
        }

        if (leftDoor)
        {
            var leftDoorRotationScript = leftDoor.GetComponent<OpenTutorialDoors>();
            if (leftDoorRotationScript)
            {
                leftDoorRotationScript.open();
            }
        }

        if (rightDoor)
        {
            var rightDoorRotationScript = rightDoor.GetComponent<OpenTutorialDoors>();
            if (rightDoorRotationScript)
            {
                rightDoorRotationScript.open();
            }
        }

        Debug.Log("OBTAINING KEY");
        this.key_got = true;
    }

    public bool KeyObtained()
    {
        return this.key_got;
    }

    public void SetupCollisionsIgnore()
    {
        player = GameObject.Find("Player");
        Component[] playerCols = player.gameObject.GetComponentsInChildren(typeof(Collider));
        Component[] thisObjCols = this.gameObject.GetComponentsInChildren(typeof(Collider));

        //Physics.IgnoreCollision(playerCols, thisObjCols);
        foreach (Collider colP in playerCols)
        {
            foreach (Collider colO in thisObjCols)
            {
                Physics.IgnoreCollision(colP, colO);
            }
        }
    }


}
