using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
        : base(_player, _stateMachine, _AnimationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.sword_Skill.DotsActive(true);
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        //�����Ҷ� ���߰������ 
        player.VectorIsZero();

        //���콺 ���� ��ġ ���ϱ�
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //�����ؼ� �� ������ ���콺 ��ġ�� Ȯ����
        //�÷��̾ ������ �����ִµ� ���콺�� ���ʿ� ������
        //���콺 ��Ŭ���ϸ� ���콺 ��Ŭ���� �������� �ø�
        if (player.transform.position.x > MousePosition.x && player.facingDir == 1)
            player.flip();

        //���� ��
        else if (player.transform.position.x < MousePosition.x && player.facingDir == -1)
            player.flip();


    }

    public override void Exit()
    {
        base.Exit();
        player.skill.sword_Skill.DotsActive(false);

        //ȸ�� �� 0.2�� ������ ���ǵ帮�� 
        player.StartCoroutine("ImBusyFor", 0.2f);
    }

}
