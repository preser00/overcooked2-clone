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

    public TableReverter thisTableReverter;


    private TableSelector _tableSelector; //player's table selector
                                          //note this operates under assumption there's 1 player only 
    private GameManager _gm;
    private AudioSource _audioSrc;

    private void Start()
    {
        _tableSelector = GameObject.FindGameObjectWithTag("Player").GetComponent<TableSelector>(); //get table selector
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); 

        thisTableReverter = GetComponent<TableReverter>();
        _audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
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
                    SpriteRenderer newSpriteRenderer = newIngredient.GetComponent<SpriteRenderer>(); 

                    //WIP - if player is currently already holding ingredient, make sure to put the ingredient somewhere it wont clip into a table. can probably be done in playercontroller

                    currentPlayerController.currentHolding = newIngredient; //set player's currently holding obj to this new ingredient

                    newIngredientController.ingredientName = ingredientSpawnerType; //set name to match spawner type's ingredient (fish, shrimp)
                    newIngredientController.held = true; //set the ingredient to being held 
                    newIngredientController.master = _tableSelector.gameObject; //set ingredient's master to the player that selected the table

                    if (ingredientSpawnerType == "fish") //setting sprite for ingredient
                    {
                        newSpriteRenderer.sprite = newIngredientController.sprites[0]; 
                    }
                    else if(ingredientSpawnerType == "shrimp")
                    {
                        newSpriteRenderer.sprite = newIngredientController.sprites[1];
                    }
                    else { Debug.Log("Sprite not set");  }
                }
            }
            else if (tableType == "plate spawner")
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    GameObject newPlate = Instantiate(plate, transform.position + new Vector3(-2, 0, 0), Quaternion.identity);
                    PlateController newPlateController = newPlate.GetComponent<PlateController>();

                    currentPlayerController.currentHolding = newPlate; 
                    newPlateController.held = true;
                    newPlateController.master = _tableSelector.gameObject;
                }
            }
            else if (tableType == "cutting board")
            {
                thisTableReverter.isCuttingBoard = true;
            }
            else if (tableType == "trash can")
            {

                if (thisTableReverter.isOccupied) //if object gets put on the trash can, delete it 
                {
                    if(thisTableReverter.content.GetComponent<PlateController>() != null) //if it's a plate, destroy the ingredient on it as well 
                    {
                        PlateController thisPlateController = thisTableReverter.content.GetComponent<PlateController>();
                        Destroy(thisPlateController.content); 
                    }

                    Destroy(thisTableReverter.content);
                    thisTableReverter.isOccupied = false;

                }
            }
            else if(tableType == "delivery zone")
            {
                if (thisTableReverter.isOccupied) 
                {
                    PlateController currentPlate = thisTableReverter.content.GetComponent<PlateController>(); 

                    if(currentPlate != null) //if there is a plate on this table, check what's on the plate 
                    {
                        //_gm.CheckDish(currentPlate);
                        Debug.Log("checking plate");

                        RecipeController recipeManager = _gm.GetComponent<RecipeController>(); //get recipecontroller
                        bool once = false; 
                        
                        if (currentPlate.ingredientOnPlate != null) //if the dish has ingredients on it 
                        {
                            //StartCoroutine("CheckDishDelivery(recipeManager, currentPlate)");

                            
                                for (int i = 0; i < recipeManager.currTasks.Count; i++) //go through the current recipes 
                                {
                                    string ingredientRequired = recipeManager.currTasks[i].ingredientName; //look at what ingredient is required for each recipe

                                    if (currentPlate.ingredientOnPlate.ingredientName.Equals(ingredientRequired)) //if the ingredient required matches ingredient on plate, 
                                    {
                                        if(!once) //run all these only once 
                                        {
                                            _gm.score += 30; //add to score 
                                            _audioSrc.Play();

                                            Destroy(currentPlate.content); //destroy dish 
                                            Destroy(currentPlate.gameObject);

                                            thisTableReverter.isOccupied = false;

                                            recipeManager.RemoveTask(i); //**where is this i coming from**//

                                            once = true; 
                                        }

                                        break; //exit loop 

                                    }
                                }
                            
                        }
                    }
                }
            }
        }
    }

    /*tried to set up a coroutine to slow down the for loop but didn't really get it. also i realized midway i don't think its necessary 
    IEnumerator CheckDishDelivery(RecipeController recipeManager, PlateController currentPlate)
    {
        for (int i = 0; i < recipeManager.currTasks.Count; i++) //go through the current recipes 
        {
            string ingredientRequired = recipeManager.currTasks[i].ingredientName; //look at what ingredient is required for each recipe
            Debug.Log(ingredientRequired);

            if (currentPlate.ingredientOnPlate.ingredientName == ingredientRequired) //if the ingredient required matches ingredient on plate, 
            {
                    _gm.score += 30; //add to score 
                    _audioSrc.Play();

                    Destroy(currentPlate.content); //destroy dish 
                    Destroy(currentPlate.gameObject);

                    thisTableReverter.isOccupied = false;

                    recipeManager.RemoveTask(i);


                yield return null; //exit loop 

            }

            yield return new WaitForSeconds(.2f);
        }
    }*/
}
