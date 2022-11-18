using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSelector : MonoBehaviour
{
    //variable declaration
    public GameObject TableSelected; //the table that is selected
    public GameObject Player;
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
    /*
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable" && !_OnlySelected)
        {
            Debug.Log("interacting");
            _OnlySelected = true; //now can select only this table
            TableSelected = collision.gameObject;//ordine selected table
            TableSelected.GetComponent<SpriteRenderer>().color = new Color(1f, 0.30196078f, 0.30196078f); //change color to signify which table is selected
            _SelectedReverter = TableSelected.GetComponent<TableReverter>(); //retrieve script of selected table
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == TableSelected)//leaving selected table's vicinity
        {
            Debug.Log("stop interacting");
            _OnlySelected = false;//now can select other table again
        }
    }
    */
    private void RayCast()
    {
        Vector2 lastDirection = direction;
     
        direction = Player.GetComponent<PlayerController>().toRotation * Vector3.up;
        /*if (direction.x == 0 && direction.y == 0)
        {
            direction = lastDirection;
        }*/
        
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
            
        }
        
        else
        { //No counters selected

            currentReverter = TableSelected.GetComponent<TableReverter>();
            currentReverter.isSelectedPlayer = false;
            TableSelected = null;
            isSelected = false;
        }

    }

}
