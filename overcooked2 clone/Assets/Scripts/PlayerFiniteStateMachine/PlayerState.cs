using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;

    }
    //all public virtual void functions pass down too all the states
    public virtual void Enter()
    {
        Dochecks();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicUpdate()
    {
        Dochecks();
    }

    public virtual void Dochecks() { } //All the physics checkings in states (like checking for walls) falls under this, see overwrite in each state
    public virtual void AnimationTrigger() { } //useful when activating something when an animation starts
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true; //useful when activating something when an animation ends
}

