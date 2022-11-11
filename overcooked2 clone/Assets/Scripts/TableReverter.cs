using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableReverter : MonoBehaviour
{
    public Color original;
    // Start is called before the first frame update
    void Start()
    {
       original = gameObject.GetComponent<SpriteRenderer>().color; //saving the original color of this table
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("interacting");
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.30196078f, 0.30196078f);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("stop interacting");
            gameObject.GetComponent<SpriteRenderer>().color = original;
        }
    }*/
}
