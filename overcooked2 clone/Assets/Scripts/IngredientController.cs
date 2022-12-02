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
    public bool isPlate = false;
    public GameObject master; //Who is holding the object?
    public BoxCollider2D bc; // this box collider
    public SpriteRenderer spriteRenderer; //this srpite renderer
    //public Rigidbody2D rb; // this rigid body

    void Start()
    {
        
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
        if (choppiness >= 600)
        {
            Debug.Log("Chop completed!");
            spriteRenderer.color = Color.blue;
            done = true;
        }
    }

    public void followMaster()
    {
        transform.position = master.transform.position + new Vector3(1, 1, 0);
    }
}
