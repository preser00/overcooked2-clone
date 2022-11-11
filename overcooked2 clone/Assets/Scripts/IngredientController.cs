using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    //  ATTRIBUTES  //
    public string ingredientName; //Name of the ingredient
    public bool held; //Is someone holding the ingredient?
    public GameObject master; //Who is holding the object?
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (held)
        {
            followMaster();
        }
    }

    public void followMaster()
    {
        transform.position = master.transform.position + new Vector3(1, 1, 0);
    }
}
