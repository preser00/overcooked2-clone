using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState startingState) //booting the StateMachine
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState) //Changing the states
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
