using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�θ� ��ġ�� ��� �־ �ڽĵ鿡 ������ ���� �ʿ� ���� �����
public class Skeleton_GroundedState : EnemyState
{
    //�������� �ڱ� �ڽ� ����ϸ� ����� �ֵ��� ���� ���ص� ��
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

        //�÷��̾ �����ǰų� �� �����ϸ� ��������
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) <2f)
            stateMachine.ChangeState(enemy.battleState);
    }
    
    public override void Exit()
    {
        base.Exit();

    }

}
