using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//y벨로시티 < 0 이면 공중에서 떨어지는 AirState
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

        ////공중에서 키 입력하면 움직이는 속도 저하
        //if (X_Input != 0)
        //   player.SetVelocity(player.moveSpeed * 0.6f * X_Input, rb.velocity.y);

        //공중에서도 0.8배율의 속도로 움직 일 수 있게 설정
        player.SetVelocity(player.moveSpeed * X_Input * 0.8f, rb.velocity.y);

    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);
    }

}
