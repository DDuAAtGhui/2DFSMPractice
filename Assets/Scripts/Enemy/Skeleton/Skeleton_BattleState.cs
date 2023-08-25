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
            //stateTimer�� �����ð����� �ʱ�ȭ
            stateTimer = enemy.battleTime;
            //attackDistance �ȿ� �÷��̾� ������ ������·� �����ϴ� Attack���·� ��ȯ
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
                if (CanAttack())
                    enemy.stateMachine.ChangeState(enemy.attackState);

            
        }

        //�÷��̾� ��ã������
        else
        {
            //stateTimer�� ��� �پ��ϱ� �����ð���ŭ ������ Idle ���·� ��ȯ
            //Ȥ�� �Ÿ��� 7 �̻� �������� Idle ���·� ��ȯ
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position)>7f)
                stateMachine.ChangeState(enemy.idleState);
        }

        //�÷��̾ ���ͺ��� �����ʿ� �����ϸ�
        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;

            if (!enemy.isfacingRight) enemy.flip();
        }
        //�÷��̾ ���ͺ��� ���ʿ� �����ϸ�
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

    //���Ͱ� �����ϰ� ���ݹ������� �÷��̾ ����ٰ� �ٽ� ���ö� �ٽ� �����ϴ� �ð��� �����Ϸ��� ���� �޼ҵ�
    bool CanAttack()
    {
        //���������� ������ �ð��� ������Ÿ�Ӹ�ŭ ������
        //lastTimeAttacked�� ����ð����� �ʱ�ȭ�ϰ� CanAttack�� true�� ��ȯ
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCoolDown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        //������ �Ұ����� ���¸� false�� ��ȯ
        return false;
    }

}
