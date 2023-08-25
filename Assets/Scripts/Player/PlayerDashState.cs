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

        //�뽬���� �����ϸ� �нż�
        player.skill.clone.CreateClone(player.transform);

        stateTimer = player.dash_duration;
    }
    public override void Update()
    {
        base.Update();

        //�뽬 �ϸ鼭�� ����� �����̵� �����ϰ�
        if (!player.isGroundedDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        //X_Input������ ������������ 0 �̶� �뽬 ����
        //player.SetVelocity(player.dashDir*(player.moveSpeed + player.dash_addSpeed), rb.velocity.y);

        player.SetVelocity(player.facingDir * (player.moveSpeed + player.dash_addSpeed), rb.velocity.y);
        Debug.Log("player.facingDir : " + player.facingDir);


        if (stateTimer < 0) stateMachine.ChangeState(player.idleState);


    }

    public override void Exit()
    {
        base.Exit();
        //������ X�� ����ӵ� 0
        player.VectorIsZero();
    }
}
