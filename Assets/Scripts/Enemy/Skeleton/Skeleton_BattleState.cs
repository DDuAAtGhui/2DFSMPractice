using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Skeleton_BattleState : EnemyState
{
    Transform player;
    Enemy_Skeleton enemy;
    int moveDir;
    public Skeleton_BattleState(Enemy _enemyBase, EnemyStateMachine _enemystatemachine, string _animBoolName, Enemy_Skeleton _enemy)
        : base(_enemyBase, _enemystatemachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();


        player = PlayerManager.instance.player.transform;

        enemy.MoveSpeed *= 2f;
        enemy.anim.speed *= 2f;
    }
    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            //stateTimer를 전투시간으로 초기화
            stateTimer = enemy.battleTime;
            //attackDistance 안에 플레이어 있으면 멈춘상태로 공격하는 Attack상태로 전환
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
                if (CanAttack())
                    enemy.stateMachine.ChangeState(enemy.attackState);

            
        }

        //플레이어 못찾았을때
        else
        {
            //stateTimer는 계속 줄어드니까 전투시간만큼 지나면 Idle 상태로 전환
            //혹은 거리가 7 이상 떨어지면 Idle 상태로 전환
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position)>7f)
                stateMachine.ChangeState(enemy.idleState);
        }

        //플레이어가 몬스터보다 오른쪽에 존재하면
        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;

            if (!enemy.isfacingRight) enemy.flip();
        }
        //플레이어가 몬스터보다 왼쪽에 존재하면
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDir = -1;

            if (enemy.isfacingRight) enemy.flip();
        }

        enemy.SetVelocity(enemy.MoveSpeed * moveDir, enemy.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.MoveSpeed /= 2f;
        enemy.anim.speed /= 2f;

    }

    //몬스터가 공격하고 공격범위에서 플레이어가 벗어났다가 다시 들어올때 다시 공격하는 시간을 조절하려고 만든 메소드
    bool CanAttack()
    {
        //마지막으로 공격한 시간에 공격쿨타임만큼 지나면
        //lastTimeAttacked을 현재시간으로 초기화하고 CanAttack을 true로 반환
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCoolDown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        //공격이 불가능한 상태면 false로 반환
        return false;
    }

}
