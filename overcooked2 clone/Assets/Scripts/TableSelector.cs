using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSelector : MonoBehaviour
{
    //variable declaration
    public GameObject TableSelected; //the table that is selected
    public GameObject Player;
    public PlayerController playerController;
    public CircleCollider2D circleCollider;
    public TableReverter currentReverter;
    public GameObject progressBarTester;

    public float length = 3;
    

    public float distance;
    public Vector2 direction;
    public bool isCounter;
    public float offset;
  
    public bool isSelected; //only one table can be selected for one player at one time
    public LayerMask layerCounter;

    // private Rigidbody2D _RB;
    // private PlayerController _ThisPlayerControl;
    //private TableReverter _SelectedReverter; // the script of selected table

    void Start()
    {
        //_RB = GetComponent<Rigidbody2D>(); //grab player rigidbody
        //_ThisPlayerControl = GetComponent<PlayerController>(); //grab playercontroller script
        Player = GameObject.FindGameObjectWithTag("Player");

        
    }

  
    void Update()
    {

        RayCast();
    }
    
    private void RayCast()
    {
        Vector2 lastDirection = direction;
     
        direction = Player.GetComponent<PlayerController>().toRotation * Vector3.up;
       
        RaycastHit2D hit = Physics2D.Raycast(circleCollider.bounds.center, direction, circleCollider.bounds.extents.y+offset, layerCounter);
        Debug.DrawRay(circleCollider.bounds.center , direction * ( circleCollider.bounds.extents.y+offset), Color.red);


        if (hit)
        {
            if (isSelected && TableSelected.transform.gameObject != hit.transform.gameObject) //Something different is selected
            {
                currentReverter = TableSelected.GetComponent<TableReverter>();
                currentReverter.isSelectedPlayer = false;
                TableSelected = null;
                isSelected = false;
            }
            if (!isSelected && TableSelected == null) //Nothing is selected
            {
                TableSelected = hit.transform.gameObject;
                currentReverter = TableSelected.GetComponent<TableReverter>();
                currentReverter.isSelectedPlayer = true;
                isSelected = true;
            }
            if (currentReverter.isOccupied)
            {
                IngredientController _currentIngredient = currentReverter.content.GetComponent<IngredientController>(); // get the ingredient controller of the current reverter
                PlateController _currentPlate = currentReverter.content.GetComponent<PlateController>();
                if (playerController.currentHolding == null)
                {
                    if (playerController.isAlt)
                    {
                        if (_currentIngredient == null)
                        {
                            playerController.currentHolding = currentReverter.content; //Set current holding to the table content
                            currentReverter.content = null; //reset the table content to null
                            currentReverter.isOccupied = false; //reset the table isOccupied to false

                            playerController.currentHolding.GetComponent<PlateController>().held = true; //Tell that collided object it is being held
                            playerController.currentHolding.GetComponent<PlateController>().master = gameObject;
                        }
                        else if (_currentIngredient.choppiness == 0 || _currentIngredient.done)
                        {
                            playerController.currentHolding = currentReverter.content; //Set current holding to the table content
                            currentReverter.content = null; //reset the table content to null
                            currentReverter.isOccupied = false; //reset the table isOccupied to false

                            playerController.currentHolding.GetComponent<IngredientController>().held = true; //Tell that collided object it is being held
                            playerController.currentHolding.GetComponent<IngredientController>().master = gameObject; //Tell the collided object who is holding it
                        }


                    }

                    if (playerController.isCtrl && currentReverter.isCuttingBoard && !_currentIngredient.done)
                    {
                        Debug.Log(_currentIngredient.choppiness);
                        _currentIngredient.choppiness++;
                        _currentIngredient.ProgressBar.transform.localScale = new Vector3(_currentIngredient.choppiness / 100, .5f, 1);
                    }

                }else if (playerController.currentHolding.GetComponent<PlateController>() != null) // if player is holding a plate
                {
                    if (playerController.isAlt && playerController.currentHolding.GetComponent<PlateController>().content == null && _currentIngredient.done) //if the plate is empty and the ingredients are chopped
                    {
                        playerController.currentHolding.GetComponent<PlateController>().content = currentReverter.content; //pick up ingredient into the plate
                        _currentIngredient.dished = true; //ingredient is plated (do this before removing it from the table
                        _currentIngredient.master = playerController.currentHolding; //ingredient's master is the plate

                        currentReverter.content = null; //reset the table content to null
                        currentReverter.isOccupied = false; //reset the table isOccupied to false


                    }
                }
            }
        }

        else
        { //No counters selected
            if (currentReverter != null) currentReverter.isSelectedPlayer = false;
            TableSelected = null;
            isSelected = false;
        }

        //Debug.Log("isOccupied = " + currentReverter.isOccupied);
        //Debug.Log("currentHolding = " + playerController.currentHolding);
    }

}
