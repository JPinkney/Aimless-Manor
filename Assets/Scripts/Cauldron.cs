using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script attaches to the cauldron and checks to see if all types of
 * objects are in the cauldron
 */
public class Cauldron : Interactable
{

    private bool tag1HasBeenAdded = false;
    private bool tag2HasBeenAdded = false;
    private bool tag3HasBeenAdded = false;

    public GameObject target;

    public GameObject hiddenIngredient1;
    public GameObject hiddenIngredient2;
    public GameObject hiddenIngredient3;
    public GameObject hiddenWaterPitcher;

    private bool shouldMoveToTarget = false;
    private GameObject currentObj = null;

    protected float Animation;

    public override void Interact(Inventory inv, GameObject obj)
    {
        inv.RemoveGameObjectFromInventory(obj);
        if (this.PossibleItem(obj))
        {
            string currentItemsTags = obj.tag;
            switch (currentItemsTags)
            {
                case "ingredients_1":
                    {
                        tag1HasBeenAdded = true;
                        hiddenIngredient1.SetActive(true);
                        break;
                    }
                case "ingredients_2":
                    {
                        tag2HasBeenAdded = true;
                        hiddenIngredient2.SetActive(true);
                        break;
                    }
                case "ingredients_3":
                    {
                        tag3HasBeenAdded = true;
                        hiddenIngredient3.SetActive(true);
                        hiddenWaterPitcher.SetActive(true);
                        break;
                    }
            }

            Destroy(obj);
        }
        else
        {

            currentObj = obj;

            var targetPos = this.target.gameObject.transform.position;
            var cauldronPos = this.gameObject.transform.position;

            // First we need to put the item in the cauldron
            //this.currentObj.GetComponent<Rigidbody>().detectCollisions = false;
            //this.currentObj.GetComponent<Collider>().enabled = false;

            //this.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            //this.gameObject.GetComponent<Collider>().enabled = false;

            this.currentObj.GetComponent<Rigidbody>().useGravity = true;

            var t = new Vector3(cauldronPos.x, cauldronPos.y + .4f, cauldronPos.z);
            this.currentObj.transform.position = t;
            //shouldMoveToTarget = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Pear" || other.name == "chair1")
        {
            Debug.Log("So it was hit?");
            shouldMoveToTarget = true;
        }
    }

    private void Update()
    {
        if (shouldMoveToTarget && currentObj != null)
        {
            this.currentObj.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.currentObj.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            Animation += Time.deltaTime;

            Animation = Animation % 2f;

            this.currentObj.gameObject.transform.position = MathParabola.Parabola(this.currentObj.gameObject.transform.position, target.gameObject.transform.position, .3f, Animation / 8f);
            //this.currentObj.gameObject.transform.position = Vector3.MoveTowards(this.currentObj.gameObject.transform.position, this.target.gameObject.transform.position, 5f * Time.deltaTime);
            //this.currentObj.GetComponent<Rigidbody>().detectCollisions = true;
            if (Vector3.Distance(this.currentObj.gameObject.transform.position, this.target.gameObject.transform.position) < 1)
            {
                shouldMoveToTarget = false;
                currentObj = null;
                Animation = 0;
            }
        }

        if (this.HasPuzzleFinished())
        {
            Debug.Log("Testing");
        }
    }

    public override bool PossibleItem(GameObject obj)
    {
        return obj.tag.StartsWith("ingredient", System.StringComparison.Ordinal);
    }

    private bool HasPuzzleFinished()
    {
        return this.tag1HasBeenAdded && this.tag2HasBeenAdded && this.tag3HasBeenAdded;
    }

}
