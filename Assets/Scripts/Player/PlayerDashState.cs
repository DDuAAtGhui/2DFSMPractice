using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
        : base(_player, _stateMachine, _AnimationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //대쉬상태 진입하면 분신술
        player.skill.clone.CreateClone(player.transform);

        stateTimer = player.dash_duration;
    }
    public override void Update()
    {
        base.Update();

        //대쉬 하면서도 벽잡고 슬라이딩 가능하게
        if (!player.isGroundedDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        //X_Input넣으면 정지해있을때 0 이라 대쉬 안함
        //player.SetVelocity(player.dashDir*(player.moveSpeed + player.dash_addSpeed), rb.velocity.y);

        player.SetVelocity(player.facingDir * (player.moveSpeed + player.dash_addSpeed), rb.velocity.y);
        Debug.Log("player.facingDir : " + player.facingDir);


        if (stateTimer < 0) stateMachine.ChangeState(player.idleState);


    }

    public override void Exit()
    {
        base.Exit();
        //끝나면 X값 현재속도 0
        player.VectorIsZero();
    }
}
