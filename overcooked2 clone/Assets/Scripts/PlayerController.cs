using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 movement;
    public float movementSpeed;
    public Rigidbody2D rigidbody;
    void Start()
    {
        
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        
        Debug.Log(movement.x);
        
        
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * movementSpeed);
    }

}
