using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableReverter : MonoBehaviour
{
    public Color original;
    public Color red = Color.red;

    public bool isSelectedPlayer;
    public bool isOccupied = false;
    public bool isCuttingBoard = false;

    public SpriteRenderer spriteRenderer;
    public GameObject content;
   
    // Start is called before the first frame update
    void Start()
    {
        //original = gameObject.GetComponent<SpriteRenderer>().color; //saving the original color of this table
        original = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelectedPlayer)
        {
            spriteRenderer.color = red;
        }
        else
        {
            spriteRenderer.color = original;
        }
    }
    
    
}
