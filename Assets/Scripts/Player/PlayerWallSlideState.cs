using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
        : base(_player, _stateMachine, _AnimationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            //�ڵ尡 �Ʒ��� ���� ���鼭 ������ ��� return �߰�
            return;
        }

        //�����̴µ� �����̴� ����� �ٶ󺸴� ������ ��ġ���� ������� 
        // = ������ �پ �����̵��ϸ鼭 �������ٰ� �ű⼭ Ż���Ϸ��� �Ұ��
        if (X_Input != 0 && player.facingDir != X_Input)
            stateMachine.ChangeState(player.idleState);

        //�� �����̵� �ӵ� ����
        //�Ʒ� Ű ������ ���� �������̵�
        if (Y_Input < 0) rb.velocity = new Vector2(0, rb.velocity.y * 0.95f);
        else rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);

        if (player.isGroundedDetected())
            stateMachine.ChangeState(player.idleState);


    }

    public override void Exit()
    {
        base.Exit();
    }

}
