using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
        : base(_player, _stateMachine, _AnimationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;
        //SuccessFullCounter �ִϸ��̼� bool�� �ʱ�ȭ
        player.anim.SetBool("SuccessFullCounter", false);
    }
    public override void Update()
    {
        base.Update();
        //ī���� �ڼ� ������ �̵� �Ұ�
        player.VectorIsZero();
        //������ ����Ÿ��
        //player.attackCheck.position���� player.attackCheckRadius��ŭ�� �������� ���� �� �ȿ� �ִ� �ݶ��̴����� �迭�� ������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        //�迭�� �� �ݶ��̴��� ����
        foreach (var hit in colliders)
        {
            //�迭�� �� �ݶ��̴��� �߿� Enemy��ũ��Ʈ �޸��ֵ� ������
            if (hit.GetComponent<Enemy>() != null)
            {
                //stateTimer �ð� �ȿ� �� ��ũ��Ʈ �޸��ֵ� ������
                //�������� ����� ���� ������ ���� �ִϸ��̼� �߻�
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    //�׽�Ʈ��
                    stateTimer = 10;

                    player.anim.SetBool("SuccessFullCounter", true);
                }
            }
        }

        //�ݰ� ���� Ȥ�� ���� ��� ������ ����
        if (stateTimer < 0 || istriggerCalled) stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
