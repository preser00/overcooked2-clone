using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBasicState //inherit from BasicState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Dochecks()
    {
        base.Dochecks();
    }

    public override void Enter()
    {

        base.Enter();
        player.SetVelocityX(0f); //kill speed when idle
        player.SetVelocityY(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if ((xInput != 0 ||yInput != 0)&& !isExitingState)
        {
            stateMachine.ChangeState(player.MoveState); //change to MoveState when move input
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}

