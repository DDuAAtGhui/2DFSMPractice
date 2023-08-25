using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_StunState : EnemyState
{
    Enemy_Skeleton enemy;
    public Skeleton_StunState(Enemy _enemyBase, EnemyStateMachine _enemystatemachine, string _animBoolName, Enemy_Skeleton enemy)
        : base(_enemyBase, _enemystatemachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.InvokeRepeating("Blink_Yellow", 0, 0.1f);
        //�����ð� �⺻ 1��
        stateTimer = enemy.stunDuration;

        //���� ���� ���Խ� �˹�. �Ϲ� �˹��̶� �ٸ��� ū �˹����� �����ϸ� ����
        //y�൵ ��ġ �־��ָ� �� Ƣ����鼭 ���ϵǵ��� ������ ����(������ ���鿡��)

        enemy.rb.velocity = new Vector2(-enemy.facingDir * enemy.stunDir.x, enemy.stunDir.y);
    }
    public override void Update()
    {
        base.Update();

        if (stateTimer <= 0) stateMachine.ChangeState(enemy.idleState);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.fx.Invoke("CancleInvoke_Blink_Yellow", 0);
    }

}
