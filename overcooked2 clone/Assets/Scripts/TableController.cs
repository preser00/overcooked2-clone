using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public string tableType; //all lowercase strings
                               //ingredient spawner, plate spawner, cutting board, trash can 
    public int ingredientType; //for ingredient spawning counters
                               //0 = fish, 1 = shrimp 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player") //if the player is touching a counter, check for key inputs
                                                  //based on the type of counter the current one is
        {
            if (tableType == "ingredient spawner")
            {

            }
            else if (tableType == "plate spawner")
            {

            }
            else if (tableType == "cutting board")
            {

            }
            else if (tableType == "trash can")
            {

            }
        }
        
    }
}
