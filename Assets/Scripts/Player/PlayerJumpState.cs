using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
        : base(_player, _stateMachine, _AnimationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumpforce);
    }

    public override void Update()
    {
        base.Update();
        //���߿����� 0.8������ �ӵ��� ���� �� �� �ְ� ����
        player.SetVelocity(player.moveSpeed*X_Input*0.8f, rb.velocity.y);

        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);

        //if (player.isGroundedDetected())
        //    stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
