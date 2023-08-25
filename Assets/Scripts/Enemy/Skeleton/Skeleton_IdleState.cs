using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_IdleState : Skeleton_GroundedState
{
    public Skeleton_IdleState(Enemy _enemyBase, EnemyStateMachine _enemystatemachine, string _animBoolName, Enemy_Skeleton enemy) 
        : base(_enemyBase, _enemystatemachine, _animBoolName, enemy)
    {
    }


    //스켈레톤 에너미 등록 (enemy가 스켈레톤 일수도있고 고블린 일수도 있고 하니까 명시해서 등록)

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }
    public override void Update()
    {
        base.Update();

        //정해진 시간만큼 idle상태였다가 움직임
        if (stateTimer < 0f) stateMachine.ChangeState(enemy.moveState);

    }

    public override void Exit()
    {
        base.Exit();
    }

}
