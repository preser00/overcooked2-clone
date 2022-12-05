using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    //  ATTRIBUTES  //
    public string ingredientName; //Name of the ingredient
    public bool held; //Is someone holding the ingredient?
    public int choppiness = 0; // How chopped is this ingredient right now?
    public bool done = false; //Is it finished chopping?

    public bool firstChop = true;
    public bool isPlate = false;

    public GameObject master; //Who is holding the object?
    public BoxCollider2D bc; // this box collider
    public SpriteRenderer spriteRenderer; //this srpite renderer
    public GameObject ProgressBar = null;
    public GameObject ProgressBarPreFab;
    //public Rigidbody2D rb; // this rigid body

    public Sprite[] sprites;
    public Sprite[] choppedSprites;

    private AudioSource audioSrc; 

    void Start()
    {
        audioSrc = GetComponent<AudioSource>(); 
    }

    
    void Update()
    {
        if (held)
        {
            bc.enabled = false; // turn off collider after picked up
            //rb.simulated = false; // turn off rigidbody after picked up
            followMaster();
        }
        else
        {
            bc.enabled = true;
            //rb.simulated = true;
        }
        
        if (choppiness > 0 && firstChop)
        {
            ProgressBar = Instantiate(ProgressBarPreFab, transform);
            ProgressBar.transform.position = gameObject.transform.position + new Vector3(-1, -1, 0);
            firstChop = false;

            audioSrc.Play();
            audioSrc.loop = true; 

        }
        if (choppiness >= 600)
        {
            Debug.Log("Chop completed!");

            if (ingredientName == "fish") { spriteRenderer.sprite = choppedSprites[0]; }
            else if (ingredientName == "shrimp") { spriteRenderer.sprite = choppedSprites[1]; }

            done = true;

            audioSrc.Stop();
            audioSrc.loop = false;
        }
        if (done)
        {
            ProgressBar.transform.position = new Vector3(-30, 30, 0);
            firstChop = true;
            choppiness = 0;
        }
    }

    public void followMaster()
    {
        transform.position = master.transform.position + new Vector3(1, 1, 0);
    }
}
