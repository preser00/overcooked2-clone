using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSelector : MonoBehaviour
{
    //variable declaration
    public GameObject TableSelected; //the table that is selected 

    private Rigidbody2D _RB;
    private PlayerController _ThisPlayerControl;
    private TableReverter _SelectedReverter; // the script of selected table
    private bool _OnlySelected = false; //only one table can be selected for one player at one time

    void Start()
    {
        _RB = GetComponent<Rigidbody2D>(); //grab player rigidbody
        _ThisPlayerControl = GetComponent<PlayerController>(); //grab playercontroller script
    }

  
    void Update()
    {
        
    }

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

    /*public void FixedUpdate()
    {
        Vector3 foward = transform.TransformDirection(Vector3.forward);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 1);
            Debug.DrawRay(transform.position, foward, Color.green);

        
    }
    */


}
