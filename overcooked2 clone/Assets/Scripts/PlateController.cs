using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    //  ATTRIBUTES  //
    public string plateName; //Name of the plate
    public bool held; //Is someone holding the plate?
    public int cleanliness = 0; // How clean is this plate right now?
    


    public GameObject master; //Who is holding the object?
    public CircleCollider2D cc; // this Circle collider
    public SpriteRenderer spriteRenderer; //this srpite renderer
    public GameObject content = null; //stuffs that goes into the plate
    
    public Sprite[] sprites;
    public Sprite[] dirtySprites;

    private AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (held)
        {
            cc.enabled = false; // turn off collider after picked up
            //rb.simulated = false; // turn off rigidbody after picked up
            followMaster();
        }
        else
        {
            cc.enabled = true;
            //rb.simulated = true;
        }

    }

    public void followMaster()
    {
        transform.position = master.transform.position + new Vector3(1, 1, 0);
    }
    
}
