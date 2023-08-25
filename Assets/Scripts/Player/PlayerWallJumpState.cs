using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
        : base(_player, _stateMachine, _AnimationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 0.4f;

        //�������� �ݴ������� �پ���ϴϱ�
        player.SetVelocity(5 * -player.facingDir, player.jumpforce);
        player.flip();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0) stateMachine.ChangeState(player.airState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
