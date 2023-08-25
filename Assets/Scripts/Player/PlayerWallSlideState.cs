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
            //코드가 아래쪽 까지 가면서 간섭할 경우 return 추가
            return;
        }

        //움직이는데 움직이는 방향과 바라보는 방향이 일치하지 않을경우 
        // = 벽에서 붙어서 슬라이딩하면서 내려오다가 거기서 탈출하려고 할경우
        if (X_Input != 0 && player.facingDir != X_Input)
            stateMachine.ChangeState(player.idleState);

        //벽 슬라이딩 속도 조절
        //아래 키 누르면 빠른 벽슬라이딩
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
