using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//검 잡았을때 살짝 밀리는 효과 추가
public class PlayerCatchSwordState : PlayerState
{
    Transform sword;
    public PlayerCatchSwordState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
        : base(_player, _stateMachine, _AnimationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        sword = player.sword.transform;

        //조준해서 검 던질때 마우스 위치로 확인함
        //플레이어가 오른쪽 보고있는데 마우스는 왼쪽에 있을때
        //마우스 우클릭하면 마우스 우클릭한 방향으로 플립
        if (player.transform.position.x > sword.position.x && player.facingDir == 1)
            player.flip();

        //같은 논리
        else if (player.transform.position.x < sword.position.x && player.facingDir == -1)
            player.flip();

        rb.velocity = new Vector2(player.swordReturnImpact * -player.facingDir, rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();

        if (istriggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
    public override void Exit()
    {
        base.Exit();

        //회수 후 0.2초 동안은 못건드리게 
        player.StartCoroutine("ImBusyFor", 0.2f);
    }

}
