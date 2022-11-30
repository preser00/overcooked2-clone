using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public string tableType; //all lowercase strings:
                             //ingredient spawner, plate spawner, cutting board, trash can, delivery zone 
    
    public string ingredientSpawnerType; //for ingredient spawning counters; choices are fish, shrimp
    
    public GameObject ingredient; //ingredient template goes here
    public GameObject plate; //plate template goes here
    
    private TableSelector _tableSelector; //player's table selector
                                        //note this operates under assumption there's 1 player only 

    private void Start()
    {
        _tableSelector = GameObject.FindGameObjectWithTag("Player").GetComponent<TableSelector>(); //get table selector
    }

    private void Update()
    {
        //Debug.Log(_tableSelector.TableSelected);

        OnSelection(); 
    }

    private void OnSelection()
    {
        if (_tableSelector.TableSelected == gameObject) //if this table is selected do stuff based on type of table
        {
            PlayerController currentPlayerController = _tableSelector.gameObject.GetComponent<PlayerController>(); //get player's controller

            if (tableType == "ingredient spawner")
            {
                if (Input.GetKeyUp(KeyCode.Space)) //getkeyup so it doesn't spawn multiple ingredients
                {
                    GameObject newIngredient = Instantiate(ingredient, transform.position + new Vector3(1, 1, 0), Quaternion.identity);
                    IngredientController newIngredientController = newIngredient.GetComponent<IngredientController>(); //get ingredient's IngredientController script
                    
                    //WIP - if player is currently already holding ingredient, make sure to put the ingredient somewhere it wont clip into a table. can probably be done in playercontroller

                    currentPlayerController.currentHolding = newIngredient; //set player's currently holding obj to this new ingredient

                    newIngredientController.ingredientName = ingredientSpawnerType; //set name to match spawner type's ingredient (fish, shrimp)
                    newIngredientController.held = true; //set the ingredient to being held 
                    newIngredientController.master = _tableSelector.gameObject; //set ingredient's master to the player that selected the table
                }
            }
            else if (tableType == "plate spawner")
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    GameObject newPlate = Instantiate(plate, transform.position + new Vector3(1, 1, 0), Quaternion.identity); 

                    //WIP - need player pickup access
                }
            }
            else if (tableType == "cutting board")
            {

            }
            else if (tableType == "trash can")
            {
                /*if(Input.GetKeyUp(KeyCode.Space) && currentPlayerController.currentHolding.layer == 7) //if player is holding an ingredient (layer = 7) & presses space
                {
                    Destroy(currentPlayerController.currentHolding); //destroy ingredient 

                }*/

                TableReverter thisTableReverter = gameObject.GetComponent<TableReverter>();

                if (thisTableReverter.isOccupied) //if object gets put "on this table", delete it 
                {
                    Destroy(thisTableReverter.content);
                    thisTableReverter.isOccupied = false;

                }
            }
            else if(tableType == "delivery zone")
            {
                //add to score, remove plate, etc
            }
        }
    }
}
