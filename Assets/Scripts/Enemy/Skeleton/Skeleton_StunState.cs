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
        //기절시간 기본 1초
        stateTimer = enemy.stunDuration;

        //스턴 상태 진입시 넉백. 일반 넉백이랑 다르게 큰 넉백으로 설정하면 좋음
        //y축도 수치 넣어주면 통 튀어나가면서 스턴되듯이 보여서 좋음(가벼운 적들에게)

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
