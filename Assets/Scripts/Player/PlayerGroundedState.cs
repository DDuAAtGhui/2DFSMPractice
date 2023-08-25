using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//땅 체크를 하는 상태이므로 모든 상태에서 이 상태에서 올 수 있게끔
public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
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

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
            stateMachine.ChangeState(player.aimSwordState);

        if (Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(player.counterState);

        //땅에서 공격 허용 (누르고 있으면 공격)
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Mouse0))
            stateMachine.ChangeState(player.playerPrimaryAttack);

        if (!player.isGroundedDetected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.isGroundedDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    bool HasNoSword()
    {
        if (!player.sword) return true;
        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
