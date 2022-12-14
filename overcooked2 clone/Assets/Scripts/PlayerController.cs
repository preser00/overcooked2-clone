using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Initial Variables
    public Vector2 movement; //Track Movement of Player

    [SerializeField]private float movementSpeed; //Determine movement speed, serialized so can be changed directly in unity

    public TableSelector tableSelector;
    public Rigidbody2D rigidbodyPlayer; //RigidBody to move player
    public GameObject currentHolding; //What object is the player currently holding if any

    private AudioSource audioSrc; 

    public Quaternion finalRotation;
    public Quaternion toRotation;
    private enum Layers //Enum of all layer masks in scene
    {
        Default,
        TransparentFX,
        Ignore,
        Empty,
        Water,
        UI,
        Counter,
        Plate,
        Ingredient,

    }
    public bool isSpace; //Track whether space has been pressed
    public bool isAlt; //Track whether alt has been pressed
    public bool isCtrl; //Track whether alt has been pressed
    public int framesReload = 30; //Wait for frames to reload before checking drop
    #endregion
    void Start()
    {
        tableSelector = GetComponent<TableSelector>();
        audioSrc = GetComponent<AudioSource>(); 
    }
    #region Input and Updates
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //Get horizontal input
        movement.y = Input.GetAxisRaw("Vertical"); //Get vertical input
        
        isSpace = Input.GetKey(KeyCode.Space); //Get space bar input
        isAlt = Input.GetKey(KeyCode.LeftAlt); //Get alt key input
        isCtrl = Input.GetKey(KeyCode.LeftControl); //Get Ctrl key input
        
        
    }
    #endregion
    private void FixedUpdate()
    {
        #region Put Down Object Method
        if (currentHolding != null)
        {
            framesReload = (framesReload > 0) ? --framesReload :  0; //Decrement frames
            if (isSpace && framesReload == 0) //If frames have reloaded and space is pressed
            {
                if(currentHolding.GetComponent<IngredientController>() != null) //Decide if holding ingredient or plate
                {
                    currentHolding.GetComponent<IngredientController>().held = false; //ingredient is no longer held
                }
                if (currentHolding.GetComponent<PlateController>() != null) //Decide if holding plate
                {
                    currentHolding.GetComponent<PlateController>().held = false; //plate is no longer held
                }

                if (tableSelector.TableSelected != null)
                {
                    if (!tableSelector.currentReverter.isOccupied) //table put down method when table is empty
                    {
                        currentHolding.transform.position = tableSelector.TableSelected.transform.position; // place object on selected table's position
                        tableSelector.currentReverter.content = currentHolding;
                        tableSelector.currentReverter.isOccupied = true;
                    }
                    else if(tableSelector.currentReverter.content.GetComponent<PlateController>().content == null && currentHolding.GetComponent<IngredientController>().done)//table put down when plate is empty and ingredient chopped
                    {

                        currentHolding.transform.position = tableSelector.TableSelected.transform.position;
                        currentHolding.GetComponent<IngredientController>().dished = true; //the ingredients are now part of a dish 
                        currentHolding.GetComponent<IngredientController>().master = tableSelector.currentReverter.content;

                        PlateController currentPlateController = tableSelector.currentReverter.content.GetComponent<PlateController>(); 
                        currentPlateController.content = currentHolding;
                        currentPlateController.ingredientOnPlate = currentHolding.GetComponent<IngredientController>(); //plate can now track the ingredient held on it  

                    } 
                }
                else
                {
                    currentHolding.gameObject.transform.position = rigidbodyPlayer.position + new Vector2(2, 0); //Move object slightly away from trigger (prevents the player from colldiing with it again)
                }

                audioSrc.Play(); 
                currentHolding = null; //Reset current holding to none

                framesReload = 30; //Reload the frames
            }

        }
        #endregion
        
        #region turning Method
        rigidbodyPlayer.MovePosition(rigidbodyPlayer.position + movement * movementSpeed); //Move player via rigidbody
        
        if(movement != Vector2.zero)//if we are moving
        {
            finalRotation = Quaternion.LookRotation(Vector3.forward, movement);//facing direction is input direction
            toRotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime*10);//smooth out the change in direction
            rigidbodyPlayer.MoveRotation(toRotation);//rotate player via rigidbody   
        }
        #endregion

        
    }

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

                    audioSrc.Play(); 

                }
            }
            if (collision.gameObject.layer == (int)Layers.Plate && currentHolding == null) //Check if object has the layer Ingredient
            {
                if (isSpace)
                {
                    currentHolding = collision.gameObject; //Set plate holding to collided object
                    collision.gameObject.GetComponent<PlateController>().held = true; //Tell that collided object it is being held
                    collision.gameObject.GetComponent<PlateController>().master = gameObject; //Tell the collided object who is holding it

                }
            }
        }
    }
    #endregion
    

}
