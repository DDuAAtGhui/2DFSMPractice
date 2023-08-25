using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_IdleState : Skeleton_GroundedState
{
    public Skeleton_IdleState(Enemy _enemyBase, EnemyStateMachine _enemystatemachine, string _animBoolName, Enemy_Skeleton enemy) 
        : base(_enemyBase, _enemystatemachine, _animBoolName, enemy)
    {
    }


    //���̷��� ���ʹ� ��� (enemy�� ���̷��� �ϼ����ְ� ��� �ϼ��� �ְ� �ϴϱ� ����ؼ� ���)

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }
    public override void Update()
    {
        base.Update();

        //������ �ð���ŭ idle���¿��ٰ� ������
        if (stateTimer < 0f) stateMachine.ChangeState(enemy.moveState);

    }

    public override void Exit()
    {
        base.Exit();
    }

}
