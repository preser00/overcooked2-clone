using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }



    //public bool DashInput { get; private set; } (tba)
    //public bool ChopInput { get; private set; } (tba)
    //public bool GrabInput { get; private set; } (tba)




    public void OnMoveInput(InputAction.CallbackContext context) //feeding the input values
    {
        
        RawMovementInput = context.ReadValue<Vector2>();
        
        if (Mathf.Abs(RawMovementInput.x) > 0.5f) //x deadzone 
        {
            NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormInputX = 0;
        }
        if (Mathf.Abs(RawMovementInput.y) > 0.5f) //y deadzone
        {
            NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            NormInputY = 0;
        }
        
    }
}
