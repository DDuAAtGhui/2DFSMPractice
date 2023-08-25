using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//y���ν�Ƽ < 0 �̸� ���߿��� �������� AirState
public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName) 
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

        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);
        if (player.isGroundedDetected())
            stateMachine.ChangeState(player.idleState);

        ////���߿��� Ű �Է��ϸ� �����̴� �ӵ� ����
        //if (X_Input != 0)
        //   player.SetVelocity(player.moveSpeed * 0.6f * X_Input, rb.velocity.y);

        //���߿����� 0.8������ �ӵ��� ���� �� �� �ְ� ����
        player.SetVelocity(player.moveSpeed * X_Input * 0.8f, rb.velocity.y);

    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);
    }

}
