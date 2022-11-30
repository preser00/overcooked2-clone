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
            if (currentReverter.isOccupied && playerController.currentHolding == null)
            {
                if (playerController.isAlt)
                {
                    playerController.currentHolding = currentReverter.content; //Set current holding to the table content
                    currentReverter.content = null; //reset the table content to null
                    currentReverter.isOccupied = false; //reset the table isOccupied to false

                    playerController.currentHolding.gameObject.GetComponent<IngredientController>().held = true; //Tell that collided object it is being held
                    playerController.currentHolding.gameObject.GetComponent<IngredientController>().master = gameObject; //Tell the collided object who is holding it

                }
            }
        }
        
        else
        { //No counters selected

           
            currentReverter.isSelectedPlayer = false;
            TableSelected = null;
            isSelected = false;
        }

        Debug.Log("isOccupied = " + currentReverter.isOccupied);
        Debug.Log("currentHolding = " + playerController.currentHolding);
    }

}
