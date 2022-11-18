using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSelector : MonoBehaviour
{
    //variable declaration
    public GameObject TableSelected; //the table that is selected
    public GameObject Player;
    public CircleCollider2D circleCollider;
    public float distance;
    public Vector2 direction;
    public bool isCounter;
    public float offset;
    private Rigidbody2D _RB;
    private PlayerController _ThisPlayerControl;
    private TableReverter _SelectedReverter; // the script of selected table
    public bool isSelected; //only one table can be selected for one player at one time
    public LayerMask layerCounter;
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
        direction = Player.GetComponent<PlayerController>().movement;
        if (direction.x == 0 && direction.y == 0)
        {
            direction = lastDirection;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(circleCollider.bounds.center, direction, circleCollider.bounds.extents.y+offset, layerCounter);
        Debug.DrawRay(circleCollider.bounds.center , direction * ( circleCollider.bounds.extents.y+offset), Color.red);
        
        
        if (hit)
        {
            if (isSelected && TableSelected.transform.gameObject != hit.transform.gameObject) //Something different is selected
            {
                TableSelected.GetComponent<TableReverter>().isSelectedPlayer = false;
                TableSelected = null;
                isSelected = false;
            }
            if (!isSelected && TableSelected == null) //Nothing is selected
            {
                TableSelected = hit.transform.gameObject;
                TableSelected.GetComponent<TableReverter>().isSelectedPlayer = true;
                isSelected = true;
            }
            
        }
        
        else
        { //No counters selected
            
            TableSelected.GetComponent<TableReverter>().isSelectedPlayer = false;
            TableSelected = null;
            isSelected = false;
        }

    }
    private void FixedUpdate()
    {
        
        //RayCast();
        /*
        Vector3 foward = transform.TransformDirection(Vector3.forward);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 1);
            Debug.DrawRay(transform.position, foward, Color.green);
        */
        /*
        Vector2 direction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().movement;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 1f,
            gameObject.transform.position.y - 1f), direction, distance);
        Debug.DrawRay(transform.position, direction * distance);
        if (hit.transform.gameObject.layer == 6)
        {
            TableSelected = hit.transform.gameObject;
        }
        */
    }
    


}
