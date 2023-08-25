using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
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

        player.SetVelocity(X_Input*player.moveSpeed, rb.velocity.y);
        //�����Ӱ��� �������� �ʰų� �����̴ٰ� ���� Ž���Ǹ� Idle ���·� ��ȯ
        if (X_Input == 0 || player.IsWallDetected())
            player.stateMachine.ChangeState(player.idleState);

    }

    public override void Exit()
    {
        base.Exit();
    }
}
