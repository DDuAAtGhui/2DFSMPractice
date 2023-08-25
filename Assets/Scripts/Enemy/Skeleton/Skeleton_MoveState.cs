using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_MoveState : Skeleton_GroundedState
{
    public Skeleton_MoveState(Enemy _enemyBase, EnemyStateMachine _enemystatemachine, string _animBoolName, Enemy_Skeleton enemy) :
        base(_enemyBase, _enemystatemachine, _animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.MoveSpeed * enemy.facingDir, enemy.rb.velocity.y);

        if(enemy.IsWallDetected() || !enemy.isGroundedDetected())
        {
            enemy.flip();
            stateMachine.ChangeState(enemy.idleState);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

}
