using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_AttackState : EnemyState
{
    Enemy_Skeleton enemy;
    public Skeleton_AttackState(Enemy _enemyBase, EnemyStateMachine _enemystatemachine, string _animBoolName, Enemy_Skeleton enemy)
        : base(_enemyBase, _enemystatemachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        enemy.VectorIsZero();

        if(triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        //마지막으로 공격한 시간 기록
        enemy.lastTimeAttacked = Time.time;
    }
}
