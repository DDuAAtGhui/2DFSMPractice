using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//부모엔 겹치는 기능 넣어서 자식들에 여러번 넣을 필요 없게 만들기
public class Skeleton_GroundedState : EnemyState
{
    //상위에서 자기 자신 명시하면 상속한 애들은 선언 안해도 됨
    protected Enemy_Skeleton enemy;

    protected Transform player;
    public Skeleton_GroundedState(Enemy _enemyBase, EnemyStateMachine _enemystatemachine, string _animBoolName, Enemy_Skeleton enemy)
        : base(_enemyBase, _enemystatemachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }
    public override void Update()
    {
        base.Update();

        //플레이어가 감지되거나 초 근접하면 전투상태
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) <2f)
            stateMachine.ChangeState(enemy.battleState);
    }
    
    public override void Exit()
    {
        base.Exit();

    }

}
