using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBasicState //inherit from BasicState
{

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Dochecks()
    {
        base.Dochecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityX(playerData.movementVelocity * xInput); //set X speed
        player.SetVelocityY(playerData.movementVelocity * yInput); //set Y speed
        if (xInput == 0 && yInput == 0 && !isExitingState)
        {
            stateMachine.ChangeState(player.IdleState); // change state when stop move input
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

}
