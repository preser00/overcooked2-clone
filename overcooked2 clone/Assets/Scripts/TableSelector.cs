using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSelector : MonoBehaviour
{
    //variable declaration
    public GameObject TableSelected; //the table that is selected 
    private TableReverter _SelectedReverter;

    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            Debug.Log("interacting");
            TableSelected = collision.gameObject;
            TableSelected.GetComponent<SpriteRenderer>().color = new Color(1f, 0.30196078f, 0.30196078f);
            _SelectedReverter = TableSelected.GetComponent<TableReverter>();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            Debug.Log("stop interacting");
            TableSelected.GetComponent<SpriteRenderer>().color = _SelectedReverter.original;
        }
    }
}
