using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Glimmer_Gone : Interactable
{
    // key.activeInHierarchy == true

    public GameObject keyObj;
    public ParticleSystem glimmer;
    public ParticleSystem confettiBurst;
    public ParticleSystem confettiRain;
    private int count;
    private bool done;
    public AudioClip party;
    private AudioSource partySource;

    private AudioManager audioManager;

    public void Start()
    {
        //audioManager = AudioManager.instance;
    }

    public override void Interact(Inventory inv, GameObject obj)
    {
        //this.gameObject.SetActive(false);

        // Instantiate(brokenGlass, transform.position, transform.rotation);
        // brokenGlass.localScale = transform.localScale;
        // highlightLight.SetActive(false);

        if (party && done == false)
        {
             AudioSource.PlayClipAtPoint(party, this.transform.position);
        }

        if (keyObj)
        {
            //keyObj.transform.position = newKeyLocation.position;
            //keyObj.SetActive(true);
        }


        if (glimmer)
        {
            glimmer.Stop();
        }

        if (confettiBurst && done == false)
        {
            /*for (int count = 0; count < 3; count++)
            {
                
            }*/

            if (count == 3)
            {

                done = true;
            }

            confettiBurst.Play();

        }

        if (confettiRain && done == false)
        {
            /*for (int count = 0; count < 3; count++)
            {
                
            }*/
            count++;

            if (count == 3){

                done = true;
            }

            confettiRain.Play();

        }

        //Physics.IgnoreLayerCollision(9, 9);
        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());

    }
}
