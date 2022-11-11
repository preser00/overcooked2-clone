using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Vector2 movement;
    public float movementSpeed;
    public Rigidbody2D rigidbody;
    void Start()
    {
        
    }

    #region Movement Code
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * movementSpeed);
    }
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            //Change State to that action
=======
    #region Initial Variables
    public Vector2 movement; //Track Movement of Player
    public float movementSpeed; //Determine movement speed
    public Rigidbody2D rigidbodyPlayer; //RigidBody to move player
    public GameObject currentHolding; //What object is the player currently holding if any
    private enum Layers //Enum of all layer masks in scene
    {
        Default,
        TransparentFX,
        Ignore,
        Empty,
        Water,
        UI,
        Counter,
        Ingredient
    }
    public bool isSpace; //Track whether space has been pressed
    public int framesReload = 30; //Wait for frames to reload before checking drop
    #endregion


    #region Input and Updates
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //Get horizontal input
        movement.y = Input.GetAxisRaw("Vertical"); //Get vertical input
        isSpace = Input.GetKey(KeyCode.Space); //Get space bar input
        
            

    }

    private void FixedUpdate()
    {
        #region Put Down Object Method
        if (currentHolding != null)
        {
            framesReload = (framesReload > 0) ? --framesReload :  0; //Decrement frames
            if (isSpace && framesReload == 0) //If frames have reloaded and space is pressed
            {
                currentHolding.gameObject.GetComponent<IngredientController>().held = false; //Object is no longer held
                currentHolding.gameObject.transform.position = rigidbodyPlayer.position + new Vector2(2, 0); //Move object slightly away from trigger (prevents the player from colldiing with it again)
                currentHolding = null; //Reset current holding to none
                framesReload = 30; //Reload the frames
            }
        }
        #endregion
        rigidbodyPlayer.MovePosition(rigidbodyPlayer.position + movement * movementSpeed); //Move player via rigidbody
    }
    #endregion

    #region Collision Pick Up Method
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Interactable") //Check if object is interactable
        {   
            if (collision.gameObject.layer == (int)Layers.Ingredient && currentHolding == null) //Check if object has the layer Ingredient
            {  
                if (isSpace)
                {
                    currentHolding = collision.gameObject; //Set current holding to collided object
                    collision.gameObject.GetComponent<IngredientController>().held = true; //Tell that collided object it is being held
                    collision.gameObject.GetComponent<IngredientController>().master = gameObject; //Tell the collided object who is holding it
                    
                }
                
            }
        }

        
    }

=======
    #endregion

}
