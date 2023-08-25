using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�� ������� ��¦ �и��� ȿ�� �߰�
public class PlayerCatchSwordState : PlayerState
{
    Transform sword;
    public PlayerCatchSwordState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
        : base(_player, _stateMachine, _AnimationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        sword = player.sword.transform;

        //�����ؼ� �� ������ ���콺 ��ġ�� Ȯ����
        //�÷��̾ ������ �����ִµ� ���콺�� ���ʿ� ������
        //���콺 ��Ŭ���ϸ� ���콺 ��Ŭ���� �������� �ø�
        if (player.transform.position.x > sword.position.x && player.facingDir == 1)
            player.flip();

        //���� ��
        else if (player.transform.position.x < sword.position.x && player.facingDir == -1)
            player.flip();

        rb.velocity = new Vector2(player.swordReturnImpact * -player.facingDir, rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();

        if (istriggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
    public override void Exit()
    {
        base.Exit();

        //ȸ�� �� 0.2�� ������ ���ǵ帮�� 
        player.StartCoroutine("ImBusyFor", 0.2f);
    }

}
