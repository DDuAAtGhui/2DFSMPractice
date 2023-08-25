using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    //빨간줄 뜰때 컨트롤 + . 하면 폼 만들어줌
    //생성자 생성하고 부모클래스에 생성자 연결
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
                            : base(_player, _stateMachine, _AnimationBoolName)
    {

    }
    //PlayerIdelState 드래그하고 알트 엔터 - Create Override
    public override void Enter()
    {
        base.Enter();
        rb.velocity = Vector2.zero;
    }
    public override void Update()
    {
        base.Update();
        //좌 우로 움직임값이 입력되면 Move 상태로 전환
        
        //바라보는 방향으로 키 입력 하는데 벽이 감지되고 있으면 바로 return해 moveState이행 하지 않음
        if (X_Input == player.facingDir && player.IsWallDetected()) return;

        //기본 공격에서 Exit하면 0.1초 바쁜상태로 설정됨
        if (X_Input != 0 && !player.isNowBusy)
            player.stateMachine.ChangeState(player.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
